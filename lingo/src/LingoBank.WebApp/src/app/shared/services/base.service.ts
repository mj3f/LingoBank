import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import jwtDecode, { JwtPayload } from 'jwt-decode';

export abstract class BaseService {
	protected apiUrl;
	private readonly httpPrefix;
	private readonly headers: HttpHeaders;

	constructor(http: HttpClient) {
		this.headers = new HttpHeaders()
			.set('Content-Type', 'application/json')
			.set('Accept', 'application/json');

		this.httpPrefix = environment.useHttps ? 'https://' : 'http://';
		this.apiUrl = this.httpPrefix + environment.url + '/api/v0';
	}

	protected getRequestOptions(): RequestOptions {
		const tokenString = this.getJwtToken();
		let jwtToken = null;

		if (tokenString) {
			jwtToken = jwtDecode<JwtPayload>(tokenString);
		}

		let hasCredentials = false;
		let newHeaders: HttpHeaders = null;

		if (jwtToken) {
			if (jwtToken.exp > Math.floor(Date.now() / 1000)) {
				hasCredentials = true;
				newHeaders = new HttpHeaders()
					.set('Content-Type', 'application/json')
					.set('Accept', 'application/json')
					.set('Authorization', 'Bearer ' + tokenString);
			} else {
				this.clearJwtToken();
			}
		}

		return {
			headers: newHeaders ?? this.headers,
			withCredentials: hasCredentials
		};
	}

	protected storeJwtToken(token: string): void {
		localStorage.setItem(environment.tokenKeyForStorage, token);
	}

	protected getJwtToken(): string {
		return localStorage.getItem(environment.tokenKeyForStorage);
	}

	protected clearJwtToken(): void {
		localStorage.removeItem(environment.tokenKeyForStorage);
	}
}

export interface RequestOptions {
	headers: HttpHeaders;
	withCredentials: boolean;
	body?: any;
}

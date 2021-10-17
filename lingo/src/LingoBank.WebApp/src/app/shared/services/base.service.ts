import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from 'src/environments/environment';


export abstract class BaseService {
    protected apiUrl; // = 'http://localhost:5000/api/v0';
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
        const jwtToken = this.getJwtToken();
        let hasCredentials = false;
        let newHeaders: HttpHeaders = null;
        if (jwtToken) {
            hasCredentials = true;
            newHeaders = new HttpHeaders()
                .set('Content-Type', 'application/json')
                .set('Accept', 'application/json')
                .set('Authorization', jwtToken);
        }

        return {
            headers: newHeaders ?? this.headers,
            withCredentials: hasCredentials
        };
    }

    protected storeJwtToken(token: string) {
        localStorage.setItem(environment.tokenKeyForStorage, token);
    }

    protected getJwtToken(): string {
        return localStorage.getItem(environment.tokenKeyForStorage);
    }

    protected clearJwtToken() {
        localStorage.removeItem(environment.tokenKeyForStorage);
    }
}

export interface RequestOptions {
    headers: HttpHeaders,
    withCredentials: boolean,
    body?: any
}
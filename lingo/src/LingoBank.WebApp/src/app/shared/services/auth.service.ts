import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { User, UserWithPassword } from '../../users/models/user.model';
import { BaseService } from './base.service';
import { CurrentUserService } from '../../users/services/current-user.service';
import jwtDecode, { JwtPayload } from 'jwt-decode';

@Injectable({
	providedIn: 'root'
})
export class AuthService extends BaseService {
	isTokenValid = new BehaviorSubject(false);

	constructor(
		private http: HttpClient,
		private currentUserService: CurrentUserService) {
		super(http);
		this.apiUrl += '/auth';
	}

	public login(email: string, password: string): Observable<string> {
		const user = new UserWithPassword(email, password);
		return new Observable<string>(subscriber => {
			this.http.post<string>(this.apiUrl + '/login', JSON.stringify(user), this.getRequestOptions()).subscribe((token: string) => {
					this.storeJwtToken(token);
					this.isTokenValid.next(true);
					subscriber.next();
					subscriber.complete();
				},
				(error) => {
					subscriber.error(error);
					this.isTokenValid.next(false);
				});
		});
	}

	public logout(): Observable<null> {
		return new Observable(subscriber => {
			this.clearJwtToken();
			this.isTokenValid.next(false);
			subscriber.complete();
		});
	}

	public getCurrentUser(): Observable<User> {
		return new Observable<User>(subscriber => {
			this.http.get<User>(this.apiUrl + '/current-user', this.getRequestOptions()).subscribe((user: User) => {
				this.currentUserService.userSubject.next(user); // Notify listeners.
				subscriber.next();
				subscriber.complete();
			},
			error => subscriber.error(error));
		});
	}

	/**
	 * Called by AuthGuard every 30 seconds to check token is still valid.
	 */
	public hasJwtTokenExpired(): void {
		const token: string = this.getJwtToken();
		if (!token) {
			this.isTokenValid.next(false);
			return;
		}

		const decodedToken = jwtDecode<JwtPayload>(token);
		const isValid: boolean = Date.now() <= decodedToken.exp * 1000;

		console.log('token is valid? ', isValid); // fixme: doesn't set to false after 30 mins when token is supposed to expire.
		this.isTokenValid.next(isValid);
	}
}

import { Component, OnDestroy, OnInit } from '@angular/core';
import { CurrentUserService } from './users/services/current-user.service';
import { User } from './users/models/user.model';
import { AuthService } from './shared/services/auth.service';
import { filter, switchMap, tap } from 'rxjs/operators';
import { Observable, Subscription } from 'rxjs';

@Component({
	selector: 'app-root',
	templateUrl: './app.component.html',
	styleUrls: ['./app.component.css'],
})
export class AppComponent implements OnInit, OnDestroy {
	title = 'LingoBank';
	isLoggedIn = false;
	subscription: Subscription;

	constructor(
		private currentUserService: CurrentUserService,
		private authService: AuthService) {}

	ngOnInit(): void {
		// Checks if the jwt token is valid, if not, then the user will automatically be redirected to the login screen (via authGuard).
		// Here, the navbar will be removed from the DOM.
		// This also checks if the token is valid, but the current user subject is somehow null, this will re-fetch the current user
		// from the API.
		this.subscription = this.authService.isTokenValid
			.pipe(
				tap(isValid => this.isLoggedIn = isValid),
				filter(isValid => isValid === true && !this.currentUserService.userSubject.value),
				switchMap(() => this.getCurrentUser())
			).subscribe(_ => console.log('current user now = ', this.currentUserService.userSubject.value));
		console.log('dd');
	}

	ngOnDestroy(): void {
		this.subscription.unsubscribe();
	}

	private getCurrentUser(): Observable<User> {
		return this.authService.getCurrentUser();
	}

}

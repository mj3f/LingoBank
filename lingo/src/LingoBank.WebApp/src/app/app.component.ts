import { Component, OnDestroy, OnInit } from '@angular/core';
import { CurrentUserService } from './users/services/current-user.service';
import { User } from './users/models/user.model';
import { AuthService } from './shared/services/auth.service';

@Component({
	selector: 'app-root',
	templateUrl: './app.component.html',
	styleUrls: ['./app.component.css'],
})
export class AppComponent implements OnInit, OnDestroy {
	title = 'LingoBank-WebApp';
	isLoggedIn = false;

	constructor(
		private authService: AuthService,
		private currentUserService: CurrentUserService) {}

	ngOnInit(): void {
		this.currentUserService.userSubject.subscribe(
			(user: User) => {
				if (user) {
					this.isLoggedIn = true;
				}
			},
			error => this.isLoggedIn = false);

		// get current user to see whether isLoggedIn should be true (getCurrentUser will notify the subject).
		this.authService.getCurrentUser().subscribe();
	}

	ngOnDestroy(): void {
		this.currentUserService.userSubject.unsubscribe();
	}

}

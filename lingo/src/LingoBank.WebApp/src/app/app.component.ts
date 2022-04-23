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
		private authService: AuthService) {}

	ngOnInit(): void {
		this.authService.isTokenValid.subscribe((isValid: boolean) => this.isLoggedIn = isValid);
	}

	ngOnDestroy(): void {
		this.authService.isTokenValid.unsubscribe();
	}

}

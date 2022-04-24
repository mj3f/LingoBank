import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../shared/services/auth.service';
import { take } from 'rxjs/operators';

@Component({
	selector: 'app-navbar',
	templateUrl: './navbar.component.html'
})
export class NavbarComponent {

	constructor(
		private authService: AuthService,
		private router: Router) {}

	logout(): void {
		this.authService.logout().pipe(take(1)).subscribe(_ => this.router.navigate(['/login']));
	}

}

import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthService } from '../../services/auth.service';

@Injectable({
	providedIn: 'root'
})
export class AuthGuard  { // , OnDestroy {
	// private destroy$ = new Subject();

	constructor(
		private router: Router,
		private authService: AuthService) {
		// interval(30000).pipe(
		// 	tap(_ => this.authService.hasJwtTokenExpired()),
		// 	takeUntil(this.destroy$)
		// ).subscribe();
	}

	canActivate(
		route: ActivatedRouteSnapshot,
		state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
		this.authService.hasJwtTokenExpired();
		const isTokenValid: boolean = this.authService.isTokenValid.value;

		if (!isTokenValid) {
			this.router.navigate(['/login']);
		}
		return isTokenValid;
	}

	// ngOnDestroy(): void { // fixme: I don't think this ever gets called?
	// 	this.destroy$.next();
	// 	this.destroy$.complete();
	// }

}

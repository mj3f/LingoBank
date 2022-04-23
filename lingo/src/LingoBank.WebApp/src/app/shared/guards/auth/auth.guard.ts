import { Injectable, OnDestroy } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, RouterStateSnapshot, UrlTree } from '@angular/router';
import { interval, Observable, Subject } from 'rxjs';
import { AuthService } from '../../services/auth.service';
import { takeUntil, tap } from 'rxjs/operators';

@Injectable({
	providedIn: 'root'
})
export class AuthGuard implements CanActivate, OnDestroy {
	private destroy$ = new Subject();

	constructor(private authService: AuthService) {
		interval(30000).pipe(
			tap(_ => this.authService.hasJwtTokenExpired()),
			takeUntil(this.destroy$)
		).subscribe();
	}

	canActivate(
		route: ActivatedRouteSnapshot,
		state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
		return this.authService.isTokenValid.value;
	}

	ngOnDestroy(): void { // fixme: I don't think this ever gets called?
		this.destroy$.next();
		this.destroy$.complete();
	}

}

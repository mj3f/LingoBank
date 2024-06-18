import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { LoginRegisterComponent } from './login-register/login-register.component';
import { AuthGuard } from './shared/guards/auth/auth.guard';

const routes: Routes = [
	{ path: '', redirectTo: '/login', pathMatch: 'full' },
	{ path: 'home', component: HomeComponent, canActivate: [AuthGuard] },
	{ path: 'login', component: LoginRegisterComponent },
	{ path: 'languages', loadChildren: () => import('./languages/languages.module').then(m => m.LanguagesModule) },
	{ path: 'settings', loadChildren: () => import('./settings/settings.module').then(m => m.SettingsModule) },
	{ path: 'users', loadChildren: () => import('./users/users.module').then(m => m.UsersModule) }
];

@NgModule({
	imports: [RouterModule.forRoot(routes, {})],
	exports: [RouterModule]
})
export class AppRoutingModule { }

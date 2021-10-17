import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LanguageListComponent } from './languages/language-list/language-list.component';
import { LanguageViewComponent } from './languages/language-view/language-view.component';
import { LoginRegisterComponent } from './login-register/login-register.component';

const routes: Routes = [
    { path: '', redirectTo: '/login', pathMatch: 'full' },
    // { path: 'languages', component: LanguageListComponent },
    // { path: 'languages/:id', component: LanguageViewComponent },
    { path: 'login', component: LoginRegisterComponent }
   // { path: 'home', component: HomeComponent },
   // { path: 'language/:id', component: LanguageComponent },
];

@NgModule({
	imports: [RouterModule.forRoot(routes, { relativeLinkResolution: 'legacy' })],
	exports: [RouterModule]
})
export class AppRoutingModule { }

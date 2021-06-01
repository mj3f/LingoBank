import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LanguageListComponent } from './languages/list/language-list.component';

const routes: Routes = [
    { path: '', redirectTo: '/languages', pathMatch: 'full' },
    { path: 'languages', component: LanguageListComponent },
   // { path: 'home', component: HomeComponent },
   // { path: 'language/:id', component: LanguageComponent },
];

@NgModule({
	imports: [RouterModule.forRoot(routes)],
	exports: [RouterModule]
})
export class AppRoutingModule { }

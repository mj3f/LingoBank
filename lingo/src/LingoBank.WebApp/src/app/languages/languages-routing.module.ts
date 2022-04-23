import { RouterModule, Routes } from '@angular/router';
import { NgModule } from '@angular/core';
import { LanguageListComponent } from './components/language-list/language-list.component';
import { LanguageViewComponent } from './components/language-view/language-view.component';
import { AuthGuard } from '../shared/guards/auth/auth.guard';

const routes: Routes = [
	{ path: '', component: LanguageListComponent, canActivate: [AuthGuard] },
	{ path: 'languages/:id', component: LanguageViewComponent, canActivate: [AuthGuard] },
];

@NgModule({
	imports: [RouterModule.forChild(routes)],
	exports: [RouterModule]
})
export class LanguagesRoutingModule { }

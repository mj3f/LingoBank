import { RouterModule, Routes } from '@angular/router';
import { NgModule } from '@angular/core';
import { LanguageListComponent } from './components/language-list/language-list.component';
import { LanguageViewComponent } from './components/language-view/language-view.component';

const routes: Routes = [
	{ path: '', component: LanguageListComponent },
	{ path: 'languages/:id', component: LanguageViewComponent },
];

@NgModule({
	imports: [RouterModule.forChild(routes)],
	exports: [RouterModule]
})
export class LanguagesRoutingModule { }

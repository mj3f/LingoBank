import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LanguageListComponent } from './language-list/language-list.component';
import { LanguageViewComponent } from './language-view/language-view.component';
import { SharedModule } from '../shared/shared.module';
import { LanguageCardComponent } from './language-list/language-card/language-card.component';
import { ReactiveFormsModule } from '@angular/forms';


@NgModule({
	declarations: [LanguageListComponent, LanguageViewComponent, LanguageCardComponent],
	imports: [
		CommonModule,
		SharedModule,
		ReactiveFormsModule
	],
	exports: [
		LanguageListComponent
	]
})
export class LanguagesModule { }

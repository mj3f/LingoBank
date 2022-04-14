import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LanguageListComponent } from './language-list/language-list.component';
import { LanguageViewComponent } from './language-view/language-view.component';
import { SharedModule } from '../shared/shared.module';
import { LanguageCardComponent } from './language-list/language-card/language-card.component';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import { LanguagePhrasesListComponent } from './language-view/language-phrases-list/language-phrases-list.component';


@NgModule({
	declarations: [LanguageListComponent, LanguageViewComponent, LanguageCardComponent, LanguagePhrasesListComponent],
	imports: [
		CommonModule,
		SharedModule,
		ReactiveFormsModule,
		FormsModule
	],
	exports: [
		LanguageListComponent
	]
})
export class LanguagesModule { }

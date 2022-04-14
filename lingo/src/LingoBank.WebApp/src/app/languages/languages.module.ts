import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LanguageListComponent } from './components/language-list/language-list.component';
import { LanguageViewComponent } from './components/language-view/language-view.component';
import { SharedModule } from '../shared/shared.module';
import { LanguageCardComponent } from './components/language-list/language-card/language-card.component';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import { LanguagePhrasesListComponent } from './components/language-view/language-phrases-list/language-phrases-list.component';
import { LanguagesRoutingModule } from './languages-routing.module';
import { LanguageService } from './services/language/language.service';
import { PhraseService } from './services/phrase/phrase.service';


@NgModule({
	declarations: [LanguageListComponent, LanguageViewComponent, LanguageCardComponent, LanguagePhrasesListComponent],
	imports: [
		CommonModule,
		SharedModule,
		ReactiveFormsModule,
		FormsModule,
		LanguagesRoutingModule
	],
	providers: [
		LanguageService,
		PhraseService
	]
})
export class LanguagesModule { }

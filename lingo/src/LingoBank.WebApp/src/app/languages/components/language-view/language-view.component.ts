import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';
import { Language } from 'src/app/languages/models/language.model';
import { LanguageService } from 'src/app/languages/services/language/language.service';
import { Phrase } from '../../models/phrase.model';
import { tap } from 'rxjs/operators';

@Component({
	selector: 'app-language-view',
	templateUrl: './language-view.component.html'
})
export class LanguageViewComponent implements OnInit {
	language$: Observable<Language>;
	language: Language;

	constructor(
		private languageService: LanguageService,
		private route: ActivatedRoute) { }

	ngOnInit(): void {
		const id = this.route.snapshot.paramMap.get('id');
		if (id) {
			this.language$ = this.getLanguage(id)
				.pipe(tap(language => this.language = language));
		}
	}

	createPhrase(phrase: Phrase): void {
		console.log('kanguage = ', this.language);
		if (this.language.phrases) {
			this.language.phrases.push(phrase);
		}
	}

	private getLanguage(id: string): Observable<Language> {
		return this.languageService.getById(id);
	}
}

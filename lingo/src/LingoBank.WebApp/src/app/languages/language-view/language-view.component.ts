import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';
import { Language } from 'src/app/languages/language.model';
import { LanguageService } from 'src/app/languages/language.service';

@Component({
	selector: 'app-language-view',
	templateUrl: './language-view.component.html'
})
export class LanguageViewComponent implements OnInit {
	language$: Observable<Language>;

	constructor(
		private languageService: LanguageService,
		private route: ActivatedRoute) { }

	ngOnInit(): void {
		const id = this.route.snapshot.paramMap.get('id');
		if (id) {
			this.language$ = this.getLanguage(id);
		}
	}

	private getLanguage(id: string): Observable<Language> {
		return this.languageService.getById(id);
	}
}

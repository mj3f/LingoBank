import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import {Observable, Subscription} from 'rxjs';
import { Language } from 'src/app/shared/models/language.model';
import { LanguageService } from 'src/app/shared/services/language.service';
import {take} from 'rxjs/operators';

@Component({
	selector: 'app-language-view',
	templateUrl: './language-view.component.html'
})
export class LanguageViewComponent implements OnInit {
	// language: Language;
	language$: Observable<Language>;

	constructor(
		private languageService: LanguageService,
		private route: ActivatedRoute) { }

	ngOnInit(): void {
		const id = this.route.snapshot.paramMap.get('id');
		if (id) {
			this.languageService.getById(id);
		}
	}
}

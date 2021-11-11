import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { Language } from 'src/app/shared/models/language.model';
import { LanguageService } from 'src/app/shared/services/language.service';

@Component({
	selector: 'app-language-list',
	templateUrl: './language-list.component.html'
})
export class LanguageListComponent implements OnInit {
	languages: Language[];
	showModal = false;

	constructor(private languageService: LanguageService, private router: Router) { }

	ngOnInit(): void {
		// this.getLanguages();
		this.languages = [
			new Language('232323', 'English', 'gb', []),
			new Language('436566', 'Spanish', 'es', []),
			new Language('365788', 'German', 'de', []),
			new Language('675685', 'Italian', 'it', [])
		];

		for (const language of this.languages) {
			language.description = 'This is a dummy description';
		}
	}

	public goToLanguageView(id: string): void {
		this.router.navigate(['/languages/', id]);
	}

	public addLanguage(): void {
		this.toggleModal();
	}

	public toggleModal(): void {
		this.showModal = !this.showModal;
	}

	private getLanguages(): Subscription {
		return this.languageService.getAll().subscribe(data => this.languages = data);
	}
}

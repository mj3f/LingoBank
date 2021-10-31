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

	constructor(private languageService: LanguageService,
				private router: Router) { }

	ngOnInit(): void {
		// this.getLanguages();
		this.languages = [
			new Language('232323', 'English', []),
			new Language('436566', 'Spanish', []),
			new Language('365788', 'German', []),
			new Language('675685', 'Italian', [])
		]
	}

	goToLanguageView(id: string) {
		this.router.navigate(['/languages/', id]);
	}

	private getLanguages(): Subscription {
		return this.languageService.getAll().subscribe(data => this.languages = data);
	}

}

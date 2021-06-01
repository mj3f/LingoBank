import { Component, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { Language } from 'src/app/shared/models/language.model';
import { LanguageService } from 'src/app/shared/services/language.service';

@Component({
	selector: 'app-language-list',
	templateUrl: './language-list.component.html',
	styleUrls: ['./language-list.component.css']
})
export class LanguageListComponent implements OnInit {
	languages: Language[];

	constructor(private languageService: LanguageService) { }

	ngOnInit(): void {
		this.getLanguages();
	}

	private getLanguages(): Subscription {
		return this.languageService.getAll().subscribe(data => this.languages = data);
	}

}

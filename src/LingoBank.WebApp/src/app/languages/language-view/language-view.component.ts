import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';
import { Language } from 'src/app/shared/models/language.model';
import { LanguageService } from 'src/app/shared/services/language.service';

@Component({
	selector: 'app-language-view',
	templateUrl: './language-view.component.html',
	styleUrls: ['./language-view.component.css']
})
export class LanguageViewComponent implements OnInit {
	language: Language;

	constructor(private languageService: LanguageService,
				private route: ActivatedRoute) { }

	ngOnInit(): void {
		const id = this.route.snapshot.paramMap.get('id');
		if (id) {
			this.getLanguage(id);
		}
	}

	private getLanguage(id: string): Subscription {
		return this.languageService.getById(id).subscribe(data => this.language = data);
	}

}

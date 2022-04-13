import { Component, Input } from '@angular/core';
import { Language } from '../../language.model';
import {Router} from '@angular/router';

@Component({
	selector: 'app-language-card',
	templateUrl: './language-card.component.html',
})
export class LanguageCardComponent {
	@Input()
	public language: Language;

	constructor(private router: Router) { }

	public goToLanguageView(): void {
		this.router.navigate(['/languages/', this.language.id]);
	}

}

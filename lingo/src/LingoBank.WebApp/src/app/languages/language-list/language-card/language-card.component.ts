import { Component, Input, OnInit } from '@angular/core';
import { Language } from '../../../shared/models/language.model';

@Component({
	selector: 'app-language-card',
	templateUrl: './language-card.component.html',
})
export class LanguageCardComponent implements OnInit {
	@Input()
	public language: Language;

	constructor() { }

	ngOnInit(): void {
	}

}

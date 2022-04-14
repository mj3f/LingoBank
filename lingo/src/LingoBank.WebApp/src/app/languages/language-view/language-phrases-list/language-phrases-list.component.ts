import { Component, Input, OnInit } from '@angular/core';
import { Phrase } from '../../models/phrase.model';

@Component({
	selector: 'app-language-phrases-list',
	templateUrl: './language-phrases-list.component.html'
})
export class LanguagePhrasesListComponent implements OnInit {

	@Input()
	public phrases: Phrase[];

	constructor() { }

	ngOnInit(): void {
	}

}

import { Component, Input, OnInit } from '@angular/core';
import { Phrase } from '../../models/phrase.model';
import {AbstractControl, FormBuilder, FormControl, FormGroup} from '@angular/forms';
import {from, Observable, of, Subject} from 'rxjs';
import {combineLatest, debounceTime, distinctUntilChanged, startWith, switchMap} from 'rxjs/operators';
import {fromArray} from 'rxjs/internal/observable/fromArray';

@Component({
	selector: 'app-language-phrases-list',
	templateUrl: './language-phrases-list.component.html'
})
export class LanguagePhrasesListComponent implements OnInit {

	@Input()
	public phrases: Phrase[];

	filteredPhrases$: Observable<Phrase[]>;

	searchBarForm: FormGroup;

	constructor(fb: FormBuilder) {
		this.searchBarForm = fb.group({
			search: new FormControl('')
		});
	}

	get searchInput(): AbstractControl { return this.searchBarForm.get('search'); }

	ngOnInit(): void {
		// Testing only
		if (!this.phrases) {
			this.phrases = [
				new Phrase('1', '1', 'English', 'German', 'This is a test', 'whatever', 0),
				new Phrase('2', '1', 'English', 'German', 'tes', 'whatever', 0),
				new Phrase('3', '1', 'English', 'German', 'Revolution', 'whatever', 0),
				new Phrase('4', '1', 'English', 'German', 'Test me!', 'whatever', 0),
				new Phrase('5', '1', 'English', 'German', 'Hello', 'whatever', 0),
			];
		}

		console.log('do it');
		this.filteredPhrases$ = this.searchInput.valueChanges.pipe(
			debounceTime(200),
			distinctUntilChanged(),
			switchMap((term: string) => {
				const phrases: Phrase[] = this.phrases.filter(phrase =>
					phrase.text.toLowerCase().includes(term.toLowerCase())); // search case in-sensitive.
				return of(phrases); // Observable<Phrase[]> instead of Phrase[].
			})
		);
	}

}

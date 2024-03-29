import { ChangeDetectionStrategy, Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Phrase } from '../../../models/phrase.model';
import { AbstractControl, UntypedFormBuilder, UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import { Observable, of } from 'rxjs';
import { debounceTime, distinctUntilChanged, startWith, switchMap, take } from 'rxjs/operators';
import { PhraseService } from '../../../services/phrase/phrase.service';
import { Language } from '../../../models/language.model';

@Component({
	selector: 'app-language-phrases-list',
	templateUrl: './language-phrases-list.component.html',
	changeDetection: ChangeDetectionStrategy.OnPush
})
export class LanguagePhrasesListComponent implements OnInit {

	@Input()
	language: Language;

	@Output()
	phraseCreated: EventEmitter<Phrase> = new EventEmitter<Phrase>();

	showModal: boolean;
	phrases$: Observable<Phrase[]>;
	searchBarForm: UntypedFormGroup;
	phraseForm: UntypedFormGroup;

	private phrases: Phrase[];

	constructor(
		private phraseService: PhraseService,
		fb: UntypedFormBuilder) {
		this.searchBarForm = fb.group({
			search: new UntypedFormControl('')
		});

		this.phraseForm = fb.group({
			sourceLanguage: new UntypedFormControl('', [Validators.required]),
			text: new UntypedFormControl('', [Validators.required]),
			translation: new UntypedFormControl('', [Validators.required]),
			description: new UntypedFormControl('')
		});
	}

	get searchInput(): AbstractControl { return this.searchBarForm.get('search'); }

	ngOnInit(): void {
		this.phrases = this.language.phrases;

		this.phrases$ = this.searchInput.valueChanges.pipe(
			debounceTime(400),
			distinctUntilChanged(),
			switchMap((term: string) => {
				const phrases: Phrase[] = this.phrases.filter(phrase =>
					phrase.text.toLowerCase().includes(term.toLowerCase())); // search case in-sensitive.
				return of(phrases); // Observable<Phrase[]> instead of Phrase[].
			}),
			startWith(this.phrases)
		);
	}

	createPhrase(): void {
		this.toggleModal();
	}

	onModalOkButtonClicked(): void {
		const phrase: Phrase = Object.assign({}, this.phraseForm.value);
		phrase.id = '';
		phrase.languageId = this.language.id;
		phrase.targetLanguage = this.language.name;
		phrase.category = 0;

		this.phraseService.create(phrase).pipe(take(1)).subscribe((createdPhrase: Phrase) => {
			this.clearForm();
			this.toggleModal();
			this.phraseCreated.emit(createdPhrase);
		});
	}

	public onModalCancelButtonClicked(): void {
		this.clearForm();
		this.toggleModal();
	}

	private toggleModal(): void {
		this.showModal = !this.showModal;
	}

	private clearForm(): void {
		this.phraseForm.reset();
	}
}

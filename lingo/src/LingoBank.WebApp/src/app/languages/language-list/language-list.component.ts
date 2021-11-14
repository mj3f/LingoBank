import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { Language } from 'src/app/shared/models/language.model';
import { LanguageService } from 'src/app/shared/services/language.service';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
	selector: 'app-language-list',
	templateUrl: './language-list.component.html'
})
export class LanguageListComponent implements OnInit {

	public form: FormGroup;
	languages: Language[];
	showModal = false;

	constructor(private languageService: LanguageService,
				private router: Router,
				formBuilder: FormBuilder) {
		this.form = formBuilder.group({
			name: new FormControl('', [Validators.required]),
			code: new FormControl('', [Validators.required, Validators.maxLength(2)]), // Probably will be a dropdown for this tbh.
			description: new FormControl('', [])
		});
	}

	get name(): string { return this.form.get('name').value; }
	get code(): string { return this.form.get('code').value; }
	get description(): string { return this.form.get('description').value; }

	ngOnInit(): void {
		// this.getLanguages();
		this.languages = [
			new Language('English', 'gb', '',[]),
			new Language('Spanish', 'es', '', []),
			new Language('German', 'de', '', []),
			new Language('Italian', 'it', '', [])
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

	public clearForm(): void {
		this.form.reset();
	}

	public onModalOkButtonClicked(): void {
		this.toggleModal();
		const language = new Language(this.name, this.code, this.description, []);
		language.userId = '????';
		this.createLanguage(language).add(_ => this.clearForm());
	}

	public onModalCancelButtonClicked(): void {
		this.clearForm();
		this.toggleModal();
	}

	private toggleModal(): void {
		this.showModal = !this.showModal;
	}

	private getLanguages(): Subscription {
		return this.languageService.getAll().subscribe(data => this.languages = data);
	}

	private createLanguage(language: Language): Subscription {
		return this.languageService.create(language).subscribe();
	}
}

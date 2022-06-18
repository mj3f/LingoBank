import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { Language } from 'src/app/languages/models/language.model';
import { LanguageService } from 'src/app/languages/services/language/language.service';
import { UntypedFormBuilder, UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import { UserService } from '../../../users/services/user.service';
import { CurrentUserService } from '../../../users/services/current-user.service';
import { User } from '../../../users/models/user.model';
import { map, retry, take } from 'rxjs/operators';

@Component({
	selector: 'app-language-list',
	templateUrl: './language-list.component.html'
})
export class LanguageListComponent implements OnInit {

	public form: UntypedFormGroup;
	languages$: Observable<Language[]>;
	showModal = false;
	currentUser: User;

	constructor(
		private currentUserService: CurrentUserService,
		private languageService: LanguageService,
		private userService: UserService,
		formBuilder: UntypedFormBuilder) {
		this.form = formBuilder.group({
			name: new UntypedFormControl('', [Validators.required]),
			code: new UntypedFormControl('', [Validators.required, Validators.maxLength(2)]), // Probably will be a dropdown for this tbh.
			description: new UntypedFormControl('', [])
		});
	}

	get name(): string { return this.form.get('name').value; }
	get code(): string { return this.form.get('code').value; }
	get description(): string { return this.form.get('description').value; }

	ngOnInit(): void {
		this.currentUserService.userSubject.pipe(take(1)).subscribe((user: User) => {
			this.currentUser = user;
			this.languages$ = this.getLanguages(user.id);
		});
	}

	public addLanguage(): void {
		this.toggleModal();
	}

	public onModalOkButtonClicked(): void {
		if (!this.currentUser) {
			return;
		}

		this.toggleModal();
		const language = new Language(this.name, this.code, this.description, []);
		language.id = '';
		language.userId = this.currentUser.id; // TODO: get current user from jwt token

		this.createLanguage(language)
			.pipe(take(1))
			.subscribe(_ => {
				this.clearForm();
			}
		);
	}

	public onModalCancelButtonClicked(): void {
		this.clearForm();
		this.toggleModal();
	}

	private clearForm(): void {
		this.form.reset();
	}

	private toggleModal(): void {
		this.showModal = !this.showModal;
	}

	private getLanguages(userId: string): Observable<Language[]> {
		return this.userService.getById(userId)
			.pipe(
				map((user: User) => user.languages),
				retry(3));
	}

	private createLanguage(language: Language): Observable<Language> {
		return this.languageService.create(language);
	}
}

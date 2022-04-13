import { Component, OnInit } from '@angular/core';
import {Observable} from 'rxjs';
import {Language} from 'src/app/languages/language.model';
import {LanguageService} from 'src/app/languages/language.service';
import {FormBuilder, FormControl, FormGroup, Validators} from '@angular/forms';
import {UserService} from '../../shared/services/user.service';
import {CurrentUserService} from '../../shared/services/current-user.service';
import {User} from '../../shared/models/user.model';
import {take, tap} from 'rxjs/operators';

@Component({
	selector: 'app-language-list',
	templateUrl: './language-list.component.html'
})
export class LanguageListComponent implements OnInit {

	public form: FormGroup;
	languages: Language[];
	languages$: Observable<Language[]>;
	showModal = false;
	currentUser: User;

	constructor(
		private currentUserService: CurrentUserService,
		private languageService: LanguageService,
		private userService: UserService,
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
		this.currentUserService.userSubject.pipe(take(1)).subscribe((user: User) => {
			this.currentUser = user;
			this.languages$ = this.getLanguages(user.id);
		});
	}

	public addLanguage(): void {
		this.toggleModal();
	}

	public clearForm(): void {
		this.form.reset();
	}

	public onModalOkButtonClicked(): void {
		if (!this.currentUser) {
			return;
		}

		this.toggleModal();
		const language = new Language(this.name, this.code, this.description, []);
		language.userId = this.currentUser.id; // TODO: get current user from jwt token

		this.createLanguage(language)
			.pipe(take(1))
			.subscribe((createdLanguage: Language) => {
				this.clearForm();
				this.languages.push(createdLanguage);
			}
		);
	}

	public onModalCancelButtonClicked(): void {
		this.clearForm();
		this.toggleModal();
	}

	private toggleModal(): void {
		this.showModal = !this.showModal;
	}

	private getLanguages(userId: string): Observable<Language[]> {
		return this.userService.getLanguages(userId)
			.pipe(tap((languages: Language[]) => this.languages = languages));
	}

	private createLanguage(language: Language): Observable<Language> {
		return this.languageService.create(language);
	}
}

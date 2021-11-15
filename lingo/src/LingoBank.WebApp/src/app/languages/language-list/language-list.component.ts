import { Component, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { Language } from 'src/app/shared/models/language.model';
import { LanguageService } from 'src/app/shared/services/language.service';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { UserService } from '../../shared/services/user.service';
import { CurrentUserService } from '../../shared/services/current-user.service';
import { User } from '../../shared/models/user.model';

@Component({
	selector: 'app-language-list',
	templateUrl: './language-list.component.html'
})
export class LanguageListComponent implements OnInit, OnDestroy {

	public form: FormGroup;
	languages: Language[];
	showModal = false;
	currentUser: User;

	constructor(
		private currentUserService: CurrentUserService,
		private languageService: LanguageService,
		private router: Router,
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
		this.currentUserService.userSubject.subscribe((user: User) => {
			this.currentUser = user;
			this.getLanguages();
		});
	}

	ngOnDestroy(): void {
		this.currentUserService.userSubject.unsubscribe();
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
		if (!this.currentUser) {
			return;
		}

		this.toggleModal();
		const language = new Language(this.name, this.code, this.description, []);
		language.userId = this.currentUser.id; // TODO: get current user from jwt token
		this.createLanguage(language).add(_ => {
			this.clearForm();
			this.getLanguages();
		});
	}

	public onModalCancelButtonClicked(): void {
		this.clearForm();
		this.toggleModal();
	}

	private toggleModal(): void {
		this.showModal = !this.showModal;
	}

	private getLanguages(): Subscription {
		if (!this.currentUser) {
			return null;
		}
		const id = this.currentUser.id;
		return this.userService.getLanguages(id).subscribe(data => this.languages = data);
	}

	private createLanguage(language: Language): Subscription {
		return this.languageService.create(language).subscribe();
	}
}

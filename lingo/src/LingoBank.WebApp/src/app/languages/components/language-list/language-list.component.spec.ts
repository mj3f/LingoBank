import { ComponentFixture, TestBed } from '@angular/core/testing';
import { Language } from '../../models/language.model';
import { By } from '@angular/platform-browser';
import { LanguageListComponent } from './language-list.component';
import { LanguageCardComponent } from './language-card/language-card.component';
import { of } from 'rxjs';
import createSpyObj = jasmine.createSpyObj;
import { CurrentUserService } from '../../../users/services/current-user.service';
import { LanguageService } from '../../services/language/language.service';
import { UserService } from '../../../users/services/user.service';
import { HttpClient } from '@angular/common/http';
import { FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import { RouterTestingModule } from '@angular/router/testing';


describe('LanguageViewComponent', () => {
	let component: LanguageListComponent;
	let fixture: ComponentFixture<LanguageListComponent>;

	const httpClient = createSpyObj<HttpClient>('HttpClient', ['post', 'get', 'put', 'delete']);
	// const formBuilder = createSpyObj<FormBuilder>('FormBuilder', []);

	beforeEach(async () => {
		await TestBed.configureTestingModule({
			imports: [RouterTestingModule],
			declarations: [ LanguageListComponent, LanguageCardComponent ],
			providers: [
				{ provide: HttpClient, useValue: httpClient },
				FormBuilder
			]
		}).compileComponents();

		TestBed.inject(CurrentUserService);
		TestBed.inject(LanguageService);
		TestBed.inject(UserService);
	});

	beforeEach(() => {
		fixture = TestBed.createComponent(LanguageListComponent);
		component = fixture.componentInstance;
		fixture.detectChanges();
	});

	it('should create', () => {
		expect(component).toBeTruthy();
	});

	it('should render a list of languages', () => {
		const languages = [
			new Language('English', 'GB', 'Test', []),
			new Language('French', 'FR', 'Test', [])
		];

		component.languages$ = of(languages);

		fixture.detectChanges(); // trigger change detection.

		// render list of languages in cards.
		const languageCardComponents = fixture.debugElement.queryAll(By.directive(LanguageCardComponent));
		expect(languageCardComponents.length).toBe(2);

		// we can check if the language is correctly initialized in the card component.
		const firstLang = languageCardComponents[0].componentInstance.language;
		expect(firstLang.name).toBe('English');
	});

});

import { ComponentFixture, TestBed } from '@angular/core/testing';
import { LanguageViewComponent } from './language-view.component';
import { Language } from '../../models/language.model';
import { LanguagePhrasesListComponent } from './language-phrases-list/language-phrases-list.component';
import { By } from '@angular/platform-browser';


describe('LanguageViewComponent', () => {
	let component: LanguageViewComponent;
	let fixture: ComponentFixture<LanguageViewComponent>;

	beforeEach(async () => {
		await TestBed.configureTestingModule({
			declarations: [ LanguageViewComponent, LanguagePhrasesListComponent ]
		}).compileComponents();
	});

	beforeEach(() => {
		fixture = TestBed.createComponent(LanguageViewComponent);
		component = fixture.componentInstance;
		fixture.detectChanges();
	});

	it('should create', () => {
		expect(component).toBeTruthy();
	});

	it('should render the language name', () => {
		component.language = new Language('English', 'GB', 'Test', []);

		fixture.detectChanges(); // trigger change detection.

		const element = fixture.nativeElement;
		expect(element.querySelector('h3').textContent).toBe('English');
	});

});

import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LanguagePhrasesListComponent } from './language-phrases-list.component';

describe('LanguagePhrasesListComponent', () => {
	let component: LanguagePhrasesListComponent;
	let fixture: ComponentFixture<LanguagePhrasesListComponent>;

	beforeEach(async () => {
		await TestBed.configureTestingModule({
			declarations: [ LanguagePhrasesListComponent ]
		})
			.compileComponents();
	});

	beforeEach(() => {
		fixture = TestBed.createComponent(LanguagePhrasesListComponent);
		component = fixture.componentInstance;
		fixture.detectChanges();
	});

	it('should create', () => {
		expect(component).toBeTruthy();
	});

	it('should toggle modal when createPhrase() is invoked', () => {
		component.createPhrase();
		expect(component.showModal).toBeTrue();
	});

	xit('should emit an event when a phrase is created.', () => {
		spyOn(component.phraseCreated, 'emit');
		// need to construct a form and get the values from it to get the phrase object???
	});
});

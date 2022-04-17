import { TestBed, waitForAsync } from '@angular/core/testing';

import { PhraseService } from './phrase.service';
import { Phrase } from '../../models/phrase.model';
import createSpyObj = jasmine.createSpyObj;
import { HttpClient } from '@angular/common/http';
import { of } from 'rxjs';

describe('PhraseService', () => {
	let service: PhraseService;
	let phrase: Phrase;
	const httpClient = createSpyObj<HttpClient>('HttpClient', ['post']);

	beforeEach(() => {
		TestBed.configureTestingModule({
			providers: [{ provide: HttpClient, useValue: httpClient }]
		});
		service = TestBed.inject(PhraseService);

		phrase = new Phrase('English', 'Test phrase', 'Unknown');
		phrase.targetLanguage = 'Unknown';
		phrase.languageId = 'test1234';
	});

	it('should be created', () => {
		expect(service).toBeTruthy();
	});

	it('should create a phrase and return it an an Observable', () => {
		httpClient.post.and.returnValue(of(phrase));

		waitForAsync(() => {
			service.create(phrase).subscribe(returnedPhrase => expect(returnedPhrase).toBeDefined());
		});
	});
});

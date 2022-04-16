import { LanguagePhrasePipe } from './language-phrase.pipe';
import { Phrase } from '../../models/phrase.model';

describe('LanguagePhrasePipe', () => {
	let phrase: Phrase;
	let pipe: LanguagePhrasePipe;

	beforeEach(() => {
		phrase = new Phrase('English', 'Test phrase', 'Unknown');
		pipe = new LanguagePhrasePipe();
	});

	it('create an instance', () => {
		expect(pipe).toBeTruthy();
	});

	it('should return a string in the correct format', () => {
		const str = `Source Language: ${phrase.sourceLanguage} - Text: ${phrase.text} - Target Language: ${phrase.targetLanguage} - Translation of text: ${phrase.translation}`;
		expect(pipe.transform(phrase)).toEqual(str);
	});
});

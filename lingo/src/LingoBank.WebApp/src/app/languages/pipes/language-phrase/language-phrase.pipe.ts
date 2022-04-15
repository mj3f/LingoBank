import { Pipe, PipeTransform } from '@angular/core';
import { Phrase } from '../../models/phrase.model';

@Pipe({
	name: 'languagePhrase'
})
export class LanguagePhrasePipe implements PipeTransform {

	transform(value: Phrase): unknown {
		return `Source Language: ${value.sourceLanguage} - Text: ${value.text} - Target Language: ${value.targetLanguage} - Translation of text: ${value.translation}`;
	}

}

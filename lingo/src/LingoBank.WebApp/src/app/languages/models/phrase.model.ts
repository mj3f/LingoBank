
export class Phrase {
	public id: string;
	public languageId: string;
	public targetLanguage: string;
	public category: number;

	constructor(
		public sourceLanguage: string,
		public text: string,
		public translation: string,
		public description?: string
	) {}
}


export class Phrase {
	constructor(
		public id: string,
		public languageId: string,
		public sourceLanguage: string,
		public targetLanguage: string,
		public text: string,
		public translation: string,
		public category: number,
		public description?: string
	) {}
}

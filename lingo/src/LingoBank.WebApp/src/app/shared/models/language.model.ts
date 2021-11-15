
export class Language {
	public id: string;
	public userId: string;

	constructor(public name: string, public code: string, public description: string, public phrases: any[]) {}
}

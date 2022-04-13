import { Language } from '../../languages/language.model';

export class User {
	public id: string;
	public userName: string;
	public role: string;
	public languages: Language[];

	constructor(public emailAddress: string) {}
}

export class UserWithPassword extends User {
	constructor(email: string, public password: string) {
		super(email);
	}
}

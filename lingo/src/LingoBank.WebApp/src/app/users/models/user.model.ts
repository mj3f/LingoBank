import { Language } from '../../languages/models/language.model';
import {Paged} from '../../shared/models/paged.model';

export class User {
	public id: string;
	public userName: string;
	public role: string;
	public languages: Paged<Language>;

	constructor(public emailAddress: string) {}
}

export class UserWithPassword extends User {
	constructor(email: string, public password: string) {
		super(email);
	}
}


export class User {
    constructor(public emailAddress: string) {
        
    }
}

export class UserWithPassword extends User {
    constructor(email: string, public password: string) {
        super(email);
    }
}
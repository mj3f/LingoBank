
export class User {
    constructor(public username: string) {
        
    }
}

export class UserWithPassword extends User {
    constructor(username: string, public password: string) {
        super(username);
    }
}
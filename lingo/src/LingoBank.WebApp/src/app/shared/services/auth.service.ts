import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { UserWithPassword } from "../models/user.model";
import { BaseService } from "./base.service";

@Injectable()
export class AuthService extends BaseService {

    constructor(private http: HttpClient) {
        super(http);
        this.apiUrl += '/auth/';
    }

    public login(email: string, password: string): Observable<string> {
        const user = new UserWithPassword(email, password);
        return new Observable(subscriber => {
            this.http.post<string>(this.apiUrl + 'login', JSON.stringify(user), this.getRequestOptions()).subscribe((token: string) => {
                // do something with the token here!
                // this.storeJwtToken(token);
                this.clearJwtToken();
                subscriber.complete();
            },
            (error) => subscriber.error(error))
        });
    }
}
import { BaseService } from '../../shared/services/base.service';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { User } from '../models/user.model';
import { Language } from '../../languages/models/language.model';

@Injectable({
	providedIn: 'root'
})
export class UserService extends BaseService {
	constructor(private http: HttpClient) {
		super(http);
		this.apiUrl += '/users';
	}

	public getAll(): Observable<User[]> {
		return this.http.get<User[]>(this.apiUrl, this.getRequestOptions());
	}

	public getById(id: string): Observable<User> {
		return this.http.get<User>(this.apiUrl + '/' + id, this.getRequestOptions());
	}
}

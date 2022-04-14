import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Language } from '../models/language.model';
import { BaseService } from '../../shared/services/base.service';

@Injectable({
	providedIn: 'root'
})
export class LanguageService extends BaseService {

	constructor(private http: HttpClient) {
		super(http);
		this.apiUrl += '/languages';
	}

	public getAll(): Observable<Language[]> {
		return this.http.get<Language[]>(this.apiUrl, this.getRequestOptions());
	}

	public getById(id: string): Observable<Language> {
		return this.http.get<Language>(this.apiUrl + '/' + id, this.getRequestOptions());
	}

	public create(language: Language): Observable<Language> {
		return this.http.post<Language>(this.apiUrl, JSON.stringify(language), this.getRequestOptions());
	}

	public edit(language: Language): Observable<Language> {
		return this.http.put<Language>(this.apiUrl + '/' + language?.id, JSON.stringify(language), this.getRequestOptions());
	}

}

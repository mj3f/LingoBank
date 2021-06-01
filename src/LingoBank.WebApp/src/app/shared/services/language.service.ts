import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Language } from '../models/language.model';
import { BaseService } from './base.service';

@Injectable()
export class LanguageService extends BaseService {

    constructor(private http: HttpClient) {
        super(http);
        this.apiUrl += '/languages/';
    }

    public getAll(): Observable<Language[]> {
        return this.http.get<Language[]>(this.apiUrl);
    }

    public getById(id: string): Observable<Language> {
        return this.http.get<Language>(this.apiUrl + id);
    }

    public create(language: Language): Observable<string> {
        return this.http.post<string>(this.apiUrl, JSON.stringify(language));
    }

    public edit(language: Language): Observable<string> {
        return this.http.put<string>(this.apiUrl + language?.id, JSON.stringify(language));
    }

}
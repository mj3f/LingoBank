import { Injectable } from '@angular/core';
import {BaseService} from '../../../shared/services/base.service';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {Phrase} from '../../models/phrase.model';

@Injectable()
export class PhraseService extends BaseService {

	constructor(private http: HttpClient) {
		super(http);
		this.apiUrl += '/phrases';
	}

	public create(phrase: Phrase): Observable<Phrase> {
		return this.http.post<Phrase>(this.apiUrl, JSON.stringify(phrase), this.getRequestOptions());
	}
}

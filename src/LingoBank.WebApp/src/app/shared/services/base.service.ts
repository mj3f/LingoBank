import { HttpClient } from '@angular/common/http';


export abstract class BaseService {
    protected apiUrl = 'http://localhost:5000/api/v0';

    constructor(http: HttpClient) {

    }
}

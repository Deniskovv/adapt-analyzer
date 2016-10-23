import { Injectable } from '@angular/core';
import { Http } from '@angular/http';

@Injectable()
export class DatacardService {
    constructor(private http: Http) {}

    upload(file: File) {
        this.http.post('/upload', file);
    }
}
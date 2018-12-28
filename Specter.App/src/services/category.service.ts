import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { environment } from '../environments/environment'
import { Category } from 'src/models/category';

@Injectable({ providedIn: 'root' })
export class CategoryService {
    constructor(private http: HttpClient) { }

    getAll() {
        return this.http.get<Category[]>(`${environment.apiUrl}/category`);
    }
}
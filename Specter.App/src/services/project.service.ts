import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { environment } from '../environments/environment'
import { Project } from 'src/models/project';

@Injectable({ providedIn: 'root' })
export class ProjectService {
    constructor(private http: HttpClient) { }

    getAll() {
        return this.http.get<Project[]>(`${environment.apiUrl}/project`);
    }
}
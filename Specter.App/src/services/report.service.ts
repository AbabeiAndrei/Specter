import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { environment } from '../environments/environment';
import { Report } from 'src/models/report';

@Injectable({ providedIn: 'root' })
export class ReportingService {
    constructor(private http: HttpClient) { }

    get(filter: string) {
        return this.http.get<Report>(`${environment.apiUrl}/Reporting?filter=` + encodeURIComponent(filter));
    }
}

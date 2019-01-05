import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { environment } from '../environments/environment'
import { Timesheet } from '../models/timesheet';

@Injectable({ providedIn: 'root' })
export class TimesheetService {
    constructor(private http: HttpClient) { }

    getAll(date: string) {
        return this.http.get<Timesheet[]>(`${environment.apiUrl}/timesheet/` + date);
    }

    add(ts: Timesheet) {
      return this.http.post(`${environment.apiUrl}/timesheet/`, ts);
    }
}
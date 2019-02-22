import { Injectable } from '@angular/core';

@Injectable({ providedIn: 'root' })
export class DateUtilsProvider {

  getMonday(date: Date) {
    const d = new Date(date);
    const day = d.getDay(),
          diff = d.getDate() - day + (day === 0 ? -6 : 1); // adjust when day is sunday

    return new Date(d.setDate(diff));
  }

  getSunday(date: Date) {
    const d = this.getMonday(date);
    d.setDate(d.getDate() + 6);

    return d;
  }
}

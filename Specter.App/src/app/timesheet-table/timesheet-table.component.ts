import { Component, Input, SimpleChanges } from '@angular/core';
import { MatTableDataSource } from '@angular/material';
import { FormControl, Validators } from '@angular/forms';
import { Timesheet } from 'src/models/timesheet';
import { Category } from 'src/models/category';
import { CategoryService } from 'src/services/category.service';
import { TimesheetService } from 'src/services/timesheet.service';

import {animate, state, style, transition, trigger} from '@angular/animations';



@Component({
  selector: 'timesheet-table',
  templateUrl: './timesheet-table.component.html',
  styleUrls: ['./timesheet-table.component.less'],
  animations: [
    trigger('detailExpand', [
      state('collapsed', style({height: '0px', minHeight: '0', display: 'none'})),
      state('expanded', style({height: '*'})),
      transition('expanded <=> collapsed', animate('225ms cubic-bezier(0.4, 0.0, 0.2, 1)')),
    ]),
  ],
})
export class TimesheetTableComponent {

    _date: Date;
    expandedTs: Timesheet;

    get date(): Date {
      return this._date;
    }
    @Input() 
    set date(val: Date) {
      this._date = val;
      this.onDateChanged();
    }

    timesheets: MatTableDataSource<Timesheet>;

    displayedColumns: string[] = ['category', 'project', 'name', 'time'];

    constructor(private timesheetService: TimesheetService) { }
    
    onDateChanged() {
      this.timesheetService.getAll(this._date.toISOString()).subscribe(ts => {
        this.timesheets = new MatTableDataSource(ts);//.pipe(first())
      });
    }

    
    applyFilter(filterValue: string) {
      this.timesheets.filter = filterValue.trim().toLowerCase();
    }
}
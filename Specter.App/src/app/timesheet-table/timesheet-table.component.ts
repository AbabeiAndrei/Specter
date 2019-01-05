import { Component, Input, SimpleChanges } from '@angular/core';
import { MatTableDataSource, MatDialog } from '@angular/material';
import { FormControl, Validators } from '@angular/forms';
import { Timesheet } from 'src/models/timesheet';
import { Category } from 'src/models/category';
import { CategoryService } from 'src/services/category.service';
import { TimesheetService } from 'src/services/timesheet.service';

import {animate, state, style, transition, trigger} from '@angular/animations';
import { TimesheetEditDialog } from '../timesheet/timesheet-edit-dialog/timesheet-edit-dialog.component';



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

    constructor(private timesheetService: TimesheetService, public dialog: MatDialog) { }
    
    onDateChanged() {
      this.timesheetService.getAll(this._date.toISOString()).subscribe(ts => {
        this.timesheets = new MatTableDataSource(ts);//.pipe(first())
      });
    }

    applyFilter(filterValue: string) {
      this.timesheets.filter = filterValue.trim().toLowerCase();
    }

    getTotalTime(): number {
      if(this.timesheets == null || this.timesheets.data.length <= 0)
        return 0;
        
      return this.timesheets.data.map(t => t.time).reduce((acc, value) => acc + value, 0);
    }

    editTimesheet(ts: Timesheet) {
      var copy = ts;

      const dialogRef = this.dialog.open(TimesheetEditDialog, {
        data: Object.assign({}, ts),
        width: '400px'
      });
  
      dialogRef.afterClosed().subscribe(result => {
        console.log('The dialog was closed');
        console.log(result);
        copy.name = result.name;
      });
    }
}
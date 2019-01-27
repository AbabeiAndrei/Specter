import { Component, Input } from '@angular/core';
import { MatTableDataSource, MatDialog, MatSnackBar } from '@angular/material';
import { Timesheet } from 'src/models/timesheet';
import { TimesheetService } from 'src/services/timesheet.service';

import {animate, state, style, transition, trigger} from '@angular/animations';
import { TimesheetEditDialog } from '../timesheet/timesheet-edit-dialog/timesheet-edit-dialog.component';



@Component({
  // tslint:disable-next-line:component-selector
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

    displayedColumns: string[] = ['category', 'id', 'name', 'time'];

    constructor(private timesheetService: TimesheetService,
                private dialog: MatDialog,
                private snackBar: MatSnackBar) { }

    onDateChanged() {
      this.refresh();
    }

    public refresh() {
      this._date.setHours(this._date.getHours() - this._date.getTimezoneOffset() / 60);

      this.timesheetService.getAll(this._date.toISOString()).subscribe(ts => {
        this.timesheets = new MatTableDataSource(ts);
      });
    }

    applyFilter(filterValue: string) {
      this.timesheets.filter = filterValue.trim().toLowerCase();
    }

    getTotalTime(): number {
      if (this.timesheets == null || this.timesheets.data.length <= 0) {
        return 0;
      }

      return this.timesheets.data.map(t => t.time).reduce((acc, value) => acc + value, 0);
    }

    editTimesheet(ts: Timesheet) {
      const copy = ts;

      const dialogRef = this.dialog.open(TimesheetEditDialog, {
        data: Object.assign({}, ts),
        width: '400px'
      });

      dialogRef.afterClosed().subscribe(result => {

        if (result === undefined) {
          return;
        }

        if (result.removed) {
          this.timesheetService.delete(result.timesheet.id).subscribe(_ => {
            const i = this.timesheets.data.findIndex(t => t.id === copy.id);

            if (i >= 0) {
              this.timesheets.data.splice(i, 1);
              this.timesheets = new MatTableDataSource(this.timesheets.data);
              this.expandedTs = null;
            }
          },
          error => {
            this.snackBar.open(error, 'Ok', {
              duration: 8000,
            });
          });
        } else {
          this.timesheetService.update(result.timesheet).subscribe(_ => {
            Object.assign(copy, result.timesheet);
          },
          error => {
            this.snackBar.open(error, 'Ok', {
              duration: 8000,
            });
          });
        }
      });
    }
}

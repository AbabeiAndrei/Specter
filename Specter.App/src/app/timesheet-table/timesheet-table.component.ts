import { Component, Input } from '@angular/core';


@Component({
  selector: 'timesheet-table',
  templateUrl: './timesheet-table.component.html',
  styleUrls: ['./timesheet-table.component.less']
})
export class TimesheetTableComponent {
    @Input() date: Date;
}
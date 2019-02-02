import { Component, Input} from '@angular/core';
import { MatTableDataSource } from '@angular/material';
import { Timesheet } from 'src/models/timesheet';

@Component({
  selector: 'reports-table',
  templateUrl: './reports-table.component.html',
  styleUrls: ['./reports-table.component.less']
})
export class ReportsTableComponent {

    displayedColumns: string[] = ['id', 'date', 'name', 'description', 'project', 'delivery', 'category', 'time', 'flags'];

    @Input()
    dataSource: MatTableDataSource<Timesheet> = new MatTableDataSource();

    getTotalTime(): number {
        if (this.dataSource == null || this.dataSource.data.length <= 0) {
          return 0;
        }

        return this.dataSource.data.map(t => t.time).reduce((acc, value) => acc + value, 0);
      }
}

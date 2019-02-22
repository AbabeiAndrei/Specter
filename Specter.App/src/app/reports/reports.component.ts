import { Component, OnInit } from '@angular/core';
import { ReportingService } from 'src/services/report.service';
import { MatTableDataSource } from '@angular/material';
import { ReportFilterBuilder } from 'src/services/reportFilterBuilder.service';
import { Timesheet } from 'src/models/timesheet';
import { DateUtilsProvider } from 'src/services/dateutils';

@Component({
  selector: 'reports',
  templateUrl: './reports.component.html',
  styleUrls: ['./reports.component.less']
})
export class ReportsComponent implements OnInit {

  filterText: string;
  dataSource: MatTableDataSource<Timesheet> = new MatTableDataSource();

  constructor(private reportingService: ReportingService,
    private reporitingFilterbuilder: ReportFilterBuilder,
    private dateUtils: DateUtilsProvider) {}

  ngOnInit(): void {
    const date = new Date();
    this.performSearch(this.reporitingFilterbuilder.default(this.dateUtils.getMonday(date), this.dateUtils.getSunday(date)));
  }

  performSearch(filter: string) {
    this.reportingService.get(filter).subscribe(result => {
      this.filterText = result.filterText;
      this.dataSource = new MatTableDataSource(result.timesheets);
    });
  }

  export() {
    alert('export');
  }
}

import { Component, OnInit } from '@angular/core';
import { ReportingService } from 'src/services/report.service';
import { MatTableDataSource } from '@angular/material';
import { ReportFilterBuilder } from 'src/services/reportFilterBuilder.service';
import { Timesheet } from 'src/models/timesheet';

@Component({
  selector: 'reports',
  templateUrl: './reports.component.html',
  styleUrls: ['./reports.component.less']
})
export class ReportsComponent implements OnInit {

  dataSource: MatTableDataSource<Timesheet> = new MatTableDataSource();

  constructor(private reportingService: ReportingService, private reporitingFilterbuilder: ReportFilterBuilder) {}

  ngOnInit(): void {
    this.performSearch(this.reporitingFilterbuilder.default(new Date()));
  }

  performSearch(filter: string) {
    this.reportingService.get(filter).subscribe(result => {
      this.dataSource = new MatTableDataSource(result.timesheets);
    });
  }

  export() {
    alert('export');
  }
}

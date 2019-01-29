import { Component } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';
import { User } from '../../../models/user';
import { MatDialog, MatTableDataSource } from '@angular/material';
import { AdvancedFilterDialog } from './advanced-filter-dialog.component';
import { ReportFilterBuilder } from 'src/services/reportFilterBuilder.service';
import { ReportingService } from 'src/services/report.service';
import { Report } from 'src/models/report';

@Component({
  selector: 'reports-filter',
  templateUrl: './reports-filter.component.html',
  styleUrls: ['./reports-filter.component.less']
})
export class ReportsFilterComponents {
  showAdvancedFilter = false;

  dateNow = new Date();
  dateFrom = new FormControl(this.dateNow, []);
  dateTo = new FormControl(this.dateNow, []);
  textControl = new FormControl('', []);

  categoryControl = new FormControl('', []);
  projectControl = new FormControl('', []);
  deliveryControl = new FormControl('', []);
  usersController = new FormControl('', []);

  filter = '=';

  dataSource: MatTableDataSource<Report>;

  constructor(private dialog: MatDialog, private filterBuilder: ReportFilterBuilder, private reportingService: ReportingService) {}

  getUserFullName(user: User): string {
    return user.firstName + ' ' + user.lastName;
  }

  showAdvancedFilterDialog() {

    const dialogRef = this.dialog.open(AdvancedFilterDialog, {
      width: '80vw'
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result == null) {
        return;
      }

      this.showAdvancedFilter = result.show && result.value;
      this.filter = result.value.trim();
    });
  }

  performSearch(): void {
    let filter: string;

    if (this.filter.length <= 1 ) {
      filter = this.getFilterFromDefault();
    } else {
      filter = this.filter;
    }

    this.reportingService.get(filter).subscribe(result => {
      this.dataSource = new MatTableDataSource(result);
    });
  }

  getFilterFromDefault(): string {
    this.filterBuilder.clear();

    const dateFrom = this.getDateString(this.dateFrom.value as Date);
    const dateTo = this.getDateString(this.dateTo.value as Date);

    this.filterBuilder.set('DATE', dateFrom + '-' + dateTo);

    if (this.textControl.value.trim().length > 0) {
      this.filterBuilder.set('TEXT', this.textControl.value);
    }

    return this.filterBuilder.toString();
  }

  getDateString(date: Date): string {
    return date.toISOString();
  }
}

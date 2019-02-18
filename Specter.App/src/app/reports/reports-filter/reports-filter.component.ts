import { Component, Output, EventEmitter } from '@angular/core';
import { FormControl } from '@angular/forms';
import { User } from '../../../models/user';
import { MatDialog } from '@angular/material';
import { AdvancedFilterDialog } from './advanced-filter-dialog.component';
import { ReportFilterBuilder } from 'src/services/reportFilterBuilder.service';

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

  @Output()
  performSearch: EventEmitter<any> = new EventEmitter();

  constructor(private dialog: MatDialog, private filterBuilder: ReportFilterBuilder) {}

  getUserFullName(user: User): string {
    return user.firstName + ' ' + user.lastName;
  }

  week(): void {

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

  onPerformSearch(): void {
    let filter: string;

    if (this.filter.length <= 1 ) {
      filter = this.getFilterFromDefault();
    } else {
      filter = this.filter;
    }

    if (this.performSearch != null) {
      this.performSearch.emit(filter);
    }
  }

  getFilterFromDefault(): string {
    this.filterBuilder.clear();

    const dateFrom = this.getDateString(this.dateFrom.value as Date);
    const dateTo = this.getDateString(this.dateTo.value as Date);

    this.filterBuilder.set('DATE', dateFrom + '-' + dateTo);

    if (this.textControl.value.trim().length > 0) {
      this.filterBuilder.set('TEXT', this.textControl.value);
    }

    this.filterBuilder.set('USER', '#Me');

    return this.filterBuilder.toString();
  }

  getDateString(date: Date): string {
    return this.filterBuilder.formatDate(date);
  }
}

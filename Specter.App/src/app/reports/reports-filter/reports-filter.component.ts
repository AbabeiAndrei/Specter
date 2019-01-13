import { Component } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';
import { User } from '../../../models/user';
import { MatDialog } from '@angular/material';
import { AdvancedFilterDialog } from './advanced-filter-dialog.component';

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

  constructor(private dialog: MatDialog) {}

  getUserFullName(user: User): string {
    return user.firstName + ' ' + user.lastName;
  }

  showAdvancedFilterDialog() {
    const dialogRef = this.dialog.open(AdvancedFilterDialog, {
      width: '80vw'
    });

    dialogRef.afterClosed().subscribe(result => {
      this.showAdvancedFilter = result;
    });
  }
}

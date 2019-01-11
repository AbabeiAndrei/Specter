import { Component } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';
import { User } from '../../../models/user';

@Component({
  selector: 'reports-filter',
  templateUrl: './reports-filter.component.html',
  styleUrls: ['./reports-filter.component.less']
})
export class ReportsFilterComponents {
  expanded = false;

  dateNow = new Date();
  dateFrom = new FormControl(this.dateNow, []);
  dateTo = new FormControl(this.dateNow, []);
  textControl = new FormControl('', []);

  categoryControl = new FormControl('', []);
  projectControl = new FormControl('', []);
  deliveryControl = new FormControl('', []);
  usersController = new FormControl('', []);

  getUserFullName(user: User): string {
    return user.firstName + ' ' + user.lastName;
  }
}

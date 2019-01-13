import { Component } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';
import { User } from '../../../models/user';
import { MatDialogRef, MatTableDataSource } from '@angular/material';

export interface FilterItem {
  index: number;
  name: string;
  value: any;
}

@Component({
  selector: 'advanced-filter-dialog',
  templateUrl: './advanced-filter-dialog.component.html',
  styleUrls: ['./advanced-filter-dialog.component.less']
})
export class AdvancedFilterDialog {

  displayedColumns: string[] = ['name', 'value', 'actions'];

  filters: string[] = ['Project', 'Delivery', 'User', 'Date', 'Hours', 'Text'];

  dataSource: MatTableDataSource<FilterItem>;

  constructor(public dialogRef: MatDialogRef<AdvancedFilterDialog>) {
    this.dataSource = new MatTableDataSource();
  }

  confirm() {
    this.dialogRef.close(true);
  }

  cancel() {
    this.dialogRef.close(false);
  }

  addFilter() {
    const newDs = this.dataSource.data;
    newDs.push({index: newDs.length, name: 'User' + newDs.length, value: '#Me'});

    this.dataSource = new MatTableDataSource(newDs);
  }

  remove(filter: FilterItem) {

    if (this.dataSource.data.length <= 1) {
      this.dataSource = new MatTableDataSource();
    } else {
      const index = this.dataSource.data.findIndex(fi => fi.index === filter.index);

      if (index < 0) {
       return;
      }

      this.dataSource.data.splice(index, 1);

      this.dataSource = new MatTableDataSource(this.dataSource.data);
    }
  }
}

import { Component } from '@angular/core';
import { FormControl, Validators, ValidatorFn } from '@angular/forms';
import { MatDialogRef, MatTableDataSource, MatDialog } from '@angular/material';
import { InfoboxDialog, InfoboxData } from 'src/app/misc/infobox/infobox-dialog.component';
import { ReportFilterBuilder } from 'src/services/reportFilterBuilder.service';

export interface FilterItem {
  index: number;
  name: string;
  form: FormControl;
  options: FilterItemDefaults;
}

export interface FilterItemDefaults {
  default: string;
  readonly: boolean;
  info?: string
}

@Component({
  selector: 'advanced-filter-dialog',
  templateUrl: './advanced-filter-dialog.component.html',
  styleUrls: ['./advanced-filter-dialog.component.less']
})
export class AdvancedFilterDialog {

  displayedColumns: string[] = ['name', 'value', 'actions'];

  filters: string[] = ['Project', 'Delivery', 'User', 'Date', 'Hours', 'Text'];
  defaultValues: { [filter: string]: FilterItemDefaults } = 
  {
    'Project': {default: '', readonly: true},
    'User': {default: '#Me', readonly: true, info: 'For users you can use specific tags such as <b>#Me</b> or <b>#Team</b>'},
    'Delivery': {default: '', readonly: true},
    'Date' : {default: '#Week', readonly: true, info: 'For dates you can use specific keywords such as <b>#Yesterday</b>, <b>#Today</b>, <b>#Week</b> or others. </br> Check the documentation of the application for other options.'},
    'Hours': {default: '', readonly: false, info: 'For hours you can specify fixed values or intervals (2, 2-5, "1-2" OR "5")'}, 
    'Text': {default: '', readonly: false},
  };

  dataSource: MatTableDataSource<FilterItem>;
  rawFilterControl = new FormControl('');
  showRaw = false;

  constructor(public dialogRef: MatDialogRef<AdvancedFilterDialog>, private dialog: MatDialog, private filterBuilder: ReportFilterBuilder) {
    this.dataSource = new MatTableDataSource();
  }

  confirm() {
    this.dialogRef.close({show: true, value: this.rawFilterControl.value});
  }

  cancel() {
    this.dialogRef.close();
  }

  addFilter() {
    const newDs = this.dataSource.data;
    const options = this.defaultValues['User'];
    const item: FilterItem = 
    {
      index: newDs.length, 
      name: 'User',
      form: new FormControl(options.default),
      options: options
    };
    newDs.push(item);

    this.dataSource = new MatTableDataSource(newDs);
    
    this.rawFilterControl.setValue('User: #Me');
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

  changed(value: string, filter: FilterItem) {
    const def = this.defaultValues[value];

    filter.form.setValue(def.default);
    filter.options = def;
  }

  showInfo(filter: FilterItem) {
    const data: InfoboxData = {
      title: 'Filtering info guide',
      text: '<p>You can declare entities as simple strings or in quotes and ' +
            'also using AND or OR keywords to make more complex filtering.</p><p>' + 
            (filter.options.info || '') +
            
            '</p><p><code><b>Eg.</b> ("This item" AND "Other item") OR "This one"</code></p>'
    };
    const dialogRef = this.dialog.open(InfoboxDialog, {
      width: '40vw',
      data: data
    });

    dialogRef.afterClosed().subscribe(result => { });
  }
}

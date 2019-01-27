import { Component, ViewChild } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';
import { CategoryService } from 'src/services/category.service';
import { Category } from 'src/models/category';
import { Project } from 'src/models/project';
import { ProjectService } from 'src/services/project.service';
import { Delivery } from 'src/models/delivery';
import { TimesheetService } from 'src/services/timesheet.service';
import { Timesheet } from 'src/models/timesheet';
import { TimesheetTableComponent } from '../timesheet-table/timesheet-table.component';
import { MatSnackBar } from '@angular/material';

@Component({
  selector: 'app-timesheet-component',
  templateUrl: './timesheet.component.html',
  styleUrls: ['./timesheet.component.less']
})
export class TimesheetComponent {

  date = new FormControl(new Date());

  categoryControl = new FormControl('', [Validators.required]);
  projectControl = new FormControl('', [Validators.required]);
  deliveryControl = new FormControl('', [Validators.required]);
  title = new FormControl('', [Validators.required, Validators.maxLength(256)]);
  description = new FormControl('', [Validators.maxLength(1024)]);
  hours = new FormControl('1', [Validators.required, Validators.min(0.1), Validators.max(12)]);

  categories: Category[] = [];
  projects: Project[] = [];
  deliveries: Delivery[] = [];
  errors: string[] = [];

  @ViewChild('timesheetTable') timesheetTable: TimesheetTableComponent;

  constructor(private categoryService: CategoryService,
              private projectService: ProjectService,
              private tsService: TimesheetService,
              private snackBar: MatSnackBar) {  }

  // tslint:disable-next-line:use-life-cycle-interface
  ngOnInit() {
    this.categoryService.getAll().subscribe(c => {
      this.categories = c;
    });

    this.projectService.getAll().subscribe(p => {
      this.projects = p;
    });
  }

  setDelivery(project: Project) {
    this.deliveries = project.deliveries;
    this.deliveryControl.reset();
  }

  today() {
    this.date.setValue(new Date());
  }

  sendTimesheet() {
    const errors = [];

    const ts = new Timesheet();

    if (!this.title.valid) {
      this.title.markAsTouched();
      errors.push('Please fill the title');
    }

    if (!this.hours.valid) {
      this.hours.markAsTouched();
      errors.push('Please input a corect number of hours');
    }

    if (!this.description.valid) {
      this.description.markAsTouched();
      errors.push('Description too long');
    }

    if (!this.categoryControl.valid) {
      this.categoryControl.markAsTouched();
      errors.push('Please selecte the category');
    }

    if (!this.projectControl.valid) {
      this.projectControl.markAsTouched();
      errors.push('Please select the project');
    }

    if (!this.deliveryControl.valid && this.deliveries.length > 0) {
      this.deliveryControl.markAsTouched();
      errors.push('Please select a delivery');
    }

    const date:Date = this.date.value;

    date.setHours(date.getHours() - date.getTimezoneOffset() / 60);

    ts.name = this.title.value;
    ts.description = this.description.value;
    ts.time = this.hours.value;
    ts.date = date;
    ts.categoryId = this.categoryControl.value.id;
    ts.projectId = this.projectControl.value.id;
    if (this.deliveryControl.value != null) {
      ts.deliveryId = this.deliveryControl.value.id;
    }

    this.errors = errors;

    if (errors.length > 0) {
      return;
    }

    this.tsService.add(ts).subscribe(
      result => {
        this.timesheetTable.refresh();
        this.clearFields();
      },
      error => {
        this.snackBar.open(error, 'Ok', {
          duration: 8000,
      });
    });
  }

  clearFields() {
    this.title.setValue('');
    this.title.markAsUntouched();
    this.description.setValue('');
    this.description.markAsUntouched();
  }
}

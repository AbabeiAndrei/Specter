import { Component, Inject, Injectable } from "@angular/core";
import { MatDialogRef, MAT_DIALOG_DATA } from "@angular/material";
import { Timesheet } from "src/models/timesheet";
import { FormControl, Validators } from "@angular/forms";

@Component({
    selector: 'timesheet-edit-dialog',
    templateUrl: 'timesheet-edit-dialog.component.html',
    styleUrls: ['timesheet-edit-dialog.component.less']
  })
  export class TimesheetEditDialog {
    
    title = new FormControl(this.ts.name, [Validators.required, Validators.maxLength(256)]);
    description = new FormControl(this.ts.description, [Validators.maxLength(1024)]);
    hours = new FormControl(this.ts.time, [Validators.required, Validators.min(0), Validators.max(12)]);

    removed: boolean;    
  
    constructor(
      public dialogRef: MatDialogRef<TimesheetEditDialog>,
      @Inject(MAT_DIALOG_DATA) public ts: Timesheet) { }

    confirm() {
      
      var errors = false;
    
      if(!this.title.valid)
      {
        this.title.markAsTouched();
        errors = true;
      }

      if(!this.hours.valid)
      {
        this.hours.markAsTouched();
        errors = true;
      }

      if(!this.description.valid)
      {
        this.description.markAsTouched();
        errors = true;
      }

      if(errors)
        return;

      this.ts.name = this.title.value;
      this.ts.description = this.description.value;
      this.ts.time = this.hours.value;

      this.dialogRef.close({removed: this.removed, timesheet: this.ts});
    }
  
    cancel() {
      this.dialogRef.close();
    }
  
  }
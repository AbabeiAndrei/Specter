
import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';

export interface InfoboxData {
  title: string;
  text: string;
}

@Component({
    selector: 'infobox-dialog',
    templateUrl: 'infobox-dialog.component.html',
    styleUrls: ['infobox-dialog.component.less']
})
export class InfoboxDialog {
  data: InfoboxData;

  constructor(
    public dialogRef: MatDialogRef<InfoboxDialog>,
    @Inject(MAT_DIALOG_DATA) data: InfoboxData) {
      this.data = data;
    }

  confirm() {
    this.dialogRef.close(true);
  }

  cancel() {
    this.dialogRef.close(false);
  }
}

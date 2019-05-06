import { Component, Inject } from '@angular/core';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { ImageProcessed } from '../models/ImageProcessed';

@Component({
  selector: 'app-dialog',
  templateUrl: './dialog.component.html',
  styleUrls: ['./dialog.component.scss']
})
export class DialogComponent {
  resultString = "";
  certainty = "";
  betterTag = "";

  constructor(public dialogRef: MatDialogRef<DialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: ImageProcessed) {
    if (!data.isHotDog) {
      this.resultString = "No Hot Dog! Try again!"
      this.betterTag = "I rather see a " + data.tagName + " and";
    }
    else {
      this.resultString = "A Hot Dog! YAY!"
    }
    let prob: number = <number>data.probability;
    var probASString = (prob * 100).toFixed(2);
    this.certainty = "I am " + probASString + "% sure!"

  }

  onNoClick(): void {
    this.dialogRef.close();
  }

}

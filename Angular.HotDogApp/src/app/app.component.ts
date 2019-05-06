import { Component } from '@angular/core';
import { MatDialog } from '@angular/material';
import { DialogComponent } from './dialog/dialog.component';
import { AppService } from './app.service';
import { Prediction } from './models/Prediction';
import { FormControl } from '@angular/forms';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})

export class AppComponent {
  title = 'hotdogapp';
  file: File;
  fileUrl: any;
  filePresent: boolean = false;
  url = new FormControl('');
  constructor(public dialog: MatDialog, private predService: AppService) {
  }

  onFileChanged(event) {
    // this.openDialog(true, "99", "Mouse");

    this.filePresent = false;
    this.file = event.target.files[0];
    console.log(this.file);

    var reader = new FileReader();
    // this.fileUrl = this.file.blob;
    reader.readAsDataURL(this.file);
    this.filePresent = true;
    reader.onload = (_event) => {
      this.fileUrl = reader.result;
    }
  }

  getPrediction() {
    if (this.file != null || this.file != undefined) {
      this.predService.sendImageToEndpoint(this.file).subscribe(v => {
        var isHotDog: boolean = false;
        var prefElement: Prediction = new Prediction();
        prefElement.probability = 0;
        v.predictions.forEach((element: Prediction) => {
          console.log(element)
          if (element.probability > 0.7 && element.probability > prefElement.probability) {
            prefElement = element;
          }
        });
        if (prefElement == null) {
          // if none mathes we take the last element
          prefElement = v.predictions.pop();
        }
        isHotDog = prefElement.tagName == "HotDog" ? true : false;
        this.openDialog(isHotDog, prefElement.probability.toString(), prefElement.tagName);
      });

    }
    if (this.url.value) {
      this.predService.sentImageAsUrl(this.url.value).subscribe(v => {
        var isHotDog: boolean = false;
        var prefElement: Prediction = new Prediction();
        prefElement.probability = 0;
        v.predictions.forEach((element: Prediction) => {
          console.log(element)
          if (element.probability > 0.7 && element.probability > prefElement.probability) {
            prefElement = element;
          }
        });
        if (prefElement == null) {
          // if none mathes we take the last element
          prefElement = v.predictions.pop();
        }
        isHotDog = prefElement.tagName == "HotDog" ? true : false;
        this.openDialog(isHotDog, prefElement.probability.toString(), prefElement.tagName);
      });

    }
    this.file = null;
    this.url = new FormControl('');

  }

  openDialog(hotDog: boolean, probability: string, tagName: string): void {
    const dialogRef = this.dialog.open(DialogComponent, { width: '600px', data: { isHotDog: hotDog, probability: probability, tagName: tagName } });
  }
}

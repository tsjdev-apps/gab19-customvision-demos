import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { PredictionResponse } from './models/PredictionResponse';

@Injectable({
  providedIn: 'root'
})
export class AppService {
  predKey = "";

  constructor(private http: HttpClient) {
    this.predKey = environment.CognitiveServices.predictionKey;
  }

  sendImageToEndpoint(file: any) {

    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/octet-stream',
        'Prediction-Key': environment.CognitiveServices.predictionKey,
      })
    };
    return this.http.post<PredictionResponse>(environment.CognitiveServices.url + "/image", file, httpOptions)
  }

  sentImageAsUrl(url: string) {

    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Prediction-Key': environment.CognitiveServices.predictionKey,
      })
    };
    var body = `{
      "Url": '${url}'
    }`
    var result = this.http.post<PredictionResponse>(environment.CognitiveServices.url + '/url', body, httpOptions);
    return result;

  }
  /*
  
      Set Prediction-Key Header to : 4b470d77b4a0435884e27a339f0e6247
      Set Content-Type Header to : application/json
      Set Body to : {"Url": "https://example.com/image.png"}
      If you have an image file:
      Set Prediction-Key Header to : 4b470d77b4a0435884e27a339f0e6247
      Set Content-Type Header to : application/octet-stream
      Set Body to : <image file>
    
      */
}

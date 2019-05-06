import { Prediction } from './Prediction';

export class PredictionResponse {

    created: string;

    id: string;

    iteration: string;

    predictions: Prediction[];

}
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { WeatherForecast } from '../models/weather-forecast';

@Injectable({ providedIn: 'root' })
export class WeatherForecastService {

  resource = 'weatherforecast';

  constructor(private httpClient: HttpClient) { }

  get(): Observable<Array<WeatherForecast>> {
    return this.httpClient.get<Array<WeatherForecast>>(`${this.resource}/`);
  }

  post(weatherForecast: WeatherForecast): Observable<WeatherForecast> {
    return this.httpClient.post<WeatherForecast>(`${this.resource}/`, JSON.stringify(weatherForecast));
  }
}

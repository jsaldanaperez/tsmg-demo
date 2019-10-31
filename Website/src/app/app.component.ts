import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { WeatherForecastService } from './services/weather-forecast.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'Website';
  loading: boolean;
  result;

  constructor(private weatherForecastService: WeatherForecastService) { }

  getData() {
    this.loading = true;
    this.weatherForecastService.get()
      .subscribe((result) => {
        this.result = result;
        this.loading = false;
      });
  }
}

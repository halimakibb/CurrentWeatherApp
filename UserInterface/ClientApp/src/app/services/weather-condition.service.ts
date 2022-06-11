import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { WeatherCondition } from '../model/weather-condition.model';

@Injectable({
  providedIn: 'root'
})
export class WeatherConditionService {
    readonly url = 'https://localhost:44300/CurrentWeather/';

    weatherCondition: WeatherCondition;
    constructor(private http: HttpClient) { }

    GetWeatherCondition(cityName: string, countryCode: string) {
        this.http.get(this.url + 'GetWeatherCondition/' + cityName + '/' + countryCode).toPromise().then(result => this.weatherCondition = result as WeatherCondition)
    }

}

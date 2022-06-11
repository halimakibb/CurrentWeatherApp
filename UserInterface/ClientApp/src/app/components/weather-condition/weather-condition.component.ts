import { Component, OnInit } from '@angular/core';
import { City } from '../../model/city.model';
import { Country } from '../../model/country.model';
import { WeatherCondition } from '../../model/weather-condition.model';
import { CountriesCitiesService } from '../../services/countries-cities.service';
import { WeatherConditionService } from '../../services/weather-condition.service';
@Component({
  selector: 'weather-condition',
  templateUrl: './weather-condition.component.html',
})

export class WeatherConditionComponent implements OnInit {
    constructor(private countriesCitiesService: CountriesCitiesService, private weatherConditionService: WeatherConditionService) {
    }
    ngOnInit() {
        this.countriesCitiesService.CountryList();
    }
    BindCity(country: Country) {
        this.weatherConditionService.weatherCondition = null;
        this.countriesCitiesService.CityByCountry(country.countryCode);
    }
    BindWeatherCondition(city: City) {
        this.weatherConditionService.weatherCondition = null;
        this.weatherConditionService.GetWeatherCondition(city.cityName, city.countryCode);
    }
}   


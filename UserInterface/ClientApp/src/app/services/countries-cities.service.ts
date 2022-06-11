import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Country } from '../model/country.model';
import { City } from '../model/city.model';
@Injectable({
  providedIn: 'root'
})
export class CountriesCitiesService {

    readonly url = 'https://localhost:44300/CountriesAndCities/';
    listCountry: Country[];
    listCity: City[];
    constructor(private http: HttpClient) { }

    CountryList() {
        this.http.get(this.url + 'GetCountries').toPromise().then(result => this.listCountry = result as Country[])
    }

    CityByCountry(countryCode: string) {
        return this.http.get(this.url + 'GetCities/' + countryCode).toPromise().then(result => this.listCity = result as City[])
    }

}

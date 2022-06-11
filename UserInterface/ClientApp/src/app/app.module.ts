import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { WeatherConditionComponent } from './components/weather-condition/weather-condition.component';
import { CountriesCitiesService } from './services/countries-cities.service';
import { WeatherConditionService } from './services/weather-condition.service';


@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        WeatherConditionComponent
    ],
    imports: [
        BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
        HttpClientModule,
        FormsModule,
        RouterModule.forRoot([
            { path: '', component: WeatherConditionComponent, pathMatch: 'full' }
        ])
    ],
    providers: [CountriesCitiesService, WeatherConditionService],
    bootstrap: [AppComponent]
})
export class AppModule { }

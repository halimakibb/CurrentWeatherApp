import { Time } from "@angular/common";



export class WeatherCondition {
    timezone: number;
    visibility: string;
    dewPoint: string;
    coord: Coord;
    time: Date;
    weather: Array<Weather>;
    main: Main;

}

export class Coord {
    lon: string;
    lat: string;
}

export class Weather {
    id: string;
    main: string;
    description: string;
}

export class Main {
    temp: string;
    tempC: string;
    pressure: string;
    humidity: string;
}

export class Wind {
    speed: string;
    deg: string;
}

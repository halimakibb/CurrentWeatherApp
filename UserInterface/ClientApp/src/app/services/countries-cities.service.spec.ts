import { TestBed } from '@angular/core/testing';

import { CountriesCitiesService } from './countries-cities.service';

describe('CountriesCitiesService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: CountriesCitiesService = TestBed.get(CountriesCitiesService);
    expect(service).toBeTruthy();
  });
});

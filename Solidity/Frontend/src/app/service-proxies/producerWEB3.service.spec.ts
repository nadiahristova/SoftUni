import { TestBed, inject } from '@angular/core/testing';

import { ProducersWEB3Service } from './producerWEB3.service';

describe('WEB3Service', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [ProducersWEB3Service]
    });
  });

  it('should be created', inject([ProducersWEB3Service], (service: ProducersWEB3Service) => {
    expect(service).toBeTruthy();
  }));
});

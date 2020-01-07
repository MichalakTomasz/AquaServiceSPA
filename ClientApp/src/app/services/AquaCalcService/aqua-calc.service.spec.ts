import { TestBed } from '@angular/core/testing';

import { AquaCalcService } from './aqua-calc.service';

describe('AquaCalcService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: AquaCalcService = TestBed.get(AquaCalcService);
    expect(service).toBeTruthy();
  });
});

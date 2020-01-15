import { TestBed } from '@angular/core/testing';

import { CommonStringsService } from './common-strings.service';

describe('CommonStringsService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: CommonStringsService = TestBed.get(CommonStringsService);
    expect(service).toBeTruthy();
  });
});

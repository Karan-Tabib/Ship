import { TestBed } from '@angular/core/testing';

import { SalarySummaryService } from './salary-summary.service';

describe('SalarySummaryService', () => {
  let service: SalarySummaryService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SalarySummaryService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});

import { TestBed } from '@angular/core/testing';

import { LeavesSummaryService } from './leaves-summary.service';

describe('LeavesSummaryService', () => {
  let service: LeavesSummaryService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(LeavesSummaryService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});

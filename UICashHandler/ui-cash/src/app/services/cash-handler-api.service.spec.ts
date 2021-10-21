import { TestBed } from '@angular/core/testing';

import { CashHandlerApiService } from './cash-handler-api.service';

describe('CashHandlerApiService', () => {
  let service: CashHandlerApiService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CashHandlerApiService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});

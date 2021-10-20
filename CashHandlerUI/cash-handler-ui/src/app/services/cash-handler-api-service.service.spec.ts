import { TestBed } from '@angular/core/testing';

import { CashHandlerApiServiceService } from './cash-handler-api-service.service';

describe('CashHandlerApiServiceService', () => {
  let service: CashHandlerApiServiceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CashHandlerApiServiceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});

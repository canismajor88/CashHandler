import { TestBed } from '@angular/core/testing';

import { CashHandlerAuthService } from './cash-handler-auth.service';

describe('CashHandlerApiService', () => {
  let service: CashHandlerAuthService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CashHandlerAuthService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});

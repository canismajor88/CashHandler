import { TestBed } from '@angular/core/testing';

import { MoneyAmountService } from './money-amount.service';

describe('MoneyAmountService', () => {
  let service: MoneyAmountService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(MoneyAmountService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});

import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MoneyAmountsDisplayComponent } from './money-amounts-display.component';

describe('MoneyAmountsDisplayComponent', () => {
  let component: MoneyAmountsDisplayComponent;
  let fixture: ComponentFixture<MoneyAmountsDisplayComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MoneyAmountsDisplayComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(MoneyAmountsDisplayComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

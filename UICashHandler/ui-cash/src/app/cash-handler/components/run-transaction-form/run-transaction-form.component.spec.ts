import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RunTransactionFormComponent } from './run-transaction-form.component';

describe('RunTransactionFormComponent', () => {
  let component: RunTransactionFormComponent;
  let fixture: ComponentFixture<RunTransactionFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RunTransactionFormComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(RunTransactionFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

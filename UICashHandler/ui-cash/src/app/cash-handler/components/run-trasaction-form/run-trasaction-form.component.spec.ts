import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RunTrasactionFormComponent } from './run-trasaction-form.component';

describe('RunTrasactionFormComponent', () => {
  let component: RunTrasactionFormComponent;
  let fixture: ComponentFixture<RunTrasactionFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RunTrasactionFormComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(RunTrasactionFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

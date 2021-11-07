import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InternalNavComponent } from './internal-nav.component';

describe('InternalNavComponent', () => {
  let component: InternalNavComponent;
  let fixture: ComponentFixture<InternalNavComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ InternalNavComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(InternalNavComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

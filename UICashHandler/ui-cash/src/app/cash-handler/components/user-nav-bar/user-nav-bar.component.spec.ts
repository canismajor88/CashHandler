import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UserNavBarComponent } from './user-nav-bar.component';

describe('HeaderComponent', () => {
  let component: UserNavBarComponent;
  let fixture: ComponentFixture<UserNavBarComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UserNavBarComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(UserNavBarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

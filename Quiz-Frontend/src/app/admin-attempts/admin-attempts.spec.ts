import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminAttempts } from './admin-attempts';

describe('AdminAttempts', () => {
  let component: AdminAttempts;
  let fixture: ComponentFixture<AdminAttempts>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AdminAttempts]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AdminAttempts);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

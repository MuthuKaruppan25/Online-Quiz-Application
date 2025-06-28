import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ViewAnswer } from './view-answer';

describe('ViewAnswer', () => {
  let component: ViewAnswer;
  let fixture: ComponentFixture<ViewAnswer>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ViewAnswer]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ViewAnswer);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

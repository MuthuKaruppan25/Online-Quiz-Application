import { ComponentFixture, TestBed } from '@angular/core/testing';

import { QuestionCreateCard } from './question-create-card';

describe('QuestionCreateCard', () => {
  let component: QuestionCreateCard;
  let fixture: ComponentFixture<QuestionCreateCard>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [QuestionCreateCard]
    })
    .compileComponents();

    fixture = TestBed.createComponent(QuestionCreateCard);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

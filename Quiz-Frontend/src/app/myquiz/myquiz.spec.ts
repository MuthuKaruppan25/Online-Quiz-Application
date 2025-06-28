import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Myquiz } from './myquiz';

describe('Myquiz', () => {
  let component: Myquiz;
  let fixture: ComponentFixture<Myquiz>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [Myquiz]
    })
    .compileComponents();

    fixture = TestBed.createComponent(Myquiz);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

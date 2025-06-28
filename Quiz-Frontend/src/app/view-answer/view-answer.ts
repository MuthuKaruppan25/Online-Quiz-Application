import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { QuizAttempt } from '../Models/QuizAttempt';
import { Question, QuizData } from '../Models/QuizDataModel';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-view-answer',
  imports: [CommonModule],
  templateUrl: './view-answer.html',
  styleUrl: './view-answer.css',
})
export class ViewAnswer {
  quizAttempt!: QuizAttempt;
  quizData!: QuizData;

  constructor(private router: Router, private route: ActivatedRoute) {}

  ngOnInit(): void {
    const nav = history.state;
    if (!nav || !nav.quizAttempt || !nav.quiz) {
      this.router.navigate(['/dashboard']);
      return;
    }
    this.quizAttempt = nav.quizAttempt as QuizAttempt;
    this.quizData = nav.quiz as QuizData;

    console.log(this.quizAttempt);
    console.log(this.quizData);
  }

  getQuestion(questionId: string): Question | undefined {
    return this.quizData.questions?.find((q) => q.guid === questionId);
  }

  getAnswerClass(isCorrect: boolean): string {
    return isCorrect ? 'correct' : 'wrong';
  }
  getOptionText(question: Question, optionCode: string | undefined): string {
    if (!optionCode) return 'No answer';
    switch (optionCode) {
      case 'A':
        return question.optionA;
      case 'B':
        return question.optionB;
      case 'C':
        return question.optionC;
      case 'D':
        return question.optionD;
      default:
        return 'No answer';
    }
  }
}

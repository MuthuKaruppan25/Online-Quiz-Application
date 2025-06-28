import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { QuestionsAddDto, QuizAddDto } from '../Models/QuizModel';
import { QuizService } from '../services/QuizService';
import { SnackBar } from '../snack-bar/snack-bar';
import { Store } from '@ngrx/store';
import { selectAdminProfile } from '../store/Admin/admin.selectors';
import { Admin } from '../Models/UserModel';

@Component({
  selector: 'app-question-create-card',
  standalone: true,
  imports: [CommonModule, FormsModule, SnackBar],
  templateUrl: './question-create-card.html',
  styleUrl: './question-create-card.css',
  providers: [QuizService],
})
export class QuestionCreateCard {
  quizConfig: any;
  totalQuestions: number = 0;
  currentQuestionIndex = 0;
  adminData!: Admin;

  currentQuestion: QuestionsAddDto = new QuestionsAddDto();
  allQuestions: QuestionsAddDto[] = [];
  fullQuizData: QuizAddDto | null = null;

  // snackbar state
  snackMessage: string = '';
  snackSuccess: boolean = true;

  constructor(
    private router: Router,
    private quizService: QuizService,
    private store: Store
  ) {
    const nav = this.router.getCurrentNavigation();
    this.quizConfig = nav?.extras.state;

    if (this.quizConfig) {
      this.totalQuestions = this.quizConfig.totalQuestions;
    }
    this.store.select(selectAdminProfile).subscribe({
      next: (data: any) => {
        this.adminData = data as Admin;
        console.log(this.adminData);
      },
    });
  }

  isCurrentQuestionValid(): boolean {
    const q = this.currentQuestion;
    return (
      q.text.trim().length > 0 &&
      q.optionA.trim().length > 0 &&
      q.optionB.trim().length > 0 &&
      q.optionC.trim().length > 0 &&
      q.optionD.trim().length > 0 &&
      ['A', 'B', 'C', 'D'].includes(q.correctOption)
    );
  }

  saveAndNext() {
    if (!this.isCurrentQuestionValid()) {
      this.showSnack(
        'Please complete all question fields and select the correct option.',
        false
      );
      return;
    }

    this.allQuestions.push({ ...this.currentQuestion });
    this.currentQuestion = new QuestionsAddDto();
    this.currentQuestionIndex++;

    if (this.currentQuestionIndex === this.totalQuestions) {
      this.fullQuizData = new QuizAddDto({
        title: this.quizConfig.title,
        description: this.quizConfig.description,
        durationMinutes: this.quizConfig.duration,
        categoryId: this.quizConfig.categoryId,
        adminId: this.adminData.guid,
        questions: this.allQuestions,
      });
      console.log("Quiz Data",this.fullQuizData);

      this.quizService.InsertQuiz(this.fullQuizData).subscribe({
        next: () => {
          this.showSnack('Quiz created successfully!', true);
          setTimeout(() => {
            this.router.navigate(['/dashboard']);
          }, 3000); // wait so user can see the snackbar
        },
        error: (err) => {
          this.showSnack('Error creating quiz: ' + err.message, false);
          console.log(err);
        },
      });
    }
  }

  private showSnack(message: string, success: boolean) {
    this.snackMessage = message;
    this.snackSuccess = success;
  }
}

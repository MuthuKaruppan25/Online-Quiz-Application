import { Component, Input, OnInit } from '@angular/core';
import { QuizData } from '../Models/QuizDataModel';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { Category } from '../Models/categoryModel';
import { Router } from '@angular/router';
import { Store } from '@ngrx/store';
import { Attender } from '../Models/UserModel';
import { selectAttenderProfile } from '../store/User/user.selectors';
import { Attempts } from '../Models/AttemptorAttempts';
import { AttemptService } from '../services/AttemptService';

@Component({
  selector: 'app-quiz-card',
  imports: [FormsModule, CommonModule],
  templateUrl: './quiz-card.html',
  styleUrl: './quiz-card.css',
})
export class QuizCard implements OnInit {
  @Input() quiz!: QuizData;
  @Input() categories: Category[] = [];
  @Input() adminView: boolean = false;
  attenderData!: Attender;
  checkAttempt :boolean = false;
  selectedQuizForModal: QuizData | null = null;

  constructor(private router: Router, private store: Store,private attemptService:AttemptService) {
    this.store.select(selectAttenderProfile).subscribe({
      next: (data: any) => {
        this.attenderData = data as Attender;
        console.log(this.attenderData);
      },
    });
  }

  ngOnInit(): void {
        this.attemptService.checkExistingAttempt(this.quiz.id,this.attenderData.guid).subscribe({
      next:(data:any)=>{
        this.checkAttempt = data.success;
        console.log(this.checkAttempt,"attemot");
      }
    })
  }
  openTakeQuizPopup(event: MouseEvent, quiz: QuizData) {
    event.stopPropagation();
    this.selectedQuizForModal = quiz;
  }

  closeTakeQuizPopup() {
    this.selectedQuizForModal = null;
  }

  confirmTakeQuiz(quizId: string) {
    console.log('Taking quiz with id', quizId);

    this.selectedQuizForModal = null;
    this.router.navigate(['/dashboard/take/quiz'], {
      state: {
        quiz: this.quiz,
      },
    });
  }

  getRandomGradient(): string {
    const gradients = [
      'linear-gradient(135deg, #f43f5e, #f97316)',
      'linear-gradient(135deg, #10b981, #06b6d4)',
      'linear-gradient(135deg, #6366f1, #8b5cf6)',
      'linear-gradient(135deg, #ec4899, #ef4444)',
      'linear-gradient(135deg, #14b8a6, #3b82f6)',
    ];
    return gradients[Math.floor(Math.random() * gradients.length)];
  }
  getCategoryName(categoryId: string): string | null {
    return this.categories.find((c) => c.guid === categoryId)?.name || null;
  }
  viewQuizDetails(quizId: string) {
    console.log('Navigate to quiz details:', quizId);
  }

  viewAttempts() {
    this.router.navigate(['/dashboard/view/attempts'], {
      state: {
        quizData: this.quiz,
      },
    });
  }
}

import { Component, OnInit, HostListener } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AttemptService } from '../services/AttemptService';
import { QuizData } from '../Models/QuizDataModel';
import { QuizAttempt } from '../Models/QuizAttempt';
import { pagination } from '../Models/pagination';
import { Attempts } from '../Models/AttemptorAttempts';
import { HistoryCard } from "../history-card/history-card";
import { CommonModule } from '@angular/common';

interface PagedResult<T> {
  totalCount: number;
  data: T[];
  pageNumber: number;
  pageSize: number;
  totalPages: number;
}

@Component({
  selector: 'app-admin-attempts',
  standalone: true,
  imports: [HistoryCard,CommonModule],
  templateUrl: './admin-attempts.html',
  styleUrl: './admin-attempts.css',
})
export class AdminAttempts implements OnInit {
  quiz!: QuizData;
  attempts: Attempts[] = [];

  pageNumber = 1;
  pageSize = 10;
  totalPages = 1;
  isLoading = false;

  constructor(private router: Router, private attemptService: AttemptService) {}

  ngOnInit(): void {
    const nav = history.state;
    if (!nav || !nav.quizData) {
    
      this.router.navigate(['/dashboard']);
      return;
    }
   
    this.quiz = nav.quizData as QuizData;
    this.loadAttempts();
  }

  loadAttempts(): void {
    if (this.isLoading || this.pageNumber > this.totalPages) return;

    this.isLoading = true;
    const pageData = new pagination(this.pageNumber, this.pageSize);

    this.attemptService.getAttemptsByQuizId(this.quiz.id, pageData).subscribe({
      next: (res: PagedResult<Attempts>) => {
        this.attempts.push(...res.data);
        console.log(res);
        console.log(this.attempts);
        this.totalPages = res.totalPages;
        this.isLoading = false;
      },
      error: (err) => {
        console.error('Error loading attempts', err);
        this.isLoading = false;
      },
    });
  }

  @HostListener('window:scroll', [])
  onScroll(): void {
    if (
      window.innerHeight + window.scrollY >=
      document.body.offsetHeight - 200
    ) {
      if (!this.isLoading && this.pageNumber < this.totalPages) {
        this.pageNumber++;
        this.loadAttempts();
      }
    }
  }
}

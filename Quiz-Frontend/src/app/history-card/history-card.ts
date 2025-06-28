import { CommonModule } from '@angular/common';
import { Component, Input, OnInit } from '@angular/core';
import { Attempts } from '../Models/AttemptorAttempts';
import { Router } from '@angular/router';

@Component({
  selector: 'app-history-card',
  imports: [CommonModule],
  templateUrl: './history-card.html',
  styleUrl: './history-card.css',
})
export class HistoryCard implements OnInit {
  @Input() attempt!: Attempts;

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
  ngOnInit(): void {
    console.log(this.attempt);
  }
  constructor(private router: Router) {}
  viewScore() {
    console.log("state",this.attempt);
    console.log("state",this.attempt.quizData);
    this.router.navigate(['/dashboard/view/score'], {
      state: {
        quizAttempt: this.attempt,
        quiz: this.attempt.quizData,
      },
    });
  }
}

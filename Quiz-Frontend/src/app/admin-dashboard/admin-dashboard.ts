import { Component, OnInit } from '@angular/core';
import { Admin } from '../Models/UserModel';
import { Store } from '@ngrx/store';
import { selectAdminProfile } from '../store/Admin/admin.selectors';
import {
  AdminQuizAttemptsSummaryResponseDto,
  QuizAttemptsSummaryDto,
} from '../Models/AdminAttemptsModel';
import { AdminService } from '../services/AdminService';
import {
  LucideAngularModule,
  Layers,
  BookOpen,
  ThumbsUp,
  ThumbsDown,
  ListCheck,
} from 'lucide-angular';
import { ChartConfiguration } from 'chart.js';
import { BaseChartDirective, NgChartsModule } from 'ng2-charts';
import { ViewChildren, QueryList } from '@angular/core';
@Component({
  selector: 'app-admin-dashboard',
  imports: [LucideAngularModule, NgChartsModule],
  templateUrl: './admin-dashboard.html',
  styleUrl: './admin-dashboard.css',
})
export class AdminDashboard implements OnInit {
  readonly layers = Layers;
  readonly book = BookOpen;
  readonly up = ThumbsUp;
  readonly down = ThumbsDown;
  readonly list = ListCheck;
  @ViewChildren(BaseChartDirective) charts!: QueryList<BaseChartDirective>;
  totalQuizzes: number = 0;
  totalAttempts: number = 0;
  totalAbove50: number = 0;
  totalBelow50: number = 0;

  adminData!: Admin;
  summary!: AdminQuizAttemptsSummaryResponseDto;

  quizAttemptsChartData: ChartConfiguration<'bar'>['data'] = {
    labels: [],
    datasets: [{ data: [], label: 'Attempts per Quiz' }],
  };
  barChartOptions: ChartConfiguration<'bar'>['options'] = {
    responsive: true,
    scales: {
      y: { beginAtZero: true },
    },
  };

  attemptsPieChartData: ChartConfiguration<'pie'>['data'] = {
    labels: ['Above 50%', 'Below 50%'],
    datasets: [{ data: [0, 0], backgroundColor: ['#28a745', '#dc3545'] }],
  };
  pieChartOptions: ChartConfiguration<'pie'>['options'] = {
    responsive: true,
  };

  constructor(private store: Store, private adminService: AdminService) {
    this.store.select(selectAdminProfile).subscribe({
      next: (data: any) => {
        this.adminData = data as Admin;
      },
    });
  }
  ngOnInit(): void {
    this.adminService.getAttemptsSummary(this.adminData.guid).subscribe({
      next: (data: any) => {
        this.summary = data as AdminQuizAttemptsSummaryResponseDto;
        console.log(this.summary);
        this.totalQuizzes = this.summary.quizSummaries.length;
        this.totalAttempts = this.summary.quizSummaries.reduce(
          (acc, q) => acc + q.totalAttempts,
          0
        );
        this.totalAbove50 = this.summary.quizSummaries.reduce(
          (acc, q) => acc + q.attemptsAbove50Percent,
          0
        );
        this.totalBelow50 = this.summary.quizSummaries.reduce(
          (acc, q) => acc + q.attemptsBelowOrEqual50Percent,
          0
        );
        this.generateCharts();
      },
    });
  }
  generateCharts(): void {
    this.quizAttemptsChartData.labels = this.summary.quizSummaries.map(
      (q) => q.quizTitle
    );
    this.quizAttemptsChartData.datasets[0].data =
      this.summary.quizSummaries.map((q) => q.totalAttempts);

    this.attemptsPieChartData.datasets[0].data = [
      this.totalAbove50,
      this.totalBelow50,
    ];

    setTimeout(() => {
      this.charts?.forEach((chart) => chart.update());
    }, 0);
  }
}

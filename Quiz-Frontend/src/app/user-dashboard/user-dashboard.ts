import { Component, OnInit, ViewChildren, QueryList } from '@angular/core';
import { Store } from '@ngrx/store';
import { selectAttenderProfile } from '../store/User/user.selectors';
import { UserService } from '../services/UserService';
import {
  AttenderIndividualAttemptsResponseDto,
} from '../Models/UserAttemptsModel';
import { Attender } from '../Models/UserModel';
import { ChartConfiguration } from 'chart.js';
import { BaseChartDirective, NgChartsModule } from 'ng2-charts';
import {
  LucideAngularModule,
  Layers,
  BookOpen,
  ThumbsUp,
  ThumbsDown,
  ListCheck,
} from 'lucide-angular';

@Component({
  selector: 'app-user-dashboard',
  standalone: true,
  imports: [LucideAngularModule, NgChartsModule],
  templateUrl: './user-dashboard.html',
  styleUrl: './user-dashboard.css'
})
export class UserDashboard implements OnInit {
  readonly layers = Layers;
  readonly book = BookOpen;
  readonly up = ThumbsUp;
  readonly down = ThumbsDown;
  readonly list = ListCheck;

  @ViewChildren(BaseChartDirective) charts!: QueryList<BaseChartDirective>;

  userData!: Attender;
  summary!: AttenderIndividualAttemptsResponseDto;

  totalQuizzes = 0;
  averageScore = 0;
  above50 = 0;
  below50 = 0;

  scoresBarChartData: ChartConfiguration<'bar'>['data'] = {
    labels: [],
    datasets: [{ data: [], label: 'Score per Quiz' }]
  };
  barChartOptions: ChartConfiguration<'bar'>['options'] = {
    responsive: true,
    scales: {
      y: { beginAtZero: true, max: 100 }
    }
  };

  attemptsPieChartData: ChartConfiguration<'pie'>['data'] = {
    labels: ['Above 50%', 'Below 50%'],
    datasets: [
      { data: [0, 0], backgroundColor: ['#28a745', '#dc3545'] }
    ]
  };
  pieChartOptions: ChartConfiguration<'pie'>['options'] = {
    responsive: true
  };

  constructor(private store: Store, private userService: UserService) {
    this.store.select(selectAttenderProfile).subscribe({
      next: (data: any) => {
        this.userData = data as Attender;
      }
    });
  }

  ngOnInit(): void {
    this.userService.getAttemptsSummary(this.userData.guid).subscribe({
      next: (data:any) => {
        this.summary = data as AttenderIndividualAttemptsResponseDto;
        this.totalQuizzes = this.summary.attempts.length;
        this.averageScore = this.summary.attempts.length
          ? Math.round(
              this.summary.attempts.reduce((acc, a) => acc + a.score, 0) /
                this.summary.attempts.length
            )
          : 0;
        this.above50 = this.summary.attempts.filter((a) => a.isAbove50Percent).length;
        this.below50 = this.totalQuizzes - this.above50;

        this.generateCharts();
      }
    });
  }

  generateCharts(): void {
    this.scoresBarChartData.labels = this.summary.attempts.map(a => a.quizTitle);
    this.scoresBarChartData.datasets[0].data = this.summary.attempts.map(a => a.score);

    this.attemptsPieChartData.datasets[0].data = [
      this.above50,
      this.below50
    ];

    setTimeout(() => {
      this.charts.forEach(chart => chart.update());
    }, 0);
  }
}

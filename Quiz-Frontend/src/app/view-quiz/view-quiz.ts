import { Component, OnInit, HostListener } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Category } from '../Models/categoryModel';
import { CategoryService } from '../services/CategoryService';
import { QuizService } from '../services/QuizService';
import { QuizData } from '../Models/QuizDataModel';
import { pagination } from '../Models/pagination';
import { QuizCard } from '../quiz-card/quiz-card';
import { Router } from '@angular/router';
import { SnackBar } from '../snack-bar/snack-bar';
import { AttemptService } from '../services/AttemptService';
import { Store } from '@ngrx/store';
import { Attender } from '../Models/UserModel';
import { selectAttenderProfile } from '../store/User/user.selectors';

interface PagedResult<T> {
  totalCount: number;
  data: T[];
  pageNumber: number;
  pageSize: number;
  totalPages: number;
}

@Component({
  selector: 'app-view-quiz',
  standalone: true,
  imports: [CommonModule, FormsModule, QuizCard, SnackBar],
  templateUrl: './view-quiz.html',
  styleUrl: './view-quiz.css',
})
export class ViewQuiz implements OnInit {
  quizCode: string = '';
  categorySearch: string = '';
  categories: Category[] = [];
  filteredCategories: Category[] = [];
  selectedCategory: Category | null = null;
  quizData: QuizData[] = [];
  snackMessage: string = '';
  snackSuccess: boolean = true;
  snackVisible: boolean = false;
  attenderData!: Attender;
  pageNumber = 1;
  pageSize = 10;
  totalPages = 1;
  isLoading = false;

  constructor(
    private categoryService: CategoryService,
    private quizService: QuizService,
    private router: Router,
    private attemptService: AttemptService,
    private store: Store
  ) {
    this.store.select(selectAttenderProfile).subscribe({
      next: (data: any) => {
        this.attenderData = data as Attender;
        console.log(this.attenderData);
      },
    });
  }

  ngOnInit(): void {
    this.loadCategories();
    this.loadQuizzes();
  }

  loadCategories() {
    this.categoryService.getCategories().subscribe({
      next: (data: any) => {
        this.categories = data as Category[];
      },
    });
  }

  loadQuizzes() {
    if (this.isLoading || this.pageNumber > this.totalPages) return;

    this.isLoading = true;

    const pageData = new pagination(this.pageNumber, this.pageSize);

    const obs = this.selectedCategory
      ? this.quizService.GetQuizByCategory(pageData, this.selectedCategory.guid)
      : this.quizService.GetQuiz(pageData);

    obs.subscribe({
      next: (data: PagedResult<QuizData>) => {
        this.quizData.push(...data.data);
        this.totalPages = data.totalPages;
        this.isLoading = false;
      },
      error: (err) => {
        console.error('Error fetching quizzes', err);
        this.isLoading = false;
      },
    });
  }

  filterCategories() {
    const term = this.categorySearch.toLowerCase();
    this.filteredCategories = this.categories.filter((c) =>
      c.name.toLowerCase().includes(term)
    );
  }

  selectCategory(category: Category) {
    this.selectedCategory = category;
    this.categorySearch = category.name;
    this.filteredCategories = [];
    this.pageNumber = 1;
    this.quizData = [];
    this.loadQuizzes();
  }

  searchQuizByCode() {
  console.log('Searching for code:', this.quizCode);
  this.quizService.GetQuizById(this.quizCode).subscribe({
    next: (data: QuizData) => {
      if (data) {
        this.attemptService.checkExistingAttempt(data.id, this.attenderData.guid).subscribe({
          next: (res: any) => {
            if (res.success === true) {
              this.snackMessage = 'You have already taken this quiz.';
              this.snackSuccess = false;
              this.snackVisible = true;
              setTimeout(() => {
                this.snackVisible = false;
              }, 3000);
            } else {
              this.snackMessage = 'Redirecting to test...';
              this.snackSuccess = true;
              this.snackVisible = true;
              setTimeout(() => {
                this.snackVisible = false;
                this.router.navigate(['/dashboard/take/quiz'], {
                  state: { quiz: data },
                });
              }, 2000);
            }
          },
          error: (err) => {
            console.error(err);
            this.snackMessage = 'Error checking previous attempts';
            this.snackSuccess = false;
            this.snackVisible = true;
            setTimeout(() => {
              this.snackVisible = false;
            }, 3000);
          }
        });
      } else {
        this.snackMessage = 'No quiz found for this code';
        this.snackSuccess = false;
        this.snackVisible = true;
        setTimeout(() => {
          this.snackVisible = false;
        }, 3000);
      }
    },
    error: (err) => {
      console.error(err);
      this.snackMessage = 'No Quiz Found';
      this.snackSuccess = false;
      this.snackVisible = true;
      setTimeout(() => {
        this.snackVisible = false;
      }, 3000);
    },
  });
}


  // searchQuizByCode() {
  //   console.log('Searching for code:', this.quizCode);
  //   this.quizService.GetQuizById(this.quizCode).subscribe({
  //     next: (data: QuizData) => {
  //       if (data) {

          

  //         this.snackMessage = 'Redirecting to test...';
  //         this.snackSuccess = true;

  //         // show the snack
  //         this.snackVisible = true;
  //         setTimeout(() => {
  //           this.snackVisible = false;

  //           this.router.navigate(['/dashboard/take/quiz'], {
  //             state: { quiz: data },
  //           });
  //         }, 2000);
  //       } else {
  //         this.snackMessage = 'No quiz found for this code';
  //         this.snackSuccess = false;
  //         this.snackVisible = true;
  //         setTimeout(() => {
  //           this.snackVisible = false;
  //         }, 3000);
  //       }
  //     },
  //     error: (err) => {
  //       console.error(err);
  //       this.snackMessage = 'No Quiz Found';
  //       this.snackSuccess = false;
  //       this.snackVisible = true;
  //       setTimeout(() => {
  //         this.snackVisible = false;
  //       }, 3000);
  //     },
  //   });
  // }

  @HostListener('window:scroll', [])
  onScroll() {
    if (
      window.innerHeight + window.scrollY >=
      document.body.offsetHeight - 200
    ) {
      // user is near bottom of page
      if (!this.isLoading && this.pageNumber < this.totalPages) {
        this.pageNumber++;
        this.loadQuizzes();
      }
    }
  }
}

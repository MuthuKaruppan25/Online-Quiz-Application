import { Component, OnInit, HostListener } from '@angular/core';
import { AdminService } from '../services/AdminService';
import { QuizData } from '../Models/QuizDataModel';
import { Admin } from '../Models/UserModel';
import { Store } from '@ngrx/store';
import { selectAdminProfile } from '../store/Admin/admin.selectors';
import { pagination } from '../Models/pagination';
import { CategoryService } from '../services/CategoryService';
import { Category } from '../Models/categoryModel';
import { CommonModule } from '@angular/common';
import { QuizCard } from "../quiz-card/quiz-card";
import { FilePlus, LucideAngularModule } from 'lucide-angular';

interface PagedResult<T> {
  totalCount: number;
  quizDatas: T[];
  pageNumber: number;
  pageSize: number;
  totalPages: number;
}

@Component({
  selector: 'app-myquiz',
  standalone: true,
  imports: [CommonModule, QuizCard,LucideAngularModule],
  templateUrl: './myquiz.html',
  styleUrl: './myquiz.css'
})
export class Myquiz implements OnInit {
  quizzes: QuizData[] = [];
  pageNumber: number = 1;
  pageSize: number = 10;
  totalPages: number = 1;
  isLoading: boolean = false;
  readonly file = FilePlus;

  adminData!: Admin;
  categories: Category[] = [];

  constructor(
    private adminService: AdminService,
    private store: Store,
    private categoryService: CategoryService
  ) {}

  ngOnInit(): void {
    // first, load categories in parallel
    this.categoryService.getCategories().subscribe({
      next: (data: any) => {
        this.categories = data as Category[];
      }
    });

    // then subscribe to the admin profile
    this.store.select(selectAdminProfile).subscribe({
      next: (data: any) => {
        if (data && data.guid) {
          this.adminData = data as Admin;
          console.log('adminData loaded', this.adminData);
          this.loadQuizzes();
        } else {
          console.warn("admin profile not yet available in store");
        }
      }
    });
  }

  loadQuizzes() {
    if (this.isLoading || this.pageNumber > this.totalPages) return;

    if (!this.adminData || !this.adminData.guid) {
      console.warn("Admin data is missing, cannot load quizzes yet.");
      return;
    }

    this.isLoading = true;

    const pageData = new pagination(this.pageNumber, this.pageSize);

    this.adminService.getMyQuiz(this.adminData.guid, pageData).subscribe({
      next: (data: PagedResult<QuizData>) => {
        console.log("quiz page data", data);
        if (data && Array.isArray(data.quizDatas)) {
          this.quizzes.push(...data.quizDatas);
          this.totalPages = data.totalPages;
        } else {
          console.warn("Unexpected API response shape", data);
        }
        this.isLoading = false;
      },
      error: (err) => {
        console.error("Error loading quizzes", err);
        this.isLoading = false;
      }
    });
  }

  @HostListener('window:scroll', [])
  onScroll() {
    if (
      window.innerHeight + window.scrollY >= document.body.offsetHeight - 200
    ) {
      if (!this.isLoading && this.pageNumber < this.totalPages) {
        this.pageNumber++;
        this.loadQuizzes();
      }
    }
  }
}

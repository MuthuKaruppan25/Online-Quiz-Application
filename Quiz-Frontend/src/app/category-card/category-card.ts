import { Component, Input, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import {
  FormGroup,
  FormBuilder,
  Validators,
  ReactiveFormsModule,
  FormsModule,
} from '@angular/forms';
import { Router } from '@angular/router';
import { Category } from '../Models/categoryModel';

@Component({
  selector: 'app-category-card',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule,FormsModule],
  templateUrl: './category-card.html',
  styleUrl: './category-card.css',
})
export class CategoryCard implements OnInit {
  @Input() categories: Category[] = [];

  categoryForm!: FormGroup;
  filteredCategories: Category[] = [];
  selectedCategory: Category | null = null;

  constructor(private fb: FormBuilder, private router: Router) {}

  ngOnInit() {
    this.filteredCategories = this.categories;

    this.categoryForm = this.fb.group({
      title: ['', Validators.required],
      description: ['', Validators.required],
      searchTerm: [''],
      totalQuestions: [null, [Validators.required, Validators.min(5)]],
      duration: [null, [Validators.required, Validators.min(10)]],
    });

    this.categoryForm.get('searchTerm')?.valueChanges.subscribe((term) => {
      this.filterCategories(term);
    });
  }

  filterCategories(term: string) {
    const lowerTerm = term.toLowerCase();
    this.filteredCategories = this.categories.filter((c) =>
      c.name.toLowerCase().includes(lowerTerm)
    );
  }

  selectCategory(cat: Category) {
    this.selectedCategory = cat;
    this.categoryForm.patchValue({ searchTerm: cat.name });
    this.filteredCategories = [];
  }

  onNextClick() {
    if (this.categoryForm.invalid || !this.selectedCategory) {
      this.categoryForm.markAllAsTouched();
      alert('Please fill all fields and select a category');
      return;
    }

    const { title, description, totalQuestions, duration } =
      this.categoryForm.value;

    this.router.navigate(['dashboard/add/questions'], {
      state: {
        title,
        description,
        categoryId: this.selectedCategory.guid,
        totalQuestions,
        duration,
      },
    });
  }
}

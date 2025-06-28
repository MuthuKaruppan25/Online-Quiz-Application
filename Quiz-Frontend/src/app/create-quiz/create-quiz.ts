import { Component, OnInit } from '@angular/core';
import { Admin } from '../Models/UserModel';
import { Store } from '@ngrx/store';
import { selectAdminProfile } from '../store/Admin/admin.selectors';
import { CategoryService } from '../services/CategoryService';
import { Category } from '../Models/categoryModel';
import { CategoryCard } from "../category-card/category-card";

@Component({
  selector: 'app-create-quiz',
  imports: [CategoryCard],
  templateUrl: './create-quiz.html',
  styleUrl: './create-quiz.css'
})
export class CreateQuiz implements OnInit{
    adminData! :Admin;
    categoryData!: Category[];
    constructor(private store:Store,private categoryService:CategoryService){
      this.store.select(selectAdminProfile).subscribe({
        next:(data:any)=>{
          this.adminData = data as Admin;
          console.log(this.adminData);
        }
      })
    } 
    ngOnInit(): void {
      this.categoryService.getCategories().subscribe({
        next:(data:any)=>{
          this.categoryData = data as Category[];
          console.log(this.categoryData);
        }
      })
    }
}

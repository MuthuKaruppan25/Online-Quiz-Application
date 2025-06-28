import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../environment/environment';
import { QuizAddDto } from '../Models/QuizModel';
import { Observable } from 'rxjs';
import { pagination } from '../Models/pagination';

@Injectable()
export class QuizService {
  private http = inject(HttpClient);
  private baseUrl = environment.apiUrl;

  InsertQuiz(QuizData: QuizAddDto): Observable<any> {
    return this.http.post(`${this.baseUrl}/Quiz`, QuizData);
  }

  GetQuiz(pageData : pagination) : Observable<any>{
    return this.http.get(`${this.baseUrl}/Quiz/all?pageNumber=${pageData.pageNumber}&pageSize=${pageData.pageSize}`);
  }

  GetQuizByCategory(pageData : pagination,categoryId:string):Observable<any>{
    return this.http.get(`${this.baseUrl}/Category/${categoryId}/quizzes?pageNumber=${pageData.pageNumber}&pageSize=${pageData.pageSize}`);
  }
  GetQuizById(quizId:string):Observable<any>{
    return this.http.get(`${this.baseUrl}/Quiz/${quizId}`);
  } 
  
}

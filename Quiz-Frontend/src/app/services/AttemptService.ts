import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../environment/environment';
import { AttemptAddDto } from '../Models/AttemptModel';
import { Observable } from 'rxjs';
import { pagination } from '../Models/pagination';

@Injectable()
export class AttemptService {
  private http = inject(HttpClient);
  private baseUrl = environment.apiUrl;

  addAttempt(attempt:AttemptAddDto):Observable<any>{
    return this.http.post(`${this.baseUrl}/Attempt`,attempt);
  }
  getAttemptsByQuizId(quizId:string,pageData:pagination):Observable<any>{
    return this.http.get(`${this.baseUrl}/Attempt/by-quiz/${quizId}/paged?pageNumber=${pageData.pageNumber}&pageSize=${pageData.pageSize}`);
  }
  checkExistingAttempt(quizId:string,AttemptorId:string):Observable<any>{
    return this.http.get(`${this.baseUrl}/Attempt/check-attempt?attenderId=${AttemptorId}&quizId=${quizId}`);
  }

}

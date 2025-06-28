import { inject, Injectable } from '@angular/core';
import { UserRegister } from '../Models/UserModel';
import { environment } from '../environment/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { pagination } from '../Models/pagination';

@Injectable()
export class UserService {
  private http = inject(HttpClient);
  private baseUrl = environment.apiUrl;

  RegisterUser(userData: UserRegister): Observable<any> {
    return this.http.post(`${this.baseUrl}/Attender/Register`, userData);
  }
  getUser(username:string):Observable<any>{
    return this.http.get(`${this.baseUrl}/Attender/by-username/${username}`);
  }
  getAttempts(pageData:pagination,attenderId:string):Observable<any>{
    return this.http.get(`${this.baseUrl}/Attender/${attenderId}/attempts?pageNumber=${pageData.pageNumber}&pageSize=${pageData.pageSize}`);
  }

  getAttemptsSummary(userId:string):Observable<any[]>{
    return this.http.get<any []>(`${this.baseUrl}/Attender/${userId}/individual-attempts`);
  }
}

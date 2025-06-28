import { HttpClient } from "@angular/common/http";
import { inject, Injectable } from "@angular/core";
import { UserRegister } from "../Models/UserModel";
import { Observable } from "rxjs";
import { environment } from "../environment/environment";
import { pagination } from "../Models/pagination";

@Injectable()
export class AdminService{
    private http = inject(HttpClient);
    private baseUrl =environment.apiUrl;
    
    RegisterAdmin(userData :UserRegister) : Observable<any>{
        return this.http.post(`${this.baseUrl}/Admin/Register`,userData);
    }

    getAdmin(username:string): Observable<any>{
        return this.http.get(`${this.baseUrl}/Admin/by-username/${username}`);
    }

    getMyQuiz(adminId:string,pageData:pagination):Observable<any>{
        return this.http.get(`${this.baseUrl}/Admin/${adminId}?pageNumber=${pageData.pageNumber}&pageSize=${pageData.pageSize}`);
    }

    getAttemptsSummary(adminId:string):Observable<any[]>{
        return this.http.get<any[]>(`${this.baseUrl}/Admin/${adminId}/quiz-attempts-summary`);
    }
}
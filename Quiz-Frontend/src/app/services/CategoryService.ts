import { HttpClient } from "@angular/common/http";
import { inject, Injectable } from "@angular/core";
import { environment } from "../environment/environment";
import { Observable } from "rxjs";

@Injectable()
export class CategoryService{
    private http = inject(HttpClient);
    private baseUrl =environment.apiUrl;

    getCategories():Observable<any[]>{
        return this.http.get<any []>(`${this.baseUrl}/Category`);
    }
}
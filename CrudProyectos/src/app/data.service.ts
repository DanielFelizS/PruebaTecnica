import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DataService {
  private apiUrl = 'http://localhost:5149/api/Proyectos';

  private http = inject(HttpClient);

  getProyectos(): Observable<any[]> {
    return this.http.get<any[]>(this.apiUrl);
  }

  getProyectoId(id: number){
    return this.http.get(`${this.apiUrl}/${id}`);
  }

  createProyecto(item: any){
    return this.http.post(this.apiUrl, item);
  }

  updateProyecto(id: number, item: any){
    return this.http.put(`${this.apiUrl}/${id}`, item);
  }

  deleteProyecto(id: number){
    return this.http.delete(`${this.apiUrl}/${id}`);
  }
}

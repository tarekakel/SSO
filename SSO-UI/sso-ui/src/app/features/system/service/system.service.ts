import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiService } from '../../../core/services/api.service';
import { Router } from '@angular/router';
import { System } from '../models/models';
import { GeneralResponse, PagedRequest, PagedResult } from '../../../shared/models/pagedModel';

@Injectable({
  providedIn: 'root'
})
export class SystemService {
  constructor(private apiService: ApiService,
    private router: Router,) { }


  private apiUrl = 'system'; // Change this base URL as needed



  getAll(): Observable<GeneralResponse<System>> {
    return this.apiService.get<GeneralResponse<System>>(this.apiUrl);
  }

  getById(id: number): Observable<System> {
    return this.apiService.get<System>(`${this.apiUrl}/${id}`);
  }

  create(system: GeneralResponse<string>): Observable<GeneralResponse<string>> {
    return this.apiService.post<GeneralResponse<string>>(this.apiUrl, system);
  }

  update(id: number, system: Partial<System>): Observable<System> {
    return this.apiService.put<System>(`${this.apiUrl}/${id}`, system);
  }

  delete(id: number): Observable<void> {
    return this.apiService.delete<void>(`${this.apiUrl}/${id}`);
  }
  search(payload: PagedRequest<System>): Observable<GeneralResponse<PagedResult<System>>> {
    return this.apiService.search<GeneralResponse<PagedResult<System>>>(`${this.apiUrl}/search`, payload);
  }
}

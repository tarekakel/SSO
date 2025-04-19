import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiService } from '../../core/services/api.service';

@Injectable({
  providedIn: 'root'
})
export class AdminService {
  constructor(private api: ApiService) { }

  // getUsers(): Observable<User[]> {
  //   return this.api.get<User[]>('users');
  // }

  // getUser(id: number): Observable<User> {
  //   return this.api.get<User>(`users/${id}`);
  // }

  // createUser(user: User): Observable<User> {
  //   return this.api.post<User>('users', user);
  // }

  // updateUser(id: number, user: User): Observable<User> {
  //   return this.api.put<User>(`users/${id}`, user);
  // }

  // deleteUser(id: number): Observable<void> {
  //   return this.api.delete<void>(`users/${id}`);
  // }

  // searchUsers(query: { name?: string; email?: string }): Observable<User[]> {
  //   return this.api.search<User[]>('users/search', query);
  // }
}

// auth.service.ts
import { Injectable } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { ApiService } from './api.service'; // adjust path if needed
import { Router } from '@angular/router';
import { TokenService } from './token.service';

@Injectable({
    providedIn: 'root'
})
export class AuthService {
    private tokenKey = 'auth_token';

    constructor(
        private apiService: ApiService,
        private router: Router,
        private tokenService: TokenService
    ) { }

    login(credentials: { email: string; password: string }): Observable<any> {
        return this.apiService.post<any>('auth/login', credentials).pipe(
            tap(response => {

                if (response.token) {
                    this.tokenService.setToken(response)

                }
            })
        );
    }

    register(user: { name: string; email: string; password: string }): Observable<any> {
        return this.apiService.post<any>('auth/register', user);
    }

    logout(): void {
        // this.tokenService.clearToken();
        this.router.navigate(['/login']);
    }

}

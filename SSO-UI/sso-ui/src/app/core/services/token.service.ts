import { Injectable } from '@angular/core';

// token.service.ts
@Injectable({ providedIn: 'root' })
export class TokenService {
  getToken(): string | null {
    return localStorage.getItem('auth_token');
  }

  setToken(token: string) {
    localStorage.setItem('auth_token', token);
  }

  removeToken() {
    localStorage.removeItem('auth_token');
  }
}

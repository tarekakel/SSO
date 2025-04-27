import { Inject, Injectable, PLATFORM_ID } from '@angular/core';
import { isPlatformBrowser } from '@angular/common';

// token.service.ts
@Injectable({ providedIn: 'root' })
export class TokenService {
  constructor(@Inject(PLATFORM_ID) private platformId: Object) { }
  getToken(): string | null {

    if (isPlatformBrowser(this.platformId)) {
      console.log('Running in the browser!');

      return localStorage.getItem('auth_token');
    }
    console.log('Not Running in the browser!');

    return null;

  }

  setToken(token: string) {
    localStorage.setItem('auth_token', token);
  }

  removeToken() {
    localStorage.removeItem('auth_token');
  }
}

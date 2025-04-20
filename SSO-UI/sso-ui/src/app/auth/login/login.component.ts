import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { NzMessageService } from 'ng-zorro-antd/message';
import { AuthService } from '../../core/services/auth.service';
import { finalize } from 'rxjs';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  loginForm!: FormGroup;
  loading = false;
  constructor(
    private fb: FormBuilder,
    private message: NzMessageService,
    private router: Router,
    private authService: AuthService

  ) { }

  ngOnInit(): void {
    this.loginForm = this.fb.group({
      email: [null, [Validators.required,Validators.email]],
      password: [null, [Validators.required]],
      remember: [true]

    });
  }


  submitForm(): void {
    if (this.loginForm.invalid) {
      Object.values(this.loginForm.controls).forEach(control => {
        if (control.invalid) {
          control.markAsDirty();
          control.updateValueAndValidity({ onlySelf: true });
        }
      });
      return;
    }


    this.loading = true;

    // const { username, password } = this.loginForm.value;
    this.authService.login(this.loginForm.value).pipe(
      finalize(() => this.loading = false) // 👈 Called after success or error
    ).subscribe({
      next: (response) => {
        if (response.success) {
          const token = response.result.token;
          console.log('Login successful:', token);
          this.message.success('Login successful!');

          // Store token and redirect
          localStorage.setItem('authToken', token);
          this.router.navigate(['/dashboard']);
        } else {
          console.warn('Login failed:', response.message);
          this.message.error(response.message); // ng-zorro message service or your own UI
        }
      },
      error: (err) => {
        console.error('HTTP error:', err);
        this.message.error(err?.error?.message || 'Server error occurred. Please try again later.');
      }
    });



  }
  navigateToRegister(): void {
    this.router.navigate(['/auth/register']);
  }
}

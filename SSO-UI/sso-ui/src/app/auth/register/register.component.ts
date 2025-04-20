import { Component, inject, OnDestroy, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, NonNullableFormBuilder, ValidationErrors, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../core/services/auth.service';
import { finalize, Subject, takeUntil } from 'rxjs';
import { NzMessageService } from 'ng-zorro-antd/message';


@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})

export class RegisterComponent implements OnInit, OnDestroy {

  private fb = inject(NonNullableFormBuilder);
  private destroy$ = new Subject<void>();
  registerForm: FormGroup;
  loading = false;

  constructor(private router: Router, private authService: AuthService, private message: NzMessageService,) {
    this.registerForm = this.fb.group({
      email: this.fb.control('', [Validators.email, Validators.required]),
      firstname: this.fb.control('', [Validators.required]),
      lastname: this.fb.control('', [Validators.required]),
      password: this.fb.control('', [Validators.required, this.identityPasswordValidator]),
      confirm: this.fb.control('', [Validators.required]),
    }, { validators: this.passwordsMatchValidator });
  }

  ngOnInit(): void {
    this.registerForm.controls['password'].valueChanges.pipe(takeUntil(this.destroy$)).subscribe(() => {
      this.registerForm.controls['confirm'].updateValueAndValidity();
    });
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }
  submitForm(): void {
    if (this.registerForm.invalid) {
      Object.values(this.registerForm.controls).forEach(control => {
        if (control.invalid) {
          control.markAsDirty();
          control.updateValueAndValidity({ onlySelf: true });
        }
      });
      return;
    }


    this.loading = true;
    // const { username, password } = this.loginForm.value;
    this.authService.register(this.registerForm.value).pipe(
      finalize(() => this.loading = false) // 👈 Called after success or error
    ).subscribe({
      next: (response) => {
        if (response.success) {
          // const token = response.result.token;
          // console.log('Login successful:', token);
          this.message.success(response?.message);

          // // Store token and redirect
          // localStorage.setItem('authToken', token);
          // this.router.navigate(['/dashboard']);
        } else {
          // console.warn('Login failed:', response.message);
          this.message.error(response.message); // ng-zorro message service or your own UI
        }
      },
      error: (err) => {
        console.error('HTTP error:', err);
        const errorResponse = err?.error;

        if (errorResponse?.errors?.length > 0) {
          // Flatten and show all error messages
          const validationErrors = errorResponse.errors;
          const allMessages = Object.values(validationErrors).flat();

          allMessages.forEach((msg: any) => {
            return this.message.error(msg);
          });
        }
        // else if (errorResponse?.message) {
        //   // Handle comma-separated string format
        //   const messages = errorResponse.message.split(',');
        //   messages.forEach((msg: string) => this.message.error(msg.trim()));
        // }  
        else {
          // Fallback message
          this.message.error(errorResponse?.message || 'Server error occurred. Please try again later.');
        }
      }
    });
  }


  confirmValidator(control: AbstractControl): ValidationErrors | null {
    // console.log( this.registerForm); // here erro r Cannot read properties of undefined (reading 'registerForm')

    if (!control.value) {
      return { error: true, required: true };
    } else if (control.value !== this.registerForm?.value?.get('password')) {
      return { confirm: true, error: true };
    }
    return {};
  }

  passwordsMatchValidator(group: AbstractControl): ValidationErrors | null {
    const passwordControl = group.get('password');
    const confirmControl = group.get('confirm');

    const password = passwordControl?.value;
    const confirm = confirmControl?.value;
    if (password !== confirm) {
      confirmControl?.setErrors({ ...confirmControl?.errors, confirm: true }); // 👈 Set confirm error manually
      return { confirm: true };
    }
    return null;

  }


  identityPasswordValidator(control: AbstractControl): ValidationErrors | null {
    const value = control.value;

    if (!value) return null;

    const errors: ValidationErrors = {};

    if (value.length < 6) {
      errors['minLength'] = 'Password must be at least 6 characters long.';
    }
    if (!/[A-Z]/.test(value)) {
      errors['uppercase'] = 'Password must contain at least one uppercase letter.';
    }
    if (!/[a-z]/.test(value)) {
      errors['lowercase'] = 'Password must contain at least one lowercase letter.';
    }
    if (!/[0-9]/.test(value)) {
      errors['digit'] = 'Password must contain at least one digit.';
    }
    if (!/[^A-Za-z0-9]/.test(value)) {
      errors['symbol'] = 'Password must contain at least one non-alphanumeric character.';
    }

    return Object.keys(errors).length ? errors : null;
  }
  navigateToLogin(): void {
    this.router.navigate(['/auth/login']);
  }
}
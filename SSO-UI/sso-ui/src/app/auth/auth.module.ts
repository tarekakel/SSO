import { NgModule } from '@angular/core';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { AuthRoutingModule } from './auth-routing.module';
import { RouterModule } from '@angular/router';
import { AuthLayoutComponent } from './auth-layout/auth-layout.component';

@NgModule({
    declarations: [LoginComponent, RegisterComponent, AuthLayoutComponent],

    imports: [
        CommonModule,
        FormsModule,
        AuthRoutingModule,
        RouterModule
    ]

})
export class AuthModule { }

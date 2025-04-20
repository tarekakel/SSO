import { NgModule } from '@angular/core';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';

import { AuthRoutingModule } from './auth-routing.module';
import { AuthLayoutComponent } from './auth-layout/auth-layout.component';


import { SharedModule } from '../shared/shared.module';
@NgModule({
    declarations: [LoginComponent, RegisterComponent, AuthLayoutComponent],

    imports: [
        SharedModule,
        AuthRoutingModule,
    ]

})
export class AuthModule { }

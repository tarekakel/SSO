import { NgModule } from '@angular/core';
import { LayoutModule } from './layout/layout.module';
import { BrowserModule } from '@angular/platform-browser';
import { AppComponent } from './app.component';
import { NzIconModule } from 'ng-zorro-antd/icon';
import { CoreModule } from './core/core.module';
import { AuthModule } from './auth/auth.module';
import { RouterModule } from '@angular/router';


@NgModule({
  imports: [
    BrowserModule,
    LayoutModule,
    AppComponent,
    NzIconModule,
    CoreModule,
    AuthModule,
    RouterModule,
  ],
})
export class AppModule { }

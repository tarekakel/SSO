import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NzLayoutModule } from 'ng-zorro-antd/layout';
import { NzMenuModule } from 'ng-zorro-antd/menu';
import { NzIconModule } from 'ng-zorro-antd/icon';
import { HeaderComponent } from './components/header/header.component';
import { SidebarComponent } from './components/sidebar/sidebar.component';
import { FooterComponent } from './components/footer/footer.component';
import { MainLayoutComponent } from './components/main-layout/main-layout.component';
import { NzBreadCrumbModule } from 'ng-zorro-antd/breadcrumb';
import { IconDefinition } from '@ant-design/icons-angular';
import { AppComponent } from "../app.component";
import { RouterModule } from '@angular/router';
@NgModule({
    declarations: [
        HeaderComponent,
        SidebarComponent,
        FooterComponent,
        MainLayoutComponent
    ],
    imports: [
        CommonModule,
        NzLayoutModule,
        NzMenuModule,
        NzIconModule,
        NzBreadCrumbModule,
        AppComponent,
        RouterModule
    ],
    exports: [
        MainLayoutComponent
    ]
})
export class LayoutModule { }

import { Routes } from '@angular/router';
import { MainLayoutComponent } from './layout/components/main-layout/main-layout.component';
import { AuthLayoutComponent } from './auth/auth-layout/auth-layout.component';

export const routes: Routes = [
    { path: 'auth', component: AuthLayoutComponent, loadChildren: () => import('./auth/auth.module').then(m => m.AuthModule) },
    // { path: '', redirectTo: 'auth/login', pathMatch: 'full' },
    // { path: '**', redirectTo: 'auth/login' },

    {
        path: 'main',
        component: MainLayoutComponent,
        children: [
            {
                path: 'system',
                loadChildren: () =>
                    import('./features/system/system.module').then((m) => m.SystemModule),
            },
        ]

    },

];
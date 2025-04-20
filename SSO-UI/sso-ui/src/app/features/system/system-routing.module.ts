import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SystemListComponent } from './components/system-list/system-list.component';

const routes: Routes = [
    { path: 'system-list', component: SystemListComponent },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class SystemRoutingModule { }

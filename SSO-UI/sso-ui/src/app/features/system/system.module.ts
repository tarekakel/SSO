import { NgModule } from '@angular/core';
import { SharedModule } from '../../shared/shared.module';
import { SystemRoutingModule } from './system-routing.module';
import { SystemListComponent } from './components/system-list/system-list.component';


@NgModule({
    declarations: [SystemListComponent],

    imports: [
        SharedModule,
        SystemRoutingModule,
    ]

})
export class SystemModule { }

import { NgModule } from '@angular/core';
import { SharedModule } from '../../shared/shared.module';
import { SystemRoutingModule } from './system-routing.module';
import { SystemListComponent } from './components/system-list/system-list.component';
import { SystemAddComponent } from './components/system-add/system-add.component';


@NgModule({
    declarations: [SystemListComponent, SystemAddComponent],

    imports: [
        SharedModule,
        SystemRoutingModule,
    ]

})
export class SystemModule { }

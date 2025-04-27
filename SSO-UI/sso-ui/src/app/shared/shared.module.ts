import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { RouterModule } from "@angular/router";
import { NzButtonModule } from "ng-zorro-antd/button";
import { NzCheckboxModule } from "ng-zorro-antd/checkbox";
import { NzFormModule } from "ng-zorro-antd/form";
import { NzInputModule } from "ng-zorro-antd/input";
import { NzMessageModule } from "ng-zorro-antd/message";
import { DataTableComponent } from "./components/data-table/data-table.component";

import { NzModalModule } from 'ng-zorro-antd/modal';

import { NzDividerModule } from 'ng-zorro-antd/divider';
import { NzRadioModule } from 'ng-zorro-antd/radio';
import { NzSwitchModule } from 'ng-zorro-antd/switch';
import { NzTableModule, } from 'ng-zorro-antd/table';
import { NzIconModule } from "ng-zorro-antd/icon";
@NgModule({
    declarations: [DataTableComponent],

    imports: [
        CommonModule,
        FormsModule,
        RouterModule,
        NzFormModule,
        ReactiveFormsModule,
        NzInputModule,
        NzButtonModule,
        NzMessageModule,
        NzCheckboxModule,
        NzDividerModule,
        NzRadioModule,
        NzSwitchModule,
        NzTableModule,
        NzIconModule,
        NzModalModule
    ],
    exports: [CommonModule,
        FormsModule,
        RouterModule,
        NzFormModule,
        ReactiveFormsModule,
        NzInputModule,
        NzButtonModule,
        NzMessageModule,
        NzCheckboxModule,
        NzDividerModule,
        NzRadioModule,
        NzSwitchModule,
        NzTableModule,
        DataTableComponent,
        NzIconModule,
        NzModalModule

    ]

})
export class SharedModule { }

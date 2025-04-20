import { Component, OnInit } from '@angular/core';
import { SystemService } from '../../service/system.service';
import { GeneralResponse, PagedRequest, PagedResult } from '../../../../shared/models/pagedModel';
import { System } from '../../models/models';
import { finalize } from 'rxjs';

@Component({
  selector: 'app-system-list',
  templateUrl: './system-list.component.html',
  styleUrl: './system-list.component.css'
})

export class SystemListComponent implements OnInit {

  constructor(private systemService: SystemService) {

  }
  tableData: any = [];
  response: any = {};
  loading: boolean = false;
  tableColumns = [
    { title: 'ID', key: 'id' },
    { title: 'Name', key: 'name' },
    { title: 'Description', key: 'description' }
  ];
  ngOnInit(): void {



    this.loading = true;

    this.systemService.search({ pageIndex: 0, pageSize: 10 }).pipe(
      finalize(() => {  this.loading = false}) //  //
    ).subscribe({
      next: (response) => {
        if (response.success) {

          this.response = response.result;
          this.tableData = response.result?.data;
          console.log('res', this.tableData);

        }
      },
      error: (err) => {
        console.error(err);
      }
    });
  }
}

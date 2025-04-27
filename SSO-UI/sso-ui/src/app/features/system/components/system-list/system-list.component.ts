import { Component, OnInit } from '@angular/core';
import { SystemService } from '../../service/system.service';
import { GeneralResponse, PagedRequest, PagedResult } from '../../../../shared/models/pagedModel';
import { System } from '../../models/models';
import { finalize } from 'rxjs';
import { NzMessageService } from 'ng-zorro-antd/message';

@Component({
  selector: 'app-system-list',
  templateUrl: './system-list.component.html',
  styleUrl: './system-list.component.css'
})

export class SystemListComponent implements OnInit {

  constructor(private systemService: SystemService, private message: NzMessageService,) {

  }
  tableData: any = [];
  response: any = {};
  loading: boolean = false;
  title = 'System List';
  tableColumns = [
    { title: 'ID', key: 'id' },
    { title: 'Name', key: 'name' },
    { title: 'Description', key: 'description' }
  ];

  showAdd: boolean = false;
  ngOnInit(): void {
    //this.getSystmList()
  }


  getSystmList(params: any = {}) {
    this.loading = true;

    this.systemService.search({ pageIndex: params?.pageIndex ?? 0, pageSize: params?.pageSize ?? 10 }).pipe(
      finalize(() => { this.loading = false }) //  //
    ).subscribe({
      next: (response) => {
        if (response.success) {
          this.response = response.result;
          this.tableData = response.result?.data;
        }
        else {
          console.warn('Fetch system List failed:', response.message);
          this.message.error(response.message); // ng-zorro message service or your own UI
        }
      },
      error: (err) => {
        console.log('xxxx', err);
        if (err?.status == 401) {
          this.message.error(err?.statusText);
          return;
        }
        this.message.error(err?.error?.message || 'Server error occurred. Please try again later.');
      }
    });
  }
}

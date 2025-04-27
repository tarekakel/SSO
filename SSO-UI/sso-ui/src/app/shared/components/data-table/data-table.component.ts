import { Component, EventEmitter, Input, OnChanges, OnInit, Output, SimpleChanges } from '@angular/core';
import { FormControl, FormGroup, NonNullableFormBuilder, ReactiveFormsModule } from '@angular/forms';
import {
  NzTableLayout,
  NzTablePaginationPosition,
  NzTablePaginationType,
  NzTableQueryParams,
  NzTableSize
} from 'ng-zorro-antd/table';

// interface ItemData {
//   name: string;
//   age: number | string;
//   address: string;
//   checked: boolean;
//   expand: boolean;
//   description: string;
//   disabled?: boolean;
// }

type TableScroll = 'unset' | 'scroll' | 'fixed';

interface Setting {
  bordered: boolean;
  loading: boolean;
  pagination: boolean;
  sizeChanger: boolean;
  title: boolean;
  header: boolean;
  footer: boolean;
  expandable: boolean;
  checkbox: boolean;
  fixHeader: boolean;
  noResult: boolean;
  ellipsis: boolean;
  simple: boolean;
  size: NzTableSize;
  tableScroll: TableScroll;
  tableLayout: NzTableLayout;
  position: NzTablePaginationPosition;
  paginationType: NzTablePaginationType;
}

interface TableColumn {
  title: string;
  key: string;

}

@Component({
  selector: 'app-data-table',
  templateUrl: './data-table.component.html',
  styleUrl: './data-table.component.css'
})

export class DataTableComponent implements OnInit, OnChanges {
  @Input() tableData: any = {};

  @Input() loading: boolean = false;
  @Input() columns: TableColumn[] = [];
  @Input() tableTitle: any;
  @Output() queryParamsChange = new EventEmitter<NzTableQueryParams>();

  data: any[] = [];
  settingForm: FormGroup<{ [K in keyof Setting]: FormControl<Setting[K]> }>;
  // listOfData: readonly ItemData[] = [];
  // displayData: readonly ItemData[] = [];
  allChecked = false;
  indeterminate = false;
  fixedColumn = true;
  scrollX: string | null = null;
  scrollY: string | null = null;
  settingValue: Setting;
  listOfSwitch = [
    { name: 'Bordered', formControlName: 'bordered' },
    { name: 'Loading', formControlName: 'loading' },
    { name: 'Pagination', formControlName: 'pagination' },
    { name: 'PageSizeChanger', formControlName: 'sizeChanger' },
    { name: 'Title', formControlName: 'title' },
    { name: 'Column Header', formControlName: 'header' },
    { name: 'Footer', formControlName: 'footer' },
    { name: 'Expandable', formControlName: 'expandable' },
    { name: 'Checkbox', formControlName: 'checkbox' },
    { name: 'Fixed Header', formControlName: 'fixHeader' },
    { name: 'No Result', formControlName: 'noResult' },
    { name: 'Ellipsis', formControlName: 'ellipsis' },
    { name: 'Simple Pagination', formControlName: 'simple' }
  ];
  listOfRadio = [
    {
      name: 'Size',
      formControlName: 'size',
      listOfOption: [
        { value: 'default', label: 'Default' },
        { value: 'middle', label: 'Middle' },
        { value: 'small', label: 'Small' }
      ]
    },
    {
      name: 'Table Scroll',
      formControlName: 'tableScroll',
      listOfOption: [
        { value: 'unset', label: 'Unset' },
        { value: 'scroll', label: 'Scroll' },
        { value: 'fixed', label: 'Fixed' }
      ]
    },
    {
      name: 'Table Layout',
      formControlName: 'tableLayout',
      listOfOption: [
        { value: 'auto', label: 'Auto' },
        { value: 'fixed', label: 'Fixed' }
      ]
    },
    {
      name: 'Pagination Position',
      formControlName: 'position',
      listOfOption: [
        { value: 'top', label: 'Top' },
        { value: 'bottom', label: 'Bottom' },
        { value: 'both', label: 'Both' }
      ]
    },
    {
      name: 'Pagination Type',
      formControlName: 'paginationType',
      listOfOption: [
        { value: 'default', label: 'Default' },
        { value: 'small', label: 'Small' }
      ]
    }
  ];

  currentPageDataChange($event: readonly any[]): void {
    //  this.data = $event;
    this.refreshStatus();
  }

  editCache: { [key: string]: { edit: boolean; data: any } } = {};


  refreshStatus(): void {
    const validData = this.data?.filter(value => !value.disabled);
    const allChecked = validData?.length > 0 && validData.every(value => value.checked);
    const allUnChecked = validData?.every(value => !value.checked);
    this.allChecked = allChecked;
    this.indeterminate = !allChecked && !allUnChecked;
  }

  checkAll(value: boolean): void {
    this.data.forEach(data => {
      if (!data.disabled) {
        data.checked = value;
      }
    });
    this.refreshStatus();
  }

  startEdit(data: any): void {
    if (!this.editCache[data?.id]) {
      this.editCache[data?.id] = {
        edit: false,
        data: {}
      };
    }

    this.editCache[data?.id].edit = true;
    this.editCache[data?.id].data = JSON.parse(JSON.stringify(data)) // deep clone
  }
  // generateData(): readonly any[] {
  //   const data: any[] = [];
  //   for (let i = 1; i <= 100; i++) {
  //     data.push({
  //       name: 'John Brown',
  //       age: `${i}2`,
  //       address: `New York No. ${i} Lake Park`,
  //       description: `My name is John Brown, I am ${i}2 years old, living in New York No. ${i} Lake Park.`,
  //       checked: false,
  //       expand: false
  //     });
  //   }
  //   return data;
  // }

  constructor(private formBuilder: NonNullableFormBuilder) {
    this.settingForm = this.formBuilder.group({
      bordered: [true],
      loading: [false],
      pagination: [true],
      sizeChanger: [false],
      title: [true],
      header: [true],
      footer: [false],
      expandable: [true],
      checkbox: [false],
      fixHeader: [false],
      noResult: [false],
      ellipsis: [false],
      simple: [false],
      size: 'small' as NzTableSize,
      paginationType: 'default' as NzTablePaginationType,
      tableScroll: 'unset' as TableScroll,
      tableLayout: 'auto' as NzTableLayout,
      position: 'bottom' as NzTablePaginationPosition
    });

    this.settingValue = this.settingForm.value as Setting;
  }
  ngOnChanges(changes: SimpleChanges): void {
    this.data = this.tableData.data;
  }

  ngOnInit(): void {
    this.settingForm.valueChanges.subscribe(value => {
      this.settingValue = value as Setting;
    });
    this.settingForm.controls.tableScroll.valueChanges.subscribe(scroll => {
      this.fixedColumn = scroll === 'fixed';
      this.scrollX = scroll === 'scroll' || scroll === 'fixed' ? '100vw' : null;
    });
    this.settingForm.controls.fixHeader.valueChanges.subscribe(fixed => {
      this.scrollY = fixed ? '240px' : null;
    });
    this.data = this.tableData.data;
    console.log('from comp', this.data);


    //   this.settingForm.controls.noResult.valueChanges.subscribe(empty => {
    //     if (empty) {
    //  //     this.data = [];
    //     } else {
    //       //     this.data = this.generateData();
    //     }
    //   });
    //  this.data = this.generateData();
  }


  cancelEdit(data: any): void {
    const index = this.data.findIndex(item => item.id === data.id);


    this.editCache[data.id] = {
      data: { ...this.data[index] },
      edit: false
    };
  }

  saveEdit(data: any): void {
    const index = this.data.findIndex(item => item.id === data.id);
    Object.assign(this.data[index], this.editCache[data.id].data);
    this.editCache[data.id].edit = false;
  }


  onQueryParamsChange(params: NzTableQueryParams): void {
    console.log(params);
    const { pageSize, pageIndex, sort, filter } = params;
    const currentSort = sort.find(item => item.value !== null);
    const sortField = (currentSort && currentSort.key) || null;
    const sortOrder = (currentSort && currentSort.value) || null;
   this.queryParamsChange.emit(params);
  }
}

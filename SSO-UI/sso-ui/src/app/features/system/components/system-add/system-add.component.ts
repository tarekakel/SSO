import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NzMessageService } from 'ng-zorro-antd/message';
import { SystemService } from '../../service/system.service';
import { finalize } from 'rxjs';

@Component({
  selector: 'app-system-add',

  templateUrl: './system-add.component.html',
  styleUrl: './system-add.component.css'
})
export class SystemAddComponent {
  @Input() isVisible = false;
  @Input() title?: string;
  isOkLoading = false;
  systemForm!: FormGroup;
  @Output() modalClosed = new EventEmitter<void>();

  /**
   *
   */
  constructor(private fb: FormBuilder,
    private message: NzMessageService, private systemService: SystemService) {

  }

  ngOnInit(): void {
    this.systemForm = this.fb.group({
      name: [null, [Validators.required]],
      description: [null, [Validators.required]],

    });
  }



  handleOk(): void {
    this.isOkLoading = true;
    setTimeout(() => {
      this.isVisible = false;
      this.isOkLoading = false;

    }, 3000);
  }

  handleCancel(): void {
    this.isVisible = false;
    this.modalClosed.emit();
  }

  submitForm(): void {
    // if (this.systemForm.invalid) {
    //   Object.values(this.systemForm.controls).forEach(control => {
    //     if (control.invalid) {
    //       control.markAsDirty();
    //       control.updateValueAndValidity({ onlySelf: true });
    //     }
    //   });
    //   return;


    // }
    this.isOkLoading = true;
    this.systemService.create(this.systemForm.value).pipe(finalize(() => { this.isOkLoading = false; this.modalClosed.emit() })).subscribe(
      {
        next: (response) => {
          if (response.success) {
            this.message.success('new system added successfully!');
          } else {
            console.warn('new system added failed:', response.message);
            this.message.error(response.message); // ng-zorro message service or your own UI
          }
        },
        error: (err) => {
          console.error('HTTP error:', err);
          this.message.error(err?.error?.message || 'Server error occurred. Please try again later.');
        }
      }
    );
    // this.message.success('success')
  }

}

import { ListService, PagedResultDto } from '@abp/ng.core';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { CustomerInfoService } from '@proxy';
import { CustomerInfoDto } from '@proxy/dtos';
import { NgbDateNativeAdapter, NgbDateAdapter } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-customer',
  templateUrl: './customer.component.html',
  styleUrls: ['./customer.component.scss'],
  providers: [ListService, { provide: NgbDateAdapter, useClass: NgbDateNativeAdapter }]
})
export class CustomerComponent implements OnInit {

  customer = { items: [], totalCount: 0 } as PagedResultDto<CustomerInfoDto>;

  //create
  isModalOpen = false;
  form: FormGroup;
  isMailOpen = false;
  //isOpen = false;

  constructor(public list: ListService, private customerInfoService: CustomerInfoService, private fb: FormBuilder) { }

  ngOnInit(): void {
    const customerInfoCreator = (query) => this.customerInfoService.getCustomersByInput(query);

    this.list.hookToQuery(customerInfoCreator).subscribe((response) => {
      this.customer = response;
    })
  }

  createCustomer() {
    debugger;
    this.buildForm();
    this.isModalOpen = true;
  }

  mail() {
    //this.mailForm();
    this.isMailOpen = true;
  }

  buildForm() {

    this.form = this.fb.group({
      customerName: ['', Validators.required],
      customerEmail: [null, Validators.required],
      startDate: [null, Validators.required],
      endDate: [null, Validators.required],
      courseName: [null, Validators.required]
    })
  }

  mailForm() {
    this.form = this.fb.group({
      templateName: ['', Validators.required],
      body: ['', Validators.required]
    })
  }

  save() {
    if (this.form.invalid) {
      return;
    }

    this.customerInfoService.create(this.form.value).subscribe(() => {
      this.isModalOpen = false;
      this.form.reset();
      this.list.get();
    })
  }

  saveMail() {
    this.isMailOpen = false;
    alert()
  }
}

import { ListService } from '@abp/ng.core';
import { Component, OnInit } from '@angular/core';
import * as ClassicEditor from '@ckeditor/ckeditor5-build-classic';
import { CustomerInfoDto, GetCustomerDto } from '@proxy/dtos/models';
import { TemplateService } from '@proxy/templates';
import { NgbDateNativeAdapter, NgbDateAdapter } from '@ng-bootstrap/ng-bootstrap';
import { FormBuilder, FormGroup, NgForm } from '@angular/forms';
import { CustomerInfoService } from '@proxy';

@Component({
  selector: 'app-new-template',
  templateUrl: './new-template.component.html',
  styleUrls: ['./new-template.component.scss'],
  providers: [ListService, { provide: NgbDateAdapter, useClass: NgbDateNativeAdapter }]
})

export class NewTemplateComponent implements OnInit {
  public Editor = ClassicEditor;
  filter: GetCustomerDto;
  form: FormGroup;
  isModalOpen = false;
  customer: CustomerInfoDto;
  customerFormObject: any = {
    customerName: '',
    customerEmail: '',
    startDate: null,
    endDate: null,
    courseName: '',
    templateData: '',
    templateName: ''
  }

  constructor(private tempService: TemplateService,
    private list: ListService, private fb: FormBuilder,
    private customerService: CustomerInfoService
  ) { }

  ngOnInit(): void {
    //this.customerFormObject.customerName = 'Your Name'
    //this.customerFormObject.startDate = 'Start Date'
    //this.customerFormObject.endDate = 'End Date'
    
  }

  createTemplate() {
    this.buildForm();
    this.isModalOpen = true;
  }

  buildForm() {
    this.form = this.fb.group({
      customerName: [''],
      customerEmail: [''],
      startDate: [''],
      endDate: [''],
      courseName: [''],
      templateData: [``],
      templateName: [``]
    })
  }
  abc = "xyz";

  // buildForm() {
  //   this.form = this.fb.group({
  //     customerName: [''],
  //     customerEmail: [''],
  //     startDate: [null],
  //     endDate: [null],
  //     courseName: [''],
  //     templateData: [''],
  //     templateName: ['']
  //   })
  // }

  save(form1: NgForm) {
    // if (this.form.invalid) {
    //   return;
    // }
    console.log('form.value', form1.form.value);

    this.tempService.createTemp(form1.form.value).subscribe(() => {
      this.isModalOpen = false;
      alert('created')
      this.form.reset();
      this.list.get();
    })
  }
}

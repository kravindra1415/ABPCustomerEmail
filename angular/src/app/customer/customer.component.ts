import { ListService, PagedResultDto } from '@abp/ng.core';
import { Component, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { CustomerInfoService } from '@proxy';
import { CustomerInfoDto,  SendAllEmailDto, TemplateInfoDto } from '@proxy/dtos';
import { NgbDateNativeAdapter, NgbDateAdapter } from '@ng-bootstrap/ng-bootstrap';
import { TemplateService } from '@proxy/templates';
import { Router } from '@angular/router';
import { EmailTemplateTypes } from '@proxy/email-templates';

@Component({
  selector: 'app-customer',
  templateUrl: './customer.component.html',
  styleUrls: ['./customer.component.scss'],
  providers: [ListService, { provide: NgbDateAdapter, useClass: NgbDateNativeAdapter }]
})
export class CustomerComponent implements OnInit {

  customer = { items: [], totalCount: 0 } as PagedResultDto<CustomerInfoDto>;;
  tempInfo:any;

  //create
  isModalOpen = false;
  form: FormGroup;
  isMailOpen = false;
  filter: any;
  selectedItem: any;
  //isOpen = false;
  selected: any[];
  sendEmail: any;
  SelectionType: any;
  tempTypeReg: EmailTemplateTypes.Registration;
  tempTypeComp: EmailTemplateTypes.Confirmation;

  constructor(public list: ListService,
    private customerInfoService: CustomerInfoService,
    private fb: FormBuilder,
    private tempService: TemplateService, private router: Router
  ) { }

  ngOnInit(): void {
    const customerInfoCreator = (query) => this.customerInfoService.getCustomersByInput(query);

    this.list.hookToQuery(customerInfoCreator).subscribe((response) => {
      this.customer = response;
    })

    this.getMailTemplates();
    //this.sendAllEmail();

    const tempInfoCreator = (query) => this.tempService.getTemplatesByInput(query);

    this.list.hookToQuery(tempInfoCreator).subscribe((res) => {
      console.log(res);
    })
  }
  
  createCustomer() {
    //debugger;
    this.buildForm();
    this.isModalOpen = true;
  }

  buildForm() {
    this.form = this.fb.group({
      customerName: ['', Validators.required],
      customerEmail: [null, Validators.required],
      startDate: [null, Validators.required],
      endDate: [null, Validators.required],
      courseName: [null, Validators.required],
      emailTemplateType: [null]
    });
    this.form.controls['emailTemplateType'].setValue(Number.parseInt(this.selectedItem));
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

  getMailTemplates() {
    //debugger;
    //const getMailTemplatesData = () => this.tempService.getTemplateName();

    // this.customerInfoService.getTemplateName().subscribe((response) => {
    //   this.tempInfo = response;
    //   console.log(this.tempInfo);
    // })

    this.customerInfoService.getAllTemplates().subscribe((resp)=>{
      this.tempInfo=resp;
      console.log(this.tempInfo);
      this.tempInfo.forEach(element => {
        console.log(element);
        
      });
    })
  }

  getData(event: any) {
    debugger
    //this.selectedItem = event.target.value;
    this.selectedItem = (event.target.value);
    console.log(this.selectedItem);
  }

  checkValue: any;
  arrData: any
  data: any = [];

  onSelect(selected: any) {
    //debugger
    console.log('Select Event', selected, this.selected);
    this.arrData = selected.selected
    //let input = {} as SendAllEmailDto;
    this.data = [];
    this.arrData.forEach(em => {
      //input.emails = em.customerEmail
      this.data.push(em.customerEmail);
    })
  }

  onActivate(event: any) {
    console.log('Activate Event', event);
  }

  onNavigation(data) {
    console.log(data);
    let getArrData = this.arrData
    let tempsData=this.tempInfo
    this.router.navigate(['/registers'],
      {
        queryParams: { info: (JSON.stringify(getArrData)),temps:(JSON.stringify(tempsData)),d:data }
      }
    )
  }

  sendAllEmail() {
  //   if (this.data.length > 0) {
  //     console.log(this.data);

  //     this.customerInfoService.sendEmailAllByInput({ emails: this.data } as SendAllEmailDto).subscribe((response => {
  //     }))
  //   }
  //   else {
  //     alert("enter the values to send the mail")
  //   }

  //   this.data = [];
  // }
}
}
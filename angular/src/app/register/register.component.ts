import { ListService } from '@abp/ng.core';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import * as ClassicEditor from '@ckeditor/ckeditor5-build-classic';
import { CustomerInfoService } from '@proxy';
import { GetCustomerDto, SendAllEmailDto } from '@proxy/dtos';
import { TemplateService } from '@proxy/templates';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss'],
  providers: [ListService]
})

export class RegisterComponent implements OnInit {
  public Editor = ClassicEditor;
  form:FormGroup;

  constructor(private route: ActivatedRoute,
    private customerService: CustomerInfoService,
    private tempService: TemplateService,
    private list: ListService,
    private fb:FormBuilder
    ) { }
 
  getTempdata: any;

  filter: GetCustomerDto;

  templateObj:any={
    templateName:'',
    templateData:''
  }

  ngOnInit(): void {
    this.buildForm();
    this.getTempDynamicData();
    this.getTemplates();

    this.route.queryParams.subscribe((params)=>{
      this.tempName=params.d;
      console.log(this.tempName);
    })

    this.customerService.getEmailTemplateByEmailTemplateName(this.tempName).subscribe(dt=>{
      this.tempData=dt;
      console.log(this.tempData);
      
    })
  }

  tempData:any;
  tempName:any;

  buildForm() {
    this.form = this.fb.group({
      customerName: [''],
      customerEmail: [''],
    })
  }

  getTempData: any;

  getTemplates() {
    const customerInfoCreator = (query) => this.tempService.getTemplatesByInput(query);

    this.list.hookToQuery(customerInfoCreator).subscribe((resse) => {
      console.log(resse);
      resse.items.forEach(e => {
        //debugger

        console.log(this.getTempData);
      })
    })
  }

  mailList: any = [];

  getTempDynamicData() {
    //debugger
    this.route.queryParams.subscribe((params => {
      console.log(params.temps);
      console.log(params.d);
      
      console.log(params.info);
      this.getTempdata = JSON.parse(params.info);
      console.log(this.getTempdata);
      this.getTempdata.forEach(element => {
        console.log(element.customerEmail);
        this.mailList.push(element.customerEmail)
      });
    }))
  }

  emailTempName:any;
  tempList: any = [];

  getAllMailTemplates(){
    this.customerService.getAllTemplates().subscribe((res)=>{
      this.emailTempName=res;
      this.emailTempName.forEach(element => {
        this.tempList.push(element);
      });
      console.log(this.emailTempName);
    })
  }

  allTemplates:any;
  templateData:any;

  getEmailTemplateName(){

    // this.customerService.getAllTemplates().subscribe(aa=>{
    //   this.allTemplates=aa;
    //   console.log(this.allTemplates);

    //   aa.forEach(ele=>{
    //     console.log(ele);
    //     this.customerService.getEmailTemplateByEmailTemplateName(ele).subscribe((as=>{
    //       console.log(as);
    //       this.templateData=as
    //     }))
    // })
    //   })
  }

  sendMail() {
    //debugger 
    console.log(this.emailTempName);
    this.customerService.sendEmailAllByInputAndEmailTemplateName({ emails: this.mailList } as SendAllEmailDto,
      this.tempName)
      .subscribe((response) => {
        console.log(response);
      })

      alert('Mail Send successfully..')
  }
}

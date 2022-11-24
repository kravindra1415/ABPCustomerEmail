import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NgbDatepickerModule } from '@ng-bootstrap/ng-bootstrap';
import { CustomerRoutingModule } from './customer-routing.module';
import { CustomerComponent } from './customer.component';
import { SharedModule } from '../shared/shared.module';


@NgModule({
  declarations: [
    CustomerComponent
  ],
  imports: [
    SharedModule,
    CustomerRoutingModule,
    NgbDatepickerModule
  ]
})
export class CustomerModule { }

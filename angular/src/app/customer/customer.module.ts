import { NgModule } from '@angular/core';
import { NgbDatepickerModule } from '@ng-bootstrap/ng-bootstrap';
import { CustomerRoutingModule } from './customer-routing.module';
import { CustomerComponent } from './customer.component';
import { SharedModule } from '../shared/shared.module';
import { NgbDropdownModule } from '@ng-bootstrap/ng-bootstrap';
@NgModule({
  declarations: [
    CustomerComponent
  ],
  imports: [
    SharedModule,
    CustomerRoutingModule,
    NgbDatepickerModule,NgbDropdownModule
  ]
})
export class CustomerModule { }

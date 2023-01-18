import { NgModule } from '@angular/core';
import { CKEditorModule } from '@ckeditor/ckeditor5-angular'
import { RegisterRoutingModule } from './register-routing.module';
import { RegisterComponent } from './register.component';
import { SharedModule } from '../shared/shared.module';

@NgModule({
  declarations: [
    RegisterComponent
  ],
  imports: [
    SharedModule,
    RegisterRoutingModule, CKEditorModule
  ]
})
export class RegisterModule { }

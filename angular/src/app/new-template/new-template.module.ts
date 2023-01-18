import { NgModule } from '@angular/core';

import { NewTemplateRoutingModule } from './new-template-routing.module';
import { NewTemplateComponent } from './new-template.component';
import { CKEditorModule } from '@ckeditor/ckeditor5-angular'
import { SharedModule } from '../shared/shared.module';
import { NgbDatepickerModule } from '@ng-bootstrap/ng-bootstrap';
import { NgbDropdownModule } from '@ng-bootstrap/ng-bootstrap';

@NgModule({
  declarations: [
    NewTemplateComponent
  ],
  imports: [
    SharedModule,
    CKEditorModule,
    NewTemplateRoutingModule,
    NgbDatepickerModule,NgbDropdownModule
  ]
})
export class NewTemplateModule { }

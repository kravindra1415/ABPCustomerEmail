import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { NewTemplateComponent } from './new-template.component';

const routes: Routes = [{ path: '', component: NewTemplateComponent }];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class NewTemplateRoutingModule { }

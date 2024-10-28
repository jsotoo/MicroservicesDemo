import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PoliciesRoutingModule } from './policies-routing.module';
import { SharedModule } from '../shared/shared.module';
import { PoliciesListComponent } from './policies-list/policies-list.component';
import { ReportService } from '../services/data/policies/report.service';

@NgModule({
  declarations: [PoliciesRoutingModule.components, PoliciesListComponent],
  imports: [
    CommonModule,
    SharedModule,
    PoliciesRoutingModule
  ],
  providers: [
    ReportService  
  ]
})
export class PoliciesModule { }

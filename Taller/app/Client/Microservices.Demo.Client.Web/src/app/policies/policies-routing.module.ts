import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';
import { PoliciesComponent } from './policies.component';
import { PolicyCreateComponent } from './policy-create/policy-create.component';
import { PoliciesListComponent } from './policies-list/policies-list.component';


const routes: Routes = [
  {
    path: '', component: PoliciesComponent,
    children: [
      { path: 'fromOffer/:offerNumber', component: PolicyCreateComponent },
      { path: 'list', component: PoliciesListComponent },
    ]
  }
];

@NgModule({
  declarations: [],
  exports: [RouterModule],
  imports: [
    RouterModule.forChild(routes),
    CommonModule
  ]
})
export class PoliciesRoutingModule {
  static components = [
    PolicyCreateComponent,
    PoliciesListComponent
  ];
}

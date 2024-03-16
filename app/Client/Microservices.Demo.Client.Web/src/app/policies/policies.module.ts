import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PoliciesRoutingModule } from './policies-routing.module';
import { SharedModule } from '../shared/shared.module';
import {MatTableModule} from "@angular/material/table";


@NgModule({
  declarations: [PoliciesRoutingModule.components],
  imports: [
    CommonModule,
    SharedModule,
    PoliciesRoutingModule,MatTableModule
  ]
})
export class PoliciesModule { }

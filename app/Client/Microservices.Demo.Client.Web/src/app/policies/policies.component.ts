import { Component, OnInit } from '@angular/core';
import { PoliciesService } from '../services/data/policies/policies.service';
import { IPolicies } from '../../app/models/ipolicies';

@Component({
  selector: 'app-policies',
  templateUrl: './policies.component.html',
  styleUrls: ['./policies.component.scss']
})
export class PoliciesComponent implements OnInit {


  constructor(

  ) { }

  ngOnInit(): void {

  }


}

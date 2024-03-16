import { Component, OnInit } from '@angular/core';
import { ProductsService } from '../../services/data/products.service';
import { IProduct } from '../../models/iproduct';
import {IPolicies} from "../../models/ipolicies";
import {PoliciesService} from "../../services/data/policies/policies.service";

@Component({
  selector: 'app-policy-list',
  templateUrl: './policy-list.component.html',
  styleUrls: ['./policy-list.component.scss']
})



export class PolicyListComponent implements OnInit {
  title!: string;
  policies: IPolicies[] = [];
  public displayedColumns: string[] = ['Number','DateFrom','DateTo','PolicyHolder','TotalPremium', 'CodeProductName', 'ProductImage','ProductDescription', 'ProductMaxNumberOfInsured','AgentLogin',

  ];

  constructor(
    private policiesService: PoliciesService,
  ) { }

  ngOnInit(): void {
    this.getPolicies();
  }

  getPolicies() {
    this.policiesService.getPolicies()
      .subscribe((response: IPolicies[]) => {
        this.policies = response;
        console.log(this.policies);
      });
  }
}


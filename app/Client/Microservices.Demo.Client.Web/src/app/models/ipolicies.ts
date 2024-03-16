import { ICover } from "./icover";
import { IQuestion } from "./iquestion";

export interface IPolicies {
  Number: string;
  DateFrom: string;
  DateTo: string;
  PolicyHolder: string;
  TotalPremium: number;
  ProductCode: string;
  ProductName: string;
  ProductImage: string;
  ProductDescription: string;
  ProductMaxNumberOfInsured: number;
  AgentLogin: string;
}

import { Guid } from "guid-typescript"

export interface IArticleData
{
  articleGuid: string; 
  articleName: string; 
  description: string; 
  gtin?: number; 
  unit: string; 
  position: number; 
  status: string;
  quantity: number; 
  boxGuid: string;  
  articleBoxAssignment: string;
  expiryDate?: Date; 
}
export interface IPropertyBase{
  Id?: number;
  SellRent?: number; //1=SEll 2=Rent
  Name: string;
  PType: string;
  FType: string;
  Price?: number;
  BHK?: number;
  BuiltArea?: number;
  City: string;
  RTM?: number;
  Image: string;
}

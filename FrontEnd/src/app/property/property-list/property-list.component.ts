import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { HousingService } from 'src/app/services/housing.service';
import { IPropertyBase } from 'src/app/model/ipropertybase';
import { datepickerAnimation } from 'ngx-bootstrap/datepicker/datepicker-animations';

@Component({
  selector: 'app-property-list',
  templateUrl: './property-list.component.html',
  styleUrls: ['./property-list.component.css']
})
export class PropertyListComponent implements OnInit {
  //properties: Array<IProperty>=[];
  properties!:IPropertyBase[];
  SellRent=1;
  Today=new Date();
  City = '';
  SearchCity = '';
  SortbyParam = '';
  SortDirection = 'asc';

  constructor(private route: ActivatedRoute, private housingService: HousingService) { }

  ngOnInit(): void {
    if(this.route.snapshot.url.toString()){
      this.SellRent=2; //this means that there is sellrent attribute in url other wise it is from buy
    }

    this.housingService.getAllProperties(this.SellRent).subscribe(
      data=>{
        this.properties=data;
        //console.log(data);
        //console.log(this.route.snapshot.url.toString());
      },error=>{
        console.log(error);
      }
    )
    // this.http.get('data/properties.json').subscribe(
    //   data=>{
    //     console.log(data);
    //     this.properties=data;
    //   }
    //   );
  }

  onCityFilter() {
    this.SearchCity = this.City;
  }

  onCityFilterClear() {
    this.SearchCity = '';
    this.City = '';
  }

  onSortDirection() {
    if (this.SortDirection === 'desc') {
      this.SortDirection = 'asc';
    } else {
      this.SortDirection = 'desc';
    }
  }


}

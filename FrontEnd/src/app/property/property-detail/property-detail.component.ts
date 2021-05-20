import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { HousingService } from 'src/app/services/housing.service';
import { Property } from 'src/app/model/property';

@Component({
  selector: 'app-property-detail',
  templateUrl: './property-detail.component.html',
  styleUrls: ['./property-detail.component.css']
})
export class PropertyDetailComponent implements OnInit {
public propertyId:number=0;
property = new Property();

constructor(private route: ActivatedRoute,
  private router: Router,
  private housingService: HousingService) { }

    ngOnInit(): void {
    //we can convert string to number by using Number()
    //or by adding + to the begining of string
    //this.propertyId=Number(this.route.snapshot.params['id']);
    this.propertyId= +this.route.snapshot.params['id'];

    // this.route.params.subscribe(
    //   (params)=>{
    //     this.propertyId = +params['id'];
    //   });

    this.route.params.subscribe(
      (params) => {
        this.propertyId = +params['id'];
        this.housingService.getProperty(this.propertyId).subscribe(
          (data) => {
            this.property = <Property> data;
          }, error => this.router.navigate(['/'])
        );
      }
    );
  }

  onSelectNext(){
  this.propertyId+=1;
  this.router.navigate(['property-detail',this.propertyId]);
  }
}

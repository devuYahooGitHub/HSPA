import { core } from "@angular/compiler"
import {Component, Input} from '@angular/core'
import { IPropertyBase } from "src/app/model/ipropertybase";


@Component({
  selector:'app-property-card',
  //template:'<h1>Hi I am a card</h1>'
  templateUrl: 'property-card.component.html',
  //styles:['h1 {font-weight: normal;}']
  styleUrls:['property-card.component.css']
})

export class PropertyCardComponent{
 @Input() property!: IPropertyBase;
 @Input() hideIcons!:boolean;
// Property: any=
//     {
//     "id":1,
//     "Name":"Dev House",
//     "Type":"House",
//     "Price":11000
//     }
}

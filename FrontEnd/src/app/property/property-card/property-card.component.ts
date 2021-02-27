import { core } from "@angular/compiler"
import {Component} from '@angular/core'

@Component({
  selector:'app-property-card',
  //template:'<h1>Hi I am a card</h1>'
  templateUrl: 'property-card.component.html',
  //styles:['h1 {font-weight: normal;}']
  styleUrls:['property-card.component.css']
})

export class PropertyCardComponent{
Property: any={
  "id":1,
  "Name":"Dev House",
  "Type":"House",
  "Price":12000
}
}

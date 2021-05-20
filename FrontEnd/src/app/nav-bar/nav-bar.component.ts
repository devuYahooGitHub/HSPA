import { Component, OnInit } from '@angular/core';

@Component({
	selector: 'app-nav-bar',
	templateUrl: 'nav-bar.component.html'
})

export class NavBarComponent implements OnInit {

	ngOnInit() { }

  loggedIn(){
    //console.log(localStorage.getItem('token'));
    return localStorage.getItem('token');
  }

  onLogOut(){
    localStorage.removeItem('token');
  }
}

import { TestBed, inject } from '@angular/core/testing';

import { NavBarComponent } from './nav-bar.component';

describe('a nav-bar component', () => {
	let component: NavBarComponent;

	// register all needed dependencies
	beforeEach(() => {
		TestBed.configureTestingModule({
			providers: [
				NavBarComponent
			]
		});
	});

	// instantiation through framework injection
	beforeEach(inject([NavBarComponent], (NavBarComponent) => {
		component = NavBarComponent;
	}));

	it('should have an instance', () => {
		expect(component).toBeDefined();
	});
});
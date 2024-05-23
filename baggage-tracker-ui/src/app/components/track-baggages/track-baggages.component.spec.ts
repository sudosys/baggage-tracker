import { ComponentFixture, TestBed } from '@angular/core/testing';
import { TrackBaggagesComponent } from './track-baggages.component';

describe('TrackBaggagesComponent', () => {
	let component: TrackBaggagesComponent;
	let fixture: ComponentFixture<TrackBaggagesComponent>;

	beforeEach(async () => {
		await TestBed.configureTestingModule({
			imports: [TrackBaggagesComponent]
		}).compileComponents();

		fixture = TestBed.createComponent(TrackBaggagesComponent);
		component = fixture.componentInstance;
		fixture.detectChanges();
	});

	it('should create', () => {
		expect(component).toBeTruthy();
	});
});

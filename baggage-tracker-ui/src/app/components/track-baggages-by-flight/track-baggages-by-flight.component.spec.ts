import { ComponentFixture, TestBed } from '@angular/core/testing';
import { TrackBaggagesByFlightComponent } from './track-baggages-by-flight.component';

describe('TrackBaggagesByFlightComponent', () => {
	let component: TrackBaggagesByFlightComponent;
	let fixture: ComponentFixture<TrackBaggagesByFlightComponent>;

	beforeEach(async () => {
		await TestBed.configureTestingModule({
			imports: [TrackBaggagesByFlightComponent]
		}).compileComponents();

		fixture = TestBed.createComponent(TrackBaggagesByFlightComponent);
		component = fixture.componentInstance;
		fixture.detectChanges();
	});

	it('should create', () => {
		expect(component).toBeTruthy();
	});
});

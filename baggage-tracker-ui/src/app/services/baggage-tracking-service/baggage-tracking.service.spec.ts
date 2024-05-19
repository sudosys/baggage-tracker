import { TestBed } from '@angular/core/testing';
import { BaggageTrackingService } from './baggage-tracking.service';

describe('BaggageTrackingService', () => {
	let service: BaggageTrackingService;

	beforeEach(() => {
		TestBed.configureTestingModule({});
		service = TestBed.inject(BaggageTrackingService);
	});

	it('should be created', () => {
		expect(service).toBeTruthy();
	});
});

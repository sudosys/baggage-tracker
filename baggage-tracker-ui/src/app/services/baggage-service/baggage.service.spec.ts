import { TestBed } from '@angular/core/testing';

import { BaggageService } from './baggage.service';

describe('BaggageService', () => {
    let service: BaggageService;

    beforeEach(() => {
        TestBed.configureTestingModule({});
        service = TestBed.inject(BaggageService);
    });

    it('should be created', () => {
        expect(service).toBeTruthy();
    });
});

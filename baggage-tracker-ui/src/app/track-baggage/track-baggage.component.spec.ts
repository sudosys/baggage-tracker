import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TrackBaggageComponent } from './track-baggage.component';

describe('TrackBaggageComponent', () => {
    let component: TrackBaggageComponent;
    let fixture: ComponentFixture<TrackBaggageComponent>;

    beforeEach(async () => {
        await TestBed.configureTestingModule({
            declarations: [TrackBaggageComponent],
        }).compileComponents();
    });

    beforeEach(() => {
        fixture = TestBed.createComponent(TrackBaggageComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });
});

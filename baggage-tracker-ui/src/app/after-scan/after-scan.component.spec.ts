import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AfterScanComponent } from './after-scan.component';

describe('AfterScanComponent', () => {
    let component: AfterScanComponent;
    let fixture: ComponentFixture<AfterScanComponent>;

    beforeEach(async () => {
        await TestBed.configureTestingModule({
            declarations: [AfterScanComponent],
        }).compileComponents();
    });

    beforeEach(() => {
        fixture = TestBed.createComponent(AfterScanComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });
});

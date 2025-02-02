import { ComponentFixture, TestBed } from '@angular/core/testing';
import { QrCodeScanComponent } from './qr-code-scan.component';

describe('QrCodeScanComponent', () => {
	let component: QrCodeScanComponent;
	let fixture: ComponentFixture<QrCodeScanComponent>;

	beforeEach(async () => {
		await TestBed.configureTestingModule({
			imports: [QrCodeScanComponent]
		}).compileComponents();

		fixture = TestBed.createComponent(QrCodeScanComponent);
		component = fixture.componentInstance;
		fixture.detectChanges();
	});

	it('should create', () => {
		expect(component).toBeTruthy();
	});
});

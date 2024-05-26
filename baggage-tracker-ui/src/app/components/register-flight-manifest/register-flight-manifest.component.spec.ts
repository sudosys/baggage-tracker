import { ComponentFixture, TestBed } from '@angular/core/testing';
import { RegisterFlightManifestComponent } from './register-flight-manifest.component';

describe('RegisterFlightManifestComponent', () => {
	let component: RegisterFlightManifestComponent;
	let fixture: ComponentFixture<RegisterFlightManifestComponent>;

	beforeEach(async () => {
		await TestBed.configureTestingModule({
			imports: [RegisterFlightManifestComponent]
		}).compileComponents();

		fixture = TestBed.createComponent(RegisterFlightManifestComponent);
		component = fixture.componentInstance;
		fixture.detectChanges();
	});

	it('should create', () => {
		expect(component).toBeTruthy();
	});
});

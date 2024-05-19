import { ComponentFixture, TestBed } from '@angular/core/testing';
import { PostScanComponent } from './post-scan.component';

describe('PostScanComponent', () => {
	let component: PostScanComponent;
	let fixture: ComponentFixture<PostScanComponent>;

	beforeEach(async () => {
		await TestBed.configureTestingModule({
			imports: [PostScanComponent]
		}).compileComponents();

		fixture = TestBed.createComponent(PostScanComponent);
		component = fixture.componentInstance;
		fixture.detectChanges();
	});

	it('should create', () => {
		expect(component).toBeTruthy();
	});
});

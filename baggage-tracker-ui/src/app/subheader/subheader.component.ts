import { Component, Input } from '@angular/core';

@Component({
    selector: 'subheader',
    templateUrl: './subheader.component.html',
    styleUrls: ['./subheader.component.css'],
})
export class SubheaderComponent {
    @Input() iconName: string;
    @Input() text: string;
}

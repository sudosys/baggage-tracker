import { Component } from '@angular/core';
import { Location } from '@angular/common';

@Component({
    selector: 'go-back',
    templateUrl: './go-back.component.html',
    styleUrls: ['./go-back.component.css'],
})
export class GoBackComponent {
    constructor(private location: Location) {}

    goBack() {
        this.location.back();
    }
}

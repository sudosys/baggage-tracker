import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';

@Component({
    selector: 'go-back',
    templateUrl: './go-back.component.html',
    styleUrls: ['./go-back.component.css'],
})
export class GoBackComponent implements OnInit {
    constructor(private location: Location) {}

    ngOnInit(): void {}

    goBack() {
        this.location.back();
    }
}

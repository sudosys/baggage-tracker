import { Component, OnInit } from '@angular/core';
import { BaggageTrackerClient } from '../../../../open-api/bt-api.client';

@Component({
	selector: 'app-help',
	templateUrl: './help.component.html',
	styleUrl: './help.component.scss'
})
export class HelpComponent implements OnInit {
	constructor(private btClient: BaggageTrackerClient) {}

	protected helpText: string | undefined;

	ngOnInit(): void {
		this.btClient.help().subscribe((r) => (this.helpText = r.response));
	}
}

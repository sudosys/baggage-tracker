import { FormControl } from '@angular/forms';

export type FormOf<T> = {
	[prop in keyof T]: FormControl<T[prop]>;
};

import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';
import { AppModule } from './app/app.module';
import './app/extensions/string.extension';

platformBrowserDynamic()
	.bootstrapModule(AppModule)
	.catch((err) => console.error(err));

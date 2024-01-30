import { bootstrapApplication } from '@angular/platform-browser';
import { AppComponent } from './app/app.component';
import { provideAnimations } from '@angular/platform-browser/animations';
import { PreloadAllModules, provideRouter, withDebugTracing, withPreloading, withRouterConfig } from '@angular/router';
import { routes } from './app/routes/routes';

bootstrapApplication(AppComponent, {
  providers: [
    provideRouter(routes, withPreloading(PreloadAllModules), withDebugTracing(), withRouterConfig({ onSameUrlNavigation: 'reload' })),
    provideAnimations(),
    provideAnimations()
],
}).catch((err) => console.error(err));

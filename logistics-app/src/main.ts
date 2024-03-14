import { bootstrapApplication } from '@angular/platform-browser';
import { AppComponent } from './app/app.component';
import { provideAnimations } from '@angular/platform-browser/animations';
import { PreloadAllModules, provideRouter, withDebugTracing, withPreloading, withRouterConfig } from '@angular/router';
import { routes } from './app/routes/routes';
import { MAT_FORM_FIELD_DEFAULT_OPTIONS } from '@angular/material/form-field';
import { importProvidersFrom } from '@angular/core';
import { HttpClientModule, provideHttpClient, withInterceptors } from '@angular/common/http';
import { authInterceptor } from './app/services/authentication/auth-interceptor.service';
import { AuthGuard } from './app/services/authentication/authguard';
import { DatePipe } from '@angular/common';
import { provideNativeDateAdapter } from '@angular/material/core';
import { MatSnackBar } from '@angular/material/snack-bar';
bootstrapApplication(AppComponent, {
  providers: [
    importProvidersFrom(
      HttpClientModule
    ),
    provideRouter(routes, withPreloading(PreloadAllModules), withDebugTracing(), withRouterConfig({ onSameUrlNavigation: 'reload' })),
    provideAnimations(),
    provideAnimations(),
    { provide: MAT_FORM_FIELD_DEFAULT_OPTIONS, useValue: { appearance: 'outline', subscriptSizing: 'dynamic' } },
    provideHttpClient(withInterceptors([authInterceptor])),
    AuthGuard,
    DatePipe,
    MatSnackBar,
    provideNativeDateAdapter()
  ],
}).catch((err) => console.error(err));

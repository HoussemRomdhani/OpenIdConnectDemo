import { ApplicationConfig, importProvidersFrom, provideZoneChangeDetection } from '@angular/core';
import { provideRouter } from '@angular/router';
import { AuthInterceptor, authInterceptor, AuthModule, LogLevel } from 'angular-auth-oidc-client';
import { routes } from './app.routes';
import { BrowserModule } from '@angular/platform-browser';
import { HTTP_INTERCEPTORS, provideHttpClient, withInterceptors } from '@angular/common/http';
import { UnauthorizedInterceptor  } from './unauthorized.interceptor'; 

export const appConfig: ApplicationConfig = 
{
  providers: [provideZoneChangeDetection({ eventCoalescing: true }),
    importProvidersFrom(BrowserModule, AuthModule.forRoot({
      config: {
          authority: 'https://localhost:5001',
          redirectUrl: `${window.location.origin}`,
          postLogoutRedirectUri: window.location.origin,
          clientId: 'publicclient', 
          scope: 'openid profile offline_access bookstoreapi.read',
          responseType: 'code',
          silentRenew: true,
          useRefreshToken: true,
          renewTimeBeforeTokenExpiresInSeconds: 30,
          logLevel: LogLevel.Debug,
          secureRoutes: ['https://localhost:5000/', 'https://localhost:5000/books/'],
      },
  })),
     provideHttpClient(withInterceptors([authInterceptor()])),
     provideRouter(routes),
     { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true },
     { provide: HTTP_INTERCEPTORS, useClass: UnauthorizedInterceptor, multi: true },
    ]
};

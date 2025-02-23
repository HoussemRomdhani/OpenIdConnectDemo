import { CanActivateFn } from '@angular/router';

import { inject, Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { map, Observable } from 'rxjs';
import { AuthenticatedResult, OidcSecurityService } from 'angular-auth-oidc-client';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  router: Router = inject(Router);
  oidcSecurityService: OidcSecurityService = inject(OidcSecurityService);

  canActivate(
    next: ActivatedRouteSnapshot,
    
    state: RouterStateSnapshot): Observable<boolean> | Promise<boolean> | boolean {

      let isAuthenticated = false;  
      this.oidcSecurityService.isAuthenticated$.pipe(
          map((r: AuthenticatedResult) => r.isAuthenticated)
        ).subscribe((authenticated: boolean) => {
          isAuthenticated = authenticated;
        });

    if (!isAuthenticated) {
       this.router.navigate(['/unauthorized']);
       return false;
    }
    
    return true;
  }
}

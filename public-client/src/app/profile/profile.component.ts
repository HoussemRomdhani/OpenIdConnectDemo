import { AsyncPipe } from '@angular/common';
import { Component, inject } from '@angular/core';
import { OidcSecurityService, UserDataResult } from 'angular-auth-oidc-client';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-profile',
  imports: [AsyncPipe],
  templateUrl: './profile.component.html',
})
export class ProfileComponent {
  oidcSecurityService: OidcSecurityService = inject(OidcSecurityService);
  claims$: Observable<UserDataResult> = this.oidcSecurityService.userData$;
}

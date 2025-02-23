import { Component, inject, OnDestroy, OnInit } from '@angular/core';
import { AuthenticatedResult, OidcSecurityService, UserDataResult } from 'angular-auth-oidc-client';
import { map, Observable, SubscriptionLike } from 'rxjs';
import { Router } from '@angular/router';
import { AsyncPipe, NgIf } from '@angular/common';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  standalone: true,
  imports: [NgIf, AsyncPipe, RouterModule]
})
export class AppComponent implements OnInit, OnDestroy{
  oidcSecurityService: OidcSecurityService = inject(OidcSecurityService);
  router: Router = inject(Router);
  title = 'Book store';
  private authSub$!: SubscriptionLike;
  name$: Observable<UserDataResult> = this.oidcSecurityService.userData$.pipe(map(d => d.userData?.['name']));
  isAuthenticated$: Observable<boolean> = this.oidcSecurityService.isAuthenticated$.pipe(
    map((r: AuthenticatedResult) => r.isAuthenticated)
  );

  login(): void {
    this.oidcSecurityService.authorize();
  }

  logout(): void {
    this.oidcSecurityService.logoff().subscribe((result) => console.log(result));
  }
  goToProfile() {
    this.router.navigate(['/profile']);
  }

  goToBooks() {
    this.router.navigate(['/books']);
  }
  goToHome() {
    this.router.navigate(['/']);
  }

  ngOnInit(): void {
    this.authSub$ = this.oidcSecurityService.checkAuth().subscribe(
      ({ isAuthenticated }) =>{
        console.log('app authenticated', isAuthenticated)
      }
    );
  }

  ngOnDestroy(): void {
    this.authSub$.unsubscribe();
  }
 }

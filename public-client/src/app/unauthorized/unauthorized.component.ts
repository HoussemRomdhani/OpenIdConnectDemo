import { Component, inject } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-unauthorized',
  imports: [],
  templateUrl: './unauthorized.component.html'
})
export class UnauthorizedComponent {
  router: Router = inject(Router);
  
  goToHome() {
    this.router.navigate(['/']);
  }
}

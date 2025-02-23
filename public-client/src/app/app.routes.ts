import { Routes } from '@angular/router';
import { ProfileComponent } from './profile/profile.component';
import { BooksComponent } from './books/books.component';
import { HomeComponent } from './home/home.component';
import { AuthGuard } from './auth.guard';
import { UnauthorizedComponent } from './unauthorized/unauthorized.component';

export const routes: Routes =
 [{ path: '', redirectTo: 'home',  pathMatch: 'full' },
  { path: 'profile', component: ProfileComponent,  canActivate: [AuthGuard] },
  { path: 'books', component: BooksComponent,  canActivate: [AuthGuard] },
  { path: 'unauthorized', component: UnauthorizedComponent }, 
  { path: 'home', component: HomeComponent },
  { path: '**', redirectTo: '/unauthorized' } 
 ];
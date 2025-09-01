import { CanActivateFn, Router } from '@angular/router';
import { AuthService } from '../services/auth.service';
import { Observable } from 'rxjs';
import { inject } from '@angular/core';

export const AuthGuard: CanActivateFn = (route, state) => {
  const authService = inject(AuthService);
  const router = inject(Router);

  // Check if user is authenticated
  if (authService.isLoggedIn()) {
    return true; // Allow access if authenticated
  } else {
    // Redirect to login if not authenticated
    router.navigate(['/login']);
    return false;
  }
};
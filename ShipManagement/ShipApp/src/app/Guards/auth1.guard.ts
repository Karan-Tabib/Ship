import { CanActivateFn } from '@angular/router';

export const auth1Guard: CanActivateFn = (route, state) => {
  return true;
};

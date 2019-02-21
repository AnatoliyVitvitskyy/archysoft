import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import {
  AuthModule as AuthenticationModule,
  AUTH_SERVICE,
  PUBLIC_FALLBACK_PAGE_URI,
  PROTECTED_FALLBACK_PAGE_URI
} from 'ngx-auth';

import { AuthRoutingModule } from './auth-routing.module';
import { SharedModule } from '../shared/shared.module';
import { AuthService } from './services/auth.service';
import { TokenService } from './services/token.service';
import { AuthComponent } from './auth.component';
import { LoginComponent } from './components/login/login.component';

export function factory(authService: AuthService) {
  return authService;
}

@NgModule({
  declarations: [AuthComponent, LoginComponent],
  imports: [
    CommonModule,
    AuthRoutingModule,
    SharedModule
  ],
  exports: [
    AuthenticationModule
  ],
  providers: [
    AuthService,
    TokenService,
    { provide: PROTECTED_FALLBACK_PAGE_URI, useValue: '/' },
    { provide: PUBLIC_FALLBACK_PAGE_URI, useValue: '/auth/login' },
    {
      provide: AUTH_SERVICE,
      deps: [AuthService],
      useFactory: factory
    }
  ]
})
export class AuthModule { }

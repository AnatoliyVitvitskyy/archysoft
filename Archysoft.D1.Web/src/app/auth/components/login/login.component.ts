import { Component, OnInit } from '@angular/core';
import { LoginModel } from '../../models/login.model';
import { AuthService } from '../../services/auth.service';
import { ApiResponse } from 'src/app/shared/models/api-response.model';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  model: LoginModel = {login: 'admin@d1.archysoft.com', password: 'admin', remeberMe: false};

  constructor(
    private authService: AuthService,
    private router: Router
  ) { }

  ngOnInit() {
  }

  login() {
    this.authService.login(this.model).subscribe((response: ApiResponse<any>) => {
      if (response.status === 1) {
        this.router.navigateByUrl('/');
      }
    });
  }

}

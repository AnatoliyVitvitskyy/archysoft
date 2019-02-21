import { Injectable } from '@angular/core';
import { AuthService as AuthenticationService} from 'ngx-auth';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { TokenService } from './token.service';
import { Observable } from 'rxjs';
import { tap, map, switchMap, catchError } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { TokenModel } from '../models/token.model';
import { LoginModel } from '../models/login.model';
import { ApiResponse } from 'src/app/shared/models/api-response.model';

@Injectable()
export class AuthService implements AuthenticationService {

  constructor(
    private http: HttpClient,
    private tokenService: TokenService
  ) {}

  public isAuthorized(): Observable<boolean> {
    return this.tokenService.getAccessToken().pipe(map(token => !!token));
  }

  public getAccessToken(): Observable<string> {
    return this.tokenService.getAccessToken();
  }

  public refreshToken(): Observable <TokenModel> {
    return this.tokenService
      .getRefreshToken()
      .pipe(
        switchMap((refreshToken: string) =>
          this.http.post(`${environment.apiUrl}/auth/refresh`, { refreshToken })
        ),
        tap((tokens: TokenModel) => this.saveAccessData(tokens)),
        catchError((err) => {
          this.logout();

          return Observable.throw(err);
        })
      );
  }

  public refreshShouldHappen(response: HttpErrorResponse): boolean {
    return response.status === 401;
  }

  public verifyTokenRequest(url: string): boolean {
    return url.endsWith('/refresh');
  }

  public login(loginModel: LoginModel): Observable<ApiResponse<TokenModel>> {
    const headers = new HttpHeaders();
    headers.set('Content-Type', 'application/json; charset=utf-8');

    return this.http.post(`${environment.apiUrl}/auth/login`, loginModel, {headers: headers})
    .pipe(tap((response: ApiResponse<TokenModel>) => this.saveAccessData(response.model)));
  }

  public logout(): void {
    this.tokenService.clear();
    location.reload(true);
  }

  private saveAccessData(tokenModel: TokenModel) {
    if (tokenModel) {
      this.tokenService
      .setAccessToken(tokenModel.accessToken)
      .setRefreshToken(tokenModel.refreshToken);
    }
  }
}

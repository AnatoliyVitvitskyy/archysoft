import { HttpClient, HttpParams } from '@angular/common/http';
import { BaseFilter } from 'src/app/shared/models/base-filter.model';
import { ApiResponse } from 'src/app/shared/models/api-response.model';
import { Observable } from 'rxjs';

export class BaseService {
    constructor(
        private httpClient: HttpClient
    ) {}

    get<T>(path: string, filter: BaseFilter): Observable<ApiResponse<T>> {
        return this.httpClient.get<ApiResponse<T>>(path, {
            params: new HttpParams()
                .set('search', filter.search)
                .set('orderBy', filter.orderBy)
                .set('pageIndex', filter.pageIndex.toString())
                .set('pageSize', filter.pageSize.toString())
        });
    }

    post<T>(path: string, model: any): Observable<ApiResponse<T>> {
        return this.httpClient.post<ApiResponse<T>>(path, model);
    }

    put<T>(path: string, model: any): Observable<ApiResponse<T>> {
        return this.httpClient.put<ApiResponse<T>>(path, model);
    }

    delete<T>(path: string, model: any): Observable<ApiResponse<T>> {
        return this.httpClient.post<ApiResponse<T>>(path, model);
    }
}

import { Injectable } from '@angular/core';
import { BaseService } from 'src/app/core/services/base.service';
import { HttpClient } from '@angular/common/http';
import { BaseFilter } from 'src/app/shared/models/base-filter.model';
import { Observable } from 'rxjs';
import { ApiResponse } from 'src/app/shared/models/api-response.model';
import { SearchResult } from 'src/app/shared/models/search-result.model';
import { EmployeeGridModel } from '../models/employee-grid.model';
import { environment } from 'src/environments/environment';

@Injectable({
    providedIn: 'root'
})
export class EmsployeeService extends BaseService {
    constructor(httpClient: HttpClient) {
        super(httpClient);
    }

    getEmployees(filter: BaseFilter): Observable<ApiResponse<SearchResult<EmployeeGridModel>>> {
        return this.get<SearchResult<EmployeeGridModel>>(`${environment.apiUrl}/users`, filter);
    }
}

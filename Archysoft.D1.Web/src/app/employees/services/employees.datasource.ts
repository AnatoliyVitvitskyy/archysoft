import { DataSource } from '@angular/cdk/table';
import { CollectionViewer } from '@angular/cdk/collections';
import { Observable, BehaviorSubject, of } from 'rxjs';
import { EmployeeGridModel } from '../models/employee-grid.model';
import { BaseFilter } from 'src/app/shared/models/base-filter.model';
import { EmsployeeService } from './employee.service';
import { catchError, finalize } from 'rxjs/operators';
import { ApiResponse } from 'src/app/shared/models/api-response.model';
import { SearchResult } from 'src/app/shared/models/search-result.model';

export class EmployeesDataSource implements DataSource<EmployeeGridModel> {
    private employeesSubject = new BehaviorSubject<EmployeeGridModel[]>([]);
    private loadingSubject = new BehaviorSubject<boolean>(false);
    private totalSubject = new BehaviorSubject<number>(0);

    constructor(private employeeService: EmsployeeService) {}

    connect(collectionViewer: CollectionViewer): Observable<EmployeeGridModel[]> {
        return this.employeesSubject.asObservable();
    }

    disconnect(collectionViewer: CollectionViewer): void {
        this.employeesSubject.complete();
        this.loadingSubject.complete();
        this.totalSubject.complete();
    }

    loadEmployees(filter: BaseFilter) {
        this.loadingSubject.next(true);
        this.employeeService.getEmployees(filter).pipe(
            catchError(() => of([])),
            finalize(() => this.loadingSubject.next(false))
        ).subscribe((response: ApiResponse<SearchResult<EmployeeGridModel>>) => {
            this.employeesSubject.next(response.model.data);
            this.totalSubject.next(response.model.total);
        });
    }

}

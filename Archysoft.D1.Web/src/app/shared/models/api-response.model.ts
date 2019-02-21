export class ApiResponse<T> {
    status: number;
    description: string;
    timestamp: number;
    model: T;
}

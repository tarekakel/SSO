export class PagedRequest<T> {
    pageIndex!: number;  // Current page
    pageSize!: number;   // Page size
    filter?: T;
}
export interface PagedResult<T> {
    totalCount: number;
    pageIndex: number;
    pageSize: number;
    data: T[];
}

export class GeneralResponse<T> {
    success: boolean = false;                         // Indicates success or failure
    message: string = '';                             // Message for the client
    result?: T;                                       // The actual data returned
    timestamp: Date = new Date();                     // Time of response generation
    traceId: string = '';                             // Correlation ID
    errors: string[] = [];                            // List of validation or error messages

    static successResponse<T>(result: T, message: string = 'Request successful'): GeneralResponse<T> {
        const response = new GeneralResponse<T>();
        response.success = true;
        response.message = message;
        response.result = result;
        return response;
    }

    static errorResponse<T>(message: string, errors: string[] = []): GeneralResponse<T> {
        const response = new GeneralResponse<T>();
        response.success = false;
        response.message = message;
        response.errors = errors;
        return response;
    }
}

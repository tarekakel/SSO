
export interface RegisterRequestDto {
    email: string;
    password: string;
    firstName: string;
    lastName: string;
    confirm: string;
}

export interface LoginRequestDto {
    email: string;
    password: string;
}

export interface LoginResponse {
    success: boolean;
    message: string;
    data: {
        token: string;
    };
}
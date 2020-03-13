import { ApiError } from './ApiError';

export type ApiResponse<T> = { isSuccess: true; content: T } | { isSuccess: false; content: ApiError };

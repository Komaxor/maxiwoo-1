export class ApiError {
  public statusCode?: number;
  public functionCode?: string;
  public correlationId?: string;
  public messages?: string[];
}

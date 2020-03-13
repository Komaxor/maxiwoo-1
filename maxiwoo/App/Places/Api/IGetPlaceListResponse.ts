export interface IGetPlaceListResponse {
  results: Array<{ id: string; name: string; type: string; previewImage: string }>;
  resultsLength: number;
}

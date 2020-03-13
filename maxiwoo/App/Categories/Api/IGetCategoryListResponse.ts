export interface IGetCategoryListResponse {
  results: Array<{ id: string; title: string; lang_Key: string; previewImage: string }>;
  resultsLength: number;
}

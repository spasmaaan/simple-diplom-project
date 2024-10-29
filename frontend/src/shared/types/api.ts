export type FetchBody = BodyInit | object | null;
export type FetchHeaders = Record<string, string>;

export interface IPaginationResult<TResult> {
  currentPage: number;
  hasNextPage: boolean;
  hasPerviousPage: boolean;
  items: TResult[];
  pageCount: number;
  pageSize: number;
}

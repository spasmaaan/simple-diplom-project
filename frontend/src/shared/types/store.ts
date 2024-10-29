export type WithSelectors<S> = S extends { getState: () => infer T }
  ? S & { use: { [K in keyof T]: () => T[K] } }
  : never;

export interface IImageItem {
  loading: boolean;
  data: Blob | null;
  url: string | null;
}

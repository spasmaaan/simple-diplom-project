import { WithSelectors } from 'shared/types';
import { StoreApi, UseBoundStore } from 'zustand';

export const createSelectors = <S extends UseBoundStore<StoreApi<object>>>(_store: S) => {
  const store = _store as WithSelectors<typeof _store>;
  store.use = {};

  Object.keys(store.getState()).forEach((k) => {
    // eslint-disable-next-line @typescript-eslint/no-explicit-any
    (store.use as any)[k] = () => store((s) => s[k as keyof typeof s]);
  });

  return store;
};

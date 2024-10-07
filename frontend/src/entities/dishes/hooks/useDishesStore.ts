import { createSelectors } from 'shared/helpers';
import { useDishesStoreBase } from '../model';

export const useDishesStore = createSelectors(useDishesStoreBase);

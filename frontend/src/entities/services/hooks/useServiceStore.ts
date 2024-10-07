import { createSelectors } from 'shared/helpers';
import { useServiceStoreBase } from '../model';

export const useServiceStore = createSelectors(useServiceStoreBase);

import { createSelectors } from 'shared/helpers';
import { useAuthStoreBase } from '../model';

export const useAuthStore = createSelectors(useAuthStoreBase);

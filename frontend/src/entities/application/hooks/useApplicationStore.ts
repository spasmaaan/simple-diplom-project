import { createSelectors } from 'shared/helpers';
import { useApplicationStoreBase } from '../model';

export const useApplicationStore = createSelectors(useApplicationStoreBase);

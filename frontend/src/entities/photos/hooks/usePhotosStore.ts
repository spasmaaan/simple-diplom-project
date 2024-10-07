import { createSelectors } from 'shared/helpers';
import { usePhotosStoreBase } from '../model';

export const usePhotosStore = createSelectors(usePhotosStoreBase);

import { createSelectors } from 'shared/helpers';
import { useReviewsStoreBase } from '../model';

export const useReviewsStore = createSelectors(useReviewsStoreBase);

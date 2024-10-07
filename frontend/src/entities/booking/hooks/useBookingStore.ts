import { createSelectors } from 'shared/helpers';
import { useBookingStoreBase } from '../model';

export const useBookingStore = createSelectors(useBookingStoreBase);

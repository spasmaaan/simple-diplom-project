import { createSelectors } from 'shared/helpers';
import { useSpreadSheetWidgetStoreBase } from '../model/SpreadSheetWidgetStore';

export const useSpreadSheetWidgetStore = createSelectors(useSpreadSheetWidgetStoreBase);

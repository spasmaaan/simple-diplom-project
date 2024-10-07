import { create } from 'zustand';
import { SpreadSheetWidgetAction, SpreadSheetWidgetState } from '../lib';

export const useSpreadSheetWidgetStoreBase = create<
  SpreadSheetWidgetState & SpreadSheetWidgetAction
>()((set) => ({}));

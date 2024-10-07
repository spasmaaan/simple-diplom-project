import { create } from 'zustand';
import { ApplicationAction, ApplicationOptionId, ApplicationState } from '../lib';
import { loadNotifications, loadOptions, setOption } from '../api';

export const useApplicationStoreBase = create<ApplicationState & ApplicationAction>()(
  (set, get) => ({
    options: {},
    optionsLoaded: false,
    optionsLoading: false,
    notifications: [],
    notificationsLoaded: false,
    notificationsLoading: false,
    loadOptions: async () => {
      const { optionsLoaded, optionsLoading } = get();
      if (optionsLoaded || optionsLoading) {
        return;
      }
      set(() => ({ optionsLoaded: false, optionsLoading: true }));
      const options = await loadOptions();
      set(() => ({ optionsLoaded: true, optionsLoading: false, options }));
    },
    loadNotifications: async () => {
      const { notificationsLoaded, notificationsLoading } = get();
      if (notificationsLoaded || notificationsLoading) {
        return;
      }
      set(() => ({ notificationsLoaded: false, notificationsLoading: true }));
      const notifications = await loadNotifications();
      set(() => ({ notificationsLoaded: true, notificationsLoading: false, notifications }));
    },
    setOption: async (id: ApplicationOptionId, value: string | null) => {
      if (!get().optionsLoaded) {
        await get().loadOptions();
      }
      if (get().optionsLoading) {
        return;
      }
      set(() => ({ optionsLoading: true }));
      const optionChanged = await setOption(id, value);
      if (optionChanged) {
        set((state) => ({
          optionsLoading: false,
          options: {
            ...state.options,
            [id]: value,
          },
        }));
      } else {
        set(() => ({ optionsLoading: false }));
      }
    },
  })
);

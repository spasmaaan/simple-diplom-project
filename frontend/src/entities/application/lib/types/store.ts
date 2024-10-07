import { ApplicationOptionId, IApplicationOptions, IOutboxMessage } from './types';

export type ApplicationState = {
  optionsLoading: boolean;
  optionsLoaded: boolean;
  options: IApplicationOptions;
  notificationsLoading: boolean;
  notificationsLoaded: boolean;
  notifications: IOutboxMessage[];
};

export type ApplicationAction = {
  loadOptions: () => Promise<void>;
  loadNotifications: () => Promise<void>;
  setOption: (id: ApplicationOptionId, value: string | null) => Promise<void>;
};

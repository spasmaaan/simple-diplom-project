import { backendGet, backendPut } from 'shared/api';
import { ApplicationBackendUrl } from 'shared/config';
import { ApplicationOptionId, IApplicationOptions, IOutboxMessage } from '../lib';
import { MOCK_NOTIFICATIONS } from './mock';

const getApplicationPath = (url: string) => `${ApplicationBackendUrl}/${url}`;

export const loadOptions = async (): Promise<IApplicationOptions> => {
  return (await backendGet(null, getApplicationPath('options'))).json();
};
export const loadNotifications = async (): Promise<IOutboxMessage[]> => {
  return MOCK_NOTIFICATIONS; // (await backendGet(null, getApplicationPath('notifications'))).json();
};
export const setOption = async (
  id: ApplicationOptionId,
  value: string | null
): Promise<boolean> => {
  return (await backendPut(null, getApplicationPath('options'), { id, value })).json();
};

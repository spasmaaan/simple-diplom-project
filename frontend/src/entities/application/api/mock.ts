import { IApplicationOptions, IOutboxMessage } from '../lib';

export const MOCK_OPTIONS: IApplicationOptions = {
  option1: 'value1',
};

export const MOCK_NOTIFICATIONS: IOutboxMessage[] = [
  {
    id: 0,
    type: 'message',
    payload: 'text',
    createdAt: new Date(),
    processedAt: null,
    error: '',
  },
];

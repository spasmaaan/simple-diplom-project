import { IPaginationResult } from 'shared/types';
import { IBooking } from '../lib';
import { BookingStatus } from '../lib/constants';

export const MOCK_BOOKINGS: IPaginationResult<IBooking> = {
  currentPage: 0,
  hasNextPage: false,
  hasPerviousPage: false,
  items: [
    {
      id: 1,
      creationDate: new Date('2024-09-15T14:09:24.745Z').toISOString(),
      startDate: new Date('2024-09-20T11:00:00.000Z').toISOString(),
      endDate: new Date('2024-09-20T12:15:00.000Z').toISOString(),
      comment: '',
      statusName: 'Завершено',
      statusId: BookingStatus.Completed,
      userId: 'Пользователь Обычный',
      dishes: {
        1: 2,
        3: 1,
      },
      services: {
        3: 1,
      },
    },
    {
      id: 2,
      creationDate: new Date('2024-09-15T14:09:24.745Z').toISOString(),
      startDate: new Date('2024-09-20T13:15:00.000Z').toISOString(),
      endDate: new Date('2024-09-20T12:30:00.000Z').toISOString(),
      comment: '',
      statusName: 'Завершено',
      statusId: BookingStatus.Rejected,
      userId: 'Пользователь Обычный',
      dishes: {
        1: 1,
        3: 1,
        6: 2,
        8: 1,
      },
      services: {
        1: 10,
        3: 1,
      },
    },
  ],
  pageCount: 1,
  pageSize: 1,
};

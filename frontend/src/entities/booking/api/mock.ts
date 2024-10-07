import { IBooking } from '../lib';
import { BookingStatus } from '../lib/constants';

export const MOCK_BOOKINGS: IBooking[] = [
  {
    id: 1,
    userId: 'User1',
    statusId: BookingStatus.Approved,
    creationDate: new Date('2000/01/01'),
    startDate: new Date('2000/01/02'),
    endDate: new Date('2000/01/03'),
    dishes: [],
    services: [],
  },
];

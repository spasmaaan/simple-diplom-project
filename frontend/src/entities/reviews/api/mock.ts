import { IReview } from '../lib';

export const MOCK_REVIEWS: IReview[] = [
  {
    id: 1,
    userId: 'User0',
    userName: 'Вася Пупочкин',
    creationDate: new Date('2015/03/02'),
    message: 'Что-то я написал.',
    rating: 5,
  },
];

import { IReview } from '../lib';

export const MOCK_REVIEWS: IReview[] = [
  {
    id: 1,
    shortName: 'ВП',
    fullName: 'Вася Пупочкин',
    creationDate: new Date('2015/03/02'),
    message: 'Что-то я написал.',
    rating: 1,
    me: true,
  },
  {
    id: 2,
    shortName: 'ВП',
    fullName: 'Вася Пупочкин',
    creationDate: new Date('2015/03/02'),
    message: 'Что-то я написал.',
    rating: 4,
    me: false,
  },
  {
    id: 3,
    shortName: 'ВП',
    fullName: 'Вася Пупочкин',
    creationDate: new Date('2015/03/02'),
    message: 'Что-то я написал.',
    rating: 5,
    me: false,
  },
  {
    id: 4,
    shortName: 'ВП',
    fullName: 'Вася Пупочкин',
    creationDate: new Date('2015/03/02'),
    message: 'Что-то я написал.',
    me: false,
  },
];

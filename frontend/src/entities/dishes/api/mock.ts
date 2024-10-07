import { IDish, IDishCategory } from '../lib';

export const MOCK_DISHES: IDish[] = [
  {
    id: 1,
    name: 'Блюдо 1',
    categoryId: 0,
    description: 'Описание блюда 1',
    previewImage: '',
    price: 1,
  },
  {
    id: 2,
    name: 'Блюдо 2',
    categoryId: 1,
    description: 'Описание блюда 2',
    previewImage: '',
    price: 120.2,
  },
];

export const MOCK_DISH_CATEGORIES: IDishCategory[] = [
  {
    id: 0,
    name: 'Итальянская кухня',
    description: 'Описание 0',
    previewImage: '',
  },
  {
    id: 1,
    name: 'Заморская кухня',
    description: 'Описание 1',
    previewImage: '',
  },
];

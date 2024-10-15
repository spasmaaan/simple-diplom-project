import { backendDelete, backendGet, backendPost, backendPut } from 'shared/api';
import { DishesBackendUrl } from 'shared/config';
import { DishCategoryId, DishId, IDish, IDishCategory, IDishCategoryData, IDishData } from '../lib';
import { MOCK_DISH_CATEGORIES, MOCK_DISHES } from './mock';

const getDishesPath = (url: string = '') => `${DishesBackendUrl}${url}`;
const getDishCategoriesPath = (url: string = '') => getDishesPath(`/categories${url}`);

export const loadDishes = async (): Promise<IDish[]> => {
  return MOCK_DISHES; // (await backendGet(null, getDishesPath())).json();
};
export const addDish = async (dishData: IDishData): Promise<IDish> => {
  return (await backendPost(null, getDishesPath(), dishData)).json();
};
export const editDish = async (dish: IDish): Promise<IDish> => {
  return (await backendPut(null, getDishesPath(), dish)).json();
};
export const removeDish = async (dishId: DishId): Promise<IDish> => {
  return (await backendDelete(null, getDishesPath(`/${dishId}`))).json();
};

export const loadDishCategories = async (): Promise<IDishCategory[]> => {
  return MOCK_DISH_CATEGORIES; // (await backendGet(null, getDishCategoriesPath())).json();
};
export const addDishCategory = async (categoryData: IDishCategoryData): Promise<IDishCategory> => {
  return (await backendPost(null, getDishCategoriesPath(), categoryData)).json();
};
export const editDishCategory = async (category: IDishCategory): Promise<IDishCategory> => {
  return (await backendPut(null, getDishCategoriesPath(), category)).json();
};
export const removeDishCategory = async (categoryId: DishCategoryId): Promise<IDishCategory> => {
  return (await backendDelete(null, getDishCategoriesPath(`/${categoryId}`))).json();
};

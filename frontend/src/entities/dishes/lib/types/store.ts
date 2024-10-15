import {
  DishCategoryId,
  DishId,
  IDish,
  IDishCategory,
  IDishCategoryData,
  IDishData,
} from './types';

export type DishesState = {
  dishesLoading: boolean;
  dishesLoaded: boolean;
  dishes: IDish[];
  categoriesLoading: boolean;
  categoriesLoaded: boolean;
  categories: IDishCategory[];
};

export type DishesAction = {
  loadDish: () => Promise<void>;
  addDish: (dishData: IDishData) => Promise<void>;
  editDish: (dish: IDish) => Promise<void>;
  removeDish: (dishId: DishId) => Promise<void>;
  loadCategories: () => Promise<void>;
  addCategory: (dishCategoryData: IDishCategoryData) => Promise<void>;
  editCategory: (dishCategory: IDishCategory) => Promise<void>;
  removeCategory: (dishCategoryId: DishCategoryId) => Promise<void>;
};

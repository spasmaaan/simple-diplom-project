import {
  DishCategoryId,
  DishId,
  IDish,
  IDishCategory,
  IDishCategoryData,
  IDishCategoryState,
  IDishData,
  IDishState,
} from './types';

export type DishesState = {
  dishesLoading: boolean;
  dishesLoaded: boolean;
  dishes: IDishState[];
  categoriesLoading: boolean;
  categoriesLoaded: boolean;
  categories: IDishCategoryState[];
};

export type DishesAction = {
  loadDish: () => Promise<void>;
  loadDishImage: (id: DishId) => Promise<void>;
  addDish: (dishData: IDishData) => Promise<void>;
  editDish: (dish: IDish) => Promise<void>;
  removeDish: (dishId: DishId) => Promise<void>;
  loadCategories: () => Promise<void>;
  loadCategoryImage: (id: DishCategoryId) => Promise<void>;
  addCategory: (dishCategoryData: IDishCategoryData) => Promise<void>;
  editCategory: (dishCategory: IDishCategory) => Promise<void>;
  removeCategory: (dishCategoryId: DishCategoryId) => Promise<void>;
};

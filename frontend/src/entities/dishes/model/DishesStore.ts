import { create } from 'zustand';
import { deleteElementWithId, getItemById, setElementWithId } from 'shared/helpers';
import { DishCategoryId, DishesAction, DishesState, DishId, IDish, IDishCategory } from '../lib';
import {
  addDish,
  addDishCategory,
  editDish,
  editDishCategory,
  loadDishCategories,
  loadDishes,
  removeDish,
  removeDishCategory,
} from '../api';

export const useDishesStoreBase = create<DishesState & DishesAction>()((set, get) => ({
  dishesLoading: false,
  dishesLoaded: false,
  dishes: [],
  categoriesLoading: false,
  categoriesLoaded: false,
  categories: [],
  loadDish: async () => {
    const { dishesLoaded, dishesLoading } = get();
    if (dishesLoaded || dishesLoading) {
      return;
    }
    set(() => ({ dishesLoaded: false, dishesLoading: true }));
    const dishes = await loadDishes();
    set(() => ({ dishesLoaded: true, dishesLoading: false, dishes }));
  },
  addDish: async (dish: IDish) => {
    if (!get().dishesLoaded) {
      await get().loadDish();
    }
    const { dishesLoading, dishes } = get();
    if (dishesLoading || getItemById(dishes, dish.id)) {
      return;
    }
    set(() => ({ dishesLoading: true }));
    const addedDish = await addDish(dish);
    set((state) => ({ dishesLoading: false, dishes: [...state.dishes, addedDish] }));
  },
  editDish: async (dish: IDish) => {
    if (!get().dishesLoaded) {
      await get().loadDish();
    }
    const { dishesLoading, dishes } = get();
    if (dishesLoading || !getItemById(dishes, dish.id)) {
      return;
    }
    set(() => ({ dishesLoading: true }));
    const editedDish = await editDish(dish);
    set((state) => ({ dishesLoading: false, dishes: setElementWithId(state.dishes, editedDish) }));
  },
  removeDish: async (dishId: DishId) => {
    if (!get().dishesLoaded) {
      await get().loadDish();
    }
    const { dishesLoading, dishes } = get();
    if (dishesLoading || !getItemById(dishes, dishId)) {
      return;
    }
    set(() => ({ dishesLoading: true }));
    const removedDish = await removeDish(dishId);
    set((state) => ({
      dishesLoading: false,
      dishes: deleteElementWithId(state.dishes, removedDish),
    }));
  },

  loadCategories: async () => {
    const { categoriesLoaded, categoriesLoading } = get();
    if (categoriesLoaded || categoriesLoading) {
      return;
    }
    set(() => ({ categoriesLoaded: false, categoriesLoading: true }));
    const categories = await loadDishCategories();
    set(() => ({ categoriesLoaded: true, categoriesLoading: false, categories }));
  },
  addCategory: async (dishCategory: IDishCategory) => {
    if (!get().categoriesLoaded) {
      await get().loadCategories();
    }
    const { categoriesLoading, categories } = get();
    if (categoriesLoading || getItemById(categories, dishCategory.id)) {
      return;
    }
    set(() => ({ categoriesLoading: true }));
    const addedCategory = await addDishCategory(dishCategory);
    set((state) => ({
      categoriesLoading: false,
      categories: [...state.categories, addedCategory],
    }));
  },
  editCategory: async (dishCategory: IDishCategory) => {
    if (!get().categoriesLoaded) {
      await get().loadCategories();
    }
    const { categoriesLoading, categories } = get();
    if (categoriesLoading || !getItemById(categories, dishCategory.id)) {
      return;
    }
    set(() => ({ categoriesLoading: true }));
    const editedCategory = await editDishCategory(dishCategory);
    set((state) => ({
      categoriesLoading: false,
      dishes: setElementWithId(state.dishes, editedCategory),
    }));
  },
  removeCategory: async (dishCategoryId: DishCategoryId) => {
    if (!get().categoriesLoaded) {
      await get().loadCategories();
    }
    const { categoriesLoading, categories } = get();
    if (categoriesLoading || !getItemById(categories, dishCategoryId)) {
      return;
    }
    set(() => ({ categoriesLoading: true }));
    const removedCategory = await removeDishCategory(dishCategoryId);
    set((state) => ({
      categoriesLoading: false,
      categories: deleteElementWithId(state.categories, removedCategory),
    }));
  },
}));

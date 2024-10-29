import { create } from 'zustand';
import { deleteElementWithId, getItemById, setElementWithId } from 'shared/helpers';
import {
  DishCategoryId,
  DishesAction,
  DishesState,
  DishId,
  IDish,
  IDishCategory,
  IDishCategoryData,
  IDishCategoryState,
  IDishData,
  IDishState,
} from '../lib';
import {
  addDish,
  addDishCategory,
  editDish,
  editDishCategory,
  loadDishCategories,
  loadDishCategoryImage,
  loadDishes,
  loadDishImage,
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
    const result = await loadDishes();
    set(() => ({
      dishesLoaded: true,
      dishesLoading: false,
      dishes: result.items.map((item) => ({ ...item, loading: false, url: null, data: null })),
    }));
  },
  loadDishImage: async (id: DishId) => {
    const { dishesLoaded, dishesLoading, dishes } = get();
    if (!dishesLoaded || dishesLoading) {
      return;
    }
    const currentPhoto = dishes.find((dish) => dish.id === id);
    if (currentPhoto && (currentPhoto.loading || currentPhoto.previewImage != null)) {
      return;
    }
    set((state) => ({
      dishes: state.dishes.map((dish) => {
        if (dish.id !== id) {
          return dish;
        }
        return {
          ...dish,
          loading: true,
        };
      }),
    }));
    let data = null;
    let url = null;
    let previewImage = '';
    try {
      data = await loadDishImage(id);
      url = URL.createObjectURL(data);
      previewImage = await data.text();
    } catch (error) {
      console.error(error);
    }
    set((state) => ({
      dishes: state.dishes.map((dish) => {
        if (dish.id !== id) {
          return dish;
        }
        return {
          ...dish,
          loading: false,
          previewImage,
          url,
          data,
        };
      }),
    }));
  },
  addDish: async (dishData: IDishData) => {
    if (!get().dishesLoaded) {
      await get().loadDish();
    }
    const { dishesLoading } = get();
    if (dishesLoading) {
      return;
    }
    set(() => ({ dishesLoading: true }));
    const addedDish = await addDish(dishData);
    set((state) => ({
      dishesLoading: false,
      dishes: [...state.dishes, { ...addedDish, loading: false, data: null, url: null }],
    }));
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
      dishes: deleteElementWithId(state.dishes, removedDish as unknown as IDishState),
    }));
  },

  loadCategories: async () => {
    const { categoriesLoaded, categoriesLoading } = get();
    if (categoriesLoaded || categoriesLoading) {
      return;
    }
    set(() => ({ categoriesLoaded: false, categoriesLoading: true }));
    const result = await loadDishCategories();
    set(() => ({
      categoriesLoaded: true,
      categoriesLoading: false,
      categories: result.items.map((item) => ({ ...item, loading: false, url: null, data: null })),
    }));
  },
  loadCategoryImage: async (id: DishCategoryId) => {
    const { categoriesLoaded, categoriesLoading, categories } = get();
    if (!categoriesLoaded || categoriesLoading) {
      return;
    }
    const currentCategory = categories.find((category) => category.id === id);
    if (currentCategory && (currentCategory.loading || currentCategory.previewImage != null)) {
      return;
    }
    set((state) => ({
      categories: state.categories.map((category) => {
        if (category.id !== id) {
          return category;
        }
        return {
          ...category,
          loading: true,
        };
      }),
    }));
    let data = null;
    let url = null;
    let previewImage = '';
    try {
      data = await loadDishCategoryImage(id);
      url = URL.createObjectURL(data);
      previewImage = await data.text();
    } catch (error) {
      console.error(error);
    }
    set((state) => ({
      categories: state.categories.map((category) => {
        if (category.id !== id) {
          return category;
        }
        return {
          ...category,
          loading: false,
          previewImage,
          url,
          data,
        };
      }),
    }));
  },
  addCategory: async (dishCategoryData: IDishCategoryData) => {
    if (!get().categoriesLoaded) {
      await get().loadCategories();
    }
    const { categoriesLoading } = get();
    if (categoriesLoading) {
      return;
    }
    set(() => ({ categoriesLoading: true }));
    const addedCategory = await addDishCategory(dishCategoryData);
    set((state) => ({
      categoriesLoading: false,
      categories: [
        ...state.categories,
        { ...addedCategory, loading: false, data: null, url: null },
      ],
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
      categories: deleteElementWithId(
        state.categories,
        removedCategory as unknown as IDishCategoryState
      ),
    }));
  },
}));

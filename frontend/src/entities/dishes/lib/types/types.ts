import { IImageItem } from 'shared/types';

export type DishCategoryId = number;
export type DishId = number;

export interface IDishCategoryData {
  name: string;
  description: string;
  previewImage: string;
}

export interface IDishCategory extends IDishCategoryData {
  id: DishCategoryId;
}

export interface IDishCategoryState extends IDishCategory, IImageItem {}

export interface IDishData {
  name: string;
  description: string;
  previewImage: string;
  price: number;
  categoryId: DishCategoryId;
  maxCount?: number;
}

export interface IDish extends IDishData {
  id: DishId;
}

export interface IDishState extends IDish, IImageItem {}

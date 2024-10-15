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

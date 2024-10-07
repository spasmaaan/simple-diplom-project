export type DishCategoryId = number;
export type DishId = number;

export interface IDishCategory {
  id: DishCategoryId;
  name: string;
  description: string;
  previewImage: string;
}

export interface IDish {
  id: DishId;
  name: string;
  description: string;
  previewImage: string;
  price: number;
  categoryId: DishCategoryId;
}

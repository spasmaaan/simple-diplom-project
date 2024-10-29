import { DishCategoryId, IDishCategoryState } from 'entities/dishes';

export interface IDishCategoriesPanelProps {
  className?: string;
  dishCategories: IDishCategoryState[];
  onAdd: () => void;
  onEdit: (id: DishCategoryId) => void;
  onRemove: (id: DishCategoryId) => void;
}

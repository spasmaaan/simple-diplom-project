import { DishCategoryId, IDishCategory } from 'entities/dishes';

export interface IDishCategoriesPanelProps {
  className?: string;
  dishCategories: IDishCategory[];
  onAdd: () => void;
  onEdit: (id: DishCategoryId) => void;
  onRemove: (id: DishCategoryId) => void;
}

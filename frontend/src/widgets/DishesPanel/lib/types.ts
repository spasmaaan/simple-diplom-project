import { DishId, IDish, IDishCategory } from 'entities/dishes';

export interface IDishesPanelProps {
  className?: string;
  category?: IDishCategory;
  dishes: IDish[];
  showCounts: boolean;
  counts: Record<DishId, number>;
  pricePostfix?: string;
  onAdd: () => void;
  onEdit: (id: DishId) => void;
  onRemove: (id: DishId) => void;
  onChangeCount: (id: DishId, count: number) => void;
}

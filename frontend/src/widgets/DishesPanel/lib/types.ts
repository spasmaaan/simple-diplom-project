import { DishId, IDishCategory, IDishState } from 'entities/dishes';

export interface IDishesPanelProps {
  className?: string;
  category?: IDishCategory;
  dishes: IDishState[];
  showCounts: boolean;
  counts: Record<DishId, number>;
  pricePostfix?: string;
  onAdd: () => void;
  onEdit: (id: DishId) => void;
  onRemove: (id: DishId) => void;
  onChangeCount: (id: DishId, count: number) => void;
}

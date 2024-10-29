import { ServiceId, IServiceState } from 'entities/services';

export interface IServicesPanelProps {
  className?: string;
  services: IServiceState[];
  showCounts: boolean;
  counts: Record<ServiceId, number>;
  pricePostfix?: string;
  onAdd: () => void;
  onEdit: (id: ServiceId) => void;
  onRemove: (id: ServiceId) => void;
  onChangeCount: (id: ServiceId, count: number) => void;
}

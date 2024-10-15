import { FaqId, IFaq } from 'entities/faqs';

export interface IFaqsPanelProps {
  className?: string;
  faqs: IFaq[];
  onAdd: () => void;
  onEdit: (id: FaqId) => void;
  onRemove: (id: FaqId) => void;
}

import { IDishCategory, IDishData } from 'entities/dishes/lib';

export interface IDishDialogProps {
  className?: string;
  open: boolean;
  defaults: IDishData | undefined;
  categories: IDishCategory[];
  title: string;
  okText: string;
  onOk: (data: IDishData) => void;
  onClose: () => void;
}

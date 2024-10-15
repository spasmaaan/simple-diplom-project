import { IDishCategoryData } from 'entities/dishes/lib';

export interface IDishCategoryDialogProps {
  className?: string;
  open: boolean;
  defaults: IDishCategoryData | undefined;
  title: string;
  okText: string;
  onOk: (data: IDishCategoryData) => void;
  onClose: () => void;
}

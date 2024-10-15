import { IFaqData } from 'entities/faqs/lib';

export interface IFaqDialogProps {
  className?: string;
  open: boolean;
  defaults: IFaqData | undefined;
  title: string;
  okText: string;
  onOk: (data: IFaqData) => void;
  onClose: () => void;
}

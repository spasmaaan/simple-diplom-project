import { IReviewData } from 'entities/reviews';

export interface IReviewDialogProps {
  className?: string;
  open: boolean;
  defaults: IReviewData | undefined;
  title: string;
  okText: string;
  onOk: (data: IReviewData) => void;
  onClose: () => void;
}

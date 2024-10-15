import { IServiceData } from 'entities/services';

export interface IServiceDialogProps {
  className?: string;
  open: boolean;
  defaults: IServiceData | undefined;
  title: string;
  okText: string;
  onOk: (data: IServiceData) => void;
  onClose: () => void;
}

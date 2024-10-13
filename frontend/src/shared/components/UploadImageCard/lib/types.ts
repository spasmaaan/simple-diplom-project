import { GetProp, UploadProps } from 'antd';

export interface IUploadImageCardProps {
  className?: string;
  text: string;
  previewLoadedImage?: string;
  onImageStartLoading?: () => void;
  onImageEndLoading?: (imageBase64: string) => void;
}

export type FileType = Parameters<GetProp<UploadProps, 'beforeUpload'>>[0];

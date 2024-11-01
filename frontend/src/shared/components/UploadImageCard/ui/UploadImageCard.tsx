import { LoadingOutlined, PlusOutlined } from '@ant-design/icons';
import { Upload, UploadProps } from 'antd';
import { useState } from 'react';
import { AddItemContent } from 'shared/components/AddItemContent';
import { beforeUpload, FileType, getBase64, IUploadImageCardProps } from '../lib';

import * as styles from './UploadImageCard.module.scss';

export const UploadImageCard = ({
  className,
  text,
  previewLoadedImage,
  onImageEndLoading,
  onImageStartLoading,
}: IUploadImageCardProps) => {
  const [imageUploading, setImageUploading] = useState(false);

  const handleChange: UploadProps['onChange'] = (info) => {
    setImageUploading(true);
    onImageStartLoading?.();

    getBase64(info.file.originFileObj as FileType, (base64Url) => {
      setImageUploading(false);
      onImageEndLoading?.(base64Url);
    });
  };

  return (
    <Upload
      name="upload-image"
      listType="picture-card"
      className={className}
      showUploadList={false}
      beforeUpload={beforeUpload}
      onChange={handleChange}
      customRequest={() => {}}
    >
      {previewLoadedImage && previewLoadedImage.length > 0 ? (
        <img src={previewLoadedImage} alt="Upload" className={styles.PreviewImage} />
      ) : (
        <AddItemContent
          text={text}
          icon={imageUploading ? <LoadingOutlined /> : <PlusOutlined />}
        />
      )}
    </Upload>
  );
};

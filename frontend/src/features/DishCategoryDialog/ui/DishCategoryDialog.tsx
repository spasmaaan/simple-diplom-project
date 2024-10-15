import { Input, Modal, Space } from 'antd';
import { ChangeEvent, useCallback, useEffect, useState } from 'react';
import { UploadImageCard } from 'shared/components/UploadImageCard';
import { IDishCategoryDialogProps, DialogWidth } from '../lib';

import * as styles from './DishCategoryDialog.module.scss';

export const DishCategoryDialog = ({
  className,
  open,
  defaults,
  title,
  okText,
  onClose,
  onOk,
}: IDishCategoryDialogProps) => {
  const [name, setName] = useState<string>('');
  const [description, setDescription] = useState<string>('');
  const [previewImage, setPreviewImage] = useState<string>('');

  const onNameChanged = useCallback((event?: ChangeEvent<HTMLInputElement>) => {
    setName(event?.target.value || '');
  }, []);
  const onDescriptionChanged = useCallback((event?: ChangeEvent<HTMLInputElement>) => {
    setDescription(event?.target.value || '');
  }, []);
  const onPreviewImage = useCallback((imageBase64: string) => {
    setPreviewImage(imageBase64);
  }, []);

  const closeDialog = useCallback(() => {
    setName('');
    setDescription('');
    setPreviewImage('');

    onClose();
  }, [onClose]);

  const handleOk = useCallback(() => {
    onOk({
      name,
      description,
      previewImage,
    });
  }, [description, name, previewImage, onOk]);

  useEffect(() => {
    setName(defaults?.name || '');
    setDescription(defaults?.description || '');
    setPreviewImage(defaults?.previewImage || '');
  }, [defaults]);

  return (
    <Modal
      className={className}
      open={open}
      title={title}
      okText={okText}
      cancelText="Отмена"
      width={DialogWidth}
      onOk={handleOk}
      onCancel={closeDialog}
      onClose={closeDialog}
    >
      <Space className={styles.Content} direction="vertical" size="middle">
        <Input placeholder="Наименование" value={name} onChange={onNameChanged} />
        <Input placeholder="Описание" value={description} onChange={onDescriptionChanged} />
        <UploadImageCard
          text="Изображение"
          previewLoadedImage={previewImage}
          onImageEndLoading={onPreviewImage}
        />
      </Space>
    </Modal>
  );
};

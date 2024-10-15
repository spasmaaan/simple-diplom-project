import { Input, InputNumber, Modal, Space } from 'antd';
import { ChangeEvent, useCallback, useEffect, useState } from 'react';
import { UploadImageCard } from 'shared/components/UploadImageCard';
import { IServiceDialogProps, DialogWidth } from '../lib';

import * as styles from './ServiceDialog.module.scss';

export const ServiceDialog = ({
  className,
  open,
  defaults,
  title,
  okText,
  onClose,
  onOk,
}: IServiceDialogProps) => {
  const [name, setName] = useState<string>('');
  const [description, setDescription] = useState<string>('');
  const [previewImage, setPreviewImage] = useState<string>('');
  const [price, setPrice] = useState<number>(0);
  const [maxCount, setMaxCount] = useState<number | undefined>();

  const onNameChanged = useCallback((event?: ChangeEvent<HTMLInputElement>) => {
    setName(event?.target.value || '');
  }, []);
  const onDescriptionChanged = useCallback((event?: ChangeEvent<HTMLInputElement>) => {
    setDescription(event?.target.value || '');
  }, []);
  const onPreviewImage = useCallback((imageBase64: string) => {
    setPreviewImage(imageBase64);
  }, []);
  const onPriceChanged = useCallback((value: number | null) => {
    setPrice(value || 0);
  }, []);
  const onMaxCountChanged = useCallback((value: number | null) => {
    let currentValue: number | undefined = value || 0;
    if (!currentValue || currentValue <= 0) {
      currentValue = undefined;
    }
    setMaxCount(currentValue);
  }, []);

  const closeDialog = useCallback(() => {
    setName('');
    setDescription('');
    setPreviewImage('');
    setPrice(0);
    setMaxCount(undefined);

    onClose();
  }, [onClose]);

  const handleOk = useCallback(() => {
    onOk({
      name,
      description,
      previewImage,
      price,
      maxCount,
    });
  }, [description, maxCount, name, previewImage, price, onOk]);

  useEffect(() => {
    setName(defaults?.name || '');
    setDescription(defaults?.description || '');
    setPreviewImage(defaults?.previewImage || '');
    setPrice(defaults?.price || 0);
    setMaxCount(defaults?.maxCount);
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
        <InputNumber
          className={styles.InputNumber}
          placeholder="Цена"
          value={price}
          min={0}
          onChange={onPriceChanged}
        />
        <InputNumber
          className={styles.InputNumber}
          placeholder="Максимальное количество"
          value={maxCount}
          min={0}
          onChange={onMaxCountChanged}
        />
      </Space>
    </Modal>
  );
};

import { Image, Flex, MenuProps, Dropdown } from 'antd';
import { PhotoId } from 'entities/photos';
import { useAuthStore } from 'entities/auth';
import { DeleteOutlined, EditOutlined } from '@ant-design/icons';
import React, { useCallback } from 'react';
import { AddItemCard } from 'shared/components/AddItemCard';
import cn from 'classnames';
import { IPhotosPanelProps } from '../lib';

import * as styles from './PhotosPanel.module.scss';

export const PhotosPanel = ({
  className,
  photos,
  photoWidth,
  onAdd,
  onEdit,
  onRemove,
}: IPhotosPanelProps) => {
  const { isAdmin } = useAuthStore();

  const renderAdminImage = useCallback(
    (id: PhotoId, photoItem: React.ReactElement) => {
      const items: MenuProps['items'] = [
        {
          key: '1',
          icon: <EditOutlined />,
          label: 'Изменить',
          onClick: () => onEdit(id),
        },
        {
          key: '2',
          icon: <DeleteOutlined />,
          label: 'Удалить',
          onClick: () => onRemove(id),
        },
      ];
      return (
        <Dropdown menu={{ items }} placement="bottom" arrow>
          {photoItem}
        </Dropdown>
      );
    },
    [onEdit, onRemove]
  );

  return (
    <Flex className={cn(styles.Photos, className)} wrap>
      {isAdmin && (
        <AddItemCard
          className={styles.Item}
          text="Изображение"
          width={photoWidth}
          onClick={onAdd}
        />
      )}
      <Image.PreviewGroup items={photos.map(({ image }) => image)}>
        {photos.map(({ id, image }) => {
          const imageRender = <Image className={styles.Item} width={photoWidth} src={image} />;
          return <div key={id}> {isAdmin ? renderAdminImage(id, imageRender) : imageRender}</div>;
        })}
      </Image.PreviewGroup>
    </Flex>
  );
};

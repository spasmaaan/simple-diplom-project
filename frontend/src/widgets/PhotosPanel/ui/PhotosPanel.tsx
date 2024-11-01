import { Image, Flex, MenuProps, Dropdown, Spin } from 'antd';
import { PhotoId } from 'entities/photos';
import { useAuthStore } from 'entities/auth';
import { DeleteOutlined, EditOutlined } from '@ant-design/icons';
import React, { useCallback, useMemo } from 'react';
import { AddItemCard } from 'shared/components/AddItemCard';
import cn from 'classnames';
import { UserRole } from 'entities/auth/lib/constants';
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
  const { userInfo } = useAuthStore();
  const isAdmin = useMemo(() => Boolean(userInfo?.roles.includes(UserRole.Admin)), [userInfo]);
  const photosList = useMemo(
    () => photos.map((photo) => photo.url).filter((url) => url != null) as string[],
    [photos]
  );

  const renderAdminImage = useCallback(
    (id: PhotoId, photoItem: React.ReactElement | null) => {
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
  const imagesElements = useMemo(
    () =>
      photos.map(({ id, loading, url }) => {
        if (loading) {
          return <Spin key={id} style={{ height: photoWidth, width: photoWidth }} />;
        }
        const imageRender = loading ? null : (
          <Image className={styles.Item} width={photoWidth} src={url || ''} />
        );
        const image = isAdmin ? renderAdminImage(id, imageRender) : imageRender;
        return <div key={id}>{image}</div>;
      }),
    [isAdmin, photoWidth, photos, renderAdminImage]
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
      <Image.PreviewGroup items={photosList}>{imagesElements}</Image.PreviewGroup>
    </Flex>
  );
};

import { Typography, Flex, Card, Space, Button } from 'antd';
import { useAuthStore } from 'entities/auth';
import { DeleteOutlined, EditOutlined } from '@ant-design/icons';
import React, { useCallback, useState } from 'react';
import { AddItemCard } from 'shared/components/AddItemCard';
import cn from 'classnames';
import { useNavigate } from 'react-router-dom';
import { DishCategoryId } from 'entities/dishes';
import { IDishCategoriesPanelProps, ItemCartWidth, ItemImageHeight } from '../lib';

import * as styles from './DishCategoriesPanel.module.scss';

const { Title, Paragraph } = Typography;

export const DishCategoriesPanel = ({
  className,
  dishCategories,
  onAdd,
  onEdit,
  onRemove,
}: IDishCategoriesPanelProps) => {
  const { isAdmin } = useAuthStore();
  const navigateTo = useNavigate();

  const toDishes = useCallback(
    (dishCategory: DishCategoryId) => {
      navigateTo(`../dishes/${dishCategory}`);
    },
    [navigateTo]
  );
  const onEditCategory = useCallback(
    (event: React.MouseEvent<HTMLElement>, dishCategoryId: DishCategoryId) => {
      event.stopPropagation();
      onEdit(dishCategoryId);
    },
    [onEdit]
  );
  const onRemoveCategory = useCallback(
    (event: React.MouseEvent<HTMLElement>, dishCategoryId: DishCategoryId) => {
      event.stopPropagation();
      onRemove(dishCategoryId);
    },
    [onRemove]
  );

  return (
    <Flex className={styles.Container} justify="center">
      <Flex className={cn(styles.DishCategories, className)} wrap>
        {isAdmin && (
          <AddItemCard
            text="Категория блюд"
            height={ItemImageHeight}
            width={ItemCartWidth}
            onClick={onAdd}
          />
        )}
        {dishCategories.map((dishCategory) => (
          <Card
            key={dishCategory.id}
            style={{
              width: ItemCartWidth,
              height: ItemImageHeight,
              backgroundImage: `linear-gradient(to bottom, rgba(255,255,255,.2), rgba(255,255,255,.8)), url(${dishCategory.previewImage})`,
            }}
            className={styles.Item}
            classNames={{
              body: styles.ItemBody,
            }}
            onClick={() => toDishes(dishCategory.id)}
          >
            <Flex vertical justify="space-between" className={styles.Container}>
              <div className={styles.Title}>
                <Title level={2}>{dishCategory.name}</Title>
              </div>
              <Paragraph strong className={styles.Description}>
                {dishCategory.description}
              </Paragraph>
              {isAdmin && (
                <Space direction="horizontal">
                  <Button
                    icon={<EditOutlined />}
                    onClick={(event) => onEditCategory(event, dishCategory.id)}
                  />
                  <Button
                    icon={<DeleteOutlined />}
                    onClick={(event) => onRemoveCategory(event, dishCategory.id)}
                  />
                </Space>
              )}
            </Flex>
          </Card>
        ))}
      </Flex>
    </Flex>
  );
};

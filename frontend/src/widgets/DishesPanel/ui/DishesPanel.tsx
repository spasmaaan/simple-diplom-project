import { Typography, Flex, Card, Space, Button, Statistic, InputNumber, Spin } from 'antd';
import { useAuthStore } from 'entities/auth';
import { DeleteOutlined, EditOutlined } from '@ant-design/icons';
import { useCallback, useMemo } from 'react';
import { AddItemCard } from 'shared/components/AddItemCard';
import cn from 'classnames';
import { useNavigate } from 'react-router-dom';
import { UserRole } from 'entities/auth/lib/constants';
import { IDishesPanelProps, ItemCartWidth, ItemImageHeight, MinItemsCount } from '../lib';

import * as styles from './DishesPanel.module.scss';

const { Title, Paragraph } = Typography;

export const DishesPanel = ({
  className,
  category,
  dishes,
  counts,
  showCounts,
  pricePostfix,
  onAdd,
  onEdit,
  onRemove,
  onChangeCount,
}: IDishesPanelProps) => {
  const { userInfo } = useAuthStore();
  const navigateTo = useNavigate();

  const isAdmin = useMemo(() => Boolean(userInfo?.roles.includes(UserRole.Admin)), [userInfo]);

  const toDishCategories = useCallback(() => {
    navigateTo('../cuisine');
  }, [navigateTo]);

  return (
    <Flex vertical>
      <Flex className={styles.TopControls} justify="space-between">
        <Button className={styles.Back} onClick={toDishCategories}>
          {'< Назад'}
        </Button>
        <Title className={styles.Category} level={2}>
          {category?.name}
        </Title>
      </Flex>
      <Flex className={cn(styles.Dishes, className)} wrap>
        {isAdmin && (
          <AddItemCard className={styles.Item} text="Блюдо" width={ItemCartWidth} onClick={onAdd} />
        )}
        {dishes.map((dish) => {
          const dishCount = counts[dish.id] || 0;
          const serviceMaxCount = dish.maxCount;
          return (
            <Card
              key={dish.id}
              className={styles.Card}
              classNames={{ body: styles.CardBody }}
              style={{ width: ItemCartWidth }}
            >
              <Flex justify="space-between" vertical className={styles.CardContent}>
                <Space className={styles.Item} direction="vertical">
                  {dish.loading || !dish.url ? (
                    <Spin style={{ width: ItemCartWidth, height: ItemCartWidth }} />
                  ) : (
                    <img src={dish.url} alt="Dish" width={ItemImageHeight} />
                  )}
                  {isAdmin && (
                    <Space
                      className={styles.Controls}
                      direction="horizontal"
                      size="small"
                      align="end"
                    >
                      <Button icon={<EditOutlined />} onClick={() => onEdit(dish.id)} />
                      <Button icon={<DeleteOutlined />} onClick={() => onRemove(dish.id)} />
                    </Space>
                  )}
                  <Paragraph strong>{dish.name}</Paragraph>
                  <Paragraph>{dish.description}</Paragraph>
                </Space>
                <Flex justify="space-between">
                  <Statistic title="Цена" value={`${dish.price}${pricePostfix || ''}`} />
                  {showCounts && (
                    <Space className={styles.PriceControls} direction="horizontal" size="small">
                      <InputNumber
                        value={dishCount}
                        min={MinItemsCount}
                        max={serviceMaxCount}
                        onChange={(currentCount) => onChangeCount(dish.id, currentCount || 0)}
                      />
                    </Space>
                  )}
                </Flex>
              </Flex>
            </Card>
          );
        })}
      </Flex>
    </Flex>
  );
};

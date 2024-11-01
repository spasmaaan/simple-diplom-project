import { Typography, Flex, Card, Space, Statistic, Button, InputNumber, Spin } from 'antd';
import { useAuthStore } from 'entities/auth';
import { DeleteOutlined, EditOutlined, MinusOutlined, PlusOutlined } from '@ant-design/icons';
import React, { useCallback, useMemo, useState } from 'react';
import { AddItemCard } from 'shared/components/AddItemCard';
import cn from 'classnames';
import { UserRole } from 'entities/auth/lib/constants';
import { IServicesPanelProps, ItemCartWidth, ItemImageHeight, MinItemsCount } from '../lib';

import * as styles from './ServicesPanel.module.scss';

const { Paragraph } = Typography;

export const ServicesPanel = ({
  className,
  services,
  showCounts,
  counts,
  pricePostfix,
  onAdd,
  onEdit,
  onRemove,
  onChangeCount,
}: IServicesPanelProps) => {
  const { userInfo } = useAuthStore();
  const isAdmin = useMemo(() => Boolean(userInfo?.roles.includes(UserRole.Admin)), [userInfo]);

  return (
    <Flex className={cn(styles.Services, className)} wrap>
      {isAdmin && (
        <AddItemCard className={styles.Item} text="Сервис" width={ItemCartWidth} onClick={onAdd} />
      )}
      {services.map((service) => {
        const serviceCount = counts[service.id] || 0;
        const serviceMaxCount = service.maxCount;
        return (
          <Card
            key={service.id}
            className={styles.Card}
            classNames={{ body: styles.CardBody }}
            style={{ width: ItemCartWidth }}
          >
            <Flex justify="space-between" vertical className={styles.CardContent}>
              <Space className={styles.Item} direction="vertical">
                {service.loading || !service.url ? (
                  <Spin style={{ width: ItemCartWidth, height: ItemCartWidth }} />
                ) : (
                  <img src={service.url} alt="Service" width={ItemImageHeight} />
                )}
                {isAdmin && (
                  <Space
                    className={styles.Controls}
                    direction="horizontal"
                    size="small"
                    align="end"
                  >
                    <Button icon={<EditOutlined />} onClick={() => onEdit(service.id)} />
                    <Button icon={<DeleteOutlined />} onClick={() => onRemove(service.id)} />
                  </Space>
                )}
                <Paragraph strong>{service.name}</Paragraph>
                <Paragraph>{service.description}</Paragraph>
              </Space>
              <Flex justify="space-between">
                <Statistic title="Цена" value={`${service.price}${pricePostfix || ''}`} />
                {showCounts && (
                  <Space className={styles.PriceControls} direction="horizontal" size="small">
                    <InputNumber
                      value={serviceCount}
                      min={MinItemsCount}
                      max={serviceMaxCount}
                      onChange={(currentCount) => onChangeCount(service.id, currentCount || 0)}
                    />
                  </Space>
                )}
              </Flex>
            </Flex>
          </Card>
        );
      })}
    </Flex>
  );
};

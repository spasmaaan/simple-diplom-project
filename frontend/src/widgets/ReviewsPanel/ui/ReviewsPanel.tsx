import { Typography, Flex, Card, Space, Avatar, Rate, Button } from 'antd';
import { useAuthStore } from 'entities/auth';
import { DeleteOutlined, EditOutlined } from '@ant-design/icons';
import { useMemo } from 'react';
import cn from 'classnames';
import { UserRole } from 'entities/auth/lib/constants';
import { IReviewsPanelProps } from '../lib';

import * as styles from './ReviewsPanel.module.scss';

const { Paragraph, Text } = Typography;

export const ReviewsPanel = ({
  className,
  reviews,
  onAdd,
  onEdit,
  onRemove,
}: IReviewsPanelProps) => {
  const { userInfo } = useAuthStore();
  const isAdmin = useMemo(() => Boolean(userInfo?.roles.includes(UserRole.Admin)), [userInfo]);

  return (
    <Flex className={cn(styles.Container, className)} justify="center">
      <Flex className={styles.Reviews} vertical>
        <Flex className={cn(styles.CreateReview)} vertical>
          <Button type="primary" onClick={onAdd}>
            Оставить отзыв
          </Button>
        </Flex>
        {reviews.map((review) => (
          <Card key={review.id}>
            <Space direction="vertical" className={styles.Content}>
              <Flex justify="space-between" align="center">
                <Avatar>{review.shortName}</Avatar>
                <Flex vertical className={styles.Info}>
                  <Text strong>{review.fullName}</Text>
                  <Text>{new Date(review.creationDate).toLocaleDateString()}</Text>
                </Flex>
                <Space direction="horizontal">
                  {review.me && (
                    <Button icon={<EditOutlined />} onClick={() => onEdit(review.id)} />
                  )}
                  {(isAdmin || review.me) && (
                    <Button icon={<DeleteOutlined />} onClick={() => onRemove(review.id)} />
                  )}
                </Space>
              </Flex>
              <Rate value={review.rating} disabled />
              <Paragraph>{review.message}</Paragraph>
            </Space>
          </Card>
        ))}
      </Flex>
    </Flex>
  );
};

import { Typography, Flex, Space, CollapseProps, Collapse, Button } from 'antd';
import { useAuthStore } from 'entities/auth';
import { DeleteOutlined, EditOutlined, PlusOutlined } from '@ant-design/icons';
import { useCallback, useMemo } from 'react';
import cn from 'classnames';
import { IFaq } from 'entities/faqs';
import { UserRole } from 'entities/auth/lib/constants';
import { IFaqsPanelProps } from '../lib';

import * as styles from './FaqsPanel.module.scss';

const { Text } = Typography;

export const FaqsPanel = ({ className, faqs, onAdd, onEdit, onRemove }: IFaqsPanelProps) => {
  const { userInfo } = useAuthStore();
  const isAdmin = useMemo(() => Boolean(userInfo?.roles.includes(UserRole.Admin)), [userInfo]);

  const renderLabel = useCallback(
    (faq: IFaq) => {
      if (!isAdmin) {
        return <Text strong>{faq.question}</Text>;
      }
      return (
        <Flex justify="space-between">
          <Text strong>{faq.question}</Text>
          <Space className={styles.ItemControls} direction="horizontal" size="small">
            <Button icon={<EditOutlined />} onClick={() => onEdit(faq.id)} />
            <Button icon={<DeleteOutlined />} onClick={() => onRemove(faq.id)} />
          </Space>
        </Flex>
      );
    },
    [isAdmin, onEdit, onRemove]
  );

  const items: CollapseProps['items'] = useMemo(
    () =>
      faqs.map((faq) => ({
        key: faq.id,
        label: renderLabel(faq),
        children: <Text>{faq.answer}</Text>,
      })),
    [faqs, renderLabel]
  );

  return (
    <Flex className={cn(styles.Faqs, className)} vertical>
      {isAdmin && (
        <Flex className={styles.Controls} justify="flex-end">
          <Button icon={<PlusOutlined />} onClick={onAdd} />
        </Flex>
      )}
      <Collapse accordion items={items} />
    </Flex>
  );
};

import cn from 'classnames';
import { Button, Card, Flex, Modal, Select, Table, TableProps, Tag, Typography } from 'antd';
import { Space } from 'antd/lib';
import { UserRole } from 'entities/auth/lib/constants';
import { DeleteOutlined, EditOutlined, LockFilled, LockOutlined } from '@ant-design/icons';
import { useCallback, useState } from 'react';
import { IAdminPageProps } from '../lib';

import * as styles from './AdminPage.module.scss';

const { Text, Paragraph } = Typography;

interface DataType {
  key: string;
  name: string;
  email: string;
  role: string;
  locked: boolean;
}

const colors: Record<UserRole, string> = {
  [UserRole.Admin]: 'volcano',
  [UserRole.Manager]: 'green',
  [UserRole.Client]: 'geekblue',
};

export const AdminPage = ({ className }: IAdminPageProps) => {
  const [open, setOpen] = useState(false);
  const [modal, contextHolder] = Modal.useModal();

  const onDeleteUser = useCallback(
    (user: DataType) => {
      void modal.confirm({
        title: 'Удаление пользователя',
        content: (
          <Paragraph>
            Вы точно уверены, что хотите удалить пользователя <Text strong>{user.name}</Text>?
          </Paragraph>
        ),
        okText: 'Удалить',
        cancelText: 'Отмена',
        onOk: () => {
          // Забронировать.
        },
      });
    },
    [modal]
  );

  const columns: TableProps<DataType>['columns'] = [
    {
      title: 'Фамилия Имя',
      dataIndex: 'name',
      key: 'name',
      render: (text) => <Text strong>{text}</Text>,
    },
    {
      title: 'Электронная почта',
      dataIndex: 'email',
      key: 'email',
    },
    {
      title: 'Роль',
      key: 'role',
      dataIndex: 'role',
      render: (_, { role }) => (
        <Tag color={colors[role as UserRole]} key={role}>
          {role.toUpperCase()}
        </Tag>
      ),
    },
    {
      title: <Flex justify="flex-end">Действие</Flex>,
      key: 'action',
      render: (_, record) => (
        <Flex justify="flex-end">
          <Space size="small">
            <Button icon={record.locked ? <LockOutlined /> : <LockFilled />} />
            <Button icon={<EditOutlined />} onClick={() => setOpen(true)} />
            <Button icon={<DeleteOutlined />} onClick={() => onDeleteUser(record)} />
          </Space>
        </Flex>
      ),
    },
  ];

  const data: DataType[] = [
    {
      key: '1',
      name: 'Админ Главный',
      email: 'admin@admin.ru',
      role: UserRole.Admin,
      locked: false,
    },
    {
      key: '2',
      name: 'Тестовый Пользователь',
      email: 't1@t.test',
      role: UserRole.Client,
      locked: false,
    },
    {
      key: '3',
      name: 'Второй Тестовый',
      email: 't2@t.test',
      role: UserRole.Client,
      locked: true,
    },
    {
      key: '4',
      name: 'Сотрудник Пользователь',
      email: 'm@t.test',
      role: UserRole.Manager,
      locked: false,
    },
  ];

  return (
    <div className={cn(className, styles.AdminPage)}>
      <Card classNames={{ body: styles.CardBody }}>
        {contextHolder}
        <Modal
          open={open}
          title="Редактирование пользователя"
          okText="Сохранить"
          cancelText="Отмена"
          onClose={() => setOpen(false)}
          width={400}
        >
          <Space direction="vertical" size="middle" style={{ width: '100%' }}>
            <Paragraph>
              Редактирование пользователя <Text strong>Второй Тестовый</Text>
            </Paragraph>
            <Flex justify="center">
              <Select
                style={{ marginBottom: '2rem', width: 200 }}
                defaultValue={UserRole.Client}
                options={[
                  {
                    value: UserRole.Admin,
                    label: 'ADMIN',
                  },
                  {
                    value: UserRole.Manager,
                    label: 'MANAGER',
                  },
                  {
                    value: UserRole.Client,
                    label: 'CLIENT',
                  },
                ]}
              />
            </Flex>
          </Space>
        </Modal>
        <Table<DataType> columns={columns} dataSource={data} pagination={false} />
      </Card>
    </div>
  );
};

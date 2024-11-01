import cn from 'classnames';
import { Avatar, Button, Card, Flex, Input, Space, Typography } from 'antd';
import { IProfilePageProps } from '../lib';

import * as styles from './ProfilePage.module.scss';

const { Title, Text } = Typography;

export const ProfilePage = ({ className }: IProfilePageProps) => {
  return (
    <Flex className={cn(className, styles.ProfilePage)} justify="center">
      <Card style={{ width: 450, margin: '2rem' }}>
        <Space size="large" direction="vertical" style={{ width: '100%' }}>
          <Flex justify="center">
            <Title type="secondary" level={3}>
              Профиль
            </Title>
          </Flex>
          <Flex justify="center">
            <Avatar
              size={72}
              shape="circle"
              style={{ background: 'rgb(65,88,208)', fontSize: '2rem', margin: '2rem' }}
            >
              АГ
            </Avatar>
          </Flex>
          <Flex justify="space-between">
            <Text type="secondary">Имя:</Text>
            <Text editable>Админ</Text>
          </Flex>
          <Flex justify="space-between">
            <Text type="secondary">Фамилия:</Text>
            <Text editable>Главный</Text>
          </Flex>
          <Flex justify="space-between">
            <Text type="secondary">Электронная почта:</Text>
            <Text editable>admin@admin.ru</Text>
          </Flex>
          <Flex justify="space-between">
            <Text type="secondary">Пароль:</Text>
            <Flex vertical style={{ gap: '0.5rem' }}>
              <Input placeholder="Введите пароль" />
              <Input placeholder="Пароль ещё раз" />
            </Flex>
          </Flex>
          <Flex justify="center" style={{ marginTop: '2rem' }}>
            <Space direction="horizontal">
              <Button>Отмена</Button>
              <Button type="primary">Сохранить</Button>
            </Space>
          </Flex>
        </Space>
      </Card>
    </Flex>
  );
};

import { Button, Input, Modal, Space, Tabs, TabsProps } from 'antd';
import { ChangeEvent, useCallback, useMemo, useState } from 'react';
import { KeyOutlined, MailOutlined, UserOutlined } from '@ant-design/icons';
import { UserId } from 'shared/types';
import { ILoginDialogProps } from '../lib';
import { LoginDialogTab, OkButtonText } from '../lib/constants';

import * as styles from './LoginDialog.module.scss';

export const LoginDialog = ({
  className,
  open,
  onLogin,
  onRegister,
  onCancel,
}: ILoginDialogProps) => {
  const [currentTab, setCurrentTab] = useState(LoginDialogTab.Login);

  const [user, setUser] = useState('');
  const [password, setPassword] = useState('');

  const [firstName, setFirstName] = useState('');
  const [lastName, setLastName] = useState('');
  const [email, setEmail] = useState('');
  const [registerPassword, setRegisterPassword] = useState('');
  const [registerPasswordRepeat, setRegisterPasswordRepeat] = useState('');

  const clearState = useCallback((partial = false) => {
    if (!partial) {
      setCurrentTab(LoginDialogTab.Login);
    }
    setUser('');
    setPassword('');

    setFirstName('');
    setLastName('');
    setEmail('');
    setRegisterPassword('');
    setRegisterPasswordRepeat('');
  }, []);

  const onChangeUser = useCallback((event?: ChangeEvent<HTMLInputElement>) => {
    setUser(event?.target.value || '');
  }, []);
  const onChangePassword = useCallback((event?: ChangeEvent<HTMLInputElement>) => {
    setPassword(event?.target.value || '');
  }, []);

  const onChangeFirstName = useCallback((event?: ChangeEvent<HTMLInputElement>) => {
    setFirstName(event?.target.value || '');
  }, []);
  const onChangeLastName = useCallback((event?: ChangeEvent<HTMLInputElement>) => {
    setLastName(event?.target.value || '');
  }, []);
  const onChangeEmail = useCallback((event?: ChangeEvent<HTMLInputElement>) => {
    setEmail(event?.target.value || '');
  }, []);
  const onChangeRegisterPassword = useCallback((event?: ChangeEvent<HTMLInputElement>) => {
    setRegisterPassword(event?.target.value || '');
  }, []);
  const onChangeRegisterPasswordRepeat = useCallback((event?: ChangeEvent<HTMLInputElement>) => {
    setRegisterPasswordRepeat(event?.target.value || '');
  }, []);

  const tabs: TabsProps['items'] = useMemo(
    () => [
      {
        key: LoginDialogTab.Login.toString(),
        label: 'Вход',
        children: (
          <div className={styles.TabBody}>
            <Input
              className={styles.TabBodyItem}
              placeholder="Логин"
              value={user}
              prefix={<UserOutlined />}
              onChange={onChangeUser}
            />
            <Input.Password
              className={styles.TabBodyItem}
              placeholder="Пароль"
              value={password}
              prefix={<KeyOutlined />}
              onChange={onChangePassword}
            />
          </div>
        ),
      },
      {
        key: LoginDialogTab.Register.toString(),
        label: 'Регистрация',
        children: (
          <div className={styles.TabBody}>
            <Input
              className={styles.TabBodyItem}
              placeholder="Электронная почта"
              value={email}
              prefix={<MailOutlined />}
              onChange={onChangeEmail}
            />
            <Input
              className={styles.TabBodyItem}
              placeholder="Фамилия"
              value={firstName}
              prefix={<UserOutlined />}
              onChange={onChangeFirstName}
            />
            <Input
              className={styles.TabBodyItem}
              placeholder="Имя"
              value={lastName}
              prefix={<UserOutlined />}
              onChange={onChangeLastName}
            />
            <Input.Password
              className={styles.TabBodyItem}
              placeholder="Пароль"
              value={registerPassword}
              prefix={<KeyOutlined />}
              onChange={onChangeRegisterPassword}
            />
            <Input.Password
              className={styles.TabBodyItem}
              placeholder="Пароль ещё раз"
              value={registerPasswordRepeat}
              prefix={<KeyOutlined />}
              onChange={onChangeRegisterPasswordRepeat}
            />
          </div>
        ),
      },
    ],
    [
      email,
      firstName,
      lastName,
      password,
      registerPassword,
      registerPasswordRepeat,
      user,
      onChangeEmail,
      onChangeFirstName,
      onChangeLastName,
      onChangePassword,
      onChangeRegisterPassword,
      onChangeRegisterPasswordRepeat,
      onChangeUser,
    ]
  );

  const handleCancel = useCallback(() => {
    onCancel?.();
    clearState();
  }, [clearState, onCancel]);

  const handleOk = useCallback(() => {
    switch (currentTab) {
      case LoginDialogTab.Register: {
        // validate.
        onRegister?.(email, firstName, lastName, password);
        break;
      }
      case LoginDialogTab.Login:
      default: {
        onLogin?.(user, password);
        break;
      }
    }
    clearState();
  }, [currentTab, email, firstName, lastName, password, user, clearState, onRegister, onLogin]);

  const onChangeTab = useCallback(
    (activeTab: string) => {
      setCurrentTab(Number(activeTab));
      clearState(true);
    },
    [clearState]
  );

  return (
    <Modal
      open={open}
      animation
      className={className}
      classNames={{
        footer: styles.DialogFooter,
      }}
      title="Аутентификация"
      onOk={handleCancel}
      onCancel={handleOk}
      width={285}
      footer={[
        <Button key="back" onClick={handleCancel}>
          Назад
        </Button>,
        <Button key="submit" type="primary" onClick={handleOk}>
          {OkButtonText[currentTab]}
        </Button>,
      ]}
    >
      <Tabs centered defaultActiveKey={currentTab.toString()} items={tabs} onChange={onChangeTab} />
    </Modal>
  );
};

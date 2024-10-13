import { Avatar, Button, Dropdown, MenuProps } from 'antd';
import { useCallback, useMemo, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { useAuthStore } from 'entities/auth';
import { LoginDialog } from 'features/LoginDialog';
import { getAvatarText, getUserFullName, IProfileButtonProps } from '../lib';

import * as styles from './ProfileButton.module.scss';

export const ProfileButton = ({ className }: IProfileButtonProps) => {
  const { user, isAdmin, authenticated, login, registerUser, logout } = useAuthStore();
  const navigateTo = useNavigate();
  const [loginDialogOpened, setLoginDialogOpened] = useState(false);

  const avatarText = useMemo(() => getAvatarText(user), [user]);

  const openLoginDialog = useCallback(() => {
    setLoginDialogOpened(true);
  }, []);

  const toProfile = useCallback(() => {
    navigateTo('/profile');
  }, [navigateTo]);

  const closeLoginDialog = useCallback(() => {
    setLoginDialogOpened(false);
  }, []);

  const startLogin = useCallback(
    (loginName: string, password: string) => {
      void login(loginName, password);
      closeLoginDialog();
    },
    [login, closeLoginDialog]
  );

  const startRegister = useCallback(
    (loginName: string, password: string, email: string) => {
      void registerUser({
        user: loginName,
        password,
        email,
      });
      closeLoginDialog();
    },
    [registerUser, closeLoginDialog]
  );

  const menuItems: MenuProps['items'] = useMemo(() => {
    if (!authenticated) {
      return [];
    }
    const additionalItems: MenuProps['items'] = [];
    if (isAdmin) {
      additionalItems.push({
        key: 'statistics',
        label: 'Статистика',
        onClick: () => navigateTo('/statistics'),
      });
    }
    return [
      {
        key: 'userName',
        label: getUserFullName(user),
        disabled: true,
      },
      {
        type: 'divider',
      },
      {
        key: 'profile',
        label: 'Профиль',
        onClick: toProfile,
      },
      ...additionalItems,
      {
        key: 'logout',
        label: 'Выйти',
        onClick: logout,
      },
    ];
  }, [authenticated, isAdmin, logout, navigateTo, toProfile, user]);

  return (
    <div className={styles.Wrapper}>
      <LoginDialog
        open={loginDialogOpened}
        onLogin={startLogin}
        onRegister={startRegister}
        onCancel={closeLoginDialog}
      />
      <div className={styles.Content}>
        {authenticated ? (
          <Dropdown menu={{ items: menuItems }}>
            <Avatar size={32} shape="square" style={{ background: 'pink' }} className={className}>
              {avatarText}
            </Avatar>
          </Dropdown>
        ) : (
          <Button type="primary" onClick={openLoginDialog}>
            Войти
          </Button>
        )}
      </div>
    </div>
  );
};

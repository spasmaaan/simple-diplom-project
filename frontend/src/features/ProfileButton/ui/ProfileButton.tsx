import { Avatar, Button, Dropdown, MenuProps } from 'antd';
import { useCallback, useMemo, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { useAuthStore } from 'entities/auth';
import { LoginDialog } from 'features/LoginDialog';
import { UserRole } from 'entities/auth/lib/constants';
import { getAvatarText, getUserFullName, IProfileButtonProps } from '../lib';

import * as styles from './ProfileButton.module.scss';

export const ProfileButton = ({ className }: IProfileButtonProps) => {
  const { userInfo, authenticated, login, registerUser, logout } = useAuthStore();
  const navigateTo = useNavigate();
  const [loginDialogOpened, setLoginDialogOpened] = useState(false);

  const isAdmin = useMemo(() => Boolean(userInfo?.roles.includes(UserRole.Admin)), [userInfo]);
  const avatarText = useMemo(() => getAvatarText(userInfo), [userInfo]);

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
    (email: string, firstName: string, lastName: string, password: string) => {
      void registerUser({
        email,
        firstName,
        lastName,
        password,
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
        label: getUserFullName(userInfo),
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
  }, [authenticated, isAdmin, userInfo, toProfile, logout, navigateTo]);

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

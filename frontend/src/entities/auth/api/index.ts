import { UserId } from 'shared/types';
import { backendGet, backendPost, backendPut } from 'shared/api';
import { AuthBackendUrl } from 'shared/config';
import {
  IAuthDataResult,
  IRefreshTokenPayload,
  IRefreshTokenResult,
  IRegisterUser,
  IRegisterUserResult,
  IUser,
  RoleId,
} from '../lib';
import { UserRole } from '../lib/constants';

const getAuthPath = (url: string = '') => `${AuthBackendUrl}${url}`;

export const loadUsers = async (): Promise<IUser[]> => {
  return (await backendGet(null, getAuthPath('/users'))).json();
};

export const loadUserInfo = async (): Promise<IUser> => {
  return (await backendGet(null, getAuthPath('/profile'))).json();
};

export const updateUserInfo = async (user: IUser): Promise<IUser> => {
  return (await backendPut(null, getAuthPath('/profile'))).json();
};

export const loadRoles = async (): Promise<Record<RoleId, UserRole>> => {
  return (await backendGet(null, getAuthPath('/roles'))).json();
};

export const login = async (email: UserId, password: string): Promise<IAuthDataResult> => {
  const result = await backendPost(null, getAuthPath('/login'), { email, password });
  return result.json();
};

export const logout = async (refreshData: IRefreshTokenPayload): Promise<void> => {
  await backendPost(null, getAuthPath('/logout'), refreshData);
};

export const registerUser = async (user: IRegisterUser): Promise<IRegisterUserResult> => {
  return (await backendPost(null, getAuthPath('/register'), user)).json();
};

export const lockUser = async (userId: UserId): Promise<void> => {
  await backendPut(null, getAuthPath(`/lock/${userId}`));
};

export const getNewToken = async (
  refreshData: IRefreshTokenPayload
): Promise<IRefreshTokenResult> => {
  return (await backendPost(null, getAuthPath('/refresh'), refreshData)).json();
};

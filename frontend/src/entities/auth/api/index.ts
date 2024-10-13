import { UserId } from 'shared/types';
import { backendGet, backendPost, backendPut } from 'shared/api';
import { AuthBackendUrl } from 'shared/config';
import { IAuthData, IRefreshTokenPayload, IRegisterUser, IUser, RoleId } from '../lib';
import { UserRole } from '../lib/constants';
import { MOCK_AUTH_DATA, MOCK_LOGIN, MOCK_ROLES, MOCK_USERS } from './mock';

const getAuthPath = (url: string = '') => `${AuthBackendUrl}${url}`;

export const loadUsers = async (): Promise<IUser[]> => {
  return MOCK_USERS; // (await backendGet(null, getAuthPath('/users'))).json();
};

export const loadRoles = async (): Promise<Record<RoleId, UserRole>> => {
  return MOCK_ROLES; // (await backendGet(null, getAuthPath('/roles'))).json();
};

export const login = async (user: UserId, password: string): Promise<IAuthData> => {
  return MOCK_AUTH_DATA; // (await backendPost(null, getAuthPath('/login'), { user, password })).json();
};

export const logout = async (): Promise<boolean> => {
  return (await backendPost(null, getAuthPath('/logout'))).json();
};

export const registerUser = async (user: IRegisterUser): Promise<boolean> => {
  return (await backendPost(null, getAuthPath('/register'), user)).json();
};

export const lockUser = async (userId: UserId): Promise<boolean> => {
  return (await backendPut(null, getAuthPath(`/lock/${userId}`))).json();
};

export const refreshToken = async (refreshData: IRefreshTokenPayload): Promise<IAuthData> => {
  return (await backendPost(null, getAuthPath('/refresh'), refreshData)).json();
};

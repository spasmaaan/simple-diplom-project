import { UserId } from 'shared/types';
import {
  IAuthData,
  IAuthErrors,
  IRefreshTokenPayload,
  IRegisterUser,
  IRegisterUserErrors,
  IUser,
  RoleId,
} from './types';
import { UserRole } from '../constants';

export type AuthState = {
  authLoading: boolean;
  authenticated: boolean;
  userInfo: IUser | null;
  userInfoLoading: boolean;
  token: IAuthData | null;
  usersLoading: boolean;
  usersLoaded: boolean;
  users: IUser[];
  rolesLoading: boolean;
  rolesLoaded: boolean;
  roles: Record<RoleId, UserRole>;
};

export type AuthAction = {
  login: (user: string, password: string) => Promise<IAuthErrors | null>;
  registerUser: (user: IRegisterUser) => Promise<IRegisterUserErrors | null>;
  loadUserInfo: () => Promise<void>;
  logout: () => Promise<void>;
  refreshToken: () => Promise<void>;
  lockUser: (userId: UserId, doLock: boolean) => Promise<void>;
  loadUsers: () => Promise<void>;
  loadRoles: () => Promise<void>;
};

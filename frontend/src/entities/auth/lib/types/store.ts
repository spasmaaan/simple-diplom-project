import { UserId } from 'shared/types';
import { IAuthData, IRefreshTokenPayload, IRegisterUser, IUser, RoleId } from './types';
import { UserRole } from '../constants';

export type AuthState = {
  authLoading: boolean;
  authenticated: boolean;
  user: IUser | null;
  token: IAuthData | null;
  usersLoading: boolean;
  usersLoaded: boolean;
  users: IUser[];
  isAdmin: boolean;
  rolesLoading: boolean;
  rolesLoaded: boolean;
  roles: Record<RoleId, UserRole>;
};

export type AuthAction = {
  login: (user: string, password: string) => Promise<void>;
  registerUser: (user: IRegisterUser) => Promise<void>;
  logout: () => Promise<void>;
  refreshToken: (authData: IRefreshTokenPayload) => Promise<void>;
  lockUser: (userId: UserId, doLock: boolean) => Promise<void>;
  loadUsers: () => Promise<void>;
  loadRoles: () => Promise<void>;
};

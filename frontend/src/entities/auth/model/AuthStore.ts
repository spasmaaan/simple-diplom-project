import { create } from 'zustand';
import { getItemById, setElementWithId } from 'shared/helpers';
import { AuthAction, AuthState, IRefreshTokenPayload, IRegisterUser, IUser, UserId } from '../lib';
import {
  loadRoles,
  loadUserInfo,
  loadUsers,
  lockUser,
  login,
  logout,
  getNewToken,
  registerUser,
} from '../api';

export const useAuthStoreBase = create<AuthState & AuthAction>()((set, get) => ({
  authLoading: false,
  authenticated: false,
  userInfo: null,
  userInfoLoading: false,
  token: null,
  usersLoading: false,
  usersLoaded: false,
  users: [],
  isAdmin: false,
  rolesLoaded: false,
  rolesLoading: false,
  roles: {},
  loadUserInfo: async () => {
    const { userInfoLoading } = get();
    if (userInfoLoading) {
      return;
    }
    set(() => ({ userInfoLoading: true }));
    const userInfo = await loadUserInfo();
    set(() => ({ userInfoLoading: false, userInfo }));
  },
  loadUsers: async () => {
    const { usersLoaded, usersLoading } = get();
    if (usersLoaded || usersLoading) {
      return;
    }
    set(() => ({ usersLoaded: false, usersLoading: true }));
    const users = await loadUsers();
    set(() => ({ usersLoaded: true, usersLoading: false, users }));
  },
  loadRoles: async () => {
    const { rolesLoaded, rolesLoading } = get();
    if (rolesLoaded || rolesLoading) {
      return;
    }
    set(() => ({ rolesLoaded: false, rolesLoading: true }));
    const roles = await loadRoles();
    set(() => ({ rolesLoaded: true, rolesLoading: false, roles }));
  },
  login: async (userId: UserId, password: string) => {
    const { authenticated, authLoading } = get();
    if (authenticated || authLoading) {
      return null;
    }
    set(() => ({ authenticated: false, authLoading: true }));
    const { errors, ...token } = await login(userId, password);
    if (!errors) {
      set(() => ({ authenticated: true, authLoading: false, token }));
      return null;
    }
    set(() => ({ authenticated: false, authLoading: false }));
    return errors;
  },
  refreshToken: async () => {
    const { authenticated, authLoading, token } = get();
    if (!(authenticated && token) || authLoading) {
      return;
    }
    set(() => ({ authLoading: true }));
    const { accessToken, expiresId, error, succeeded, refreshToken } = await getNewToken({
      accessToken: token.accessToken,
      refreshToken: token.refreshToken,
    });
    if (succeeded) {
      set((state) => ({
        authLoading: false,
        token: {
          ...state.token!,
          accessToken,
          refreshToken,
          expiresId,
        },
      }));
    } else {
      set(() => ({
        authLoading: false,
      }));
      // TODO: Доработать. Надо выводить уведомление.
      console.error(error);
    }
  },
  logout: async () => {
    const { authenticated, authLoading, token } = get();
    if (!(authenticated && token) || authLoading) {
      return;
    }
    set(() => ({ authLoading: true }));
    const { accessToken, refreshToken } = token;
    try {
      await logout({
        accessToken,
        refreshToken,
      });
      set(() => ({ authenticated: false, authLoading: false, userInfo: null, token: null }));
    } catch {
      set(() => ({ authLoading: false }));
    }
  },
  registerUser: async (user: IRegisterUser) => {
    const { authenticated, authLoading } = get();
    if (authenticated || authLoading) {
      return null;
    }
    set(() => ({ authenticated: false, authLoading: true }));
    const { errors } = await registerUser(user);
    set(() => ({ authenticated: false, authLoading: false }));
    return errors || null;
  },
  lockUser: async (userId: UserId, doLock: boolean) => {
    const { authenticated, authLoading, userInfo: currentUser } = get();
    if (authenticated || authLoading || currentUser?.id === userId) {
      return;
    }
    if (!get().usersLoaded) {
      await get().loadUsers();
    }
    if (!getItemById(get().users, userId)) {
      return;
    }
    set(() => ({ usersLoading: true }));
    try {
      await lockUser(userId);
      set((state) => ({
        usersLoading: false,
        users: setElementWithId(state.users, { id: userId, locked: doLock }),
      }));
    } catch {
      set(() => ({ usersLoading: false }));
    }
  },
}));

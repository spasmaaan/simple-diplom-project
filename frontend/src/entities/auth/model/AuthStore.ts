import { create } from 'zustand';
import { getItemById, setElementWithId } from 'shared/helpers';
import { AuthAction, AuthState, IRefreshTokenPayload, IRegisterUser, IUser, UserId } from '../lib';
import { loadRoles, loadUsers, lockUser, login, logout, refreshToken, registerUser } from '../api';

export const useAuthStoreBase = create<AuthState & AuthAction>()((set, get) => ({
  authLoading: false,
  authenticated: true, // false,
  user: null,
  token: null,
  usersLoading: false,
  usersLoaded: false,
  users: [],
  isAdmin: true, // false,
  rolesLoaded: false,
  rolesLoading: false,
  roles: {},
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
      return;
    }
    set(() => ({ authenticated: false, authLoading: true }));

    // eslint-disable-next-line no-debugger
    debugger;

    const token = await login(userId, password);
    set(() => ({ authenticated: true, authLoading: false, token }));
  },
  refreshToken: async (refreshData: IRefreshTokenPayload) => {
    const { authenticated, authLoading } = get();
    if (!authenticated || authLoading) {
      return;
    }
    set(() => ({ authLoading: true }));
    const refresedData = await refreshToken(refreshData);
    // TODO: Доработать.
    set(() => ({ authLoading: false }));
  },
  logout: async () => {
    const { authenticated, authLoading } = get();
    if (!authenticated || authLoading) {
      return;
    }
    set(() => ({ authLoading: true }));
    const isLogoutReady = await logout();
    if (isLogoutReady) {
      set(() => ({ authenticated: false, authLoading: false, user: null, token: null }));
    } else {
      set(() => ({ authLoading: false }));
    }
  },
  registerUser: async (user: IRegisterUser) => {
    const { authenticated, authLoading } = get();
    if (authenticated || authLoading) {
      return;
    }

    // eslint-disable-next-line no-debugger
    debugger;

    set(() => ({ authenticated: false, authLoading: true }));
    const isUserRegisterReady = await registerUser(user);
    set(() => ({ authenticated: false, authLoading: false, user: null, token: null }));
    if (isUserRegisterReady) {
      // Вывести уведомление.
    }
  },
  lockUser: async (userId: UserId, doLock: boolean) => {
    const { authenticated, authLoading, user: currentUser } = get();
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
    const isUserLocked = await lockUser(userId);
    if (!isUserLocked) {
      set(() => ({ usersLoading: false }));
    }
    set((state) => ({
      usersLoading: false,
      users: setElementWithId(state.users, { id: userId, locked: doLock }),
    }));
  },
}));

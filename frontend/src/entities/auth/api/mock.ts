import { IAuthData, ILoginResult, IUser, RoleId } from '../lib';
import { UserRole } from '../lib/constants';

export const MOCK_USERS: IUser[] = [
  {
    id: 'User1',
    firstName: 'First',
    lastName: 'Last',
    eMail: 'lol@lol.com',
    locked: false,
    roleId: 0,
    creationDate: new Date('1999/01/01'),
    revoked: new Date('2030/01/01'),
  },
];

export const MOCK_LOGIN: ILoginResult = {
  token: {
    id: 'test',
    jwtId: 'asd',
    token: 'tokenText',
    userId: 'TEST',
    creationDate: new Date('1991/01/01'),
    expirationDate: new Date('2030/01/01'),
    revoked: null,
  },
  user: MOCK_USERS[0]!,
};

export const MOCK_AUTH_DATA: IAuthData = {
  accessToken: 'accessToken',
  expiresId: 1000000,
  refreshToken: 'refreshToken',
  tokenType: 'Bearer',
};

export const MOCK_ROLES: Record<RoleId, UserRole> = {
  0: UserRole.None,
};

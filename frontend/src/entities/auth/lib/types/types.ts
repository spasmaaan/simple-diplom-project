import { UserRole } from '../constants';

export type UserId = string;
export type TokenId = string;
export type RoleId = number;
export type Token = string;

export interface IUser {
  id: UserId;
  firstName: string;
  lastName: string;
  eMail: string;
  roleId: UserRole;
  creationDate: Date;
  revoked: Date;
  locked: boolean;
}

export interface IToken {
  id: TokenId;
  userId: UserId;
  jwtId: string;
  token: Token;
  creationDate: Date;
  expirationDate: Date;
  revoked: Date | null;
}

export interface ILoginResult {
  user: IUser;
  token: IToken;
}

export interface IRefreshTokenPayload {
  accessToken: Token;
  refreshToken: Token;
}

export interface IAuthData {
  accessToken: Token;
  tokenType: string;
  expiresId: number;
  refreshToken: Token;
}

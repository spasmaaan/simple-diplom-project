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
  roles: UserRole[];
  creationDate: Date;
  locked: boolean;
}

export interface IRegisterUser {
  email: string;
  firstName: string;
  lastName: string;
  password: string;
}

export interface IRefreshTokenPayload {
  accessToken: Token;
  refreshToken: Token;
}

export interface IRefreshTokenResult {
  accessToken: Token;
  refreshToken: Token;
  succeeded: boolean;
  expiresId: number;
  error: string;
}

export interface IAuthData {
  accessToken: Token;
  tokenType: string;
  expiresId: number;
  refreshToken: Token;
}

export interface IAuthErrors {
  Email?: string;
  Password?: string;
}

export interface IAuthDataResult extends IAuthData {
  errors?: IAuthErrors;
}

export interface IRegisterUserErrors {
  Email?: string;
  FirstName?: string;
  LastName?: string;
  Password?: string;
}

export interface IRegisterUserResult {
  errors?: IRegisterUserErrors;
}

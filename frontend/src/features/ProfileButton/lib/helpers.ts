import { IUser } from 'entities/auth/lib';

const getFirstUpperLetterText = (text: string | undefined) => (text || '').toLocaleUpperCase();

export const getAvatarText = (user: IUser | null): string =>
  user
    ? `${getFirstUpperLetterText(user.firstName)}${getFirstUpperLetterText(user.lastName)}`
    : '?';

export const getUserFullName = (user: IUser | null): string =>
  user ? `${user.firstName} ${user.lastName}` : '-';

import cn from 'classnames';
import { IProfilePageProps } from '../lib';

import * as styles from './ProfilePage.module.scss';

export const ProfilePage = ({ className }: IProfilePageProps) => {
  return <div className={cn(className, styles.ProfilePage)}>йцуй</div>;
};

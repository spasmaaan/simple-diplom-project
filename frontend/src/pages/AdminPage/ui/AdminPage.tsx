import cn from 'classnames';
import { IAdminPageProps } from '../lib';

import * as styles from './AdminPage.module.scss';

export const AdminPage = ({ className }: IAdminPageProps) => {
  return <div className={cn(className, styles.AdminPage)}>Админка</div>;
};

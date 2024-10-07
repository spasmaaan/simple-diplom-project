import classNames from 'classnames';
import { Space, Typography } from 'antd';
import { ProfileButton } from 'features/ProfileButton';
import { INavbarProps } from '../lib';

import * as styles from './Navbar.module.scss';

const { Title } = Typography;

export const Navbar = ({ className }: INavbarProps) => {
  return (
    <div className={classNames(className, styles.Navbar)}>
      <div className={styles.FeaturesContainer}>
        <Space size="small">
          <ProfileButton />
        </Space>
      </div>
    </div>
  );
};

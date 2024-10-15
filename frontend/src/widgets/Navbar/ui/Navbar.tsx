import cn from 'classnames';
import { Space, Typography, Button } from 'antd';
import { ProfileButton } from 'features/ProfileButton';
import { useNavigate } from 'react-router-dom';
import { CameraOutlined, CommentOutlined, QuestionCircleOutlined } from '@ant-design/icons';
import { INavbarProps } from '../lib';

import * as styles from './Navbar.module.scss';

const { Title } = Typography;

export const Navbar = ({ className }: INavbarProps) => {
  const navigateTo = useNavigate();
  return (
    <div className={cn(className, styles.Navbar)}>
      <div className={styles.FeaturesContainer}>
        <Space size="small" direction="horizontal">
          <Button className={styles.BookingButton} onClick={() => navigateTo('/bookings')}>
            Забронировать
          </Button>
          <Button onClick={() => navigateTo('/cuisine')}>Кухня</Button>
          <Button onClick={() => navigateTo('/services')}>Сервисы</Button>
        </Space>
        <Space size="small" direction="horizontal">
          <Button
            shape="circle"
            icon={<QuestionCircleOutlined />}
            onClick={() => navigateTo('/faq')}
          />
          <Button shape="circle" icon={<CameraOutlined />} onClick={() => navigateTo('/gallery')} />
          <Button
            shape="circle"
            icon={<CommentOutlined />}
            onClick={() => navigateTo('/reviews')}
          />
          <ProfileButton className={styles.Profile} />
        </Space>
      </div>
    </div>
  );
};

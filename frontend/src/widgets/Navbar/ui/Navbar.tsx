import cn from 'classnames';
import { Space, Button, Typography } from 'antd';
import { ProfileButton } from 'features/ProfileButton';
import { useMatch, useNavigate } from 'react-router-dom';
import {
  CameraOutlined,
  CommentOutlined,
  HomeOutlined,
  QuestionCircleOutlined,
} from '@ant-design/icons';
import { INavbarProps } from '../lib';

import * as styles from './Navbar.module.scss';

const { Title } = Typography;

export const Navbar = ({ className }: INavbarProps) => {
  const navigateTo = useNavigate();

  const isHome = useMatch('/') != null;
  const isBookings = useMatch('/bookings') != null;
  const isCuisine = useMatch('/cuisine') != null;
  const isDishes = useMatch('/dishes/:cuisineId') != null;
  const isServices = useMatch('/services') != null;
  const isFaq = useMatch('/faq') != null;
  const isGallery = useMatch('/gallery') != null;
  const isReviews = useMatch('/reviews') != null;

  const getButtonType = (isActive: boolean) => (isActive ? 'primary' : undefined);

  return (
    <div className={cn(className, styles.Navbar)}>
      <div className={styles.FeaturesContainer}>
        <Space size="small" direction="horizontal">
          <Button
            className={cn(styles.Button, {
              [styles.ActiveButton]: isHome,
            })}
            type={getButtonType(isHome)}
            onClick={() => navigateTo('/')}
            icon={<HomeOutlined />}
          />
          <Button
            className={cn(styles.Button, {
              [styles.ActiveButton]: isBookings,
            })}
            type={getButtonType(isBookings)}
            onClick={() => navigateTo('/bookings')}
          >
            Забронировать
          </Button>
          <Button
            className={cn(styles.Button, {
              [styles.ActiveButton]: isCuisine || isDishes,
            })}
            type={getButtonType(isCuisine || isDishes)}
            onClick={() => navigateTo('/cuisine')}
          >
            Блюда
          </Button>
          <Button
            className={cn(styles.Button, {
              [styles.ActiveButton]: isServices,
            })}
            type={getButtonType(isServices)}
            onClick={() => navigateTo('/services')}
          >
            Сервисы
          </Button>
        </Space>
        <Title className={styles.Title} level={3}>
          Романтические ужины на колесе обозрения
        </Title>
        <Space size="small" direction="horizontal">
          <Button
            shape="circle"
            icon={<QuestionCircleOutlined />}
            className={cn(styles.Button, {
              [styles.ActiveButton]: isFaq,
            })}
            type={getButtonType(isFaq)}
            onClick={() => navigateTo('/faq')}
          />
          <Button
            shape="circle"
            icon={<CameraOutlined />}
            className={cn(styles.Button, {
              [styles.ActiveButton]: isGallery,
            })}
            type={getButtonType(isGallery)}
            onClick={() => navigateTo('/gallery')}
          />
          <Button
            shape="circle"
            icon={<CommentOutlined />}
            className={cn(styles.Button, {
              [styles.ActiveButton]: isReviews,
            })}
            type={getButtonType(isReviews)}
            onClick={() => navigateTo('/reviews')}
          />
          <ProfileButton className={styles.Profile} />
        </Space>
      </div>
    </div>
  );
};

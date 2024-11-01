/* eslint-disable jsx-a11y/alt-text */
import classNames from 'classnames';
import { Button, Card, Typography } from 'antd';
import { useNavigate } from 'react-router-dom';
import { BgBase64, BgHeartsBase64, IMainPageProps } from '../lib';

import * as styles from './MainPage.module.scss';

const { Title, Paragraph } = Typography;

export const MainPage = ({ className }: IMainPageProps) => {
  const navigateTo = useNavigate();
  return (
    <div className={classNames(className, styles.MainPage)}>
      <div className={styles.Hearts} style={{ backgroundImage: `url(${BgHeartsBase64})` }} />
      <Card className={styles.Text}>
        <Title level={2} style={{ marginBottom: '1rem' }}>
          Скорее бронируй кабинку для свидания!
        </Title>
        <Paragraph>Тут должен быть текст-описание.</Paragraph>
        <Paragraph>
          Много текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много
          текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много
          текста. Много текста. Много текста. Много текста. Много текста. Много текста. Много
          текста.
        </Paragraph>
        <Paragraph>
          Ещё чуть-чуть. Ещё чуть-чуть. Ещё чуть-чуть. Ещё чуть-чуть. Ещё чуть-чуть. Ещё чуть-чуть.
          Ещё чуть-чуть. Ещё чуть-чуть.
        </Paragraph>
      </Card>
      <img src={BgBase64} className={styles.Image} />
      <Button type="primary" className={styles.Button} onClick={() => navigateTo('/bookings')}>
        Забронировать!
      </Button>
    </div>
  );
};

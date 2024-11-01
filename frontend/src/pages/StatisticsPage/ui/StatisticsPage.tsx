import cn from 'classnames';
import { Card, Flex, Typography, Tabs } from 'antd';
import { AndroidOutlined, AppleOutlined } from '@ant-design/icons';
import { Column } from '@ant-design/charts';
import { IStatisticsPageProps } from '../lib';

import * as styles from './StatisticsPage.module.scss';
import { Data1 } from './data';

const { Title } = Typography;

const Chart1 = () => {
  const config = {
    data: Data1,
    xField: 'x',
    yField: 'y',
    colorField: 'name',
    group: true,
    style: {
      inset: 5,
    },
    // eslint-disable-next-line @typescript-eslint/no-explicit-any
    onReady: ({ chart }: { chart: any }) => {
      try {
        chart.on('afterrender', () => {
          chart.emit('legend:filter', {
            data: { channel: 'color', values: ['Текущая неделя'] },
          });
        });
      } catch (e) {
        console.error(e);
      }
    },
  };
  return <Column {...config} />;
};

export const StatisticsPage = ({ className }: IStatisticsPageProps) => {
  return (
    <div className={cn(className, styles.StatisticsPage)}>
      <Card classNames={{ body: styles.CardBody }}>
        <Tabs
          defaultActiveKey="1"
          items={[
            {
              key: '1',
              label: 'Посещения по дням',
              children: (
                <Flex vertical>
                  <Title type="secondary" level={3}>
                    <Chart1 />
                  </Title>
                </Flex>
              ),
              icon: <AppleOutlined />,
            },
            {
              key: '2',
              label: `Выручка по дням`,
              children: (
                <Flex vertical>
                  <Title type="secondary" level={3}>
                    Выручка по дням
                  </Title>
                </Flex>
              ),
              icon: <AndroidOutlined />,
            },
            {
              key: '3',
              label: `Самые популярные опции`,
              children: (
                <Flex vertical>
                  <Title type="secondary" level={3}>
                    Самые популярные опции
                  </Title>
                </Flex>
              ),
              icon: <AndroidOutlined />,
            },
          ]}
        />
      </Card>
    </div>
  );
};

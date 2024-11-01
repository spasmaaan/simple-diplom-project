import cn from 'classnames';
import { Button, Card, Divider, Flex, Space, Statistic, Tabs, Typography } from 'antd';
import { AndroidOutlined, AppleOutlined } from '@ant-design/icons';

import { DishId, IDish, useDishesStore } from 'entities/dishes';
import { IService, ServiceId, useServiceStore } from 'entities/services';
import { IBooking, useBookingStore } from 'entities/booking';
import React, { useEffect, useMemo } from 'react';
import { BookingStatus } from 'entities/booking/lib/constants';
import { useAuthStore } from 'entities/auth';
import * as styles from './ManagePage.module.scss';
import { IManagePageProps } from '../lib';

const { Title, Text } = Typography;

const calcBookingSum = (
  dishesMap: Record<DishId, IDish>,
  servicesMap: Record<ServiceId, IService>,
  booking: Partial<IBooking> | undefined
): number => {
  let sum = 0;
  if (!booking) {
    return sum;
  }
  // Цена бронирования.
  sum += 2000;
  Object.entries(booking.dishes || {}).forEach(([dishKeyText, count]) => {
    const dish = dishesMap[Number(dishKeyText)];
    sum += dish.price * count;
  });
  Object.entries(booking.services || {}).forEach(([serviceKeyText, count]) => {
    const service = servicesMap[Number(serviceKeyText)];
    sum += service.price * count;
  });
  return sum;
};

export const ManagePage = ({ className }: IManagePageProps) => {
  const { authenticated } = useAuthStore();
  const { dishes, dishesLoaded, dishesLoading, loadDish } = useDishesStore();
  const { services, servicesLoaded, servicesLoading, load: loadServices } = useServiceStore();
  const {
    bookings,
    bookingsLoaded,
    bookingsLoading,
    freeTime,
    freeTimeLoaded,
    freeTimeLoading,
    newBooking,
    clearNewBooking,
    setDish,
    setService,
    load,
    loadFreeTime,
  } = useBookingStore();

  const dishesMap: Record<DishId, IDish> = useMemo(
    () => dishes.reduce((accumulate, current) => ({ ...accumulate, [current.id]: current }), {}),
    [dishes]
  );
  const servicesMap: Record<ServiceId, IService> = useMemo(
    () => services.reduce((accumulate, current) => ({ ...accumulate, [current.id]: current }), {}),
    [services]
  );

  const renderBooking = (filteredBookings: IBooking[], buttons?: React.ReactElement) => {
    // eslint-disable-next-line no-debugger
    debugger;

    return (
      <Flex justify="center">
        <Space direction="vertical" size="large" style={{ width: 800, maxWidth: 800 }}>
          {filteredBookings.map((booking) => {
            const start = new Date(booking.startDate);
            const date = start.toLocaleDateString();
            const startTime = start.toLocaleTimeString([], {
              hour: '2-digit',
              minute: '2-digit',
            });
            const endTime = new Date(booking.endDate).toLocaleTimeString([], {
              hour: '2-digit',
              minute: '2-digit',
            });
            const bookingTitle = `${date} с ${startTime} до ${endTime}`;
            return (
              <Flex key={booking.id} vertical>
                <Divider plain style={{ borderColor: 'geekblue' }}>
                  {bookingTitle}
                </Divider>
                <Space direction="vertical" style={{ width: '100%' }}>
                  <Title type="secondary" level={5}>
                    Блюда
                  </Title>
                  <Flex vertical style={{ gap: '1rem' }}>
                    {dishesLoaded &&
                      Object.entries(booking?.dishes || {}).map(([dishKeyText, count]) => {
                        const dishKey = Number(dishKeyText);
                        const dish = dishesMap[dishKey];
                        return (
                          <Flex key={dishKey} justify="space-between" align="center">
                            <Text strong>{dish.name}</Text>
                            <Flex style={{ gap: '1rem' }}>
                              <Text>{dish.price} ₽</Text>
                              <Text>{count} шт.</Text>
                              <Text>{(count * dish.price).toFixed(2)} ₽</Text>
                            </Flex>
                          </Flex>
                        );
                      })}
                  </Flex>
                  <Title type="secondary" level={5}>
                    Сервисы
                  </Title>
                  {servicesLoaded &&
                    Object.entries(booking?.services || {}).map(([serviceKeyText, count]) => {
                      const serviceKey = Number(serviceKeyText);
                      const service = servicesMap[serviceKey];
                      return (
                        <Flex key={serviceKey} justify="space-between" align="center">
                          <Text strong>{service.name}</Text>
                          <Flex style={{ gap: '1rem' }}>
                            <Text>{service.price} ₽</Text>
                            <Text>{count} шт.</Text>
                            <Text>{(count * service.price).toFixed(2)} ₽</Text>
                          </Flex>
                        </Flex>
                      );
                    })}
                  <Flex
                    justify="space-between"
                    style={{ margin: '1.5rem 0 0.5rem 0', alignItems: 'end' }}
                  >
                    <Flex>
                      <Space direction="horizontal" size="small">
                        {buttons}
                      </Space>
                    </Flex>
                    <Statistic
                      title="Сумма"
                      value={`${calcBookingSum(dishesMap, servicesMap, booking)} ₽`}
                    />
                  </Flex>
                </Space>
              </Flex>
            );
          })}
        </Space>
      </Flex>
    );
  };

  useEffect(() => {
    if (authenticated && !(bookingsLoaded && bookingsLoading)) {
      load();
    }
    if (!(dishesLoaded && dishesLoading)) {
      loadDish();
    }
    if (!(servicesLoaded && servicesLoading)) {
      loadServices();
    }
  }, [
    authenticated,
    bookingsLoaded,
    bookingsLoading,
    dishesLoaded,
    dishesLoading,
    freeTimeLoaded,
    freeTimeLoading,
    servicesLoaded,
    servicesLoading,
    load,
    loadDish,
    loadFreeTime,
    loadServices,
  ]);

  return (
    <div className={cn(className, styles.ManagePage)}>
      <Card classNames={{ body: styles.CardBody }}>
        <Tabs
          defaultActiveKey="1"
          items={[
            {
              key: '1',
              label: 'Новые бронирования',
              children: renderBooking(
                bookings.filter((x) => x.statusId === BookingStatus.Processing),
                <>
                  <Button type="primary">Подтвердить</Button>
                  <Button>Отменить бронирование</Button>
                </>
              ),
              icon: <AppleOutlined />,
            },
            {
              key: '2',
              label: `Ожидают оплаты`,
              children: renderBooking(
                bookings.filter((x) => x.statusId === BookingStatus.Approved),
                <>
                  <Button type="primary">Оплачено</Button>
                  <Button>Отменить бронирование</Button>
                </>
              ),
              icon: <AndroidOutlined />,
            },
            {
              key: '3',
              label: `Оплачены`,
              children: renderBooking(
                bookings.filter((x) => x.statusId === BookingStatus.Paid),
                <Button>Отменить бронирование</Button>
              ),
              icon: <AndroidOutlined />,
            },
            {
              key: '4',
              label: `Выполненные`,
              children: renderBooking(
                bookings.filter((x) => x.statusId === BookingStatus.Completed)
              ),
              icon: <AndroidOutlined />,
            },
            {
              key: '5',
              label: `Отменённые`,
              children: renderBooking(
                bookings.filter((x) => x.statusId === BookingStatus.Rejected)
              ),
              icon: <AndroidOutlined />,
            },
          ]}
        />
      </Card>
    </div>
  );
};

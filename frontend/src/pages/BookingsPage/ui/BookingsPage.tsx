import cn from 'classnames';
import {
  DatePicker,
  Flex,
  Space,
  Typography,
  TimePicker,
  Button,
  Modal,
  Card,
  InputNumber,
  Statistic,
  Alert,
  QRCode,
} from 'antd';
import dayjs from 'dayjs';
import customParseFormat from 'dayjs/plugin/customParseFormat';
import { RangePickerProps } from 'antd/es/date-picker';
import { useCallback, useEffect, useMemo, useState } from 'react';
import { IBooking, useBookingStore } from 'entities/booking';
import { DeleteOutlined } from '@ant-design/icons';
import { useAuthStore } from 'entities/auth';
import { DishId, IDish, useDishesStore } from 'entities/dishes';
import { IService, ServiceId, useServiceStore } from 'entities/services';
import { BookingStatus } from 'entities/booking/lib/constants';
import { IBookingsPageProps } from '../lib';

import * as styles from './BookingsPage.module.scss';

dayjs.extend(customParseFormat);

const { Title, Text } = Typography;
const dateFormat = 'YYYY-MM-DD';
const timeFormat = 'HH:mm';

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

export const BookingsPage = ({ className }: IBookingsPageProps) => {
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
  //  const [bookingDate, setBookingDate] = useState<Date>(new Date(Date.now()));
  //  const [bookingTimeStart, setBookingTimeStart] = useState<Date | null>(null);
  //  const [bookingTimeEnd, setBookingTimeEnd] = useState<Date | null>(null);

  // const disabledDate: RangePickerProps['disabledDate'] = (current) => {
  //   return true; // current && current < dayjs().endOf('day');
  // };

  const [modal, contextHolder] = Modal.useModal();

  const dishesMap: Record<DishId, IDish> = useMemo(
    () => dishes.reduce((accumulate, current) => ({ ...accumulate, [current.id]: current }), {}),
    [dishes]
  );
  const servicesMap: Record<ServiceId, IService> = useMemo(
    () => services.reduce((accumulate, current) => ({ ...accumulate, [current.id]: current }), {}),
    [services]
  );
  const newBookingSum: number = useMemo(() => {
    return calcBookingSum(dishesMap, servicesMap, newBooking);
  }, [dishesMap, newBooking, servicesMap]);

  const minDate = useMemo(() => {
    const nextDay = new Date(Date.now());
    nextDay.setTime(0);
    nextDay.setDate(nextDay.getDate() + 1);
    return nextDay;
  }, []);
  const maxDate = useMemo(() => {
    const nextDay = new Date(minDate);
    nextDay.setTime(0);
    nextDay.setDate(nextDay.getDate() + 90);
    return nextDay;
  }, [minDate]);

  const onCreateBooking = useCallback(() => {
    void modal.confirm({
      title: 'Подтверждение бронирования?',
      content: 'После бронирования нельзя будет изменить список блюд и сервисов.',
      okText: 'Подтвердить',
      cancelText: 'Отмена',
      onOk: () => {
        // Забронировать.
      },
    });
  }, [modal]);
  const onClearBooking = useCallback(() => {
    void modal.confirm({
      title: 'Отменить бронирование?',
      okText: 'Да',
      cancelText: 'Нет',
      onOk: () => {
        clearNewBooking();
      },
    });
  }, [modal, clearNewBooking]);

  const onChangeDate = useCallback((event: unknown) => {
    console.log('onChangeDate(%O)', event);
  }, []);

  const onChangeTime = useCallback((event: unknown) => {
    console.log('onChangeTime(%O)', event);
  }, []);

  useEffect(() => {
    if (authenticated && !(bookingsLoaded && bookingsLoading)) {
      load();
    }
    if (!(freeTimeLoaded && freeTimeLoading)) {
      loadFreeTime();
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

  const renderStatus = (booking: IBooking) => {
    const statusesData: Record<
      BookingStatus,
      { message: string; description: string; type: 'success' | 'info' | 'warning' | 'error' }
    > = {
      [BookingStatus.Processing]: {
        message: 'В обработке',
        description:
          'Наши сотрудники проверят всё ли в порядке. После подтверждения можно будет оплатить бронирование в течении суток.',
        type: 'info',
      },
      [BookingStatus.Approved]: {
        message: 'Подтверждено',
        description:
          'Ожидание оплаты бронирования. Оплатить необходимо сегодня, иначе заказ отменится автоматически.',
        type: 'warning',
      },
      [BookingStatus.Paid]: {
        message: 'Оплачено',
        description:
          'Бронирование подтверждено и оплачено. Пожалуйста приходите за 5 минут до начала.',
        type: 'success',
      },
      [BookingStatus.InProgress]: {
        message: 'В процессе',
        description: 'Заказ сейчас выполняется.',
        type: 'info',
      },
      [BookingStatus.Completed]: {
        message: 'Выполнено',
        description: 'Заказ завершён.',
        type: 'success',
      },
      [BookingStatus.Rejected]: {
        message: 'Отменено',
        description: 'К сожалению заказ был отменён.',
        type: 'error',
      },
    };
    const { message, description, type } = statusesData[booking.statusId];

    const fullDescription = booking.comment ? `${description} ${booking.comment}` : description;

    return <Alert message={message} description={fullDescription} type={type} showIcon />;
  };

  return (
    <Flex className={cn(className, styles.BookingsPage)} justify="center">
      {contextHolder}
      <Space direction="vertical" size="large" className={styles.BookingsPanel}>
        <Card>
          <Title className={styles.CreateBookingTitle} type="secondary" level={3}>
            Новое бронирование
          </Title>
          <Flex justify="space-around">
            <Space
              className={styles.CreateBookingOptions}
              direction="vertical"
              style={{ paddingRight: '3rem' }}
            >
              <Title type="secondary" level={4}>
                Блюда
              </Title>
              <Flex vertical style={{ gap: '1rem' }}>
                {newBooking &&
                  dishesLoaded &&
                  Object.entries(newBooking?.dishes || {}).map(([dishKeyText, count]) => {
                    const dishKey = Number(dishKeyText);
                    const dish = dishesMap[dishKey];
                    return (
                      <Flex key={dishKey} justify="space-between" align="center">
                        <Text strong>{dish.name}</Text>
                        <Flex style={{ gap: '0.5rem' }}>
                          <Text>{(count * dish.price).toFixed(2)} ₽</Text>
                          <InputNumber
                            value={count}
                            min={0}
                            max={100}
                            onChange={(currentCount) => setDish(dishKey, currentCount || 0)}
                          />
                          <Button icon={<DeleteOutlined />} onClick={() => setDish(dishKey, 0)} />
                        </Flex>
                      </Flex>
                    );
                  })}
              </Flex>
              <Title type="secondary" level={4}>
                Сервисы
              </Title>
              {newBooking &&
                servicesLoaded &&
                Object.entries(newBooking?.services || {}).map(([serviceKeyText, count]) => {
                  const serviceKey = Number(serviceKeyText);
                  const service = servicesMap[serviceKey];
                  return (
                    <Flex key={serviceKey} justify="space-between" align="center">
                      <Text strong>{service.name}</Text>
                      <Flex style={{ gap: '0.5rem' }}>
                        <Text>{(count * service.price).toFixed(2)} ₽</Text>
                        <InputNumber
                          value={count}
                          min={0}
                          max={100}
                          onChange={(currentCount) => setService(serviceKey, currentCount || 0)}
                        />
                        <Button
                          icon={<DeleteOutlined />}
                          onClick={() => setService(serviceKey, 0)}
                        />
                      </Flex>
                    </Flex>
                  );
                })}
            </Space>
            <Flex className={styles.CreateBookingCalendar} vertical justify="space-between">
              <Flex vertical justify="flex-end">
                <Space
                  direction="vertical"
                  classNames={{
                    item: styles.CreateBookingCalendarItem,
                  }}
                >
                  <Title type="secondary" level={4} style={{ textAlign: 'right' }}>
                    Дата и время
                  </Title>
                  <DatePicker
                    style={{ width: 300 }}
                    placeholder="Дата"
                    format={dateFormat}
                    //      disabledDate={disabledDate}
                    //      defaultValue={dayjs(bookingDate, dateFormat)}
                    minDate={dayjs(minDate, dateFormat)}
                    maxDate={dayjs(maxDate, dateFormat)}
                    onChange={onChangeDate}
                  />
                  <TimePicker.RangePicker
                    style={{ width: 300 }}
                    format={timeFormat}
                    minuteStep={15}
                    hourStep={1}
                    onChange={onChangeTime}
                  />
                </Space>
              </Flex>
              <Flex justify="flex-end" style={{ marginTop: '2rem' }}>
                <Space>
                  <Statistic title="Сумма" value={`${newBookingSum} ₽`} />
                </Space>
              </Flex>
            </Flex>
          </Flex>
          <Flex className={styles.CreateBookingControls}>
            <Button onClick={onClearBooking}>Отменить</Button>
            <Button type="primary" onClick={onCreateBooking}>
              Забронировать
            </Button>
          </Flex>
        </Card>
        {authenticated && bookingsLoaded && (
          <Card style={{ maxWidth: 800 }}>
            <Space direction="vertical" style={{ width: '100%' }}>
              <Title type="secondary" level={3}>
                Ваши бронирования
              </Title>
              <Flex vertical>
                {bookings.map((booking) => {
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
                      <Title type="secondary" level={5} style={{ marginBottom: '1rem' }}>
                        {bookingTitle}
                      </Title>
                      {renderStatus(booking)}
                      {booking.statusId === BookingStatus.Approved && (
                        <Flex justify="center" style={{ margin: '1rem' }}>
                          <QRCode value="Тут должна быть информация для совершения оплаты через СБП." />
                        </Flex>
                      )}
                      <Space
                        className={styles.CreateBookingOptions}
                        direction="vertical"
                        style={{ width: '100%', marginTop: '1rem' }}
                      >
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
                          {[BookingStatus.Processing, BookingStatus.Approved].includes(
                            booking.statusId
                          ) ? (
                            <Button>Отменить бронирование</Button>
                          ) : (
                            <Text />
                          )}
                          <Statistic
                            title="Сумма"
                            value={`${calcBookingSum(dishesMap, servicesMap, booking)} ₽`}
                          />
                        </Flex>
                      </Space>
                    </Flex>
                  );
                })}
              </Flex>
            </Space>
          </Card>
        )}
      </Space>
    </Flex>
  );
};

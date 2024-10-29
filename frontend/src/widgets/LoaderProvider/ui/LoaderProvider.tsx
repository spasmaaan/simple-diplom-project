import cn from 'classnames';
import { useAuthStore } from 'entities/auth';
import { useMemo } from 'react';
import { Spin } from 'antd';

import { useApplicationStore } from 'entities/application';
import { useBookingStore } from 'entities/booking';
import { useDishesStore } from 'entities/dishes';
import { useFaqsStore } from 'entities/faqs';
import { usePhotosStore } from 'entities/photos';
import { useReviewsStore } from 'entities/reviews';
import { useServiceStore } from 'entities/services';
import * as styles from './LoaderProvider.module.scss';
import { ILoaderProviderProps } from '../lib';

export const LoaderProvider = ({ className, children }: ILoaderProviderProps) => {
  const appStore = useApplicationStore();
  const authStore = useAuthStore();
  const bookingStore = useBookingStore();
  const dishesStore = useDishesStore();
  const faqsStore = useFaqsStore();
  const photosStore = usePhotosStore();
  const reviewsStore = useReviewsStore();
  const servicesStore = useServiceStore();

  const loading = useMemo(
    () =>
      appStore.notificationsLoading ||
      appStore.optionsLoading ||
      authStore.authLoading ||
      authStore.userInfoLoading ||
      authStore.rolesLoading ||
      authStore.usersLoading ||
      bookingStore.bookingsLoading ||
      dishesStore.categoriesLoading ||
      dishesStore.dishesLoading ||
      faqsStore.faqsLoading ||
      photosStore.photosLoading ||
      reviewsStore.reviewsLoading ||
      servicesStore.servicesLoading,
    [
      appStore,
      authStore,
      bookingStore,
      dishesStore,
      faqsStore,
      photosStore,
      reviewsStore,
      servicesStore,
    ]
  );

  return (
    <div
      className={cn(styles.Wrapper, className, {
        [styles.WrapperBlocked]: loading,
      })}
    >
      <div
        className={cn(styles.Screen, {
          [styles.ScreenActive]: loading,
        })}
      >
        <Spin size="large" />
      </div>
      {children}
    </div>
  );
};

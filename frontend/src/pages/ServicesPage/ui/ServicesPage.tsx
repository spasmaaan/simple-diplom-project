import cn from 'classnames';
import { useServiceStore, ServiceId, IService, IServiceData } from 'entities/services';
import { ServiceDialog } from 'features/ServiceDialog';
import { useCallback, useEffect } from 'react';
import { ManagementProvider } from 'widgets/ManagementProvider';
import { ServicesPanel } from 'widgets/ServicesPanel';
import { useBookingStore } from 'entities/booking';
import { IServicesPageProps } from '../lib';

import * as styles from './ServicesPage.module.scss';

export const ServicesPage = ({ className }: IServicesPageProps) => {
  const { services, servicesLoaded, add, edit, remove, load, loadImage } = useServiceStore();
  const { newBooking, setService } = useBookingStore();

  const createDialogOkHandler = useCallback(
    (id: ServiceId | undefined, handleOk: (item: IService | IServiceData) => void) =>
      (data: IServiceData) => {
        handleOk({
          ...data,
          ...(id ? { id } : {}),
        });
      },
    []
  );

  useEffect(() => {
    if (!servicesLoaded) {
      load();
    } else {
      services.forEach(({ id }) => {
        loadImage(id);
      });
    }
  }, [load, loadImage, services, servicesLoaded]);

  return (
    <ManagementProvider
      className={cn(styles.ServicesPage, className)}
      addTitle="Новый сервис"
      editTitle="Изменить сервис"
      removeTitle="Удалить сервис?"
      renderDialog={({ open, id, title, okText, onOk, onClose }) => (
        <ServiceDialog
          open={open}
          title={title}
          defaults={services.find((service) => service.id === id)}
          okText={okText}
          onOk={createDialogOkHandler(id, onOk)}
          onClose={onClose}
        />
      )}
      renderContent={(onAdd, onEdit, onRemove) =>
        servicesLoaded ? (
          <ServicesPanel
            services={services}
            pricePostfix="₽"
            showCounts
            counts={newBooking?.services || {}}
            onChangeCount={setService}
            onAdd={onAdd}
            onEdit={onEdit}
            onRemove={onRemove}
          />
        ) : null
      }
      add={add}
      edit={edit}
      remove={remove}
    />
  );
};

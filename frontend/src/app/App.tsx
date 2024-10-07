import { MainPage } from 'pages/MainPage';
import { Route, Routes } from 'react-router-dom';
import { AppLayout } from 'pages/layouts/AppLayout';
import { AdminPage } from 'pages/AdminPage/ui/AdminPage';
import { BookingsPage } from 'pages/BookingsPage/ui/BookingsPage';
import { BookingPage } from 'pages/BookingPage';
import { DishesPage } from 'pages/DishesPage';
import { DishPage } from 'pages/DishPage';
import { FaqPage } from 'pages/FaqPage';
import { ProfilePage } from 'pages/ProfilePage';
import { ReviewsPage } from 'pages/ReviewsPage';
import { ServicesPage } from 'pages/ServicesPage';
import { ServicePage } from 'pages/ServicePage';
import { StatisticsPage } from 'pages/StatisticsPage';

import './styles/index.scss';

export const App = () => {
  return (
    <Routes>
      <Route path="/" element={<AppLayout className="app" />} />
      <Route index element={<MainPage />} />
      <Route path="/admin" element={<AdminPage />} />
      <Route path="/bookings" element={<BookingsPage />} />
      <Route path="/booking/:bookingId" element={<BookingPage />} />
      <Route path="/dishes" element={<DishesPage />} />
      <Route path="/dish/:dishId" element={<DishPage />} />
      <Route path="/faq" element={<FaqPage />} />
      <Route path="/profile" element={<ProfilePage />} />
      <Route path="/reviews" element={<ReviewsPage />} />
      <Route path="/services" element={<ServicesPage />} />
      <Route path="/service/:serviceId" element={<ServicePage />} />
      <Route path="/statistics" element={<StatisticsPage />} />
    </Routes>
  );
};

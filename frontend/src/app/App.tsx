import { MainPage } from 'pages/MainPage';
import { Route, Routes } from 'react-router-dom';
import { AppLayout } from 'pages/layouts/AppLayout';
import { AdminPage } from 'pages/AdminPage';
import { BookingsPage } from 'pages/BookingsPage';
import { BookingPage } from 'pages/BookingPage';
import { DishesPage } from 'pages/DishesPage';
import { DishPage } from 'pages/DishPage';
import { FaqPage } from 'pages/FaqPage';
import { ProfilePage } from 'pages/ProfilePage';
import { ReviewsPage } from 'pages/ReviewsPage';
import { ServicesPage } from 'pages/ServicesPage';
import { ServicePage } from 'pages/ServicePage';
import { StatisticsPage } from 'pages/StatisticsPage';
import { PhotosPage } from 'pages/PhotosPage';
import { DishCategoriesPage } from 'pages/DishCategoriesPage';

import './styles/index.scss';

export const App = () => {
  return (
    <Routes>
      <Route path="/" element={<AppLayout className="app" />}>
        <Route index element={<MainPage />} />
        <Route path="/admin" element={<AdminPage />} />
        <Route path="/bookings" element={<BookingsPage />} />
        <Route path="/booking/:bookingId" element={<BookingPage />} />
        <Route path="/cuisine" element={<DishCategoriesPage />} />
        <Route path="/dishes/:dishCategoryId" element={<DishesPage />} />
        <Route path="/faq" element={<FaqPage />} />
        <Route path="/profile" element={<ProfilePage />} />
        <Route path="/gallery" element={<PhotosPage />} />
        <Route path="/reviews" element={<ReviewsPage />} />
        <Route path="/services" element={<ServicesPage />} />
        <Route path="/statistics" element={<StatisticsPage />} />
      </Route>
    </Routes>
  );
};

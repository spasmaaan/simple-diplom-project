import { message } from 'antd';
import { FileType } from './types';

export const beforeUpload = (file: FileType) => {
  const isJpgOrPng = file.type === 'image/jpeg' || file.type === 'image/png';
  if (!isJpgOrPng) {
    message.error('Неправильный формат файла! Разрешены только JPG и PNG.');
  }
  const isLt10M = file.size / 1024 / 1024 < 10;
  if (!isLt10M) {
    message.error('Файл изображения должен быть меньше 10MB!');
  }
  return isJpgOrPng && isLt10M;
};

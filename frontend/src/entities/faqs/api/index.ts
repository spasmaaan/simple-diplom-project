import { backendDelete, backendGet, backendPost, backendPut } from 'shared/api';
import { FaqsBackendUrl } from 'shared/config';
import { FaqId, IFaq } from '../lib';
import { MOCK_FAQS } from './mock';

const getFaqsPath = (url: string = '') => `${FaqsBackendUrl}${url}`;

export const loadFaqs = async (): Promise<IFaq[]> => {
  return MOCK_FAQS; // (await backendGet(null, getFaqsPath())).json();
};
export const addFaq = async (faq: IFaq): Promise<IFaq> => {
  return (await backendPost(null, getFaqsPath(), faq)).json();
};
export const editFaq = async (faq: IFaq): Promise<IFaq> => {
  return (await backendPut(null, getFaqsPath(), faq)).json();
};
export const removeFaq = async (faqId: FaqId): Promise<IFaq> => {
  return (await backendDelete(null, getFaqsPath(`/${faqId}`))).json();
};

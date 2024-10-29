import { backendDelete, backendGet, backendPost, backendPut } from 'shared/api';
import { FaqsBackendUrl } from 'shared/config';
import { IPaginationResult } from 'shared/types';
import { FaqId, IFaq, IFaqData } from '../lib';

const getFaqsPath = (url: string = '') => `${FaqsBackendUrl}${url}`;

export const loadFaqs = async (): Promise<IPaginationResult<IFaq>> => {
  return (await backendGet(null, getFaqsPath())).json();
};
export const addFaq = async (faqData: IFaqData): Promise<IFaq> => {
  return (await backendPost(null, getFaqsPath(), faqData)).json();
};
export const editFaq = async (faq: IFaq): Promise<IFaq> => {
  return (await backendPut(null, getFaqsPath(), faq)).json();
};
export const removeFaq = async (faqId: FaqId): Promise<IFaq> => {
  return (await backendDelete(null, getFaqsPath(`/${faqId}`))).json();
};

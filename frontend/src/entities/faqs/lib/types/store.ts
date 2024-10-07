import { FaqId, IFaq } from './types';

export type FaqsState = {
  faqsLoading: boolean;
  faqsLoaded: boolean;
  faqs: IFaq[];
};

export type FaqsAction = {
  load: () => Promise<void>;
  add: (faq: IFaq) => Promise<void>;
  edit: (faq: IFaq) => Promise<void>;
  remove: (faqId: FaqId) => Promise<void>;
};

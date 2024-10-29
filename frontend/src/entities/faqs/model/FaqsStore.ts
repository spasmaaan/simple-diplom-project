import { create } from 'zustand';
import { deleteElementWithId, setElementWithId } from 'shared/helpers';
import { FaqId, FaqsAction, FaqsState, IFaq, IFaqData } from '../lib';
import { addFaq, editFaq, loadFaqs, removeFaq } from '../api';

export const useFaqsStoreBase = create<FaqsState & FaqsAction>()((set, get) => ({
  faqsLoading: false,
  faqsLoaded: false,
  faqs: [],
  load: async () => {
    const { faqsLoaded, faqsLoading } = get();
    if (faqsLoaded || faqsLoading) {
      return;
    }
    set(() => ({ faqsLoaded: false, faqsLoading: true }));
    const result = (await loadFaqs()) || [];
    set(() => ({ faqsLoaded: true, faqsLoading: false, faqs: result.items }));
  },
  add: async (faqData: IFaqData) => {
    if (!get().faqsLoaded) {
      await get().load();
    }
    if (get().faqsLoading) {
      return;
    }
    set(() => ({ faqsLoading: true }));
    const addedFaq = await addFaq(faqData);
    set((state) => ({ faqsLoading: false, faqs: [...state.faqs, addedFaq] }));
  },
  edit: async (faq: IFaq) => {
    if (!get().faqsLoaded) {
      await get().load();
    }
    const { faqsLoading, faqs } = get();
    if (faqsLoading || !faqs.find((currentDish) => currentDish.id === faq.id)) {
      return;
    }
    set(() => ({ faqsLoading: true }));
    const editedFaq = await editFaq(faq);
    set((state) => ({ faqsLoading: false, faqs: setElementWithId(state.faqs, editedFaq) }));
  },
  remove: async (faqId: FaqId) => {
    if (!get().faqsLoaded) {
      await get().load();
    }
    const { faqsLoading, faqs } = get();
    if (faqsLoading || !faqs.find((currentDish) => currentDish.id === faqId)) {
      return;
    }
    set(() => ({ faqsLoading: true }));
    const removedFaq = await removeFaq(faqId);
    set((state) => ({
      faqsLoading: false,
      faqs: deleteElementWithId(state.faqs, removedFaq),
    }));
  },
}));

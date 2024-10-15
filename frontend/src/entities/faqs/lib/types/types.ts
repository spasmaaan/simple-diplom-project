export type FaqId = number;

export interface IFaqData {
  question: string;
  answer: string;
}

export interface IFaq extends IFaqData {
  id: FaqId;
}

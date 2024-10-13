export interface ILoginDialogProps {
  className?: string;
  open: boolean;
  onCancel?: () => void;
  onLogin: (loginName: string, password: string) => void;
  onRegister: (loginName: string, password: string, eMail: string) => void;
}

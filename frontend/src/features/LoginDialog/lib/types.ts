export interface ILoginDialogProps {
  className?: string;
  open: boolean;
  onCancel?: () => void;
  onLogin: (loginName: string, password: string) => void;
  onRegister: (eMail: string, firstName: string, lastName: string, password: string) => void;
}

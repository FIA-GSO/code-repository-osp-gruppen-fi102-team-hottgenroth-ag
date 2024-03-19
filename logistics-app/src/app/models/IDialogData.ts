export interface IDialogData 
{
  icon: 'warning' | 'info';
  title: string;
  subtitle?: string;
  text: string;
  okButtonText: string;
  cancelButtonText?: string;
  hasCancelButton?: boolean;
}

namespace UserBook
{
    interface IUserFormDialogDataValidator
    {
        int ValidUsername(string username);
        int ValidPassword(string password);
    }
}
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace UserBook
{
    class UserFormDialogDataValidator : IUserFormDialogDataValidator
    {
        public int ValidUsername(string username)
        {
            int msgCode = -1;

            // Username must not be empty
            if (String.IsNullOrEmpty(username))
            {
                msgCode = Resource.String.empty_username_msg;
            }
            return msgCode;
        }
        public int ValidPassword(string password)
        {
            int msgCode = -1;

            // Password must not be empty
            if (String.IsNullOrEmpty(password))
            {
                msgCode = Resource.String.empty_password_msg;
            }
            // Password must be a mix of number and letters only
            else if (!PasswordHasLetterAndNumberOnly(password))
            {
                msgCode = Resource.String.error_password_letter_number_only_msg;
            }
            // Password must have a least a number
            else if (!PasswordHasAtLeastOneNumber(password))
            {
                msgCode = Resource.String.error_password_number_msg;
            }
            // Password must have a least a letter
            else if (!PasswordHasAtLeastOneLetter(password))
            {
                msgCode = Resource.String.error_password_letter_msg;
            }
            // Password must be between 5 and 12 characters in length 
            else if (!PasswordHasValidLength(password))
            {
                msgCode = Resource.String.error_password_length_msg;
            }
            // Password must not contain any sequence of characters immediately followed by the same sequence
            else if (!PasswordHasNotRepeatedSequence(password))
            {
                msgCode = Resource.String.error_password_sec_rep_msg;
            }
            return msgCode;
        }

        private bool PasswordHasLetterAndNumberOnly(string password) => password.All(Char.IsLetterOrDigit);
        private bool PasswordHasAtLeastOneNumber(string password) => password.Any(Char.IsNumber);
        private bool PasswordHasAtLeastOneLetter(string password) => password.Any(Char.IsLetter);
        private bool PasswordHasValidLength(string password) => password.Length >= 5 && password.Length <= 12;
        private bool PasswordHasNotRepeatedSequence(string password) => !Regex.IsMatch(password, @"(.+)\1");

    }
}
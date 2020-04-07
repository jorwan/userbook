using System;

using Android.App;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Text;
using Android.Widget;

namespace UserBook
{
    class RealtimeValidableUserFormDialog : UserFormDialog
    {
        protected IUserFormDialogDataValidator Validator { get; set; }

        public RealtimeValidableUserFormDialog(
            Activity activity,
            IUserFormDialogDataValidator validator = null,
            OnCreateUserCallback onCreatedUser = null) : base(activity, onCreatedUser)
        {
            Validator = validator ?? new UserFormDialogDataValidator();
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            UsernameTxt.TextChanged += OnUsernameTextChange;

            PasswordTxt.TextChanged += OnPasswordTextChange;
        }

        private void OnPasswordTextChange(object sender, TextChangedEventArgs e) => ValidPasswordTxt();

        private void OnUsernameTextChange(object sender, TextChangedEventArgs e) => ValidUsernameTxt();

        private bool ValidUsernameTxt()
        {
            int result = Validator.ValidUsername(UsernameTxt.Text);
            bool valid = result == -1;

            if (valid)
            {
                // Remove error message
                UsernameErrorMsg.Text = "";
                ApplyValidationStateStyle(UsernameTxt, valid);
            }
            else
            {
                // Put error message
                UsernameErrorMsg.Text = Context.GetString(result);
                ApplyValidationStateStyle(UsernameTxt, valid);
            }
            return valid;
        }

        private bool ValidPasswordTxt()
        {
            int result = Validator.ValidPassword(PasswordTxt.Text);
            bool valid = result == -1;

            if (valid)
            {
                // Remove error message
                PasswordErrorMsg.Text = "";
                ApplyValidationStateStyle(PasswordTxt, valid);
            }
            else
            {
                // Put error message
                PasswordErrorMsg.Text = Context.GetString(result);
                ApplyValidationStateStyle(PasswordTxt, false);
            }
            return valid;
        }

        private void ApplyValidationStateStyle(EditText editText, bool valid)
        {
            if (valid)
            {
                if (editText.Tag is Drawable)
                {
                    editText.Background = editText.Tag as Drawable;
                    editText.Tag = null;
                }
            }
            else
            {
                editText.Tag ??= editText.Background;
                editText.SetBackgroundResource(Resource.Drawable.invalid_edit_text_background);
            }
        }

        protected override void OnCreateBtnClick(object sender, EventArgs e)
        {
            if (ValidUsernameTxt() & ValidPasswordTxt())
                base.OnCreateBtnClick(sender, e);
        }
    }
}
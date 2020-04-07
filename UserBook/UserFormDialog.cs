using System;

using Android.App;
using Android.OS;
using Android.Widget;

namespace UserBook
{
    delegate void OnCreateUserCallback(User user);
    class UserFormDialog : Dialog
    {
        public OnCreateUserCallback OnCreatedUser { get; set; }
        public EditText UsernameTxt { get; set; }
        public TextView UsernameErrorMsg { get; set; }
        public EditText PasswordTxt { get; set; }
        public TextView PasswordErrorMsg { get; set; }
        public Button CreateBtn { get; set; }
        public Button CancelBtn { get; set; }

        public UserFormDialog(
            Activity activity,
            OnCreateUserCallback onCreatedUser = null
        ) : base(activity)
        {
            OnCreatedUser = onCreatedUser;
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetTitle("User Form");

            SetContentView(Resource.Layout.user_form_dialog);

            UsernameTxt = FindViewById<EditText>(Resource.Id.username);
            UsernameErrorMsg = FindViewById<TextView>(Resource.Id.usernameErrorMsg);
            PasswordErrorMsg = FindViewById<TextView>(Resource.Id.passwordErrorMsg);
            PasswordTxt = FindViewById<EditText>(Resource.Id.password);
            CreateBtn = FindViewById<Button>(Resource.Id.createBtn);
            CancelBtn = FindViewById<Button>(Resource.Id.cancelBtn);

            CreateBtn.Click += OnCreateBtnClick;

            CancelBtn.Click += OnCancelBtnClick;
        }

        protected virtual void OnCancelBtnClick(object sender, EventArgs e)
        {
            // Close dialog
            Dismiss();
        }

        protected virtual void OnCreateBtnClick(object sender, EventArgs e)
        {
            // Invoke callback if not null
            OnCreatedUser?.Invoke(new User(UsernameTxt.Text, PasswordTxt.Text));
            // Close dialog
            Dismiss();
        }
    }
}
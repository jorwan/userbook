using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;

namespace UserBook
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        UserAdapter UserListViewAdapter { get; set; }
        IUserRepository UserRepository { get; set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            
            SetContentView(Resource.Layout.activity_main);

            // Get user list
            UserRepository = new UserJsonRepository();
            var userList = UserRepository.GetUsers();
            UserListViewAdapter = new UserAdapter(userList);

            // Set user list view
            var userListView = FindViewById<ListView>(Resource.Id.userListView);
            userListView.Adapter = UserListViewAdapter;
            
            // Set floating action button
            var fabBtn = FindViewById<Button>(Resource.Id.fabBtn);
            fabBtn.Click += delegate {
                var userFormDialog = new RealtimeValidableUserFormDialog(this, onCreatedUser: (user) => {
                    // Add user to list view
                    UserListViewAdapter.Add(user);
                });
                userFormDialog.Show();
            };
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        protected override void OnStop()
        {
            UserRepository.Save(UserListViewAdapter.Users);
            base.OnStop();
        }
    }
}
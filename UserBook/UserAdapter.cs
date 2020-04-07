using Android.Views;
using Android.Widget;
using System.Collections.Generic;

namespace UserBook
{
    class UserAdapter : BaseAdapter<User>
    {
        public override User this[int position] => Users[position];

        public List<User> Users { get; private set; }
        public override int Count => Users.Count;

        public UserAdapter(List<User> users = null)
        {
            Users = users ?? new List<User>();
        }

        public override long GetItemId(int position) => position;

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView;
            view ??= LayoutInflater.From(parent.Context).Inflate(Android.Resource.Layout.SimpleListItem2, parent, false);
            view.FindViewById<TextView>(Android.Resource.Id.Text1).Text = this[position].Username;
            view.FindViewById<TextView>(Android.Resource.Id.Text2).Text = this[position].Password;

            return view;
        }

        public void Add(User user)
        {
            Users.Add(user);
            NotifyDataSetChanged();
        }
    }
}
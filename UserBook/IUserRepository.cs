using System.Collections.Generic;

namespace UserBook
{
    public interface IUserRepository
    {
        List<User> GetUsers();
        void Save(List<User> users);
    }
}
using System.IO;
using System.Collections.Generic;

namespace UserBook
{
    public class UserJsonRepository : IUserRepository
    {
        string FileNamePath { get; set; }
        UsersJsonReader Reader { get; set; }
        UsersJsonWriter Writer { get; set; }

        public UserJsonRepository(
            string fileNamePath = null, 
            UsersJsonReader reader = null, 
            UsersJsonWriter writer = null
        ){
            if (string.IsNullOrEmpty(fileNamePath))
            {
                string FileName = "userbook.json";
                string directory = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
                FileNamePath = Path.Combine(directory, FileName);
            }
            else
            {
                FileNamePath = fileNamePath;
            }

            Reader = reader ?? new UsersJsonReader(FileNamePath);
            Writer = writer ?? new UsersJsonWriter(FileNamePath);
        }
        public List<User> GetUsers() => Reader.Read();

        public void Save(List<User> users) => Writer.Write(users);
    }
}
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;
using System;

namespace UserBook
{
    public class UsersJsonReader {
        public string FilePath { get; set; }
        
        public UsersJsonReader(string filePath = null) {
            FilePath = filePath;
        }

        public List<User> Read()
        {
            if (string.IsNullOrEmpty(FilePath))
                throw new Exception("FilePath must be set");

            var userList = new List<User>();
            JsonSerializer serializer = new JsonSerializer();
            serializer.NullValueHandling = NullValueHandling.Ignore;

            if (File.Exists(FilePath))
            {
                using (StreamReader sr = new StreamReader(FilePath))
                using (JsonReader reader = new JsonTextReader(sr))
                {
                    userList = serializer.Deserialize<List<User>>(reader);
                }
            }
            return userList;
        }
    }
}
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;
using System;

namespace UserBook
{
    public class UsersJsonWriter
    {
        public string FilePath { get; set; }

        public UsersJsonWriter(string filePath = null)
        {
            FilePath = filePath;
        }

        public void Write(List<User> users)
        {
            if (string.IsNullOrEmpty(FilePath))
                throw new Exception("FilePath must be set");

            JsonSerializer serializer = new JsonSerializer();
            serializer.NullValueHandling = NullValueHandling.Ignore;

            using (StreamWriter sw = new StreamWriter(FilePath))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                serializer.Serialize(writer, users);
            }
        }
    }
}
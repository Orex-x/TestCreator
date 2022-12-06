using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using TestCreator.Objects;

namespace TestCreator.Service
{
    class Database
    {
        public static string FILE_LOG = "log.txt";

        public static string getData(string path)
        {
            string s = "";
            StreamReader f = new StreamReader(path);
            while (!f.EndOfStream)
            {
                s += f.ReadLine();
            }
            f.Close();
            return s;
        }

        public static void writeInFile(string path, string str, FileMode mode)
        {
            using (FileStream fstream = new FileStream(path, FileMode.OpenOrCreate))
            {
                byte[] array = Encoding.Default.GetBytes(str);
                fstream.Write(array, 0, array.Length);
            }
        }

        public static void DeleteFile(string path)
        {
            File.Delete(path);
        }


        public static void savedata(User user)
        {
            string json = JsonSerializer.Serialize(user);
            writeInFile(FILE_LOG, json, FileMode.OpenOrCreate);
        }

        public static User getdata()
        {
            string json = getData(FILE_LOG);
            User user = JsonSerializer.Deserialize<User>(json);
            return user;

        }
    }
}

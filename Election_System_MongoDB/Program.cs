using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Election_System_MongoDB
{
    class Program
    {
        public static class DbConn
        {
            public static readonly MongoClient client = new MongoClient();
        }

        static void Main(string[] args)
        {
            Menu menu = new Menu();
            menu.StartMenu();
        }
    }
}

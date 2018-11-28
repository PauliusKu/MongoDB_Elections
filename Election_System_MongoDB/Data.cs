using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Election_System_MongoDB
{
    public class Ballot
    {
        public bool Status { get; set; }
        public Candidate Candidate { get; set; }
        public Voter Voter { get; set; }
    }

    public class Candidate
    {
        public string Party { get; set; }
        public int Number_on_Ballot { get; set; }
        public Person Personal_info { get; set; }
    }

    public class Voter
    {
        public string Constituency_name { get; set; }
        public string VotingStatus { get; set; }
        public Person Personal_info { get; set; }
    }

    public class Constituency
    {
        public string Name { get; set; }
        public Address Address { get; set; }
        public string Num_of_voters { get; set; }
        public Person Commision_chairman { get; set; }
    }

    public class Person
    {
        public string Name { get; set; }
        public string Surename { get; set; }
        public string Personal_id { get; set; }
        public Address Address { get; set; }
        public string Phone_num { get; set; }
    }

    public class Address
    {
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
    }

    class Data
    {
        private readonly IMongoDatabase database;

        public Data()
        {
            database = DbConn.client.GetDatabase("elections");
        }

        public void CreateConstituency()
        {
            var collection = database.GetCollection<Constituency>("constituency");

            Console.WriteLine("Please, write values.");

            var doc = new Constituency
            {
            Name = Console.ReadLine(),
                Address = new Address
                {
                    Country = Console.ReadLine(),
                    City = Console.ReadLine(),
                    Street = Console.ReadLine()
                },
                Num_of_voters = Console.ReadLine(),
                Commision_chairman = new Person
                {
                    Name = Console.ReadLine(),
                    Surename = Console.ReadLine(),
                    Personal_id = Console.ReadLine(),
                    Address = new Address
                    {
                        Country = Console.ReadLine(),
                        City = Console.ReadLine(),
                        Street = Console.ReadLine()
                    },
                    Phone_num = Console.ReadLine()
                },
            };
            collection.InsertOne(doc);
        }

        public void CreateVoter()
        {
            var collection = database.GetCollection<Voter>("voter");

            Console.WriteLine("Please, write values.");

            var doc = new Voter
            {
                Constituency_name = Console.ReadLine(),
                VotingStatus = "0",
                Personal_info = new Person
                {
                    Name = Console.ReadLine(),
                    Surename = Console.ReadLine(),
                    Personal_id = Console.ReadLine(),
                    Address = new Address
                    {
                        Country = Console.ReadLine(),
                        City = Console.ReadLine(),
                        Street = Console.ReadLine()
                    },
                    Phone_num = Console.ReadLine()
                },
            };
            collection.InsertOne(doc);
        }

        public void CreateCandidate()
        {
            var collection = database.GetCollection<Candidate>("candidate");

            Console.WriteLine("Please, write values.");

            var doc = new Candidate
            {
                Party = Console.ReadLine(),
                Number_on_Ballot = int.Parse(Console.ReadLine()),
                Personal_info = new Person
                {
                    Name = Console.ReadLine(),
                    Surename = Console.ReadLine(),
                    Personal_id = Console.ReadLine(),
                    Address = new Address
                    {
                        Country = Console.ReadLine(),
                        City = Console.ReadLine(),
                        Street = Console.ReadLine()
                    },
                    Phone_num = Console.ReadLine()
                },
            };
            collection.InsertOne(doc);
        }

        public void CreateBallot()
        {
            Console.WriteLine("Please, write values:");
            Console.WriteLine("1 - First Name");
            Console.WriteLine("2 - Last Name");
            Console.WriteLine("3 - Candidate's Fist Name");
            Console.WriteLine("4 - Candidate's Last Name");

            string FirstName = Console.ReadLine();
            string LastName = Console.ReadLine();

            var voter = GetVoter(FirstName, LastName);

            if (voter.VotingStatus == "0")
            {
                voter.VotingStatus = "1";
                var doc = new Ballot
                {
                    Voter = voter,
                };

                string CanFirstName = Console.ReadLine();
                string CanLastName = Console.ReadLine();


            }
            else Console.WriteLine("Voter already voted");
        }

        private Voter GetVoter(string FirstName, string LastName)
        {
            var collection = database.GetCollection<Voter>("voter");
            var candidate = collection
                .Find(v => v.Personal_info.Name == FirstName && v.Personal_info.Surename == LastName)
                .Limit(1)
                .ToListAsync();
            return candidate.Result.First();
        }
    }
}

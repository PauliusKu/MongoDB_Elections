using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Election_System_MongoDB
{

    public class SimpleReduceResult<T>
    {
        public string Id { get; set; }

        public T Value { get; set; }
    }

    public class Ballot
    {
        public ObjectId _id;
        public bool Status { get; set; }
        public Candidate Candidate { get; set; }
        public Voter Voter { get; set; }
        public string Constituency_name { get; set; }
    }

    public class Candidate
    {
        public ObjectId _id;
        public string Party { get; set; }
        public int Number_on_Ballot { get; set; }
        public int Result { get; set; }
        public Person Personal_info { get; set; }
    }

    public class Voter
    {
        public ObjectId _id;
        public string Constituency_name { get; set; }
        public bool VotingStatus { get; set; }
        public Person Personal_info { get; set; }
    }

    public class Constituency
    {
        public ObjectId _id;
        public string Name { get; set; }
        public Address Address { get; set; }
        public string Num_of_voters { get; set; }
        public Person Commision_chairman { get; set; }
        public Results Results { get; set; }
    }

    public class Results
    {
        public int Num_of_Voters_Voted { get; set; }
        public List<Candidate_Results> Cand_results { get; set; }
    }

    public class Candidate_Results
    {
        public int Num_of_votes { get; set; }
        public Candidate Candid { get; set; }
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

            Console.WriteLine("Please, write values:");
            Console.WriteLine("1 - Name");
            Console.WriteLine("2 - Country");
            Console.WriteLine("3 - City");
            Console.WriteLine("4 - Street");
            Console.WriteLine("5 - Number of voters");
            Console.WriteLine("6 - Commission chairman's first name");
            Console.WriteLine("7 - Commission chairman's last name");

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
                    //Personal_id = Console.ReadLine(),
                    //Address = new Address
                    //{
                    //    Country = Console.ReadLine(),
                    //    City = Console.ReadLine(),
                    //    Street = Console.ReadLine()
                    //},
                    //Phone_num = Console.ReadLine()
                },
            };
            collection.InsertOne(doc);
        }

        public void CreateVoter()
        {
            var collection = database.GetCollection<Voter>("voter");

            Console.WriteLine("Please, write values:");
            Console.WriteLine("1 - Constituency name ");
            Console.WriteLine("2 - Name");
            Console.WriteLine("3 - Surename");
            Console.WriteLine("4 - ID");
            Console.WriteLine("5 - Country");
            Console.WriteLine("6 - City");
            Console.WriteLine("7 - Street");
            Console.WriteLine("8 - Phone number");

            var doc = new Voter
            {
                Constituency_name = Console.ReadLine(),
                VotingStatus = false,
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

            Console.WriteLine("Please, write values:");
            Console.WriteLine("1 - Party ");
            Console.WriteLine("2 - Number on ballot ");
            Console.WriteLine("3 - Name");
            Console.WriteLine("4 - Surename");
            Console.WriteLine("5 - ID");
            Console.WriteLine("6 - Country");
            Console.WriteLine("7 - City");
            Console.WriteLine("8 - Street");
            Console.WriteLine("9 - Phone number");

            var doc = new Candidate
            {
                Party = Console.ReadLine(),
                Number_on_Ballot = Convert.ToInt32(Console.ReadLine()),
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
            var collection = database.GetCollection<Ballot>("ballot");
            var doc = new Ballot();

            Voter voter;
            Candidate candidate;

            Console.WriteLine("Please, write values:");
            Console.WriteLine("1 - First Name");
            Console.WriteLine("2 - Last Name");
            Console.WriteLine("3 - Candidate's Fist Name");
            Console.WriteLine("4 - Candidate's Last Name");

            string FirstName;
            string LastName;
            string CandFirstName;
            string CandLastName;
            string vote;

        WriteVoter:
            Console.WriteLine("Write Voter's First and Last name.");
            FirstName = Console.ReadLine();
            LastName = Console.ReadLine();
            voter = GetVoter(FirstName, LastName);

            if (voter == null)
            {
                Console.WriteLine("Voter not found.");
                goto WriteVoter;
            }

            switch (voter.VotingStatus)
            {
                case true:
                    Console.WriteLine("This Voter already voted.");
                    goto End;
                case false:
                    Console.WriteLine("Voter found.");
                    doc.Voter = voter;
                    goto WriteCandidate;
            }

        WriteCandidate:
            Console.WriteLine("Write Candidate's First and Last name.");

            CandFirstName = Console.ReadLine();
            CandLastName = Console.ReadLine();

            candidate = GetCandidate(CandFirstName, CandLastName);

            if (candidate == null)
            {
                Console.WriteLine("Candidate not found.");
                goto WriteCandidate;
            }

            if (candidate.Number_on_Ballot > 0)
            {
                Console.WriteLine("Candidate selected.");
                goto Vote;
            }

        Vote:
            Console.WriteLine("Please write (y) if you want to commit your vote and (n) if you want to choose another candidate.");
            vote = Console.ReadLine();
            switch (vote)
            {
                case "y":
                    doc.Candidate = candidate;
                    UpdateVoterStatus(doc.Voter.Personal_info.Name, doc.Voter.Personal_info.Surename, true);
                    doc.Constituency_name = doc.Voter.Constituency_name;
                    doc.Voter = null;
                    doc.Status = true;
                    collection.InsertOne(doc);
                    Console.WriteLine("Vote succesfully commited!");
                    goto End;
                case "n":
                    Console.WriteLine("Choose again.");
                    goto WriteCandidate;
                default:
                    Console.WriteLine("Incorrect input.");
                    goto Vote;
            }

        End:;
        }

        public void CalculateResults()
        {
            var Constit = GetConstituencies();
            var CandNames = GetCandidates();

            foreach (var i in Constit)
            {
                Results results = GetResultsByConstit(i.Name, CandNames);
                UpdateConstResults(i.Name, results);

                var list = i.Results.Cand_results.OrderByDescending(o => o.Num_of_votes).ToList();

                Console.WriteLine(i.Name + ": " + i.Results.Num_of_Voters_Voted
                    + " voted, " + i.Num_of_voters + " total voters, First candidate in this Constituency: "
                    + list.First().Candid.Personal_info.Name + " "
                    + list.First().Candid.Personal_info.Surename + " "
                    + list.First().Candid.Party + " "
                    + list.First().Num_of_votes);
            }

            var collection1 = database.GetCollection<Ballot>("ballot");
            var ballot = collection1
                .Find(_ => true).ToList();

            var collection2 = database.GetCollection<Candidate>("candidate");
            var candidate = collection2
                .Find(_ => true).ToList();

            Console.WriteLine("---------------------------------------------");

            foreach (var i in candidate)
            {
                int sum = 0;
                foreach (var j in ballot)
                {
                    if(i.Number_on_Ballot == j.Candidate.Number_on_Ballot)
                    {
                        sum++;
                    }
                }
                UpdateCandidateResult(i.Number_on_Ballot, sum);
                Console.WriteLine(i.Personal_info.Name + " " + i.Personal_info.Surename + 
                    " " + i.Party + ": " + sum + " votes" );
            }

            //MapReduce();

        }

        private void MapReduce()
        {
            var collection = database.GetCollection<Candidate>("candidate");

            var map = @" function m() {
            //for(var i in this.Results.Cand_results)
            //{
            //    //emit(this._id, this.Results.Cand_results[i].Num_of_votes)
            //}
            }";

            var reduce = @" function r(key, value) {
                //return Array.sum(value)
            }";

            var result = collection.MapReduce<int>(map, reduce);
            
            foreach(var i in result.Current)
            {
                Console.WriteLine(i);
            }
        }

        private List<Constituency> GetConstituencies()
        {
            var collection = database.GetCollection<Constituency>("constituency");

            var constit = collection
                .Find(_ => true).ToList();
            return constit;
        }

        private List<Candidate> GetCandidates()
        {
            var collection = database.GetCollection<Candidate>("candidate");

            var cand = collection
                .Find(_ => true).ToList();

            return cand;
        }

        private Results GetResultsByConstit(string constitName, List<Candidate> cand)
        {
            Results rez = new Results()
            {
                Num_of_Voters_Voted = 0,
                Cand_results = new List<Candidate_Results>()
            };
            foreach(var itr in cand)
            {
                Candidate_Results cand_rez = GetCandRezInConst(constitName, itr);
                rez.Cand_results.Add(cand_rez);
                rez.Num_of_Voters_Voted += cand_rez.Num_of_votes;
            }
            return rez;
        }

        private Candidate_Results GetCandRezInConst(string constitName, Candidate cand)
        {
            Candidate_Results cand_rez = new Candidate_Results
            {
                Num_of_votes = CountBallots(constitName, cand.Number_on_Ballot),
                Candid = cand
            };
            
            return cand_rez;
        }

        private int CountBallots(string constitName, int candNumb)
        {

            var collection = database.GetCollection<Ballot>("ballot");
            var ballot = collection
                .Find(v => v.Constituency_name == constitName && v.Candidate.Number_on_Ballot == candNumb)
                .ToListAsync();

            return ballot.Result.Count;
        }

        private void UpdateVoterStatus(string firstname, string lastname, bool setToStatus)
        {
            var collection = database.GetCollection<Voter>("voter");

            var updateDef = Builders<Voter>.Update.Set(v => v.VotingStatus, setToStatus);

            collection.UpdateOne(v => v.Personal_info.Name == firstname && v.Personal_info.Surename == lastname, updateDef);
        }

        private Voter GetVoter(string firstName, string lastName)
        {
            var collection = database.GetCollection<Voter>("voter");
            var candidate = collection
                .Find(v => v.Personal_info.Name == firstName && v.Personal_info.Surename == lastName)
                .Limit(1)
                .ToListAsync();
            if (candidate.Result.Count == 0)
            {
                return null;
            }
            else return candidate.Result.First();
        }

        private Candidate GetCandidate(string firstName, string lastName)
        {
            var collection = database.GetCollection<Candidate>("candidate");
            var voter = collection
                .Find(v => v.Personal_info.Name == firstName && v.Personal_info.Surename == lastName)
                .Limit(1)
                .ToListAsync();
            if (voter.Result.Count == 0)
            {
                return null;
            }
            else return voter.Result.First();
        }

        private void UpdateConstResults(string name, Results results)
        {
            var collection = database.GetCollection<Constituency>("constituency");

            var updateDef = Builders<Constituency>.Update.Set(v => v.Results, results);

            collection.UpdateOne(v => v.Name == name, updateDef);
        }

        private void UpdateCandidateResult(int ballot_num, int result)
        {
            var collection = database.GetCollection<Candidate>("candidate");

            var updateDef = Builders<Candidate>.Update.Set(v => v.Result, result);

            collection.UpdateOne(v => v.Number_on_Ballot == ballot_num, updateDef);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Election_System_MongoDB
{
    class Functions
    {
        public void RegisterCandidate()
        {
            Data data = new Data();
            data.CreateCandidate();
            Console.WriteLine("Candidate Registred");
        }

        public void RegisterVoter()
        {
            Data data = new Data();
            data.CreateVoter();
            Console.WriteLine("Voter Registred.");
        }

        public void CreateConstituency()
        {
            Data data = new Data();
            data.CreateConstituency();
            Console.WriteLine("Constituency created.");
        }

        public void Vote()
        {
            Data data = new Data();
            data.CreateBallot();
            Console.WriteLine("Voted");
        }

        public void CalcResult()
        {
            Data data = new Data();
            data.CalculateResults();
            Console.WriteLine("Done");
        }

        public void InitDatabase()
        {

        }
    }
}

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
            Console.WriteLine("Constituency created");
        }

        public void RegisterVoter()
        {
            Data data = new Data();
            data.CreateVoter();
            Console.WriteLine("Constituency created");
        }

        public void CreateConstituency()
        {
            Data data = new Data();
            data.CreateConstituency();
            Console.WriteLine("Constituency created");
        }

        public void Vote()
        {

        }

        public void CalcResult()
        {

        }

        public void InitDatabase()
        {

        }
    }
}

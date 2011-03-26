using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DozorySharp;

namespace TestApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            DozoryApi api = new DozoryApi();

            OrgAuth auth = new OrgAuth(152,"*****");

            List<int> arg = new List<int>();
            arg.Add(40558);
            arg.Add(137495);
            
            //string url = UrlConstuctor.GetOrgMembersStatusUrl(152, "tOwPW4GB", arg,DateTime.Now.AddDays(1));

            Dictionary<int, PersonInfo> result = DozoryApi.GetPersonsInfo(arg);

            //OrgInfo result = DozoryApi.GetOrgMembers(152);

            //string url = UrlConstuctor.getStorageUrl(auth, DateTime.Now, StorageType.Main);

            //List<StorageOperation> result = DozoryApi.GetStorageDay(auth, DateTime.Now.AddDays(-1), StorageType.Main);

            //string url = UrlConstuctor.GetTreasureUrl(auth, DateTime.Now, TreasureType.Money);

            //List<TreasureOperation> result = DozoryApi.GetTreasureDay(auth, DateTime.Now.AddDays(-1), TreasureType.Money);

            //List<string> llist = Logs.GetLogsByDay(DateTime.Now);

            Console.WriteLine("100500");
        }
    }
}

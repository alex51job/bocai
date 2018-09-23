using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;


namespace GetLBetData
{
    class MatchDataSync
    {

        public string GetMatchData() {

            string URL_MatchListAPI = System.Configuration.ConfigurationSettings.AppSettings["API_MatchList"];
            string ret = HttpResponse.GetHttpResponseJson(URL_MatchListAPI, null);
            return ret;
            
        }

    }
}

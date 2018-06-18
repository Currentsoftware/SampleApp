using System;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json;
using RestSharp;
using SampleApi.Common.Data;
using SampleApi.Common.Entities;
using SampleApi.Data.Entities;

namespace SampleApi.Data.DataProviders
{
    /// <summary>
    /// A REST data provider for TVMaze
    /// </summary>
    public class TVMazeRESTDataProvider : ITVMazeDataProvider
    {
        private const string baseUrl = "http://api.tvmaze.com";
        private const string userAgent = "SampleApi/1.0";
        private IRestClient restClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="TVMazeRESTDataProvider"/> class.
        /// </summary>
        public TVMazeRESTDataProvider()
        {
            this.restClient = new RestClient();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TVMazeRESTDataProvider"/> class.
        /// </summary>
        public TVMazeRESTDataProvider(IRestClient restClient)
        {
            this.restClient = restClient;
        }

        /// <summary>
        /// Gets all shows
        /// </summary>
        /// <returns>A List of Show objects</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1804:RemoveUnusedLocals", MessageId = "response")]
        public List<Show> GetAllShows()
        {
            var request = new RestRequest(Method.GET);

            request.AddHeader("User-Agent", userAgent);
            request.AddHeader("Cache-Control", "no-cache");

            IRestResponse response = this.restClient.Execute(request);

            //var showResponse = JsonConvert.DeserializeObject<IEnumerable<ShowResponse>>(response.Content);
            
            return null;
        }

        /// <summary>
        /// Gets the details of a single Show
        /// </summary>
        /// <param name="showId">The ID of the show</param>
        /// <returns>A Show object</returns>
        public Show GetShowDetails(int showId)
        {

            this.restClient.BaseUrl = new System.Uri(baseUrl +  "/shows/" + showId.ToString(CultureInfo.InvariantCulture) );

            var request = new RestRequest(Method.GET);

            request.AddHeader("User-Agent", userAgent);
            request.AddHeader("Cache-Control", "no-cache");

            IRestResponse response = this.restClient.Execute(request);

            var showResponse = JsonConvert.DeserializeObject<ShowResponse>(response.Content);

            var show = new Show();
            show.Id = showResponse.Id;
            show.Name = showResponse.Name;
            show.Cast.AddRange(GetCastMembers(showId));

            return show;
        }

        /// <summary>
        /// Gets the cast members of a single Show
        /// </summary>
        /// <param name="showId">The ID of the show</param>
        /// <returns>A List of CastMembers</returns>
        public List<CastMember> GetCastMembers(int showId)
        {
            var castMembers = new List<CastMember>();

            this.restClient.BaseUrl = new System.Uri(baseUrl + "/shows/" + showId.ToString(CultureInfo.InvariantCulture) + "/cast");

            var request = new RestRequest(Method.GET);

            request.AddHeader("User-Agent", userAgent);
            request.AddHeader("Cache-Control", "no-cache");

            IRestResponse response = this.restClient.Execute(request);

            var castResponse = JsonConvert.DeserializeObject<IEnumerable<CastResponse>>(response.Content);

            foreach(var c in castResponse)
            {
                var member = new CastMember();
                member.Id = Convert.ToInt32(c.Person["id"].ToString(), CultureInfo.InvariantCulture);
                member.Name = c.Person["name"].ToString();

                if (c.Person["birthday"] != null)
                {
                    var birthDate = c.Person["birthday"].ToString();
                    DateTime parsedDate;

                    if (DateTime.TryParseExact(birthDate, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate))
                    {
                        member.Birthdate = parsedDate;
                    }

                }

                castMembers.Add(member);
            }

            return castMembers;
        }
    }
}

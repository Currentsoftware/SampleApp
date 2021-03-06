﻿using SampleApi.Common.Data;
using SampleApi.Common.Entities;
using SampleApi.Common.Logic;
using System;
using System.Collections.Generic;

namespace SampleApi.BLL.Managers
{
    /// <summary>
    /// A service object to retrieve data from the backend
    /// </summary>
    public class ShowManager : IShowManager
    {
        // use same paging strategy
        // get all cast members, in parallel, watch time
        // not a performant solution by design
        private ITVMazeDataProvider dataProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="ShowManager"/> class.
        /// </summary>
        public ShowManager(ITVMazeDataProvider dataProvider)
        {
            this.dataProvider = dataProvider;
        }

        /// <summary>
        /// Gets a page of Show objects from the data source
        /// </summary>
        /// <param name="pageId">The id of the page to get</param>
        /// <returns>A collection containing no more than 250 objects</returns>
        public IEnumerable<Show> GetShows(int pageId)
        {
            var shows = this.dataProvider.GetShows(pageId);
            return shows;
        }

        /// <summary>
        /// Returns a collection of CastMembers for a given show
        /// </summary>
        /// <param name="showId">The id of the show</param>
        /// <returns>A collection of CastMembers</returns>
        public IEnumerable<CastMember> GetCastMembers(int showId)
        {
            var members = this.dataProvider.GetCastMembers(showId);
            return members;
        }
    }
}

﻿using System.Collections.Generic;
using SampleApi.Common.Entities;

namespace SampleApi.Common.Data
{
    /// <summary>
    /// Describes the interface to a TVMaze dataprovider
    /// </summary>
    public interface ITVMazeDataProvider
    {
        /// <summary>
        /// Gets the details of a single Show
        /// </summary>
        /// <param name="showId">The ID of the show</param>
        /// <returns>A Show object</returns>
        Show GetShowDetails(int showId);

        /// <summary>
        /// Gets all shows
        /// </summary>
        /// <returns>A List of Show objects</returns>
        List<Show> GetAllShows();
    }
}
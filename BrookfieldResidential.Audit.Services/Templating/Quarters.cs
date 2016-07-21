//-----------------------------------------------------------------------
// <copyright file="Quarters.cs" company="Procure Development">
//     Copyright (c) Procure Development. All rights reserved.
// </copyright>
// <author>Victor Procure</author>
//-----------------------------------------------------------------------
namespace ProcureDevelopment.Audit.Services.Templating
{
    using System.Collections.Generic;

    /// <summary>
    ///     Static class that returns all four quarters
    /// </summary>
    public static class Quarters
    {
        /// <summary>
        ///     Gets the quarters.
        /// </summary>
        /// <returns>Four quarters to choose from</returns>
        public static List<string> GetQuarters()
        {
            var quarterList = new List<string>();

            for (int i = 1; i <= 4; i++)
            {
                quarterList.Add(string.Format("Q{0}", i));
            }

            return quarterList;
        }
    }
}
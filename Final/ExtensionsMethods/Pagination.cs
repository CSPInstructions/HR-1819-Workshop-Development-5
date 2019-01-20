// Import the required libraries
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

// Enter the namespace for the extension methodes
namespace WorkshopDev6B.ExtensionsMethods {
    // Create a class for storing single pages
    public class Page<T> {
        // Give the class some attributes that store important data
        public int Index { get; set; }
        public List<T> Items { get; set; }
        public int TotalPages { get; set; }
    }

    /// <summary>
    /// The pagination class
    /// This class will hold the actual pagination function
    /// The only reason this class exists is because we have to place the function somewhere
    /// </summary>
    public static class Pagination {
        /// <summary>
        /// The pagination function
        /// This function create a page for a certain request
        /// </summary>
        /// <returns>A page with the requested data, returns null of nothing was found</returns>
        /// <param name="list">Contains the reference to the table in the database</param>
        /// <param name="pageIndex">Contains the index of the current page</param>
        /// <param name="pageSize">Contains the size of a single page</param>
        /// <param name="orderSelection">A function that allows for specific ordering of data</param>
        /// <typeparam name="T">The class of the DbSet</typeparam>
        public static Page<T> GetPage<T>( this DbSet<T> list, int pageIndex, int pageSize, Func<T, object> orderSelection ) 
        where T : class {
            // Get the data that corresponds to the given information 
            List<T> result = list
                .OrderBy( orderSelection )
                .Skip( pageIndex * pageSize )
                .Take( pageSize )
                .ToList();

            // Check whether there is a result
            if ( result == null || !result.Any() ) {
                // If there is no result, return null
                return null;
            } else {
                // Calculate the total amount of items in the database
                int totalItems = list.Count();

                // Calculate the total amount of pages for the number of items inside the database 
                int totalPages = totalItems / pageSize;

                // Check whether there is only a single page available
                if (totalItems < pageSize) {
                    // If there is, the number of total pages becomes 1
                    totalPages = 1;
                }

                // Return a new page with the data collected
                return new Page<T> {
                    Index = pageIndex,
                    Items = result,
                    TotalPages = totalPages
                };
            }
        }
    }
}

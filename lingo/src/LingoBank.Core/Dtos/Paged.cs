using System;
using System.Collections.Generic;
using LingoBank.Core.Constants;

namespace LingoBank.Core.Dtos;

/// <summary>
/// Paginated sub-list of data.
/// </summary>
/// <typeparam name="T"></typeparam>
public class Paged<T> where T : class
{
    /// <summary>
    /// The data fetched from the database.
    /// </summary>
    public IList<T>? Data { get; }
    
    /// <summary>
    /// Total number of items/data in the database.
    /// </summary>
    public int Total { get; }

    /// <summary>
    /// Current page number.
    /// </summary>
    public int PageNumber { get; }
    
    /// <summary>
    /// The next page of data.
    /// </summary>
    public int NextPage { get; }
    
    /// <summary>
    /// The previous page of data.
    /// </summary>
    public int PrevPage { get; }

    public Paged(IList<T>? data, int total, int pageNumber)
    {
        Data = data;
        Total = total;
        PageNumber = pageNumber;
        
        double result = total / (double) CoreConstants.PagedNumberOfItemsPerPage;
        NextPage = (int) Math.Ceiling(result);
        PrevPage = NextPage == 1 ? 1 : NextPage - 1;
    }
}
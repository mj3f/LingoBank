using System.Collections.Generic;

namespace LingoBank.Core.Dtos;

public class Paged<T> where T : class
{
    public IList<T>? Data { get; }
    
    public int Total { get; }

    public int PageNumber { get; }
    
    public int NextPage { get; }
    
    public int PrevPage { get; }

    public Paged(IList<T>? data, int total, int pageNumber)
    {
        Data = data;
        Total = total;
        PageNumber = pageNumber;
        NextPage = pageNumber + 1 < total ? pageNumber + 1 : total;
        PrevPage = pageNumber - 1 > 0 ? pageNumber - 1 : 1;
    }
}
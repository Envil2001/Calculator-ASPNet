namespace WebApp.Models;

public class PagingListAsync<T>
{
    public IAsyncEnumerable<T> Data { get; }
    public int TotalItems { get; }
    public int TotalPages { get; }
    public int Page { get; }
    public int Size { get; }
    public bool IsFirst { get; }
    public bool IsLast { get; }
    public bool IsNext { get; }
    public bool IsPrevious { get; }

    private PagingListAsync(Func<int, int, IAsyncEnumerable<T>> dataGenerator, int totalItems, int page, int size)
    {
        TotalItems = totalItems;
        Page = page;
        Size = size;
        TotalPages = (int)Math.Ceiling(totalItems / (double)size);
        IsFirst = page <= 1;
        IsLast = page >= TotalPages;
        IsNext = !IsLast;
        IsPrevious = !IsFirst;
        Data = dataGenerator(page, size);
    }

    public static PagingListAsync<T> Create(Func<int, int, IAsyncEnumerable<T>> data, int totalItems, int page, int size)
    {
        return new PagingListAsync<T>(data, totalItems, page, size);
    }
}
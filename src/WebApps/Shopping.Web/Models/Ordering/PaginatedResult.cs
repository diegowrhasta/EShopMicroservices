﻿namespace Shopping.Web.Models.Ordering;

public class PaginatedResult<TEntity>(
    int pageIndex,
    int pageSize,
    long totalCount,
    IEnumerable<TEntity> data
)
    where TEntity : class
{
    public int PageIndex { get; } = pageIndex;
    public int PageSize { get; } = pageSize;
    public long TotalCount { get; } = totalCount;
    public IEnumerable<TEntity> Data { get; } = data;
}

﻿using CleanArchitecture.Application.Abstractions.Messaging;
using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Domain.Shared;

namespace CleanArchitecture.Application.Users.GetusersDapperPagination
{
    public sealed record GetusersDapperPaginationQuery : PaginationParams, IQuery<PagedDapperResults<UserPaginationData>>;
}

﻿using CleanArchitecture.Application.Abstractions.Data;
using CleanArchitecture.Application.Abstractions.Messaging;
using CleanArchitecture.Domain.Abstractions;
using Dapper;
using System.Text;

namespace CleanArchitecture.Application.Users.GetusersDapperPagination
{
    internal sealed class GetusersDapperPaginationQueryHandler :
        IQueryHandler<GetusersDapperPaginationQuery, PagedDapperResults<UserPaginationData>>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;
        public GetusersDapperPaginationQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }
        public async Task<Result<PagedDapperResults<UserPaginationData>>> Handle(GetusersDapperPaginationQuery request, CancellationToken cancellationToken)
        {


            using var connection = _sqlConnectionFactory.CreateConnection();

            var builder = new StringBuilder("""
                 SELECT 
                      usr.email, rl.name AS role, p.nombre  as permiso
                      FROM users usr
                          LEFT JOIN users_roles usrl ON usr.id = usrl.user_id
                          LEFT JOIN roles rl ON rl.id = usrl.role_id  
                          LEFT JOIN roles_permissions rp ON rl.id = rp.role_id
                          LEFT JOIN permissions p ON  p.id = rp.permission_id
                """);

            var search = string.Empty;
            var whereStatement = string.Empty;
            if (!string.IsNullOrEmpty(request.Search))
            {
                search = "%" + EncodeForLike(request.Search) + "%";
                whereStatement = $@" WHERE rl.name LIKE @Search ";
                //OR usr.email LIKE '{search}' 
                //OR p.nombre LIKE '{search}'";
                builder.AppendLine(whereStatement);
            }

            var orderBy = request.OrderBy;
            if (string.IsNullOrEmpty(orderBy))
            {
                var orderStatement = string.Empty;
                var orderAsc = request.OrderAsc ? "ASC" : "DESC";
                switch (orderBy)
                {
                    case "email":
                        orderStatement = $" ORDER BY usr.email {orderAsc}"; break;
                    case "role":
                        orderStatement = $" ORDER BY rl.name {orderAsc}"; break;
                    default:
                        orderStatement = $" ORDER BY usr.email {orderAsc}"; break;
                }
                builder.AppendLine(orderStatement);
            }

            builder.AppendLine(" LIMIT @PageSize OFFSET @Offset;");

            builder.AppendLine("""                
                     SELECT 
                          COUNT(*)
                      FROM users usr
                          LEFT JOIN users_roles usrl ON usr.id = usrl.user_id
                          LEFT JOIN roles rl ON rl.id = usrl.role_id  
                          LEFT JOIN roles_permissions rp ON rl.id = rp.role_id
                          LEFT JOIN permissions p ON  p.id = rp.permission_id                
                """);
            builder.AppendLine(whereStatement);
            builder.AppendLine(";");

            using var multi = await connection.QueryMultipleAsync(builder.ToString(), new
            {
                PageSize = request.PageSize,
                Offset = request.PageSize * (request.PageNumber - 1),
                Search = search
            });

            var items = await multi.ReadAsync<UserPaginationData>().ConfigureAwait(false);
            var totalItems = await multi.ReadFirstOrDefaultAsync<int>().ConfigureAwait(false);
            var result = new PagedDapperResults<UserPaginationData>(totalItems, request.PageNumber, request.PageSize)
            {
                Items = items
            };

            return result;

        }

        private string EncodeForLike(string value)
        {
            return value.Replace("'", "''").Replace("%", "[%]").Replace("[", "[]]");
        }
    }

}

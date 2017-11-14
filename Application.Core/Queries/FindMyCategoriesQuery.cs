using System;
using System.Collections.Generic;
using Application.Common;
using Application.Core.Dtos;

namespace Application.Core.Queries
{
    public class FindMyCategoriesQuery : IQuery<ICollection<CategoryDto>>, ICachedQuery
    {
        public Guid UserId { get; set; }
        public string NameOfUniqueProperty { get; } = nameof(UserId);
        public int DurationMinutes { get; } = 120;
    }
}
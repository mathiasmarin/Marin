using System;
using System.Collections.Generic;
using Application.Common;
using Application.Core.Dtos;

namespace Application.Core.Queries
{
    public class FindMyCategoriesQuery : IQuery<ICollection<CategoryDto>>
    {
        public Guid UserId { get; set; }
    }
}
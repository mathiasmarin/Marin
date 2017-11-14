using System;
using System.Collections.Generic;
using Application.Common;
using Application.Core.Queries;

namespace Application.Core.Commands
{
    public class AddCategoriesCommand: ICommand, ICacheRemoverCommand
    {
        public List<string> Categories { get; set; }
        public Guid UserId { get; set; }
        public ICachedQuery Query { get; } = new FindMyCategoriesQuery();
    }
}

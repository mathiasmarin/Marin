using System;
using Application.Common;

namespace Application.Core.Commands
{
    public class DeleteCategoryCommand : ICommand
    {
        public Guid Id { get; set; }
    }
}
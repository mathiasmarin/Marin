using System.Collections.Generic;
using Application.Common;

namespace Application.Core.Commands
{
    public class AddCategoriesCommand: ICommand
    {
        public List<string> Categories { get; set; }
        public string UserName { get; set; }
    }
}

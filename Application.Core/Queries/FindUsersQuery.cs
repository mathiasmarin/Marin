using Application.Common;
using Application.Core.Dtos;

namespace Application.Core.Queries
{
    public class FindUserQuery: IQuery<UserDto>
    {
        public string UserName { get; set; }
    }
}

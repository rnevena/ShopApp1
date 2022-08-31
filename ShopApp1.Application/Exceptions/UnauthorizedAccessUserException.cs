using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp1.Application.Exceptions
{
    public class UnAuthorizedAccessUserException : Exception
    {
        public UnAuthorizedAccessUserException(IApplicationActor actor, string UseCaseName)
            : base($"User with identity: {actor.Identity} with id: {actor.Id} has tried to execute Use Case {UseCaseName}")
        {

        }
    }
}

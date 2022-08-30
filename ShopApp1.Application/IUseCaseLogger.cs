using System;
using System.Collections.Generic;
using System.Text;

namespace ShopApp1.Application
{
    public interface IUseCaseLogger
    {
        void Log(IUseCase useCase, IApplicationActor applicationActor, object useCaseData);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicMicroservice.Application.Common.Interfaces.CQRS;

public interface ICommand
{
    
}

public interface ICommand<out TResponse>: IBaseCommand
{
    
}

public interface IBaseCommand
{
        
}
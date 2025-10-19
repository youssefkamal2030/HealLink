using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using healLink.Application.Commands.Connections;
using MediatR;

namespace healLink.Application.Handlers.Connection
{
    public class CreateConnectionRequestCommandHandler : IRequestHandler<CreateConnectionRequestCommand, CreateConnectionRequestResponse>
    {
    }
}

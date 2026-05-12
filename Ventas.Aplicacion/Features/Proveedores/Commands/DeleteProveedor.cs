using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ventas.Aplicacion.Interfaces;

namespace Ventas.Aplicacion.Features.Proveedores.Commands
{
    public record DeleteProveedorCommand(int Id) : IRequest<bool>;

    public class DeleteProveedorHandler
    : IRequestHandler<DeleteProveedorCommand, bool>
        {
            private readonly IProveedorRepository _repo;

            public DeleteProveedorHandler(IProveedorRepository repo)
            {
                _repo = repo;
            }

            public async Task<bool> Handle(
                DeleteProveedorCommand request,
                CancellationToken cancellationToken)
            {
                return await _repo.DeleteProveedorAsync(request.Id);
            }
        }
    }


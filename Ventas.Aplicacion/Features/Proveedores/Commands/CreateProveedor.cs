using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ventas.Aplicacion.Features.Proveedores.Commands;
using Ventas.Aplicacion.Modelos;
using Ventas.Aplicacion.Interfaces;

namespace Ventas.Aplicacion.Features.Proveedores.Commands
{
    public record CreateProveedorCommand(
    string RazonSocial,
    string RUC,
    string? Telefono,
    string? Email
) : IRequest<int>;
}
public class CreateProveedorValidator : AbstractValidator<CreateProveedorCommand>
{
    public CreateProveedorValidator()
    {
        RuleFor(x => x.RazonSocial)
            .NotEmpty()
            .MaximumLength(150);

        RuleFor(x => x.RUC)
            .NotEmpty()
            .Length(11)
            .Matches("^[0-9]+$");

        RuleFor(x => x.Email)
            .EmailAddress()
            .When(x => !string.IsNullOrEmpty(x.Email));
    }
}
public class CreateProveedorHandler
    : IRequestHandler<CreateProveedorCommand, int>
{
    private readonly IProveedorRepository _repo;

    public CreateProveedorHandler(IProveedorRepository repo)
    {
        _repo = repo;
    }

    public async Task<int> Handle(
        CreateProveedorCommand request,
        CancellationToken cancellationToken)
    {
        var existe = await _repo.ExisteProveedorAsync(request.RazonSocial);

        if (existe)
            throw new Exception("El proveedor ya existe");

        var proveedor = new Proveedor
        {
            RazonSocial = request.RazonSocial,
            RUC = request.RUC,
            Telefono = request.Telefono,
            Email = request.Email,
            Estado = true
        };

        var result = await _repo.AddProveedorAsync(proveedor);

        return result.IdProveedor;
    }
}
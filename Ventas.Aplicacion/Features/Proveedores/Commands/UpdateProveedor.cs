using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ventas.Aplicacion.Features.Proveedores.Commands;
using Ventas.Aplicacion.Features.Proveedores.DTOs;
using Ventas.Aplicacion.Interfaces;

namespace Ventas.Aplicacion.Features.Proveedores.Commands
{
    public record UpdateProveedorCommand(
    int Id,
    string RazonSocial,
    string RUC,
    string? Telefono,
    string? Email
) : IRequest<ProveedorResponseDTO?>;
}
public class UpdateProveedorValidator : AbstractValidator<UpdateProveedorCommand>
{
    public UpdateProveedorValidator()
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
public class UpdateProveedorHandler
    : IRequestHandler<UpdateProveedorCommand, ProveedorResponseDTO?>
{
    private readonly IProveedorRepository _repo;

    public UpdateProveedorHandler(IProveedorRepository repo)
    {
        _repo = repo;
    }

    public async Task<ProveedorResponseDTO?> Handle(
        UpdateProveedorCommand request,
        CancellationToken cancellationToken)
    {
        var proveedor = await _repo.GetProveedorByIdAsync(request.Id);

        if (proveedor == null)
            return null;

        proveedor.RazonSocial = request.RazonSocial;
        proveedor.RUC = request.RUC;
        proveedor.Telefono = request.Telefono;
        proveedor.Email = request.Email;

        await _repo.UpdateProveedorAsync(proveedor);

        return new ProveedorResponseDTO
        {
            IdProveedor = proveedor.IdProveedor,
            RazonSocial = proveedor.RazonSocial,
            RUC = proveedor.RUC,
            Telefono = proveedor.Telefono,
            Email = proveedor.Email,
            Estado = proveedor.Estado
        };
    }
}

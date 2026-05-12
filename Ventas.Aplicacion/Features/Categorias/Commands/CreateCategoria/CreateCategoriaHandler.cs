using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ventas.Aplicacion.Features.Categorias.DTOs;
using Ventas.Aplicacion.Features.Marcas.Commands.CreateMarca;
using Ventas.Aplicacion.Modelos;
using Ventas.Aplicacion.Interfaces;

namespace Ventas.Aplicacion.Features.Categorias.Commands.CreateCategoria
{
    public class CreateCategoriaHandler:IRequestHandler<CreateCategoriaCommand,int>
    {
        private readonly ICategoriaRepository _repository;
        public CreateCategoriaHandler(ICategoriaRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> Handle(
            CreateCategoriaCommand request,
            CancellationToken cancellationToken)
        {
            var categoria = new Categoria
            {
                Nombre = request.Nombre,
                Descripcion = request.Descripcion,
                ColorHex = request.ColorHex,
                Estado = true
            };

            await _repository.AddCategoriaAsync(categoria);

            return categoria.IdCategoria;
            
        }
    }
}

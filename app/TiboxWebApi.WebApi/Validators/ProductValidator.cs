using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TiboxWebApi.Models;

namespace TiboxWebApi.WebApi.Validators
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            //Valdiaciones lamda
            ValidatorOptions.CascadeMode = CascadeMode.StopOnFirstFailure;
            RuleFor(p => p.ProductName).NotNull().NotEmpty().Length(1, 50).WithMessage("El nombre del producto es requerido");
            RuleFor(p => p.SupplierId).NotNull().GreaterThan(0).WithName("Proveedor").WithMessage("No a seleccionado un proveedor");
            //Validacion en caso el precio sea mayor que 0
            RuleFor(p => p.UnitPrice).GreaterThan(0).WithName("Precio unitario").WithMessage("Costo tiene que se mayor que cero");
            When(p => p.UnitPrice > 0, () =>
            {
                RuleFor(p => p.UnitPrice).LessThan(100000).WithName("Precio unitario").WithMessage("Costo muy elevado");
            });

            When(p => !string.IsNullOrWhiteSpace(p.Package), () =>
            {
                RuleFor(p => p.Package).Length(1, 30).WithMessage("El nombre del paquete excedio el limite permitido");
            });
        }
    }
}
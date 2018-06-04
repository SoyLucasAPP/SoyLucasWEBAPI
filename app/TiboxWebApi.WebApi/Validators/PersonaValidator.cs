using FluentValidation;
using TiboxWebApi.Models;

namespace TiboxWebApi.WebApi.Validators
{
    public class PersonaValidator: AbstractValidator<Persona>
    {
        public PersonaValidator()
        {
            ValidatorOptions.CascadeMode = CascadeMode.StopOnFirstFailure;
            RuleFor(p => p.nCodigoVerificador).NotNull().WithName("Codigo Veirificador").WithMessage("Código verificador es requerido.");

            RuleFor(p => p.cNombres).NotNull().WithName("Nombres").WithMessage("Nombre es requerido")
                .NotEmpty().WithMessage("Nombre es requerido")
                .Length(1, 50).WithMessage("Nombre no debe de exceder de las 100 letras.");

            RuleFor(p => p.cApePat).NotNull().WithName("Ape. Paterno").WithMessage("Apellido paterno es requerido")
                .NotEmpty().WithMessage("Apellido paterno es requerido")
                .Length(1, 50).WithMessage("Apellido paterno no debe de exceder de las 100 letras");

            RuleFor(p => p.cApeMat).NotNull().WithName("Ape. Materno").WithMessage("Apellido materno es requerido")
                .NotEmpty().WithMessage("Apellido materno es requerido")
                .Length(1, 100).WithMessage("Apellido materno no debe de exceder de las 100 letras.");

            RuleFor(p => p.nTipoDoc).NotNull().WithName("Tipo Documento").WithMessage("Tipo documento es requerido")
                .GreaterThan(0).WithMessage("Tipo de documento es requerido");

            RuleFor(p => p.nNroDoc).NotNull().WithName("Documento").WithMessage("Documento es requerido")
                .NotEmpty().WithMessage("Documento es requerido")
                .Length(1, 8).WithMessage("Documento debe de tener 8 digitos.");

            RuleFor(p => p.cCelular).NotNull().WithName("Celular").WithMessage("Celular es requerido")
                .NotEmpty().WithMessage("Celular es requerido")
                .Length(1, 9).WithMessage("Celular debe de tener 9 digitos");

            RuleFor(p => p.cEmail).NotNull().WithName("Email").WithMessage("Email no puede estar vacio")
                .EmailAddress().WithMessage("Email incorrecto");

            RuleFor(p => p.cConfirmaEmail).NotNull().WithName("Email confirmación").WithMessage("Email de confirmacion es requerido")
                .EmailAddress().WithMessage("Email de confirmacion es incorrecto")
                .Equal(p => p.cEmail).WithMessage("Los email no coinciden");

            RuleFor(p => p.cCodZona).NotNull().WithName("Zona").WithMessage("Zona es requerido")
                .NotEmpty().WithMessage("Zona es requerido").
                Length(1, 15).WithMessage("Zona no debe de exceder de las 15 letras");

            RuleFor(p => p.nTipoResidencia).NotNull().WithName("Tipo de residencia").WithMessage("Tipo de residencia es requerido")
                .NotEmpty().WithMessage("Tipo de residencia es requerido");

            RuleFor(p => p.nSexo).NotNull().WithName("Tipo de sexo").WithMessage("Tipo de sexo es requerido")
                .NotEmpty().WithMessage("Tipo de sexo es requerido");

            //RuleFor(p => p.cTelefono).NotNull().WithName("Teléfono").WithMessage("Teléfono es requerido")
            //    .NotEmpty().WithMessage("Teléfono es requerido")
            //    .Length(1, 9).WithMessage("Teléfeno no debe de tener exceder de los 9 digitos");

            RuleFor(p => p.dFechaNacimiento).NotNull().WithName("Fecha de nacimiento").WithMessage("Fecha de nacimiento es requerido")
                .NotEmpty().WithMessage("Fecha de nacimiento de requerido");

            RuleFor(p => p.nEstadoCivil).NotNull().WithName("Estado civil").WithMessage("Estado civil es requerido")
                .NotEmpty().WithMessage("Estado civil es requerido");

            RuleFor(p => p.nDirTipo1).NotNull().WithName("Dirección 1").WithMessage("Dirección es requerido")
                .NotEmpty().WithMessage("Dirección es requerido");

            RuleFor(p => p.nDirTipo2).NotNull().WithName("Dirección 2").WithMessage("Dirección es requerido")
                .NotEmpty().WithMessage("Dirección es requerido");

            //RuleFor(p => p.nDirTipo3).NotNull().WithName("Dirección 3").WithMessage("Dirección es requerido")
            //    .NotEmpty().WithMessage("Dirección es requerido")
            //    .GreaterThan(0).WithMessage("Dirección es requerido");

            RuleFor(p => p.cDirValor1).NotNull().WithName("Dirección 1").WithMessage("Dirección es requerido")
                .NotEmpty().WithMessage("Dirección es requerido");

            RuleFor(p => p.cDirValor2).NotNull().WithName("Dirección 2").WithMessage("Dirección es requerido")
                .NotEmpty().WithMessage("Dirección es requerido");

            //RuleFor(p => p.cDirValor3).NotNull().WithName("Dirección 3").WithMessage("Dirección es requerido")
            //    .NotEmpty().WithMessage("Dirección es requerido");

            RuleFor(p => p.nCodAge).NotNull().WithName("Agencia").WithMessage("Agencia es requerido")
                .NotEmpty().WithMessage("Agencia es requerido")
                .GreaterThan(0).WithMessage("Agencia es requerido");

            RuleFor(p => p.nCUUI).NotNull().WithName("Actividad economica").WithMessage("Actividad esconomica es requerido")
                .NotEmpty().WithMessage("Actividad economica es requerida");

            RuleFor(p => p.nSitLab).NotNull().WithName("Situación laboral").WithMessage("Situacion laboral es requerida")
                .NotEmpty().WithMessage("Situacion laboral es requerida");

            RuleFor(p => p.nProfes).NotNull().WithName("Profesión").WithMessage("Profesión es requerida")
                .NotEmpty().WithMessage("Profesión es requerida");

            RuleFor(p => p.nTipoEmp).NotNull().WithName("Tipo empleo").WithMessage("Tipo de empleo es requerido")
                .NotEmpty().WithMessage("Tipo de empleo es requerido");

            When(p => p.nEstadoCivil == "2", () =>
            {
                RuleFor(p => p.cDniConyuge).NotNull().WithName("DNI Conyuge").WithMessage("DNI Conyuge es requerido")
                    .NotEmpty().WithMessage("DNI Conyuge es requerido")
                    .Length(1, 8).WithMessage("DNI Conyuge no debe de exceder de los 8 digitos");

                RuleFor(p => p.cNomConyuge).NotNull().WithName("Nombre conyuge").WithMessage("Nombre conyuge es requerido")
                    .NotEmpty().WithMessage("Nombre conyuge es requerido")
                    .Length(1, 50).WithMessage("Nombre conyuge no debe de exceder de las 50 letras");

                RuleFor(p => p.cApeConyuge).NotNull().WithName("Apellido conyuge").WithMessage("Apellido conyuge es requerido")
                    .NotEmpty().WithMessage("Apellido conyuge es requerido")
                    .Length(1, 50).WithMessage("Apellido conyuge no debe de exceder de las 50 letras");

            });

            //RuleFor(p => p.cRuc).NotNull().WithName("RUC").WithMessage("RUC es requerido")
            //    .NotEmpty().WithMessage("RUC es requerido")
            //    .Length(1, 11).WithMessage("RUC no debe de exceder de los 11 digitos");

            RuleFor(p => p.nIngresoDeclado).NotNull().WithName("Ingreso declarado").WithMessage("Ingreso declarado es requerido")
                .NotEmpty().WithMessage("Ingreso declarado es requerido")
                .GreaterThan(0).WithMessage("Ingreso declarado debe de ser mayor que cero");

            //RuleFor(p => p.cDirEmpleo).NotNull().WithName("Dirección empleo").WithMessage("Dirección empleo es requerido")
            //    .NotEmpty().WithMessage("Dirección empleo es requerido")
            //    .Length(1,50).WithMessage("Dirección empleo no debe de exceder de las 50 letras");

            RuleFor(p => p.cTelfEmpleo).NotNull().WithName("Teléfono empleo").WithMessage("Teléfono empleo es requerido")
                .NotEmpty().WithMessage("Teléfono empleo es requerido")
                .Length(1,9).WithMessage("Teléfono empleo no debe de exceder de los 9 digitos");

            RuleFor(p => p.dFecIngrLab).NotNull().WithName("Fecha ingreso laboral").WithMessage("Fecha ingreso laboral es requerido")
                .NotEmpty().WithMessage("Fecha ingreso laboral es requerido");

            RuleFor(p => p.bCargoPublico).NotNull().WithName("Cargo publico").WithMessage("Cargo publico es requerido");

            //RuleFor(p => p.cNomEmpresa).NotNull().WithName("Nombre empresa").WithMessage("Nombre empresa es requerido")
            //    .NotEmpty().WithMessage("Nombre empresa es requerido")
            //    .Length(1,30).WithMessage("Nombre empresa no debe de exceder de las 30 letras");

            When(p => p.nProfes == "999", () => {
                RuleFor(p => p.cProfesionOtros).NotNull().WithName("Profesion otros").WithMessage("Profesion otros es requerido")
                .NotEmpty().WithMessage("Profesion otros es requerido")
                .Length(1,40).WithMessage("Profesion otros no debe de exceder de las 50 letras");
            });

            RuleFor(p => p.cCodZonaEmpleo).NotNull().WithName("Zona Empleo")
                .NotEmpty().WithMessage("Zona Empleo es requerido").
                Length(1, 15).WithMessage("Zona Empleo no debe de exceder de las 15 letras");

            RuleFor(p => p.nDirTipo1Empleo).NotNull().WithName("Dirección 1 Empleo").WithMessage("Dirección es requerido")
                .NotEmpty().WithMessage("Dirección es requerido");

            RuleFor(p => p.nDirTipo2Empleo).NotNull().WithName("Dirección 2 Empleo").WithMessage("Dirección es requerido")
                .NotEmpty().WithMessage("Dirección es requerido");

            RuleFor(p => p.cDirValor1Empleo).NotNull().WithName("Dirección 1 Empleo").WithMessage("Dirección es requerido")
                .NotEmpty().WithMessage("Dirección es requerido");

            RuleFor(p => p.cDirValor2Empleo).NotNull().WithName("Dirección 2 Empleo").WithMessage("Dirección es requerido")
                .NotEmpty().WithMessage("Dirección es requerido");
        }
    }
}
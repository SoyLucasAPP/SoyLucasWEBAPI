using FluentValidation;
using LightInject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using TiboxWebApi.Models;
using TiboxWebApi.WebApi.Validators;

namespace TiboxWebApi.WebApi
{
    public partial class Startup
    {
        public void ConfigureInjector(HttpConfiguration config)
        {
            //Inyeccion de dependecias 
            var container = new ServiceContainer();
            container.RegisterAssembly(Assembly.GetExecutingAssembly());
            container.RegisterAssembly("TiboxWebApi.Repository*.dll");
            container.RegisterAssembly("TiboxWebApi.UnitOfWork*.dll");

            //implementacion de las validators
            container.Register<AbstractValidator<Product>, ProductValidator>();

            container.RegisterApiControllers();
            container.EnableWebApi(config);
        }
    }
}
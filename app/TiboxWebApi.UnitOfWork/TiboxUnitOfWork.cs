
using System;
using TiboxWebApi.Models;
using TiboxWebApi.Repository;
using TiboxWebApi.Repository.Interfaces;
using TiboxWebApi.Repository.Repository;

namespace TiboxWebApi.UnitOfWork
{
    public class TiboxUnitOfWork : IUnitOfWork, IDisposable
    {
        public TiboxUnitOfWork()
        {
            Products = new BaseRepository<Product>();
            Users = new UserRepository();
            CatalogoCodigo = new CatalogoCodigoRepository();
            Zona = new ZonaRepository();
            Persona = new PersonaRepository();
            FlujoMaestro = new FlujoRepository();
            Credito = new CreditoRepository();
            Lenddo = new BaseRepository<WebPersonaLenddo>();
            VarNegocio = new BaseRepository<VarNegocio>();
            Reporte = new ReporteRepository();
            Documento = new DocumentoRepository();
            Error = new ErrorRepository();
            ReglaNegocio = new ReglaNegocioRepository();
            Menu = new MenuRepository();
        }

        public IRepository<Product> Products { get; private set; }
        public IUserRepository Users { get; private set; }
        public ICatalogoCodigoRepository CatalogoCodigo { get; private set; }
        public IZonaRepository Zona { get; private set; }
        public IPersonaRepository Persona { get; private set; }
        public IFlujoRepository FlujoMaestro { get; private set; }
        public ICreditoRepository Credito { get; private set; }
        public IRepository<WebPersonaLenddo> Lenddo { get; private set; }
        public IRepository<VarNegocio> VarNegocio { get; private set; }
        public IReporteRepository Reporte { get; private set; }
        public IDocumentoRepository Documento { get; private set; }
        public IErrorRepository Error { get; private set; }
        public IReglaNegocioRepository ReglaNegocio { get; private set; }
        public IMenuRepository Menu { get; private set; }
        public void Dispose()
        {
            this.Dispose();
        }
    }
}

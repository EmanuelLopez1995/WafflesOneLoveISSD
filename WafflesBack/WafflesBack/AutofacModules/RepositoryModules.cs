using Autofac;
using System.Reflection;
using WafflesBackRepository;
using WafflesBackRepository.Helpers;
using WafflesBackRepository.Interfaces;
using WafflesBackRepository.Repositories;

namespace WafflesBack.AutofacModules
{
    public class RepositoryModules : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DataBaseConnection>().InstancePerLifetimeScope();

            builder.RegisterType<EmpleadoRepository>().As<IEmpleadoRepository>().InstancePerLifetimeScope();
            builder.RegisterType<ProveedorRepository>().As<IProveedorRepository>().InstancePerLifetimeScope();
            builder.RegisterType<SueldosBasicosRepository>().As<ISueldosBasicosRepository>().InstancePerLifetimeScope();
            builder.RegisterType<TurnoRepository>().As<ITurnoRepository>().InstancePerLifetimeScope();
            builder.RegisterType<TurnoEmpleadoRepository>().As<ITurnoEmpleadoRepository>().InstancePerLifetimeScope();
            builder.RegisterType<CajaRepository>().As<ICajaRepository>().InstancePerLifetimeScope();
            builder.RegisterType<ActivoInicialRepository>().As<IActivoInicialRepository>().InstancePerLifetimeScope();
            builder.RegisterType<UMDRepository>().As<IUMDRepository>().InstancePerLifetimeScope();
            builder.RegisterType<ArticuloRepository>().As<IArticuloRepository>().InstancePerLifetimeScope();
            builder.RegisterType<IngredienteRepository>().As<IIngredienteRepository>().InstancePerLifetimeScope();
            builder.RegisterType<ArticuloPorIngredienteRepository>().As<IArticuloPorIngredienteRepository>();
            builder.RegisterType<CompraRepository>().As<ICompraRepository>();
            builder.RegisterType<DetalleCompraRepository>().As<IDetalleCompraRepository>();



            base.Load(builder);
        }
    }
}
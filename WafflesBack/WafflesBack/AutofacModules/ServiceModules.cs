using Autofac;
using WafflesBackRepository.Interfaces;
using WafflesBackServices;
using WafflesBackServices.Interfaces;
using WafflesBackServices.Services;

namespace WafflesBack.AutofacModules
{
    public class ServiceModules : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<EmpleadoService>().As<IEmpleadoService>().InstancePerLifetimeScope();
            builder.RegisterType<ProveedorService>().As<IProveedorService>().InstancePerLifetimeScope();
            builder.RegisterType<SueldosBasicosService>().As<ISueldosBasicosService>().InstancePerLifetimeScope();
            builder.RegisterType<TurnoService>().As<ITurnoService>().InstancePerLifetimeScope();
            builder.RegisterType<ActivoInicialService>().As<IActivoInicialService>().InstancePerLifetimeScope();
            builder.RegisterType<UMDService>().As<IUMDService>().InstancePerLifetimeScope();
            builder.RegisterType<ArticuloService>().As<IArticuloService>().InstancePerLifetimeScope();
            builder.RegisterType<IngredienteService>().As<IIngredienteService>().InstancePerLifetimeScope();
            builder.RegisterType<CompraService>().As<ICompraService>().InstancePerLifetimeScope();
            builder.RegisterType<BilletesService>().As<IBilletesService>().InstancePerLifetimeScope();
            builder.RegisterType<UsuarioService>().As<IUsuarioService>().InstancePerLifetimeScope();



            base.Load(builder);
        }
    }
}
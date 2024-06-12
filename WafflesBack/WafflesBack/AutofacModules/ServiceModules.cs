using Autofac;
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


            base.Load(builder);
        }
    }
}
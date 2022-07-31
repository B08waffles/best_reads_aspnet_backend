using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Utilities;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
///     Extension methods for setting up Entity Framework related services in an <see cref="T:Microsoft.Extensions.DependencyInjection.IServiceCollection" />.
/// </summary>
public static class EntityFrameworkServiceCollectionExtensions
{
	/// <summary>
	///     <para>
	///         Registers the given context as a service in the <see cref="T:Microsoft.Extensions.DependencyInjection.IServiceCollection" />.
	///     </para>
	///     <para>
	///         Use this method when using dependency injection in your application, such as with ASP.NET Core.
	///         For applications that don't use dependency injection, consider creating <see cref="T:Microsoft.EntityFrameworkCore.DbContext" />
	///         instances directly with its constructor. The <see cref="M:Microsoft.EntityFrameworkCore.DbContext.OnConfiguring(Microsoft.EntityFrameworkCore.DbContextOptionsBuilder)" /> method can then be
	///         overridden to configure a connection string and other options.
	///     </para>
	///     <para>
	///         Entity Framework Core does not support multiple parallel operations being run on the same <see cref="T:Microsoft.EntityFrameworkCore.DbContext" />
	///         instance. This includes both parallel execution of async queries and any explicit concurrent use from multiple threads.
	///         Therefore, always await async calls immediately, or use separate DbContext instances for operations that execute
	///         in parallel. See <see href="https://aka.ms/efcore-docs-threading">Avoiding DbContext threading issues</see> for more information.
	///     </para>
	///     <para>
	///         See <see href="https://aka.ms/efcore-docs-di">Using DbContext with dependency injection</see> for more information.
	///     </para>
	/// </summary>
	/// <typeparam name="TContext">The type of context to be registered.</typeparam>
	/// <param name="serviceCollection">The <see cref="T:Microsoft.Extensions.DependencyInjection.IServiceCollection" /> to add services to.</param>
	/// <param name="optionsAction">
	///     <para>
	///         An optional action to configure the <see cref="T:Microsoft.EntityFrameworkCore.DbContextOptions" /> for the context. This provides an
	///         alternative to performing configuration of the context by overriding the
	///         <see cref="M:Microsoft.EntityFrameworkCore.DbContext.OnConfiguring(Microsoft.EntityFrameworkCore.DbContextOptionsBuilder)" /> method in your derived context.
	///     </para>
	///     <para>
	///         If an action is supplied here, the <see cref="M:Microsoft.EntityFrameworkCore.DbContext.OnConfiguring(Microsoft.EntityFrameworkCore.DbContextOptionsBuilder)" /> method will still be run if it has
	///         been overridden on the derived context. <see cref="M:Microsoft.EntityFrameworkCore.DbContext.OnConfiguring(Microsoft.EntityFrameworkCore.DbContextOptionsBuilder)" /> configuration will be applied
	///         in addition to configuration performed here.
	///     </para>
	///     <para>
	///         In order for the options to be passed into your context, you need to expose a constructor on your context that takes
	///         <see cref="T:Microsoft.EntityFrameworkCore.DbContextOptions`1" /> and passes it to the base constructor of <see cref="T:Microsoft.EntityFrameworkCore.DbContext" />.
	///     </para>
	/// </param>
	/// <param name="contextLifetime">The lifetime with which to register the DbContext service in the container.</param>
	/// <param name="optionsLifetime">The lifetime with which to register the DbContextOptions service in the container.</param>
	/// <returns>The same service collection so that multiple calls can be chained.</returns>
	public static IServiceCollection AddDbContext<TContext>(this IServiceCollection serviceCollection, Action<DbContextOptionsBuilder>? optionsAction = null, ServiceLifetime contextLifetime = ServiceLifetime.Scoped, ServiceLifetime optionsLifetime = ServiceLifetime.Scoped) where TContext : DbContext
	{
		return serviceCollection.AddDbContext<TContext, TContext>(optionsAction, contextLifetime, optionsLifetime);
	}

	/// <summary>
	///     <para>
	///         Registers the given context as a service in the <see cref="T:Microsoft.Extensions.DependencyInjection.IServiceCollection" />.
	///     </para>
	///     <para>
	///         Use this method when using dependency injection in your application, such as with ASP.NET Core.
	///         For applications that don't use dependency injection, consider creating <see cref="T:Microsoft.EntityFrameworkCore.DbContext" />
	///         instances directly with its constructor. The <see cref="M:Microsoft.EntityFrameworkCore.DbContext.OnConfiguring(Microsoft.EntityFrameworkCore.DbContextOptionsBuilder)" /> method can then be
	///         overridden to configure a connection string and other options.
	///     </para>
	///     <para>
	///         Entity Framework Core does not support multiple parallel operations being run on the same <see cref="T:Microsoft.EntityFrameworkCore.DbContext" />
	///         instance. This includes both parallel execution of async queries and any explicit concurrent use from multiple threads.
	///         Therefore, always await async calls immediately, or use separate DbContext instances for operations that execute
	///         in parallel. See <see href="https://aka.ms/efcore-docs-threading">Avoiding DbContext threading issues</see> for more information.
	///     </para>
	///     <para>
	///         See <see href="https://aka.ms/efcore-docs-di">Using DbContext with dependency injection</see> for more information.
	///     </para>
	/// </summary>
	/// <typeparam name="TContextService">The class or interface that will be used to resolve the context from the container.</typeparam>
	/// <typeparam name="TContextImplementation">The concrete implementation type to create.</typeparam>
	/// <param name="serviceCollection">The <see cref="T:Microsoft.Extensions.DependencyInjection.IServiceCollection" /> to add services to.</param>
	/// <param name="optionsAction">
	///     <para>
	///         An optional action to configure the <see cref="T:Microsoft.EntityFrameworkCore.DbContextOptions" /> for the context. This provides an
	///         alternative to performing configuration of the context by overriding the
	///         <see cref="M:Microsoft.EntityFrameworkCore.DbContext.OnConfiguring(Microsoft.EntityFrameworkCore.DbContextOptionsBuilder)" /> method in your derived context.
	///     </para>
	///     <para>
	///         If an action is supplied here, the <see cref="M:Microsoft.EntityFrameworkCore.DbContext.OnConfiguring(Microsoft.EntityFrameworkCore.DbContextOptionsBuilder)" /> method will still be run if it has
	///         been overridden on the derived context. <see cref="M:Microsoft.EntityFrameworkCore.DbContext.OnConfiguring(Microsoft.EntityFrameworkCore.DbContextOptionsBuilder)" /> configuration will be applied
	///         in addition to configuration performed here.
	///     </para>
	///     <para>
	///         In order for the options to be passed into your context, you need to expose a constructor on your context that takes
	///         <see cref="T:Microsoft.EntityFrameworkCore.DbContextOptions`1" /> and passes it to the base constructor of <see cref="T:Microsoft.EntityFrameworkCore.DbContext" />.
	///     </para>
	/// </param>
	/// <param name="contextLifetime">The lifetime with which to register the DbContext service in the container.</param>
	/// <param name="optionsLifetime">The lifetime with which to register the DbContextOptions service in the container.</param>
	/// <returns>The same service collection so that multiple calls can be chained.</returns>
	public static IServiceCollection AddDbContext<TContextService, TContextImplementation>(this IServiceCollection serviceCollection, Action<DbContextOptionsBuilder>? optionsAction = null, ServiceLifetime contextLifetime = ServiceLifetime.Scoped, ServiceLifetime optionsLifetime = ServiceLifetime.Scoped) where TContextImplementation : DbContext, TContextService
	{
		Action<DbContextOptionsBuilder> optionsAction2 = optionsAction;
		return serviceCollection.AddDbContext<TContextService, TContextImplementation>((optionsAction2 == null) ? null : ((Action<IServiceProvider, DbContextOptionsBuilder>)delegate(IServiceProvider p, DbContextOptionsBuilder b)
		{
			optionsAction2(b);
		}), contextLifetime, optionsLifetime);
	}

	/// <summary>
	///     <para>
	///         Registers the given <see cref="T:Microsoft.EntityFrameworkCore.DbContext" /> as a service in the <see cref="T:Microsoft.Extensions.DependencyInjection.IServiceCollection" />,
	///         and enables DbContext pooling for this registration.
	///     </para>
	///     <para>
	///         DbContext pooling can increase performance in high-throughput scenarios by re-using context instances.
	///         However, for most application this performance gain is very small.
	///         Note that when using pooling, the context configuration cannot change between uses, and scoped services
	///         injected into the context will only be resolved once from the initial scope.
	///         Only consider using DbContext pooling when performance testing indicates it provides a real boost.
	///     </para>
	///     <para>
	///         Use this method when using dependency injection in your application, such as with ASP.NET Core.
	///         For applications that don't use dependency injection, consider creating <see cref="T:Microsoft.EntityFrameworkCore.DbContext" />
	///         instances directly with its constructor. The <see cref="M:Microsoft.EntityFrameworkCore.DbContext.OnConfiguring(Microsoft.EntityFrameworkCore.DbContextOptionsBuilder)" /> method can then be
	///         overridden to configure a connection string and other options.
	///     </para>
	///     <para>
	///         Entity Framework Core does not support multiple parallel operations being run on the same <see cref="T:Microsoft.EntityFrameworkCore.DbContext" />
	///         instance. This includes both parallel execution of async queries and any explicit concurrent use from multiple threads.
	///         Therefore, always await async calls immediately, or use separate DbContext instances for operations that execute
	///         in parallel. See <see href="https://aka.ms/efcore-docs-threading">Avoiding DbContext threading issues</see> for more information.
	///     </para>
	///     <para>
	///         See <see href="https://aka.ms/efcore-docs-di">Using DbContext with dependency injection</see> and
	///         <see href="https://aka.ms/efcore-docs-dbcontext-pooling">Using DbContext pooling</see> for more information.
	///     </para>
	/// </summary>
	/// <typeparam name="TContext">The type of context to be registered.</typeparam>
	/// <param name="serviceCollection">The <see cref="T:Microsoft.Extensions.DependencyInjection.IServiceCollection" /> to add services to.</param>
	/// <param name="optionsAction">
	///     <para>
	///         A required action to configure the <see cref="T:Microsoft.EntityFrameworkCore.DbContextOptions" /> for the context. When using
	///         context pooling, options configuration must be performed externally; <see cref="M:Microsoft.EntityFrameworkCore.DbContext.OnConfiguring(Microsoft.EntityFrameworkCore.DbContextOptionsBuilder)" />
	///         will not be called.
	///     </para>
	/// </param>
	/// <param name="poolSize">Sets the maximum number of instances retained by the pool. Defaults to 1024.</param>
	/// <returns>The same service collection so that multiple calls can be chained.</returns>
	public static IServiceCollection AddDbContextPool<TContext>(this IServiceCollection serviceCollection, Action<DbContextOptionsBuilder> optionsAction, int poolSize = 1024) where TContext : DbContext
	{
		return serviceCollection.AddDbContextPool<TContext, TContext>(optionsAction, poolSize);
	}

	/// <summary>
	///     <para>
	///         Registers the given <see cref="T:Microsoft.EntityFrameworkCore.DbContext" /> as a service in the <see cref="T:Microsoft.Extensions.DependencyInjection.IServiceCollection" />,
	///         and enables DbContext pooling for this registration.
	///     </para>
	///     <para>
	///         DbContext pooling can increase performance in high-throughput scenarios by re-using context instances.
	///         However, for most application this performance gain is very small.
	///         Note that when using pooling, the context configuration cannot change between uses, and scoped services
	///         injected into the context will only be resolved once from the initial scope.
	///         Only consider using DbContext pooling when performance testing indicates it provides a real boost.
	///     </para>
	///     <para>
	///         Use this method when using dependency injection in your application, such as with ASP.NET Core.
	///         For applications that don't use dependency injection, consider creating <see cref="T:Microsoft.EntityFrameworkCore.DbContext" />
	///         instances directly with its constructor. The <see cref="M:Microsoft.EntityFrameworkCore.DbContext.OnConfiguring(Microsoft.EntityFrameworkCore.DbContextOptionsBuilder)" /> method can then be
	///         overridden to configure a connection string and other options.
	///     </para>
	///     <para>
	///         Entity Framework Core does not support multiple parallel operations being run on the same <see cref="T:Microsoft.EntityFrameworkCore.DbContext" />
	///         instance. This includes both parallel execution of async queries and any explicit concurrent use from multiple threads.
	///         Therefore, always await async calls immediately, or use separate DbContext instances for operations that execute
	///         in parallel. See <see href="https://aka.ms/efcore-docs-threading">Avoiding DbContext threading issues</see> for more information.
	///     </para>
	///     <para>
	///         See <see href="https://aka.ms/efcore-docs-di">Using DbContext with dependency injection</see> and
	///         <see href="https://aka.ms/efcore-docs-dbcontext-pooling">Using DbContext pooling</see> for more information.
	///     </para>
	/// </summary>
	/// <typeparam name="TContextService">The class or interface that will be used to resolve the context from the container.</typeparam>
	/// <typeparam name="TContextImplementation">The concrete implementation type to create.</typeparam>
	/// <param name="serviceCollection">The <see cref="T:Microsoft.Extensions.DependencyInjection.IServiceCollection" /> to add services to.</param>
	/// <param name="optionsAction">
	///     <para>
	///         A required action to configure the <see cref="T:Microsoft.EntityFrameworkCore.DbContextOptions" /> for the context. When using
	///         context pooling, options configuration must be performed externally; <see cref="M:Microsoft.EntityFrameworkCore.DbContext.OnConfiguring(Microsoft.EntityFrameworkCore.DbContextOptionsBuilder)" />
	///         will not be called.
	///     </para>
	/// </param>
	/// <param name="poolSize">Sets the maximum number of instances retained by the pool. Defaults to 1024.</param>
	/// <returns>The same service collection so that multiple calls can be chained.</returns>
	public static IServiceCollection AddDbContextPool<TContextService, TContextImplementation>(this IServiceCollection serviceCollection, Action<DbContextOptionsBuilder> optionsAction, int poolSize = 1024) where TContextService : class where TContextImplementation : DbContext, TContextService
	{
		Action<DbContextOptionsBuilder> optionsAction2 = optionsAction;
		Check.NotNull(optionsAction2, "optionsAction");
		return serviceCollection.AddDbContextPool<TContextService, TContextImplementation>((Action<IServiceProvider, DbContextOptionsBuilder>)delegate(IServiceProvider _, DbContextOptionsBuilder ob)
		{
			optionsAction2(ob);
		}, poolSize);
	}

	/// <summary>
	///     <para>
	///         Registers the given <see cref="T:Microsoft.EntityFrameworkCore.DbContext" /> as a service in the <see cref="T:Microsoft.Extensions.DependencyInjection.IServiceCollection" />,
	///         and enables DbContext pooling for this registration.
	///     </para>
	///     <para>
	///         DbContext pooling can increase performance in high-throughput scenarios by re-using context instances.
	///         However, for most application this performance gain is very small.
	///         Note that when using pooling, the context configuration cannot change between uses, and scoped services
	///         injected into the context will only be resolved once from the initial scope.
	///         Only consider using DbContext pooling when performance testing indicates it provides a real boost.
	///     </para>
	///     <para>
	///         Use this method when using dependency injection in your application, such as with ASP.NET Core.
	///         For applications that don't use dependency injection, consider creating <see cref="T:Microsoft.EntityFrameworkCore.DbContext" />
	///         instances directly with its constructor. The <see cref="M:Microsoft.EntityFrameworkCore.DbContext.OnConfiguring(Microsoft.EntityFrameworkCore.DbContextOptionsBuilder)" /> method can then be
	///         overridden to configure a connection string and other options.
	///     </para>
	///     <para>
	///         Entity Framework Core does not support multiple parallel operations being run on the same <see cref="T:Microsoft.EntityFrameworkCore.DbContext" />
	///         instance. This includes both parallel execution of async queries and any explicit concurrent use from multiple threads.
	///         Therefore, always await async calls immediately, or use separate DbContext instances for operations that execute
	///         in parallel. See <see href="https://aka.ms/efcore-docs-threading">Avoiding DbContext threading issues</see> for more information.
	///     </para>
	///     <para>
	///         See <see href="https://aka.ms/efcore-docs-di">Using DbContext with dependency injection</see> and
	///         <see href="https://aka.ms/efcore-docs-dbcontext-pooling">Using DbContext pooling</see> for more information.
	///     </para>
	///     <para>
	///         This overload has an <paramref name="optionsAction" /> that provides the application's
	///         <see cref="T:System.IServiceProvider" />. This is useful if you want to setup Entity Framework Core to resolve
	///         its internal services from the primary application service provider.
	///         By default, we recommend using
	///         <see cref="M:Microsoft.Extensions.DependencyInjection.EntityFrameworkServiceCollectionExtensions.AddDbContextPool``1(Microsoft.Extensions.DependencyInjection.IServiceCollection,System.Action{Microsoft.EntityFrameworkCore.DbContextOptionsBuilder},System.Int32)" /> which allows
	///         Entity Framework to create and maintain its own <see cref="T:System.IServiceProvider" /> for internal Entity Framework services.
	///     </para>
	/// </summary>
	/// <typeparam name="TContext">The type of context to be registered.</typeparam>
	/// <param name="serviceCollection">The <see cref="T:Microsoft.Extensions.DependencyInjection.IServiceCollection" /> to add services to.</param>
	/// <param name="optionsAction">
	///     <para>
	///         A required action to configure the <see cref="T:Microsoft.EntityFrameworkCore.DbContextOptions" /> for the context. When using
	///         context pooling, options configuration must be performed externally; <see cref="M:Microsoft.EntityFrameworkCore.DbContext.OnConfiguring(Microsoft.EntityFrameworkCore.DbContextOptionsBuilder)" />
	///         will not be called.
	///     </para>
	/// </param>
	/// <param name="poolSize">Sets the maximum number of instances retained by the pool. Defaults to 1024.</param>
	/// <returns>The same service collection so that multiple calls can be chained.</returns>
	public static IServiceCollection AddDbContextPool<TContext>(this IServiceCollection serviceCollection, Action<IServiceProvider, DbContextOptionsBuilder> optionsAction, int poolSize = 1024) where TContext : DbContext
	{
		return serviceCollection.AddDbContextPool<TContext, TContext>(optionsAction, poolSize);
	}

	/// <summary>
	///     <para>
	///         Registers the given <see cref="T:Microsoft.EntityFrameworkCore.DbContext" /> as a service in the <see cref="T:Microsoft.Extensions.DependencyInjection.IServiceCollection" />,
	///         and enables DbContext pooling for this registration.
	///     </para>
	///     <para>
	///         DbContext pooling can increase performance in high-throughput scenarios by re-using context instances.
	///         However, for most application this performance gain is very small.
	///         Note that when using pooling, the context configuration cannot change between uses, and scoped services
	///         injected into the context will only be resolved once from the initial scope.
	///         Only consider using DbContext pooling when performance testing indicates it provides a real boost.
	///     </para>
	///     <para>
	///         Use this method when using dependency injection in your application, such as with ASP.NET Core.
	///         For applications that don't use dependency injection, consider creating <see cref="T:Microsoft.EntityFrameworkCore.DbContext" />
	///         instances directly with its constructor. The <see cref="M:Microsoft.EntityFrameworkCore.DbContext.OnConfiguring(Microsoft.EntityFrameworkCore.DbContextOptionsBuilder)" /> method can then be
	///         overridden to configure a connection string and other options.
	///     </para>
	///     <para>
	///         Entity Framework Core does not support multiple parallel operations being run on the same <see cref="T:Microsoft.EntityFrameworkCore.DbContext" />
	///         instance. This includes both parallel execution of async queries and any explicit concurrent use from multiple threads.
	///         Therefore, always await async calls immediately, or use separate DbContext instances for operations that execute
	///         in parallel. See <see href="https://aka.ms/efcore-docs-threading">Avoiding DbContext threading issues</see> for more information.
	///     </para>
	///     <para>
	///         See <see href="https://aka.ms/efcore-docs-di">Using DbContext with dependency injection</see> and
	///         <see href="https://aka.ms/efcore-docs-dbcontext-pooling">Using DbContext pooling</see> for more information.
	///     </para>
	///     <para>
	///         This overload has an <paramref name="optionsAction" /> that provides the application's
	///         <see cref="T:System.IServiceProvider" />. This is useful if you want to setup Entity Framework Core to resolve
	///         its internal services from the primary application service provider.
	///         By default, we recommend using
	///         <see cref="M:Microsoft.Extensions.DependencyInjection.EntityFrameworkServiceCollectionExtensions.AddDbContextPool``2(Microsoft.Extensions.DependencyInjection.IServiceCollection,System.Action{Microsoft.EntityFrameworkCore.DbContextOptionsBuilder},System.Int32)" />
	///         which allows Entity Framework to create and maintain its own <see cref="T:System.IServiceProvider" /> for internal
	///         Entity Framework services.
	///     </para>
	/// </summary>
	/// <typeparam name="TContextService">The class or interface that will be used to resolve the context from the container.</typeparam>
	/// <typeparam name="TContextImplementation">The concrete implementation type to create.</typeparam>
	/// <param name="serviceCollection">The <see cref="T:Microsoft.Extensions.DependencyInjection.IServiceCollection" /> to add services to.</param>
	/// <param name="optionsAction">
	///     <para>
	///         A required action to configure the <see cref="T:Microsoft.EntityFrameworkCore.DbContextOptions" /> for the context. When using
	///         context pooling, options configuration must be performed externally; <see cref="M:Microsoft.EntityFrameworkCore.DbContext.OnConfiguring(Microsoft.EntityFrameworkCore.DbContextOptionsBuilder)" />
	///         will not be called.
	///     </para>
	/// </param>
	/// <param name="poolSize">Sets the maximum number of instances retained by the pool. Defaults to 1024.</param>
	/// <returns>The same service collection so that multiple calls can be chained.</returns>
	public static IServiceCollection AddDbContextPool<TContextService, TContextImplementation>(this IServiceCollection serviceCollection, Action<IServiceProvider, DbContextOptionsBuilder> optionsAction, int poolSize = 1024) where TContextService : class where TContextImplementation : DbContext, TContextService
	{
		Check.NotNull(serviceCollection, "serviceCollection");
		Check.NotNull(optionsAction, "optionsAction");
		AddPoolingOptions<TContextImplementation>(serviceCollection, optionsAction, poolSize);
		serviceCollection.TryAddSingleton<IDbContextPool<TContextImplementation>, DbContextPool<TContextImplementation>>();
		serviceCollection.TryAddScoped<IScopedDbContextLease<TContextImplementation>, ScopedDbContextLease<TContextImplementation>>();
		serviceCollection.TryAddScoped((IServiceProvider sp) => (TContextService)(object)sp.GetRequiredService<IScopedDbContextLease<TContextImplementation>>().Context);
		if (typeof(TContextService) != typeof(TContextImplementation))
		{
			serviceCollection.TryAddScoped((IServiceProvider p) => (TContextImplementation)(DbContext)(object)p.GetService<TContextService>());
		}
		return serviceCollection;
	}

	private static void AddPoolingOptions<TContext>(IServiceCollection serviceCollection, Action<IServiceProvider, DbContextOptionsBuilder> optionsAction, int poolSize) where TContext : DbContext
	{
		Action<IServiceProvider, DbContextOptionsBuilder> optionsAction2 = optionsAction;
		if (poolSize <= 0)
		{
			throw new ArgumentOutOfRangeException("poolSize", CoreStrings.InvalidPoolSize);
		}
		CheckContextConstructors<TContext>();
		AddCoreServices<TContext>(serviceCollection, delegate(IServiceProvider sp, DbContextOptionsBuilder ob)
		{
			optionsAction2(sp, ob);
			CoreOptionsExtension extension = (ob.Options.FindExtension<CoreOptionsExtension>() ?? new CoreOptionsExtension())!.WithMaxPoolSize(poolSize);
			((IDbContextOptionsBuilderInfrastructure)ob).AddOrUpdateExtension(extension);
		}, ServiceLifetime.Singleton);
	}

	/// <summary>
	///     <para>
	///         Registers the given context as a service in the <see cref="T:Microsoft.Extensions.DependencyInjection.IServiceCollection" />.
	///     </para>
	///     <para>
	///         Use this method when using dependency injection in your application, such as with ASP.NET Core.
	///         For applications that don't use dependency injection, consider creating <see cref="T:Microsoft.EntityFrameworkCore.DbContext" />
	///         instances directly with its constructor. The <see cref="M:Microsoft.EntityFrameworkCore.DbContext.OnConfiguring(Microsoft.EntityFrameworkCore.DbContextOptionsBuilder)" /> method can then be
	///         overridden to configure a connection string and other options.
	///     </para>
	///     <para>
	///         Entity Framework Core does not support multiple parallel operations being run on the same <see cref="T:Microsoft.EntityFrameworkCore.DbContext" />
	///         instance. This includes both parallel execution of async queries and any explicit concurrent use from multiple threads.
	///         Therefore, always await async calls immediately, or use separate DbContext instances for operations that execute
	///         in parallel. See <see href="https://aka.ms/efcore-docs-threading">Avoiding DbContext threading issues</see> for more information.
	///     </para>
	///     <para>
	///         See <see href="https://aka.ms/efcore-docs-di">Using DbContext with dependency injection</see> for more information.
	///     </para>
	/// </summary>
	/// <typeparam name="TContext">The type of context to be registered.</typeparam>
	/// <param name="serviceCollection">The <see cref="T:Microsoft.Extensions.DependencyInjection.IServiceCollection" /> to add services to.</param>
	/// <param name="contextLifetime">The lifetime with which to register the DbContext service in the container.</param>
	/// <param name="optionsLifetime">The lifetime with which to register the DbContextOptions service in the container.</param>
	/// <returns>The same service collection so that multiple calls can be chained.</returns>
	public static IServiceCollection AddDbContext<TContext>(this IServiceCollection serviceCollection, ServiceLifetime contextLifetime, ServiceLifetime optionsLifetime = ServiceLifetime.Scoped) where TContext : DbContext
	{
		return serviceCollection.AddDbContext<TContext, TContext>(contextLifetime, optionsLifetime);
	}

	/// <summary>
	///     <para>
	///         Registers the given context as a service in the <see cref="T:Microsoft.Extensions.DependencyInjection.IServiceCollection" />.
	///     </para>
	///     <para>
	///         Use this method when using dependency injection in your application, such as with ASP.NET Core.
	///         For applications that don't use dependency injection, consider creating <see cref="T:Microsoft.EntityFrameworkCore.DbContext" />
	///         instances directly with its constructor. The <see cref="M:Microsoft.EntityFrameworkCore.DbContext.OnConfiguring(Microsoft.EntityFrameworkCore.DbContextOptionsBuilder)" /> method can then be
	///         overridden to configure a connection string and other options.
	///     </para>
	///     <para>
	///         Entity Framework Core does not support multiple parallel operations being run on the same <see cref="T:Microsoft.EntityFrameworkCore.DbContext" />
	///         instance. This includes both parallel execution of async queries and any explicit concurrent use from multiple threads.
	///         Therefore, always await async calls immediately, or use separate DbContext instances for operations that execute
	///         in parallel. See <see href="https://aka.ms/efcore-docs-threading">Avoiding DbContext threading issues</see> for more information.
	///     </para>
	///     <para>
	///         See <see href="https://aka.ms/efcore-docs-di">Using DbContext with dependency injection</see> for more information.
	///     </para>
	/// </summary>
	/// <typeparam name="TContextService">The class or interface that will be used to resolve the context from the container.</typeparam>
	/// <typeparam name="TContextImplementation">The concrete implementation type to create.</typeparam>
	/// <param name="serviceCollection">The <see cref="T:Microsoft.Extensions.DependencyInjection.IServiceCollection" /> to add services to.</param>
	/// <param name="contextLifetime">The lifetime with which to register the DbContext service in the container.</param>
	/// <param name="optionsLifetime">The lifetime with which to register the DbContextOptions service in the container.</param>
	/// <returns>The same service collection so that multiple calls can be chained.</returns>
	public static IServiceCollection AddDbContext<TContextService, TContextImplementation>(this IServiceCollection serviceCollection, ServiceLifetime contextLifetime, ServiceLifetime optionsLifetime = ServiceLifetime.Scoped) where TContextService : class where TContextImplementation : DbContext, TContextService
	{
		return serviceCollection.AddDbContext<TContextService, TContextImplementation>((Action<IServiceProvider, DbContextOptionsBuilder>?)null, contextLifetime, optionsLifetime);
	}

	/// <summary>
	///     <para>
	///         Registers the given context as a service in the <see cref="T:Microsoft.Extensions.DependencyInjection.IServiceCollection" />.
	///     </para>
	///     <para>
	///         Use this method when using dependency injection in your application, such as with ASP.NET Core.
	///         For applications that don't use dependency injection, consider creating <see cref="T:Microsoft.EntityFrameworkCore.DbContext" />
	///         instances directly with its constructor. The <see cref="M:Microsoft.EntityFrameworkCore.DbContext.OnConfiguring(Microsoft.EntityFrameworkCore.DbContextOptionsBuilder)" /> method can then be
	///         overridden to configure a connection string and other options.
	///     </para>
	///     <para>
	///         Entity Framework Core does not support multiple parallel operations being run on the same <see cref="T:Microsoft.EntityFrameworkCore.DbContext" />
	///         instance. This includes both parallel execution of async queries and any explicit concurrent use from multiple threads.
	///         Therefore, always await async calls immediately, or use separate DbContext instances for operations that execute
	///         in parallel. See <see href="https://aka.ms/efcore-docs-threading">Avoiding DbContext threading issues</see> for more information.
	///     </para>
	///     <para>
	///         Entity Framework Core does not support multiple parallel operations being run on the same <see cref="T:Microsoft.EntityFrameworkCore.DbContext" />
	///         instance. This includes both parallel execution of async queries and any explicit concurrent use from multiple threads.
	///         Therefore, always await async calls immediately, or use separate DbContext instances for operations that execute
	///         in parallel. See <see href="https://aka.ms/efcore-docs-threading">Avoiding DbContext threading issues</see> for more information.
	///     </para>
	///     <para>
	///         See <see href="https://aka.ms/efcore-docs-di">Using DbContext with dependency injection</see> for more information.
	///     </para>
	///     <para>
	///         This overload has an <paramref name="optionsAction" /> that provides the application's
	///         <see cref="T:System.IServiceProvider" />. This is useful if you want to setup Entity Framework Core to resolve
	///         its internal services from the primary application service provider.
	///         By default, we recommend using
	///         <see cref="M:Microsoft.Extensions.DependencyInjection.EntityFrameworkServiceCollectionExtensions.AddDbContext``1(Microsoft.Extensions.DependencyInjection.IServiceCollection,System.Action{Microsoft.EntityFrameworkCore.DbContextOptionsBuilder},Microsoft.Extensions.DependencyInjection.ServiceLifetime,Microsoft.Extensions.DependencyInjection.ServiceLifetime)" />
	///         which allows Entity Framework to create and maintain its own <see cref="T:System.IServiceProvider" /> for internal
	///         Entity Framework services.
	///     </para>
	/// </summary>
	/// <typeparam name="TContext">The type of context to be registered.</typeparam>
	/// <param name="serviceCollection">The <see cref="T:Microsoft.Extensions.DependencyInjection.IServiceCollection" /> to add services to.</param>
	/// <param name="optionsAction">
	///     <para>
	///         An optional action to configure the <see cref="T:Microsoft.EntityFrameworkCore.DbContextOptions" /> for the context. This provides an
	///         alternative to performing configuration of the context by overriding the
	///         <see cref="M:Microsoft.EntityFrameworkCore.DbContext.OnConfiguring(Microsoft.EntityFrameworkCore.DbContextOptionsBuilder)" /> method in your derived context.
	///     </para>
	///     <para>
	///         If an action is supplied here, the <see cref="M:Microsoft.EntityFrameworkCore.DbContext.OnConfiguring(Microsoft.EntityFrameworkCore.DbContextOptionsBuilder)" /> method will still be run if it has
	///         been overridden on the derived context. <see cref="M:Microsoft.EntityFrameworkCore.DbContext.OnConfiguring(Microsoft.EntityFrameworkCore.DbContextOptionsBuilder)" /> configuration will be applied
	///         in addition to configuration performed here.
	///     </para>
	///     <para>
	///         In order for the options to be passed into your context, you need to expose a constructor on your context that takes
	///         <see cref="T:Microsoft.EntityFrameworkCore.DbContextOptions`1" /> and passes it to the base constructor of <see cref="T:Microsoft.EntityFrameworkCore.DbContext" />.
	///     </para>
	/// </param>
	/// <param name="contextLifetime">The lifetime with which to register the DbContext service in the container.</param>
	/// <param name="optionsLifetime">The lifetime with which to register the DbContextOptions service in the container.</param>
	/// <returns>The same service collection so that multiple calls can be chained.</returns>
	public static IServiceCollection AddDbContext<TContext>(this IServiceCollection serviceCollection, Action<IServiceProvider, DbContextOptionsBuilder>? optionsAction, ServiceLifetime contextLifetime = ServiceLifetime.Scoped, ServiceLifetime optionsLifetime = ServiceLifetime.Scoped) where TContext : DbContext
	{
		return serviceCollection.AddDbContext<TContext, TContext>(optionsAction, contextLifetime, optionsLifetime);
	}

	/// <summary>
	///     <para>
	///         Registers the given context as a service in the <see cref="T:Microsoft.Extensions.DependencyInjection.IServiceCollection" />.
	///     </para>
	///     <para>
	///         Use this method when using dependency injection in your application, such as with ASP.NET Core.
	///         For applications that don't use dependency injection, consider creating <see cref="T:Microsoft.EntityFrameworkCore.DbContext" />
	///         instances directly with its constructor. The <see cref="M:Microsoft.EntityFrameworkCore.DbContext.OnConfiguring(Microsoft.EntityFrameworkCore.DbContextOptionsBuilder)" /> method can then be
	///         overridden to configure a connection string and other options.
	///     </para>
	///     <para>
	///         Entity Framework Core does not support multiple parallel operations being run on the same <see cref="T:Microsoft.EntityFrameworkCore.DbContext" />
	///         instance. This includes both parallel execution of async queries and any explicit concurrent use from multiple threads.
	///         Therefore, always await async calls immediately, or use separate DbContext instances for operations that execute
	///         in parallel. See <see href="https://aka.ms/efcore-docs-threading">Avoiding DbContext threading issues</see> for more information.
	///     </para>
	///     <para>
	///         See <see href="https://aka.ms/efcore-docs-di">Using DbContext with dependency injection</see> for more information.
	///     </para>
	///     <para>
	///         This overload has an <paramref name="optionsAction" /> that provides the application's
	///         <see cref="T:System.IServiceProvider" />. This is useful if you want to setup Entity Framework Core to resolve
	///         its internal services from the primary application service provider.
	///         By default, we recommend using
	///         <see cref="M:Microsoft.Extensions.DependencyInjection.EntityFrameworkServiceCollectionExtensions.AddDbContext``2(Microsoft.Extensions.DependencyInjection.IServiceCollection,System.Action{Microsoft.EntityFrameworkCore.DbContextOptionsBuilder},Microsoft.Extensions.DependencyInjection.ServiceLifetime,Microsoft.Extensions.DependencyInjection.ServiceLifetime)" />
	///         which allows Entity Framework to create and maintain its own <see cref="T:System.IServiceProvider" /> for internal
	///         Entity Framework services.
	///     </para>
	/// </summary>
	/// <typeparam name="TContextService">The class or interface that will be used to resolve the context from the container.</typeparam>
	/// <typeparam name="TContextImplementation">The concrete implementation type to create.</typeparam>
	/// <param name="serviceCollection">The <see cref="T:Microsoft.Extensions.DependencyInjection.IServiceCollection" /> to add services to.</param>
	/// <param name="optionsAction">
	///     <para>
	///         An optional action to configure the <see cref="T:Microsoft.EntityFrameworkCore.DbContextOptions" /> for the context. This provides an
	///         alternative to performing configuration of the context by overriding the
	///         <see cref="M:Microsoft.EntityFrameworkCore.DbContext.OnConfiguring(Microsoft.EntityFrameworkCore.DbContextOptionsBuilder)" /> method in your derived context.
	///     </para>
	///     <para>
	///         If an action is supplied here, the <see cref="M:Microsoft.EntityFrameworkCore.DbContext.OnConfiguring(Microsoft.EntityFrameworkCore.DbContextOptionsBuilder)" /> method will still be run if it has
	///         been overridden on the derived context. <see cref="M:Microsoft.EntityFrameworkCore.DbContext.OnConfiguring(Microsoft.EntityFrameworkCore.DbContextOptionsBuilder)" /> configuration will be applied
	///         in addition to configuration performed here.
	///     </para>
	///     <para>
	///         In order for the options to be passed into your context, you need to expose a constructor on your context that takes
	///         <see cref="T:Microsoft.EntityFrameworkCore.DbContextOptions`1" /> and passes it to the base constructor of <see cref="T:Microsoft.EntityFrameworkCore.DbContext" />.
	///     </para>
	/// </param>
	/// <param name="contextLifetime">The lifetime with which to register the DbContext service in the container.</param>
	/// <param name="optionsLifetime">The lifetime with which to register the DbContextOptions service in the container.</param>
	/// <returns>The same service collection so that multiple calls can be chained.</returns>
	public static IServiceCollection AddDbContext<TContextService, TContextImplementation>(this IServiceCollection serviceCollection, Action<IServiceProvider, DbContextOptionsBuilder>? optionsAction, ServiceLifetime contextLifetime = ServiceLifetime.Scoped, ServiceLifetime optionsLifetime = ServiceLifetime.Scoped) where TContextImplementation : DbContext, TContextService
	{
		Check.NotNull(serviceCollection, "serviceCollection");
		if (contextLifetime == ServiceLifetime.Singleton)
		{
			optionsLifetime = ServiceLifetime.Singleton;
		}
		if (optionsAction != null)
		{
			CheckContextConstructors<TContextImplementation>();
		}
		AddCoreServices<TContextImplementation>(serviceCollection, optionsAction, optionsLifetime);
		if (serviceCollection.Any((ServiceDescriptor d) => d.ServiceType == typeof(IDbContextFactorySource<TContextImplementation>)))
		{
			ServiceDescriptor serviceDescriptor = serviceCollection.FirstOrDefault((ServiceDescriptor d) => d.ServiceType == typeof(TContextImplementation));
			if (serviceDescriptor != null)
			{
				serviceCollection.Remove(serviceDescriptor);
			}
		}
		serviceCollection.TryAdd(new ServiceDescriptor(typeof(TContextService), typeof(TContextImplementation), contextLifetime));
		if (typeof(TContextService) != typeof(TContextImplementation))
		{
			serviceCollection.TryAdd(new ServiceDescriptor(typeof(TContextImplementation), (IServiceProvider p) => (TContextImplementation)(object)p.GetService<TContextService>(), contextLifetime));
		}
		return serviceCollection;
	}

	/// <summary>
	///     <para>
	///         Registers an <see cref="T:Microsoft.EntityFrameworkCore.IDbContextFactory`1" /> in the <see cref="T:Microsoft.Extensions.DependencyInjection.IServiceCollection" /> to create instances
	///         of given <see cref="T:Microsoft.EntityFrameworkCore.DbContext" /> type.
	///     </para>
	///     <para>
	///         Registering a factory instead of registering the context type directly allows for easy creation of new
	///         <see cref="T:Microsoft.EntityFrameworkCore.DbContext" /> instances.
	///         Registering a factory is recommended for Blazor applications and other situations where the dependency
	///         injection scope is not aligned with the context lifetime.
	///     </para>
	///     <para>
	///         Use this method when using dependency injection in your application, such as with Blazor.
	///         For applications that don't use dependency injection, consider creating <see cref="T:Microsoft.EntityFrameworkCore.DbContext" />
	///         instances directly with its constructor. The <see cref="M:Microsoft.EntityFrameworkCore.DbContext.OnConfiguring(Microsoft.EntityFrameworkCore.DbContextOptionsBuilder)" /> method can then be
	///         overridden to configure a connection string and other options.
	///     </para>
	///     <para>
	///         For convenience, this method also registers the context type itself as a scoped service. This allows a context
	///         instance to be resolved from a dependency injection scope directly or created by the factory, as appropriate.
	///     </para>
	///     <para>
	///         Entity Framework Core does not support multiple parallel operations being run on the same <see cref="T:Microsoft.EntityFrameworkCore.DbContext" />
	///         instance. This includes both parallel execution of async queries and any explicit concurrent use from multiple threads.
	///         Therefore, always await async calls immediately, or use separate DbContext instances for operations that execute
	///         in parallel. See <see href="https://aka.ms/efcore-docs-threading">Avoiding DbContext threading issues</see> for more information.
	///     </para>
	///     <para>
	///         See <see href="https://aka.ms/efcore-docs-di">Using DbContext with dependency injection</see> and
	///         <see href="https://aka.ms/efcore-docs-dbcontext-factory">Using DbContext factories</see> for more information.
	///     </para>
	/// </summary>
	/// <typeparam name="TContext">The type of <see cref="T:Microsoft.EntityFrameworkCore.DbContext" /> to be created by the factory.</typeparam>
	/// <param name="serviceCollection">The <see cref="T:Microsoft.Extensions.DependencyInjection.IServiceCollection" /> to add services to.</param>
	/// <param name="optionsAction">
	///     <para>
	///         An optional action to configure the <see cref="T:Microsoft.EntityFrameworkCore.DbContextOptions" /> for the context. This provides an
	///         alternative to performing configuration of the context by overriding the
	///         <see cref="M:Microsoft.EntityFrameworkCore.DbContext.OnConfiguring(Microsoft.EntityFrameworkCore.DbContextOptionsBuilder)" /> method in your derived context.
	///     </para>
	///     <para>
	///         If an action is supplied here, the <see cref="M:Microsoft.EntityFrameworkCore.DbContext.OnConfiguring(Microsoft.EntityFrameworkCore.DbContextOptionsBuilder)" /> method will still be run if it has
	///         been overridden on the derived context. <see cref="M:Microsoft.EntityFrameworkCore.DbContext.OnConfiguring(Microsoft.EntityFrameworkCore.DbContextOptionsBuilder)" /> configuration will be applied
	///         in addition to configuration performed here.
	///     </para>
	///     <para>
	///         In order for the options to be passed into your context, you need to expose a constructor on your context that takes
	///         <see cref="T:Microsoft.EntityFrameworkCore.DbContextOptions`1" /> and passes it to the base constructor of <see cref="T:Microsoft.EntityFrameworkCore.DbContext" />.
	///     </para>
	/// </param>
	/// <param name="lifetime">
	///     The lifetime with which to register the factory and options.
	///     The default is <see cref="F:Microsoft.Extensions.DependencyInjection.ServiceLifetime.Singleton" />
	/// </param>
	/// <returns>The same service collection so that multiple calls can be chained.</returns>
	public static IServiceCollection AddDbContextFactory<TContext>(this IServiceCollection serviceCollection, Action<DbContextOptionsBuilder>? optionsAction = null, ServiceLifetime lifetime = ServiceLifetime.Singleton) where TContext : DbContext
	{
		return serviceCollection.AddDbContextFactory<TContext, DbContextFactory<TContext>>(optionsAction, lifetime);
	}

	/// <summary>
	///     <para>
	///         Registers an <see cref="T:Microsoft.EntityFrameworkCore.IDbContextFactory`1" /> in the <see cref="T:Microsoft.Extensions.DependencyInjection.IServiceCollection" /> to create instances
	///         of given <see cref="T:Microsoft.EntityFrameworkCore.DbContext" /> type.
	///     </para>
	///     <para>
	///         Registering a factory instead of registering the context type directly allows for easy creation of new
	///         <see cref="T:Microsoft.EntityFrameworkCore.DbContext" /> instances.
	///         Registering a factory is recommended for Blazor applications and other situations where the dependency
	///         injection scope is not aligned with the context lifetime.
	///     </para>
	///     <para>
	///         Use this method when using dependency injection in your application, such as with Blazor.
	///         For applications that don't use dependency injection, consider creating <see cref="T:Microsoft.EntityFrameworkCore.DbContext" />
	///         instances directly with its constructor. The <see cref="M:Microsoft.EntityFrameworkCore.DbContext.OnConfiguring(Microsoft.EntityFrameworkCore.DbContextOptionsBuilder)" /> method can then be
	///         overridden to configure a connection string and other options.
	///     </para>
	///     <para>
	///         For convenience, this method also registers the context type itself as a scoped service. This allows a context
	///         instance to be resolved from a dependency injection scope directly or created by the factory, as appropriate.
	///     </para>
	///     <para>
	///         This overload allows a specific implementation of <see cref="T:Microsoft.EntityFrameworkCore.IDbContextFactory`1" /> to be registered
	///         instead of using the default factory shipped with EF Core.
	///     </para>
	///     <para>
	///         Entity Framework Core does not support multiple parallel operations being run on the same <see cref="T:Microsoft.EntityFrameworkCore.DbContext" />
	///         instance. This includes both parallel execution of async queries and any explicit concurrent use from multiple threads.
	///         Therefore, always await async calls immediately, or use separate DbContext instances for operations that execute
	///         in parallel. See <see href="https://aka.ms/efcore-docs-threading">Avoiding DbContext threading issues</see> for more information.
	///     </para>
	///     <para>
	///         See <see href="https://aka.ms/efcore-docs-di">Using DbContext with dependency injection</see> and
	///         <see href="https://aka.ms/efcore-docs-dbcontext-factory">Using DbContext factories</see> for more information.
	///     </para>
	/// </summary>
	/// <typeparam name="TContext">The type of <see cref="T:Microsoft.EntityFrameworkCore.DbContext" /> to be created by the factory.</typeparam>
	/// <typeparam name="TFactory">The type of <see cref="T:Microsoft.EntityFrameworkCore.IDbContextFactory`1" /> to register.</typeparam>
	/// <param name="serviceCollection">The <see cref="T:Microsoft.Extensions.DependencyInjection.IServiceCollection" /> to add services to.</param>
	/// <param name="optionsAction">
	///     <para>
	///         An optional action to configure the <see cref="T:Microsoft.EntityFrameworkCore.DbContextOptions" /> for the context. This provides an
	///         alternative to performing configuration of the context by overriding the
	///         <see cref="M:Microsoft.EntityFrameworkCore.DbContext.OnConfiguring(Microsoft.EntityFrameworkCore.DbContextOptionsBuilder)" /> method in your derived context.
	///     </para>
	///     <para>
	///         If an action is supplied here, the <see cref="M:Microsoft.EntityFrameworkCore.DbContext.OnConfiguring(Microsoft.EntityFrameworkCore.DbContextOptionsBuilder)" /> method will still be run if it has
	///         been overridden on the derived context. <see cref="M:Microsoft.EntityFrameworkCore.DbContext.OnConfiguring(Microsoft.EntityFrameworkCore.DbContextOptionsBuilder)" /> configuration will be applied
	///         in addition to configuration performed here.
	///     </para>
	///     <para>
	///         In order for the options to be passed into your context, you need to expose a constructor on your context that takes
	///         <see cref="T:Microsoft.EntityFrameworkCore.DbContextOptions`1" /> and passes it to the base constructor of <see cref="T:Microsoft.EntityFrameworkCore.DbContext" />.
	///     </para>
	/// </param>
	/// <param name="lifetime">
	///     The lifetime with which to register the factory and options.
	///     The default is <see cref="F:Microsoft.Extensions.DependencyInjection.ServiceLifetime.Singleton" />
	/// </param>
	/// <returns>The same service collection so that multiple calls can be chained.</returns>
	public static IServiceCollection AddDbContextFactory<TContext, TFactory>(this IServiceCollection serviceCollection, Action<DbContextOptionsBuilder>? optionsAction = null, ServiceLifetime lifetime = ServiceLifetime.Singleton) where TContext : DbContext where TFactory : IDbContextFactory<TContext>
	{
		Action<DbContextOptionsBuilder> optionsAction2 = optionsAction;
		return serviceCollection.AddDbContextFactory<TContext, TFactory>((optionsAction2 == null) ? null : ((Action<IServiceProvider, DbContextOptionsBuilder>)delegate(IServiceProvider p, DbContextOptionsBuilder b)
		{
			optionsAction2(b);
		}), lifetime);
	}

	/// <summary>
	///     <para>
	///         Registers an <see cref="T:Microsoft.EntityFrameworkCore.IDbContextFactory`1" /> in the <see cref="T:Microsoft.Extensions.DependencyInjection.IServiceCollection" /> to create instances
	///         of given <see cref="T:Microsoft.EntityFrameworkCore.DbContext" /> type.
	///     </para>
	///     <para>
	///         Registering a factory instead of registering the context type directly allows for easy creation of new
	///         <see cref="T:Microsoft.EntityFrameworkCore.DbContext" /> instances.
	///         Registering a factory is recommended for Blazor applications and other situations where the dependency
	///         injection scope is not aligned with the context lifetime.
	///     </para>
	///     <para>
	///         Use this method when using dependency injection in your application, such as with Blazor.
	///         For applications that don't use dependency injection, consider creating <see cref="T:Microsoft.EntityFrameworkCore.DbContext" />
	///         instances directly with its constructor. The <see cref="M:Microsoft.EntityFrameworkCore.DbContext.OnConfiguring(Microsoft.EntityFrameworkCore.DbContextOptionsBuilder)" /> method can then be
	///         overridden to configure a connection string and other options.
	///     </para>
	///     <para>
	///         For convenience, this method also registers the context type itself as a scoped service. This allows a context
	///         instance to be resolved from a dependency injection scope directly or created by the factory, as appropriate.
	///     </para>
	///     <para>
	///         This overload has an <paramref name="optionsAction" /> that provides the application's
	///         <see cref="T:System.IServiceProvider" />. This is useful if you want to setup Entity Framework Core to resolve
	///         its internal services from the primary application service provider.
	///         By default, we recommend using
	///         <see cref="M:Microsoft.Extensions.DependencyInjection.EntityFrameworkServiceCollectionExtensions.AddDbContextFactory``1(Microsoft.Extensions.DependencyInjection.IServiceCollection,System.Action{Microsoft.EntityFrameworkCore.DbContextOptionsBuilder},Microsoft.Extensions.DependencyInjection.ServiceLifetime)" /> which allows
	///         Entity Framework to create and maintain its own <see cref="T:System.IServiceProvider" /> for internal Entity Framework services.
	///     </para>
	///     <para>
	///         Entity Framework Core does not support multiple parallel operations being run on the same <see cref="T:Microsoft.EntityFrameworkCore.DbContext" />
	///         instance. This includes both parallel execution of async queries and any explicit concurrent use from multiple threads.
	///         Therefore, always await async calls immediately, or use separate DbContext instances for operations that execute
	///         in parallel. See <see href="https://aka.ms/efcore-docs-threading">Avoiding DbContext threading issues</see> for more information.
	///     </para>
	///     <para>
	///         See <see href="https://aka.ms/efcore-docs-di">Using DbContext with dependency injection</see> and
	///         <see href="https://aka.ms/efcore-docs-dbcontext-factory">Using DbContext factories</see> for more information.
	///     </para>
	/// </summary>
	/// <typeparam name="TContext">The type of <see cref="T:Microsoft.EntityFrameworkCore.DbContext" /> to be created by the factory.</typeparam>
	/// <param name="serviceCollection">The <see cref="T:Microsoft.Extensions.DependencyInjection.IServiceCollection" /> to add services to.</param>
	/// <param name="optionsAction">
	///     <para>
	///         An optional action to configure the <see cref="T:Microsoft.EntityFrameworkCore.DbContextOptions" /> for the context. This provides an
	///         alternative to performing configuration of the context by overriding the
	///         <see cref="M:Microsoft.EntityFrameworkCore.DbContext.OnConfiguring(Microsoft.EntityFrameworkCore.DbContextOptionsBuilder)" /> method in your derived context.
	///     </para>
	///     <para>
	///         If an action is supplied here, the <see cref="M:Microsoft.EntityFrameworkCore.DbContext.OnConfiguring(Microsoft.EntityFrameworkCore.DbContextOptionsBuilder)" /> method will still be run if it has
	///         been overridden on the derived context. <see cref="M:Microsoft.EntityFrameworkCore.DbContext.OnConfiguring(Microsoft.EntityFrameworkCore.DbContextOptionsBuilder)" /> configuration will be applied
	///         in addition to configuration performed here.
	///     </para>
	///     <para>
	///         In order for the options to be passed into your context, you need to expose a constructor on your context that takes
	///         <see cref="T:Microsoft.EntityFrameworkCore.DbContextOptions`1" /> and passes it to the base constructor of <see cref="T:Microsoft.EntityFrameworkCore.DbContext" />.
	///     </para>
	/// </param>
	/// <param name="lifetime">
	///     The lifetime with which to register the factory and options.
	///     The default is <see cref="F:Microsoft.Extensions.DependencyInjection.ServiceLifetime.Singleton" />
	/// </param>
	/// <returns>The same service collection so that multiple calls can be chained.</returns>
	public static IServiceCollection AddDbContextFactory<TContext>(this IServiceCollection serviceCollection, Action<IServiceProvider, DbContextOptionsBuilder> optionsAction, ServiceLifetime lifetime = ServiceLifetime.Singleton) where TContext : DbContext
	{
		return serviceCollection.AddDbContextFactory<TContext, DbContextFactory<TContext>>(optionsAction, lifetime);
	}

	/// <summary>
	///     <para>
	///         Registers an <see cref="T:Microsoft.EntityFrameworkCore.IDbContextFactory`1" /> in the <see cref="T:Microsoft.Extensions.DependencyInjection.IServiceCollection" /> to create instances
	///         of given <see cref="T:Microsoft.EntityFrameworkCore.DbContext" /> type.
	///     </para>
	///     <para>
	///         Registering a factory instead of registering the context type directly allows for easy creation of new
	///         <see cref="T:Microsoft.EntityFrameworkCore.DbContext" /> instances.
	///         Registering a factory is recommended for Blazor applications and other situations where the dependency
	///         injection scope is not aligned with the context lifetime.
	///     </para>
	///     <para>
	///         Use this method when using dependency injection in your application, such as with Blazor.
	///         For applications that don't use dependency injection, consider creating <see cref="T:Microsoft.EntityFrameworkCore.DbContext" />
	///         instances directly with its constructor. The <see cref="M:Microsoft.EntityFrameworkCore.DbContext.OnConfiguring(Microsoft.EntityFrameworkCore.DbContextOptionsBuilder)" /> method can then be
	///         overridden to configure a connection string and other options.
	///     </para>
	///     <para>
	///         For convenience, this method also registers the context type itself as a scoped service. This allows a context
	///         instance to be resolved from a dependency injection scope directly or created by the factory, as appropriate.
	///     </para>
	///     <para>
	///         This overload allows a specific implementation of <see cref="T:Microsoft.EntityFrameworkCore.IDbContextFactory`1" /> to be registered
	///         instead of using the default factory shipped with EF Core.
	///     </para>
	///     <para>
	///         This overload has an <paramref name="optionsAction" /> that provides the application's
	///         <see cref="T:System.IServiceProvider" />. This is useful if you want to setup Entity Framework Core to resolve
	///         its internal services from the primary application service provider.
	///         By default, we recommend using
	///         <see cref="M:Microsoft.Extensions.DependencyInjection.EntityFrameworkServiceCollectionExtensions.AddDbContextFactory``1(Microsoft.Extensions.DependencyInjection.IServiceCollection,System.Action{Microsoft.EntityFrameworkCore.DbContextOptionsBuilder},Microsoft.Extensions.DependencyInjection.ServiceLifetime)" /> which allows
	///         Entity Framework to create and maintain its own <see cref="T:System.IServiceProvider" /> for internal Entity Framework services.
	///     </para>
	///     <para>
	///         Entity Framework Core does not support multiple parallel operations being run on the same <see cref="T:Microsoft.EntityFrameworkCore.DbContext" />
	///         instance. This includes both parallel execution of async queries and any explicit concurrent use from multiple threads.
	///         Therefore, always await async calls immediately, or use separate DbContext instances for operations that execute
	///         in parallel. See <see href="https://aka.ms/efcore-docs-threading">Avoiding DbContext threading issues</see> for more information.
	///     </para>
	///     <para>
	///         See <see href="https://aka.ms/efcore-docs-di">Using DbContext with dependency injection</see> and
	///         <see href="https://aka.ms/efcore-docs-dbcontext-factory">Using DbContext factories</see> for more information.
	///     </para>
	/// </summary>
	/// <typeparam name="TContext">The type of <see cref="T:Microsoft.EntityFrameworkCore.DbContext" /> to be created by the factory.</typeparam>
	/// <typeparam name="TFactory">The type of <see cref="T:Microsoft.EntityFrameworkCore.IDbContextFactory`1" /> to register.</typeparam>
	/// <param name="serviceCollection">The <see cref="T:Microsoft.Extensions.DependencyInjection.IServiceCollection" /> to add services to.</param>
	/// <param name="optionsAction">
	///     <para>
	///         An optional action to configure the <see cref="T:Microsoft.EntityFrameworkCore.DbContextOptions" /> for the context. This provides an
	///         alternative to performing configuration of the context by overriding the
	///         <see cref="M:Microsoft.EntityFrameworkCore.DbContext.OnConfiguring(Microsoft.EntityFrameworkCore.DbContextOptionsBuilder)" /> method in your derived context.
	///     </para>
	///     <para>
	///         If an action is supplied here, the <see cref="M:Microsoft.EntityFrameworkCore.DbContext.OnConfiguring(Microsoft.EntityFrameworkCore.DbContextOptionsBuilder)" /> method will still be run if it has
	///         been overridden on the derived context. <see cref="M:Microsoft.EntityFrameworkCore.DbContext.OnConfiguring(Microsoft.EntityFrameworkCore.DbContextOptionsBuilder)" /> configuration will be applied
	///         in addition to configuration performed here.
	///     </para>
	///     <para>
	///         In order for the options to be passed into your context, you need to expose a constructor on your context that takes
	///         <see cref="T:Microsoft.EntityFrameworkCore.DbContextOptions`1" /> and passes it to the base constructor of <see cref="T:Microsoft.EntityFrameworkCore.DbContext" />.
	///     </para>
	/// </param>
	/// <param name="lifetime">
	///     The lifetime with which to register the factory and options.
	///     The default is <see cref="F:Microsoft.Extensions.DependencyInjection.ServiceLifetime.Singleton" />
	/// </param>
	/// <returns>The same service collection so that multiple calls can be chained.</returns>
	public static IServiceCollection AddDbContextFactory<TContext, TFactory>(this IServiceCollection serviceCollection, Action<IServiceProvider, DbContextOptionsBuilder>? optionsAction, ServiceLifetime lifetime = ServiceLifetime.Singleton) where TContext : DbContext where TFactory : IDbContextFactory<TContext>
	{
		Check.NotNull(serviceCollection, "serviceCollection");
		AddCoreServices<TContext>(serviceCollection, optionsAction, lifetime);
		serviceCollection.AddSingleton<IDbContextFactorySource<TContext>, DbContextFactorySource<TContext>>();
		serviceCollection.TryAdd(new ServiceDescriptor(typeof(IDbContextFactory<TContext>), typeof(TFactory), lifetime));
		serviceCollection.TryAdd(new ServiceDescriptor(typeof(TContext), typeof(TContext), (lifetime != ServiceLifetime.Transient) ? ServiceLifetime.Scoped : ServiceLifetime.Transient));
		return serviceCollection;
	}

	/// <summary>
	///     <para>
	///         Registers an <see cref="T:Microsoft.EntityFrameworkCore.IDbContextFactory`1" /> in the <see cref="T:Microsoft.Extensions.DependencyInjection.IServiceCollection" /> to create instances
	///         of given <see cref="T:Microsoft.EntityFrameworkCore.DbContext" /> type where instances are pooled for reuse.
	///     </para>
	///     <para>
	///         Registering a factory instead of registering the context type directly allows for easy creation of new
	///         <see cref="T:Microsoft.EntityFrameworkCore.DbContext" /> instances.
	///         Registering a factory is recommended for Blazor applications and other situations where the dependency
	///         injection scope is not aligned with the context lifetime.
	///     </para>
	///     <para>
	///         Use this method when using dependency injection in your application, such as with Blazor.
	///         For applications that don't use dependency injection, consider creating <see cref="T:Microsoft.EntityFrameworkCore.DbContext" />
	///         instances directly with its constructor. The <see cref="M:Microsoft.EntityFrameworkCore.DbContext.OnConfiguring(Microsoft.EntityFrameworkCore.DbContextOptionsBuilder)" /> method can then be
	///         overridden to configure a connection string and other options.
	///     </para>
	///     <para>
	///         Entity Framework Core does not support multiple parallel operations being run on the same <see cref="T:Microsoft.EntityFrameworkCore.DbContext" />
	///         instance. This includes both parallel execution of async queries and any explicit concurrent use from multiple threads.
	///         Therefore, always await async calls immediately, or use separate DbContext instances for operations that execute
	///         in parallel. See <see href="https://aka.ms/efcore-docs-threading">Avoiding DbContext threading issues</see> for more information.
	///     </para>
	///     <para>
	///         See <see href="https://aka.ms/efcore-docs-di">Using DbContext with dependency injection</see>,
	///         <see href="https://aka.ms/efcore-docs-dbcontext-factory">Using DbContext factories</see>, and
	///         <see href="https://aka.ms/efcore-docs-dbcontext-pooling">Using DbContext pooling</see> for more information.
	///     </para>
	/// </summary>
	/// <typeparam name="TContext">The type of <see cref="T:Microsoft.EntityFrameworkCore.DbContext" /> to be created by the factory.</typeparam>
	/// <param name="serviceCollection">The <see cref="T:Microsoft.Extensions.DependencyInjection.IServiceCollection" /> to add services to.</param>
	/// <param name="optionsAction">
	///     <para>
	///         A required action to configure the <see cref="T:Microsoft.EntityFrameworkCore.DbContextOptions" /> for the context. When using
	///         context pooling, options configuration must be performed externally; <see cref="M:Microsoft.EntityFrameworkCore.DbContext.OnConfiguring(Microsoft.EntityFrameworkCore.DbContextOptionsBuilder)" />
	///         will not be called.
	///     </para>
	/// </param>
	/// <param name="poolSize">Sets the maximum number of instances retained by the pool. Defaults to 1024.</param>
	/// <returns>The same service collection so that multiple calls can be chained.</returns>
	public static IServiceCollection AddPooledDbContextFactory<TContext>(this IServiceCollection serviceCollection, Action<DbContextOptionsBuilder> optionsAction, int poolSize = 1024) where TContext : DbContext
	{
		Action<DbContextOptionsBuilder> optionsAction2 = optionsAction;
		Check.NotNull(optionsAction2, "optionsAction");
		return serviceCollection.AddPooledDbContextFactory<TContext>(delegate(IServiceProvider _, DbContextOptionsBuilder ob)
		{
			optionsAction2(ob);
		}, poolSize);
	}

	/// <summary>
	///     <para>
	///         Registers an <see cref="T:Microsoft.EntityFrameworkCore.IDbContextFactory`1" /> in the <see cref="T:Microsoft.Extensions.DependencyInjection.IServiceCollection" /> to create instances
	///         of given <see cref="T:Microsoft.EntityFrameworkCore.DbContext" /> type where instances are pooled for reuse.
	///     </para>
	///     <para>
	///         Registering a factory instead of registering the context type directly allows for easy creation of new
	///         <see cref="T:Microsoft.EntityFrameworkCore.DbContext" /> instances.
	///         Registering a factory is recommended for Blazor applications and other situations where the dependency
	///         injection scope is not aligned with the context lifetime.
	///     </para>
	///     <para>
	///         Use this method when using dependency injection in your application, such as with Blazor.
	///         For applications that don't use dependency injection, consider creating <see cref="T:Microsoft.EntityFrameworkCore.DbContext" />
	///         instances directly with its constructor. The <see cref="M:Microsoft.EntityFrameworkCore.DbContext.OnConfiguring(Microsoft.EntityFrameworkCore.DbContextOptionsBuilder)" /> method can then be
	///         overridden to configure a connection string and other options.
	///     </para>
	///     <para>
	///         Entity Framework Core does not support multiple parallel operations being run on the same <see cref="T:Microsoft.EntityFrameworkCore.DbContext" />
	///         instance. This includes both parallel execution of async queries and any explicit concurrent use from multiple threads.
	///         Therefore, always await async calls immediately, or use separate DbContext instances for operations that execute
	///         in parallel. See <see href="https://aka.ms/efcore-docs-threading">Avoiding DbContext threading issues</see> for more information.
	///     </para>
	///     <para>
	///         See <see href="https://aka.ms/efcore-docs-di">Using DbContext with dependency injection</see>,
	///         <see href="https://aka.ms/efcore-docs-dbcontext-factory">Using DbContext factories</see>, and
	///         <see href="https://aka.ms/efcore-docs-dbcontext-pooling">Using DbContext pooling</see> for more information.
	///     </para>
	/// </summary>
	/// <typeparam name="TContext">The type of <see cref="T:Microsoft.EntityFrameworkCore.DbContext" /> to be created by the factory.</typeparam>
	/// <param name="serviceCollection">The <see cref="T:Microsoft.Extensions.DependencyInjection.IServiceCollection" /> to add services to.</param>
	/// <param name="optionsAction">
	///     <para>
	///         A required action to configure the <see cref="T:Microsoft.EntityFrameworkCore.DbContextOptions" /> for the context. When using
	///         context pooling, options configuration must be performed externally; <see cref="M:Microsoft.EntityFrameworkCore.DbContext.OnConfiguring(Microsoft.EntityFrameworkCore.DbContextOptionsBuilder)" />
	///         will not be called.
	///     </para>
	/// </param>
	/// <param name="poolSize">Sets the maximum number of instances retained by the pool. Defaults to 1024.</param>
	/// <returns>The same service collection so that multiple calls can be chained.</returns>
	public static IServiceCollection AddPooledDbContextFactory<TContext>(this IServiceCollection serviceCollection, Action<IServiceProvider, DbContextOptionsBuilder> optionsAction, int poolSize = 1024) where TContext : DbContext
	{
		Check.NotNull(serviceCollection, "serviceCollection");
		Check.NotNull(optionsAction, "optionsAction");
		AddPoolingOptions<TContext>(serviceCollection, optionsAction, poolSize);
		serviceCollection.TryAddSingleton<IDbContextPool<TContext>, DbContextPool<TContext>>();
		serviceCollection.TryAddSingleton((Func<IServiceProvider, IDbContextFactory<TContext>>)((IServiceProvider sp) => new PooledDbContextFactory<TContext>(sp.GetRequiredService<IDbContextPool<TContext>>())));
		return serviceCollection;
	}

	private static void AddCoreServices<TContextImplementation>(IServiceCollection serviceCollection, Action<IServiceProvider, DbContextOptionsBuilder>? optionsAction, ServiceLifetime optionsLifetime) where TContextImplementation : DbContext
	{
		Action<IServiceProvider, DbContextOptionsBuilder> optionsAction2 = optionsAction;
		serviceCollection.TryAdd(new ServiceDescriptor(typeof(DbContextOptions<TContextImplementation>), (IServiceProvider p) => CreateDbContextOptions<TContextImplementation>(p, optionsAction2), optionsLifetime));
		serviceCollection.Add(new ServiceDescriptor(typeof(DbContextOptions), (IServiceProvider p) => p.GetRequiredService<DbContextOptions<TContextImplementation>>(), optionsLifetime));
	}

	private static DbContextOptions<TContext> CreateDbContextOptions<TContext>(IServiceProvider applicationServiceProvider, Action<IServiceProvider, DbContextOptionsBuilder>? optionsAction) where TContext : DbContext
	{
		DbContextOptionsBuilder<TContext> dbContextOptionsBuilder = new DbContextOptionsBuilder<TContext>(new DbContextOptions<TContext>(new Dictionary<Type, IDbContextOptionsExtension>()));
		dbContextOptionsBuilder.UseApplicationServiceProvider(applicationServiceProvider);
		optionsAction?.Invoke(applicationServiceProvider, dbContextOptionsBuilder);
		return dbContextOptionsBuilder.Options;
	}

	private static void CheckContextConstructors<TContext>() where TContext : DbContext
	{
		List<ConstructorInfo> list = typeof(TContext).GetTypeInfo().DeclaredConstructors.ToList();
		if (list.Count == 1 && list[0].GetParameters().Length == 0)
		{
			throw new ArgumentException(CoreStrings.DbContextMissingConstructor(typeof(TContext).ShortDisplayName()));
		}
	}
}

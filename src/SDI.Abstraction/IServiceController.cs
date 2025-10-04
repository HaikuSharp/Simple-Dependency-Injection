using System;

namespace SDI.Abstraction;

/// <summary>
/// Represents the central control point for service registration and scope management in the dependency injection system.
/// </summary>
public interface IServiceController : IServiceRegistrar, IServiceProvider, IDisposable;

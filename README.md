> A lightweight, modular, easy-to-use, minimal, and extensible dependency injection library for C#. 

## Key Features

- **Simple Registration**: Clean extension methods for different lifetimes
- **Constructor Injection**: Dependencies automatically resolved
- **Key-based Registration**: Multiple implementations of the same interface
- **Scope Management**: Proper lifetime management with scopes
- **Safe Resolution**: `GetService()` returns null if not found, `GetRequiredService()` throws exception
- **Batch Resolution**: Get all implementations with `GetServices()`

## Service Registration Flow

**Registration Process:**
```
[User Code] → [ServiceController] → [ServiceDescriptor] → [ServiceAccessor] → [Accessor List]
```

**Detailed Steps:**
|   | Step | Description |
|---|------|-------------|
| 1 | Registration | `controller.RegisterService()` |
| 2 | Validation | Check if service type exists and not already registered |
| 3 | Descripting | Create specific `IServiceDescriptor` |
| 4 | Accessor Creation | `IServiceDescriptor` creates appropriate accessor |
| 5 | Storaging | Accessor added to `ServiceController`'s list |

**Descriptor Types & Their Accessors:**
| Descriptor Type | Accessor Type | Lifetime Behavior |
|----------------|---------------|------------------|
| `SingletonServiceDescriptor` | `SingletonServiceAccessor` | Single instance for entire application |
| `WeakSingletonServiceDescriptor` | `WeakSingletonServiceAccessor` | Single instance with weak reference |
| `LazySingletonServiceDescriptor` | `LazySingletonServiceAccessor` | Created on first access, then cached |
| `ScopedServiceDescriptor` | `ScopedServiceAccessor` | One instance per scope |
| `TransientServiceDescriptor` | `TransientServiceAccessor` | New instance every time |

## Service Resolution Flow

**Service Retrieval Process:**
```
[User Request] → [ServiceProvider] → [ServiceController] → [Find Accessor] → [Access Execution] → [Instance Return]
```

**Detailed Steps:**
|   | Step | Description |
|---|------|-------------|
| 1 | Request | `provider.GetService()` |
| 2 | Accessor Search | `ServiceContainer` searches for matching `IServiceAccessor` |
| 3 | Instance Creation | `Access()` creates/retrieves instance |
| 4 | Return | Instance returned to user |

## Scope Management Flow

**Scope Lifecycle:**
```
[Root Controller] → [Create Scope] → [Scope Provider] → [Scope Container] → [Dispose Scope]
```

**Scope Usage:**
| Usage | Description |
|-------|-------------|
| Root Scope | From `ServiceContainer` |
| Child Scopes | Created from root or other scopes |
| Setup | New `ServiceContainer` for scope instances |
| Instance Storage | Each scope has its own `ServiceContainer` |
  
**Scope Creation:**
|   | Step | Description |
|---|------|-------------|
| 1 | Request | `controller.CreateScope()` |
| 2 | Provider Creation | New `ServiceProvider` with reference to root |
| 3 | Setup | New `ServiceContainer` for scope instances |
| 4 | Self Registration | Provider registers itself in its container as `IServiceProvider` |

**Scoped Service Behavior:**
```
[Scope 1] → [Container] → [Instance A]
[Scope 2] → [Container] → [Instance B]
[Scope 3] → [Container] → [Instance C]
```

## Activation Patterns

**Activator Types:**
| Activator Type | Usage | Behavior |
|----------------|-------|----------|
| `StandaloneServiceActivator` | Parameterless constructors | `new TService()` |
| `ScriptableServiceActivator` | Custom creation logic | Delegate-based activation |
| Built-in to Accessors | Direct instantiation | Various lifetime strategies |

**Lazy Service Activation Flow:**
```
[First Access] → [Lazy Accessor] → [Activator] → [Create Instance] → [Cache (if applicable)] → [Return]
```

## Key Component Interactions

**Core Components:**
Component | Description |
|-----------|-------------|
| `IServiceRegistrar` | Manages registration | 
| `IServiceDescriptor` | Contains the logic for creating an `IServiceAccessor` |
| `IServiceAccessor` | Contains the logic for retrieving the service |
| `IServiceInstanceActivator` | Contains the logic for creating a service instance. Used for lazy implementations. |
| `IServiceProvider` | Current service scope |
| `IServiceContainer` | Instance storage per scope |

## Memory Management

**Disposal Process:**
```
[Dispose Call] → [Scope Dispose] → [Container Dispose] → [Instance Dispose] → [Clear References]
```

**Cleanup Sequence:**
|   | Step | Description |
|---|------|-------------|
| 1 | Scope Dispose | Called when scope using block exits |
| 2 | Container Cleanup | All instances removed from container |
| 3 | Instance Disposal | Each instance checked for `IDisposable` |

# Quick Start

### Basic Setup

```csharp
// Create the root service controller - this is your main DI container
var controller = ServiceController.Create<ServiceController>();
```

### Service Registration

**Different Service Lifetimes:**
```csharp
// Instance - register an existing instance
controller.RegisterSingletonService<IService>(new Service());
```
```csharp
// Singleton - single instance for the entire application lifetime
controller.RegisterLazySingletonService<IService, Service>(serviceActivator);
```
```csharp
// Transient - new instance created every time it's requested  
controller.RegisterTransientService<IService, Service>(serviceActivator);
```
```csharp
// Scoped - single instance per scope
controller.RegisterScopedService<IService, Service>(serviceActivator);
```

**Multiple Implementations with Keys:**
```csharp
// Register multiple implementations of the same interface with different keys
controller.RegisterSingletonService<IService, FirstService>("first", firstServiceActivator);
controller.RegisterSingletonService<IService, SecondService>("second", secondServiceActivator); 
controller.RegisterSingletonService<IService, ThirdService>("third", thirdServiceActivator);
```

**Standalone Services (parameterless constructors):**
> Register services that don't have dependencies
```csharp
controller.RegisterLazySingletonStandaloneService<IService, Service>();
```
```csharp
controller.RegisterTransientStandaloneService<IService, Service>();
```
```csharp
controller.RegisterScopedStandaloneService<IService, Service>();
```

### Service Resolution

**Basic Resolution with Extensions:**
```csharp
// Simple resolution by type
var service = provider.GetService<IService>();
```
```csharp
// Resolution with key
var keyedService = provider.GetService<IService>("second");
```
```csharp
// Required services (throws exception if not found)
var requiredService = provider.GetRequiredService<IService>();
```

**Working with Scopes:**
```csharp
// Create a new scope
using (var scope = controller.CreateScope())
{
    // Services resolved within this scope
    var scopedService1 = scope.GetService<IService>();
    var scopedService2 = scope.GetService<IService>();
    
    // scopedService1 and scopedService2 are the same instance within this scope
}
// Scope and its scoped services are disposed here
```

**Multiple Services Resolution:**
```csharp
// Get all implementations of an interface
var allServices = provider.GetServices<IService>();

foreach (IService service in allServices)
{
    service.DoSomething();
}

// Get services by key only
var keyedServices = provider.GetServices("someKey");
```

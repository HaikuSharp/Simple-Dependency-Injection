> A lightweight, modular, easy-to-use, minimal, and extensible dependency injection library for C#. 

## Key Features

- **Simple Registration**: Clean extension methods for different lifetimes
- **Constructor Injection**: Dependencies automatically resolved
- **Key-based Registration**: Multiple implementations of the same interface
- **Scope Management**: Proper lifetime management with scopes
- **Safe Resolution**: `GetService()` returns null if not found, `GetRequiredService()` throws exception
- **Batch Resolution**: Get all implementations with `GetServices()`

# Service Registration Flow

### **Registration Process**

```
[User Code] → [ServiceController] → [ServiceDescriptor] → [ServiceAccessor] → [Accessor List]
```

**Detailed Steps:**
1. **User Registration**: `controller.RegisterService(descriptor)`
2. **ID Validation**: Check if service type exists and not already registered
3. **Descriptor Creation**: Create specific descriptor (Singleton/Scoped/Transient)
4. **Accessor Creation**: Descriptor creates appropriate accessor
5. **Storage**: Accessor added to controller's `m_Accessors` list

### **Descriptor Types & Their Accessors**

| Descriptor Type | Accessor Type | Lifetime Behavior |
|----------------|---------------|------------------|
| `SingletonServiceDescriptor` | `SingletonServiceAccessor` | Single instance for entire application |
| `WeakSingletonServiceDescriptor` | `WeakSingletonServiceAccessor` | Single instance with weak reference |
| `LazySingletonServiceDescriptor` | `LazySingletonServiceAccessor` | Created on first access, then cached |
| `ScopedServiceDescriptor` | `ScopedServiceAccessor` | One instance per scope |
| `TransientServiceDescriptor` | `TransientServiceAccessor` | New instance every time |

## Service Resolution Flow

### **Service Retrieval Process**

```
[User Request] → [ServiceProvider] → [ServiceController] → [Find Accessor] → [Access Execution] → [Instance Return]
```

**Detailed Steps:**
1. **User Request**: `provider.GetService<T>()` or `provider.GetService(ServiceId.From<T>())`
2. **Scope Resolution**: Provider determines correct scope (root or current)
3. **Accessor Search**: Controller searches `m_Accessors` for matching accessor
4. **Access Check**: `CanAccess(requestedId)` verifies match
5. **Instance Creation**: `Access(provider, id)` creates/retrieves instance
6. **Container Storage**: Scoped services stored in scope container
7. **Return**: Instance returned to user

### **Accessor Matching Logic**

```
Requested ID: ServiceId.From<IService>("key")
Accessor ID:  ServiceId.From<IService>("key")  → MATCH ✓

Requested ID: ServiceId.From<IService>("key")  
Accessor ID:  ServiceId.From<IService>(null)   → NO MATCH ✗

Requested ID: ServiceId.From<IService>(null)
Accessor ID:  ServiceId.From<IService>("key")   → NO MATCH ✗
```

## Scope Management Flow

### **Scope Lifecycle**

```
[Root Controller] → [Create Scope] → [Scope Provider] → [Scope Container] → [Dispose Scope]
```

**Scope Creation:**
1. **User Request**: `controller.CreateScope()`
2. **Provider Creation**: New `ServiceScopedProvider` with reference to root
3. **Container Setup**: New `ServiceContainer` for scope instances
4. **Self-Registration**: Provider registers itself in its container as `IServiceProvider`

**Scope Usage:**
- **Root Scope**: `m_RootScopeProvider` in controller
- **Child Scopes**: Created from root or other scopes
- **Instance Storage**: Each scope has its own `ServiceContainer`

### **Scoped Service Behavior**

```
[Scope 1] → [Container] → [Instance A]
[Scope 2] → [Container] → [Instance B]
[Scope 3] → [Container] → [Instance C]
```

## Activation Patterns

### **Activator Types**

| Activator Type | Usage | Behavior |
|---------------|--------|----------|
| `StandaloneServiceActivator` | Parameterless constructors | `new TService()` |
| `ScriptableServiceActivator` | Custom creation logic | Delegate-based activation |
| Built-in to Accessors | Direct instantiation | Various lifetime strategies |

### **Lazy Service Activation Flow**

```
[First Access] → [Lazy Accessor] → [Activator] → [Create Instance] → [Cache (if applicable)] → [Return]
```

## Key Component Interactions

### **Core Components Relationship**
| ↓ |Component|Description|
|---|---------|-----------|
| ↓ | `ServiceController` | Manages registration | 
| ↓ | `IServiceAccessProvider` | Resolution logic |
| ↓ | `IServiceAccessProvider` | Per-service access logic |
| ↓ | `IServiceAccessor` | Creation logic |
| ↓ | `ServiceContainer` | Instance storage per scope |

### **Exception Handling Flow**

```
[Operation] → [Validation] → [Exception Check] → [Error/Continue]
```

## Memory Management

### **Disposal Process**

```
[Dispose Call] → [Scope Dispose] → [Container Dispose] → [Instance Dispose] → [Clear References]
```

**Cleanup Sequence:**
1. **Scope Dispose**: Called when scope using block exits
2. **Container Cleanup**: All instances removed from container
3. **Instance Disposal**: Each instance checked for `IDisposable`
4. **GC Support**: Weak references allow garbage collection
5. **Cycle Prevention**: Self-references removed before disposal

This architecture provides a robust, extensible dependency injection system with clear separation of concerns between registration, resolution, and lifetime management.

# Quick Start

### Basic Setup

```csharp
// Create the root service controller - this is your main DI container
var controller = ServiceController.Create<ServiceController>();
```

### Service Registration

**Different Service Lifetimes**

```csharp
// Singleton - single instance for the entire application lifetime
controller.RegisterSingletonService<IService, ServiceA>(serviceAActivator);
```
```csharp
// Transient - new instance created every time it's requested  
controller.RegisterTransientService<IService, ServiceB>(serviceBActivator);
```
```csharp
// Scoped - single instance per scope
controller.RegisterScopedService<IService, ServiceC>(serviceCActivator;
```
```csharp
// Instance - register an existing instance
controller.RegisterSingletonService<IServiceD>(new ServiceD());
```

**Multiple Implementations with Keys**

```csharp
// Register multiple implementations of the same interface with different keys
controller.RegisterSingletonService<IService, FirstService>("first", firstServiceActivator);
controller.RegisterSingletonService<IService, SecondService>("second", secondServiceActivator); 
controller.RegisterSingletonService<IService, ThirdService>("third", thirdServiceActivator);
```

**Standalone Services (parameterless constructors)**

```csharp
// Register services that don't have dependencies
controller.RegisterLazySingletonStandaloneService<StandaloneService>();
controller.RegisterTransientStandaloneService<StandaloneService>();
controller.RegisterScopedStandaloneService<StandaloneService>();
```

### Service Resolution

**Basic Resolution with Extensions**

```csharp
// Simple resolution by type
IServiceA serviceA = provider.GetService<IServiceA>();
```
```csharp
// Resolution with key
IService specificService = provider.GetService<IService>("second");
```
```csharp
// Required services (throws exception if not found)
IServiceA requiredService = provider.GetRequiredService<IServiceA>();
```

**Working with Scopes**

```csharp
// Create a new scope
using (var scope = controller.CreateScope())
{
    // Services resolved within this scope
    IServiceC scopedService1 = scope.GetService<IServiceC>();
    IServiceC scopedService2 = scope.GetService<IServiceC>();
    
    // scopedService1 and scopedService2 are the same instance within this scope
}
// Scope and its scoped services are disposed here
```

**Multiple Services Resolution**

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

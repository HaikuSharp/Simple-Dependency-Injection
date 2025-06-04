# Simple-Dependency-Injection (SDI)

Simple-Dependency-Injection (SDI) - это легковесная библиотека для внедрения зависимостей в .NET приложениях.

## Основные концепции

### 1. Регистрация сервисов
Сервисы регистрируются с помощью атрибутов:

```csharp
[TypeInject(typeof(IFirstService))]
[KeyInject("0")] 
[LifeTimeInject(typeof(LazySingletonServiceLifeTime))]
public class FirstService0 : IFirstService
{
    public void PrintHello() => Console.WriteLine("Hello from FirstService0");
}
```

### 2. Жизненные циклы
Доступные варианты жизненных циклов:

| Тип | Описание | Класс |
|-----|----------|-------|
| **Singleton** | Один экземпляр на все приложение | `SingletonServiceLifeTime` |
| **Lazy Singleton** | Создается при первом запросе | `LazySingletonServiceLifeTime` |
| **Transient** | Новый экземпляр при каждом запросе | `TransientServiceLifeTime` |

### 3. Внедрение зависимостей
Библиотека поддерживает:
- Внедрение через конструктор
- Внедрение коллекций сервисов (`IEnumerable<T>`)
- Именованные сервисы (по ключу)

## Пример использования

### 1. Настройка сервисов
```csharp
// Создаем контроллер
var controller = ServiceController.Create();

// Регистрируем сервисы из сборки
controller.RegisterServices(typeof(Program).Assembly);
```
```csharp
var setup = new ServiceSetup
{
    // Можно переопределить стандартные реализации
};

var controller = ServiceController.Create(setup);
```

### 2. Использование сервисов
```csharp
// Получение сервиса по типу и ключу
var firstService = controller.GetService<IFirstService>("0");
firstService.PrintHello();

// Автоматическое внедрение IEnumerable
var secondService = controller.GetService<ISecondService>();
secondService.PrintHello();
```

### 3. Пример сервисов
```csharp
public interface IFirstService : IService { }
public interface ISecondService : IService 
{
    IFirstService First { get; }
}

[TypeInject(typeof(ISecondService))]
public class SecondService(IEnumerable<IFirstService> firsts) : ISecondService
{
    public IFirstService First => firsts.First();
}
```

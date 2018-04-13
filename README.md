
# Overview

Data Suit is a random data generator. It generates data for primitive data types and POCO classes. At the beginning, it was an experimental project for several purposes. Later, I changed it into a formal format. It is designed with SOLID principles. Mapping fields is also supported via fluent API. 

Basis of the API is shown below. For more detailed examples, you can see at  [Samples](https://github.com/lyzerk/DataSuit/tree/master/samples)

# Content
1. [Build and Nuget](https://github.com/lyzerk/DataSuit#build-and-nuget)
2. [Usage](https://github.com/lyzerk/DataSuit#usage)
3. [Customizing](https://github.com/lyzerk/DataSuit#customizing)
4. [Import/Export](https://github.com/lyzerk/DataSuit#importexport)
5. [AspNetCore](https://github.com/lyzerk/DataSuit#aspnetcore)

# Build and nuget

.NET Standard library of data suit. 

|Build Status (travis-ci)|DataSuit Nuget|DataSuit.AspNetCore Nuget|
|---|---|---|
|[![Build Status](https://travis-ci.org/lyzerk/DataSuit.svg?branch=master)](https://travis-ci.org/DataSuit/DataSuit)|[![NuGet](https://img.shields.io/nuget/v/DataSuit.svg)](https://www.nuget.org/packages/DataSuit/)| [![NuGet](https://img.shields.io/nuget/v/DataSuit.AspNetCore.svg)](https://www.nuget.org/packages/DataSuit.AspNetCore/)|
|   |   |   |
# Usage

## DataSuit Class
DataSuit class is necessary for every operation.

```csharp
DataSuit suit = new DataSuit();
```

DataSuit comes with built-in data. If you want to enable it, you have to call:

```csharp
suit.Load();
```

You can see the built-in data fields on [BuiltIn.md](https://github.com/lyzerk/DataSuit/tree/master/BuiltIn.md) file.

## POCO example
For example POCO class data generation

```csharp
var personGenerator = suit.GeneratorOf<Person>();
var listOfPersons = personGenerator.Generate(count: 10);
```
It generates 10 person classes with respect to properties of it.

```csharp
var person = personGenerator.Generate();
```

It generates a Person class


## Primitive example
```csharp
var primitiveGenerator = suit.GeneratorOfPrimitives();
var names = primitiveGenerator.String("FirstName", count: 5);
foreach (var name in names)
    Console.WriteLine($"Name:{name}");
```
It generates 5 names as string list


# Customizing
DataSuit API supports customizing very well. A fluent API design welcomes us here.

```csharp
ISettings settings = new Settings();
DataSuit suit = new DataSuit(settings);

var barList = new List<string>() { "Foo", "Bar", "Baz" };

suit.Build<Foo>()
    .Collection(i => i.Bar, barList, ProviderType.Random)
    .Range(i => i.Range, 10, 40)
    .Set(i => i.Static, "DataSuit");

var fooGenerator = suit.GeneratorOf<Foo>();
var data = fooGenerator.Generate(count: 4);

foreach (var item in data)
    Console.WriteLine($"{item.Bar} {item.Range} {item.Static}");
```

Result of the foo classes
```
Bar 19 DataSuit
Foo 23 DataSuit
Baz 11 DataSuit
Baz 33 DataSuit
```

## Mapping

### Collection
```csharp
suit.Build<T>()
    .Collection(i => i.Field, list,  ProviderType.Sequential)
```

### Static Variable
```csharp
suit.Build<T>()
    .Set(i => i.Field, Variable)
```

### Range
It requires integer or double range values
```csharp
suit.Build<T>()
    .Range(i => i.Field, 10, 20)
    .Range(i => i.Field, 10.5, 20.3)
```

### Dummy
It gives lorem ipsum text data with given length 

```csharp
suit.Build<T>()
    .Dummy(i => i.Field, 300)
```

### Incremental
It generates integer or long values by sequential order. Such as IDs.

```csharp
suit.Build<T>()
    .Incremental(i => i.Id)
```

### Func
It does run a function for every MoveNext event.

```csharp
suit.Build<T>()
    .Func(i => i.Id, () => Guid.NewGuid().ToString())
```

### Guid
```csharp
suit.Build<T>()
    .Guid(i => i.Id)
```

Also, you can use Build method without generic type.

### Non-generic type
```csharp
suit.Build()
    .Range("Salary", 3000, 5000)
    .Incremental("id");
```

# Import/Export

Following code, export settings of the current suit as JSON string.
```csharp
suit.Export();
```
Note that: FuncProvider can't be exported. Therefore, you have to re-define the Func providers when you are importing them to a suit. 

Following code, import settings with the given JSON string.
```csharp
suit.Import(data);
```

You can see an example setting JSON file from [here](https://raw.githubusercontent.com/lyzerk/DataSuit/master/samples/DataSuit.SettingExportImport/settings.json)

# AspNetCore

You can get the package DataSuit.AspNetCore from [nuget](https://www.nuget.org/packages/DataSuit.AspNetCore/).

Just you need to add DataSuit to Startup.cs on ConfigureServices method.
```csharp
services.AddDataSuit();
```

Also, you can customize the API via
```csharp
services.AddDataSuit(i =>
{
    i.DefaultData = false; // disable built-in data 
    i.Build()
        .Range("Salary", 3000, 5000)
        .Incremental("id");

    i.Ready();
});
```

At the controller, you can inject IGenerator<T>
```csharp
private readonly IGenerator<PersonViewModel> _personGenerator;

public HomeController(IGenerator<Models.PersonViewModel> personGenerator)
{
    _personGenerator = personGenerator;
}

public IActionResult GetPersons()
{
    return Json(_personGenerator.Generate(count: 5));
}
```


# TODO
1. Random select of enums
2. Class property filling on reflection
2. Fast setup for unit test
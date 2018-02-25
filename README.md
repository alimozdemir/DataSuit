# Overview

Data Suit is a random data generator. It generates the data for primitive data types and POCO classes. At the beginning, it was an experimental project for several purposes. Later, I changed it into a formal format. It is designed with SOLID principles. Customizing for your data is supported with a fluent API. 

Basis of the API is shown below. For more detailed examples, you can see at  [Samples](https://github.com/lyzerk/DataSuit/tree/master/samples)

# Build and nuget

.NET Standard library of data suit. [![Build Status](https://travis-ci.org/DataSuit/DataSuit.svg?branch=master)](https://travis-ci.org/DataSuit/DataSuit) [![NuGet](https://img.shields.io/nuget/v/DataSuit.svg)](https://www.nuget.org/packages/DataSuit/)


## Usage

### DataSuit Class
DataSuit Class is a necessary for every operation.

```csharp
ISettings settings = new Settings();
DataSuit suit = new DataSuit(settings);
```

DataSuit comes with built-in data. If you want to enable it, you have to call:

```csharp
suit.Load();
```

### POCO example
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


### Primitive example

```csharp
var primitiveGenerator = suit.GeneratorOfPrimitives();
var names = primitiveGenerator.String("FirstName", count: 5);
foreach (var name in names)
    Console.WriteLine($"Name:{name}");
```
It generates 5 names as string list


## Customizing
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

A Json example soon.





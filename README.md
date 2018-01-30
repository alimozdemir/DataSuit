# DataSuit

Data Suit is a dummy data generator for POCO classes.

.NET Standard library of data suit. [![Build Status](https://travis-ci.org/DataSuit/DataSuit.svg?branch=master)](https://travis-ci.org/DataSuit/DataSuit) [![NuGet](https://img.shields.io/nuget/v/DataSuit.svg)](https://www.nuget.org/packages/DataSuit/)


### Example

```csharp
Resources.Load();
Generator<ClassName>.SeedAsync(5); //generates 5 objects as IEnumarable<ClassName>
```

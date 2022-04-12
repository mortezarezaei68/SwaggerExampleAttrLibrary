
# SwaggerExampleAttr for .NET core

Tech stack used :  .NET 6.0, C# 9.0
## Introduction
SwaggerExampleAttr is lightweight .NET core library that allow developers to customize their request model examples in Swagger. this library would be focus on new feature to create more readable request model examples for your apis.

## Installation
SwaggerExampleAttr is now available in [NuGet](https://www.nuget.org/packages/SwaggerExampleAttrLib) extends your ```Swashbuckle.AspNetCore``` library

install nuget library
```c#
Install-Package SwaggerExampleAttrLib -Version 0.1.21
```
add schema filter in SwaggerGen
```c#
services.AddSwaggerGen(c =>  
{  
    c.SchemaFilter<ExampleSchemaFilter>();
    ...
}
```
## Example
if you want to use this library you should use `Example` attribute above of your properties like:
```c#
public class TestModel{
[Example("Test")]   
public string Address { get; set; }
[Example(12.2)]   
public decimal Lat { get; set; }
[Example(1, 2, 3)]   
public List<int> Ints { get; set; }
...
}
```
## Supported Types

- `string`
- `int`
- `double`
- `decimal`
- `System.Collections.Generic` simple types like (`List<int>`, `List<string>`,`IEnumrable<int>`)

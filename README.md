# EF Core TPC + Polymorphic API in .NET 8

## 🔥 Overview
This project showcases a production-grade implementation of **EF Core's Table-Per-Concrete Type (TPC) mapping strategy** combined with **polymorphic API response handling** using .NET 8's native `JsonPolymorphic` attribute and System.Text.Json.

> ⚡ Ideal for developers building complex domain models, versioned APIs, and clean serialization strategies for polymorphic data.

---

## 📚 Table of Contents

- [🔥 Overview](#-overview)
- [🎯 Features](#-features)
- [🧩 Sample Entities](#-sample-entities)
- [🔁 Polymorphic Serialization with JsonPolymorphic](#-polymorphic-serialization-with-jsonpolymorphic)
- [🔄 AutoMapper DTO Mapping](#-automapper-dto-mapping)
- [🔁 API Response Format](#-api-response-format)
- [🛠️ Technologies](#️-technologies)
- [🚀 Running the Project](#-running-the-project)
- [💡 Contributing](#-contributing)

---

## 🎯 Features

- ✅ **Table-Per-Concrete (TPC)** inheritance mapping using `UseTpcMappingStrategy()`
- 🔄 **Polymorphic JSON serialization** via native `JsonPolymorphicAttribute`
- 🧾 **Custom `ApiResponse<T>` wrapper** with conditional formatting
- 🔀 **DTO mapping using AutoMapper** for clean controller/service separation
- 🧪 Built-in sample data + Swagger UI

---

## 🧩 Sample Entities

```csharp
public abstract class RequestBase
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class TypeARequest : RequestBase
{
    public string Param1 { get; set; }
    public int Param2 { get; set; }
}

public class TypeBRequest : RequestBase
{
    public bool Flag { get; set; }
    public decimal Amount { get; set; }
}
```

---

## 🔁 Polymorphic Serialization with `JsonPolymorphic`

```csharp
[JsonPolymorphic(TypeDiscriminatorPropertyName = "$type")]
[JsonDerivedType(typeof(TypeARequestDto), "typeA")]
[JsonDerivedType(typeof(TypeBRequestDto), "typeB")]
public abstract class RequestBaseDto { }
```

- Built-in support using `System.Text.Json` attributes
- Type information included for correct deserialization

---

## 🔄 AutoMapper DTO Mapping

AutoMapper is used to transform entity models into polymorphic DTOs. It also supports inheritance with `Include<>` and bidirectional conversion with `ReverseMap()`:

```csharp
public class RequestMappingProfile : Profile
{
    public RequestMappingProfile()
    {
        CreateMap<RequestBase, RequestBaseDto>()
            .Include<TypeARequest, TypeARequestDto>()
            .Include<TypeBRequest, TypeBRequestDto>();

        CreateMap<TypeARequest, TypeARequestDto>().ReverseMap();
        CreateMap<TypeBRequest, TypeBRequestDto>().ReverseMap();
    }
}
```

Configuration is added in `Program.cs`:
```csharp
builder.Services.AddAutoMapper(typeof(Program));
```

---

## 🔁 API Response Format

```json
{
  "success": true,
  "message": "Data fetched successfully",
  "data": {
    "$type": "typeA",
    "param1": "Sample",
    "param2": 42
  },
  "statusCode": 200,
  "dataTypeName": "TypeARequest"
}
```

- `ApiResponse<T>` includes both generic wrapper and runtime type info
- Compatible with Swagger and client libraries

---

## 🛠️ Technologies

- .NET 8 Web API
- Entity Framework Core 8 (TPC Strategy)
- `System.Text.Json` with polymorphic attributes
- AutoMapper
- Swagger / Swashbuckle

---

## 🚀 Running the Project

```bash
dotnet build
cd src/EfCoreTPC.Api
dotnet run
```
Visit `https://localhost:<port>/swagger` for API docs.

---

## 💡 Contributing

Pull requests, bug fixes, and ideas welcome. Let’s push the boundaries of clean API design in .NET together.


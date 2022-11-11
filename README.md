[![NuGet Version](https://img.shields.io/nuget/v/TurkishId.svg)](https://www.nuget.org/packages/TurkishId/)
![Build status](https://github.com/ssg/TurkishId/actions/workflows/build.yml/badge.svg)

TurkishId
=========
Validator, model binder and generator tool for Turkish Republic's citizen ID numbers. I've decided to give this a shot last 
night while I was waiting for a download. And Turkish ID numbers were really popular in Turkish social
media last week. The Id number is called "T.C. Kimlik No" (Turkish Republic Identity Number). I decided to 
use English translation to allow easier handling in international projects.

Usage
-----
I wanted the `TurkishIdNumber` representation to be used anywhere in the code as a value to pass around when 
using id's allowing to assume an instance is already validated saving you from redundant checks.

When you want to use it as a representation of an ID number:

```csharp
using TurkishId;
var id = new TurkishIdNumber("12345678901");
// throws ArgumentException when invalid parameter is passed
```

or if you just want to validate it:

```csharp
using TurkishId;
bool reallyValid = TurkishIdNumber.IsValid("12345678901");
```

You can also use the classic `TryParse`:

```csharp
using TurkishId;
if (TurkishIdNumber.TryParse(value, out var id))
{
    // ...
}
```

or with a nullable return value which can be used with pattern matching:

```csharp
using TurkishId;
if (TurkishIdNumber.TryParse(value) is TurkishIdNumber id))
{
    // ...
}
```

NuGet package is here: <https://www.nuget.org/packages/TurkishId/>

TurkishId.ModelBinder
---------------------
There is also a model binder package that you can install at <https://www.nuget.org/packages/TurkishId.ModelBinder>.
It's a plug'n'play model binder which lets you to use TurkishIdNumber class directly in your model declarations.

It's not part of the original package because you may not want to have whole MVC as a dependency.

To set it up in an ASP.NET Core project, use this syntax in your `ConfigureServices()` method in your
`Startup` class:

```csharp
services.AddMvc(options =>
{
  options.ModelBinderProviders.Insert(0, new TurkishIdModelBinderProvider());
});
```

and you're done. If you'd like to use it in your Razor Pages app use `AddMvcOptions` instead:

```csharp
services.AddRazorPages()
  .AddMvcOptions(options => options.ModelBinderProviders.Insert(0, new TurkishIdModelBinderProvider()));
```

or, alternatively, if you only use controllers, you can add it to your `AddControllers` options:

```csharp
services.AddControllers(options =>
{
  options.ModelBinderProviders.Insert(0, new TurkishIdModelBinderProvider());
});
```

The model binder package will use ASP.NET Core's model binding message providers. You can now localize them
like how you do any other model binder.

Performance
------------
This is probably one of the fastest implementations on .NET. I didn't grind so much on performance but
it can easily handle millions of validations per second on my Core i7. 

Algorithm
----------
Turkish Republic's ID structure and verification is simple. It's an eleven digit number. 
If we name each digit as _d(n)_ where leftmost digit is called _d1_ and the rightmost _d11_, a given ID is valid if:

> _d1_ > 0

and

> _n_ = (_d1_ + _d3_ + _d5_ + _d7_ + _d9_) * 7 - (_d2_ + _d4_ + _d6_ + _d8_)
>
> if _n_ < 0 then _n_ = _n_ + 10
>
> _d10_ = _n_ mod 10

and

> _d11_ = sum(_d1_.._d10_) mod 10 

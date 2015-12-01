TurkishId
=========
Validator (and a random generator tool) for Turkish Republic's citizen ID numbers. I've decided to give this a shot last 
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

NuGet package is here: https://www.nuget.org/packages/TurkishId/

Performance
------------
This is probably one of the fastest implementations on .NET. I didn't grind so much on performance but
it can easily handle millions of validations per second on my Core i7. 

Algorithm
----------
Turkish Republic's ID structure and verification is simple. It's an eleven digit number. 
If we name each digit as _d(n)_ where leftmost digit is d1 and the rightmost is d11:

> d1 > 0

and

> d10 = (((d1 + d3 + d5 + d7 + d9) * 7) - (d2 + d4 + d6 + d8) mod 10)

and

> d11 = sum(d1..d10) mod 10 

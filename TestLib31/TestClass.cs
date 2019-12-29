using System;
using TurkishId;

/// <summary>
/// This type of code checks for nullability interop between different .NET Standard and .NET Core versions.
/// This library needs to be compiled against .NET Core 3.1 and C# 8.0 so we can make sure that
/// nullable references are honored.
/// </summary>
namespace TestLib31
{
    public class TestClass
    {
        public void Test()
        {
            _ = new TurkishIdNumber(null); // must fail compilation
        }
    }
}

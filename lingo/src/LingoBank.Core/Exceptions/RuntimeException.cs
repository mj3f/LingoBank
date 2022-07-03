using System;

namespace LingoBank.Core.Exceptions;

public class RuntimeException : Exception
{
    public RuntimeException(string message) : base(message)
    {
    }

    public RuntimeException(string message, Exception ex) : base(message, ex)
    {
    }
}
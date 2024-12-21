// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine;

public class GraphEngineException : Exception
{
    public GraphEngineException()
    {
    }

    public GraphEngineException(string message)
        : base(message)
    {
    }

    public GraphEngineException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}

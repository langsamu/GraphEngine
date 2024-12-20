// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine;

public class DebugInfo(NodeWithGraph node, INode? type = default) : Expression(node, type)
{
    public SymbolDocument Document
    {
        get => GetRequired(DebugInfoDocument, n => new SymbolDocument(n));

        set => SetRequired(DebugInfoDocument, value);
    }

    public int StartLine
    {
        get => GetRequiredS(DebugInfoStartLine, AsInt);

        set => SetRequired(DebugInfoStartLine, value);
    }

    public int StartColumn
    {
        get => GetRequiredS(DebugInfoStartColumn, AsInt);

        set => SetRequired(DebugInfoStartColumn, value);
    }

    public int EndLine
    {
        get => GetRequiredS(DebugInfoEndLine, AsInt);

        set => SetRequired(DebugInfoEndLine, value);
    }

    public int EndColumn
    {
        get => GetRequiredS(DebugInfoEndColumn, AsInt);

        set => SetRequired(DebugInfoEndColumn, value);
    }

    public override Linq.Expression LinqExpression => Linq.Expression.DebugInfo(Document.LinqDocument, StartLine, StartColumn, EndLine, EndColumn);
}

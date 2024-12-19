// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine;

public class DebugInfo(NodeWithGraph node, INode? type = default) : Expression(node, type)
{
    public SymbolDocument Document
    {
        get => this.GetRequired(DebugInfoDocument, n => new SymbolDocument(n));

        set => this.SetRequired(DebugInfoDocument, value);
    }

    public int StartLine
    {
        get => this.GetRequiredS(DebugInfoStartLine, AsInt);

        set => this.SetRequired(DebugInfoStartLine, value);
    }

    public int StartColumn
    {
        get => this.GetRequiredS(DebugInfoStartColumn, AsInt);

        set => this.SetRequired(DebugInfoStartColumn, value);
    }

    public int EndLine
    {
        get => this.GetRequiredS(DebugInfoEndLine, AsInt);

        set => this.SetRequired(DebugInfoEndLine, value);
    }

    public int EndColumn
    {
        get => this.GetRequiredS(DebugInfoEndColumn, AsInt);

        set => this.SetRequired(DebugInfoEndColumn, value);
    }

    public override Linq.Expression LinqExpression => Linq.Expression.DebugInfo(this.Document.LinqDocument, this.StartLine, this.StartColumn, this.EndLine, this.EndColumn);
}

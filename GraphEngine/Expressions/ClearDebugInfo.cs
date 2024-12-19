// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine;

public class ClearDebugInfo(NodeWithGraph node) : DebugInfo(node, Vocabulary.ClearDebugInfo)
{
    public override Linq.Expression LinqExpression => Linq.Expression.ClearDebugInfo(this.Document.LinqDocument);
}

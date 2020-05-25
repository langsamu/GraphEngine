// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine
{
    using System;
    using System.Diagnostics;
    using VDS.RDF;
    using static Vocabulary;
    using Linq = System.Linq.Expressions;

    public class DebugInfo : Expression
    {
        [DebuggerStepThrough]
        internal DebugInfo(INode node)
            : base(node)
        {
        }

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

    public class SymbolDocument : Node
    {
        [DebuggerStepThrough]
        internal SymbolDocument(INode node)
            : base(node)
        {
        }

        public string FileName
        {
            get => this.GetRequired(SymbolDocumentFileName, AsString);

            set => this.SetRequired(SymbolDocumentFileName, value);
        }

        public Guid? Language
        {
            get => this.GetOptionalS(SymbolDocumentLanguage, AsGuid);

            set => this.SetOptional(SymbolDocumentLanguage, value);
        }

        public Guid? LanguageVendor
        {
            get => this.GetOptionalS(SymbolDocumentLanguageVendor, AsGuid);

            set => this.SetOptional(SymbolDocumentLanguageVendor, value);
        }

        public Guid? DocumentType
        {
            get => this.GetOptionalS(SymbolDocumentDocumentType, AsGuid);

            set => this.SetOptional(SymbolDocumentDocumentType, value);
        }

        public Linq.SymbolDocumentInfo LinqDocument
        {
            get
            {
                if (this.Language is Guid language)
                {
                    if (this.LanguageVendor is Guid languageVendor)
                    {
                        if (this.DocumentType is Guid documentType)
                        {
                            return Linq.Expression.SymbolDocument(this.FileName, language, languageVendor, documentType);
                        }

                        return Linq.Expression.SymbolDocument(this.FileName, language, languageVendor);
                    }

                    return Linq.Expression.SymbolDocument(this.FileName, language);
                }

                return Linq.Expression.SymbolDocument(this.FileName);
            }
        }
    }
}

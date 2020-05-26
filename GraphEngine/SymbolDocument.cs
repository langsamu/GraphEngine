// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine
{
    using System;
    using System.Diagnostics;
    using VDS.RDF;
    using static Vocabulary;
    using Linq = System.Linq.Expressions;

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

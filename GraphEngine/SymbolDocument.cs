// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine;

public class SymbolDocument(NodeWithGraph node) : Node(node)
{
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

    public Linq.SymbolDocumentInfo LinqDocument => this switch
    {
        { Language: Guid language, LanguageVendor: Guid languageVendor, DocumentType: Guid documentType } => Linq.Expression.SymbolDocument(this.FileName, language, languageVendor, documentType),
        { Language: Guid language, LanguageVendor: Guid languageVendor } => Linq.Expression.SymbolDocument(this.FileName, language, languageVendor),
        { Language: Guid language } => Linq.Expression.SymbolDocument(this.FileName, language),
        _ => Linq.Expression.SymbolDocument(this.FileName)
    };
}

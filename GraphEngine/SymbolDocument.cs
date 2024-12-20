// MIT License, Copyright 2020 Samu Lang

namespace GraphEngine;

public class SymbolDocument(NodeWithGraph node) : Node(node)
{
    public string FileName
    {
        get => GetRequired(SymbolDocumentFileName, AsString);

        set => SetRequired(SymbolDocumentFileName, value);
    }

    public Guid? Language
    {
        get => GetOptionalS(SymbolDocumentLanguage, AsGuid);

        set => SetOptional(SymbolDocumentLanguage, value);
    }

    public Guid? LanguageVendor
    {
        get => GetOptionalS(SymbolDocumentLanguageVendor, AsGuid);

        set => SetOptional(SymbolDocumentLanguageVendor, value);
    }

    public Guid? DocumentType
    {
        get => GetOptionalS(SymbolDocumentDocumentType, AsGuid);

        set => SetOptional(SymbolDocumentDocumentType, value);
    }

    public Linq.SymbolDocumentInfo LinqDocument => this switch
    {
        { Language: Guid language, LanguageVendor: Guid languageVendor, DocumentType: Guid documentType } => Linq.Expression.SymbolDocument(FileName, language, languageVendor, documentType),
        { Language: Guid language, LanguageVendor: Guid languageVendor } => Linq.Expression.SymbolDocument(FileName, language, languageVendor),
        { Language: Guid language } => Linq.Expression.SymbolDocument(FileName, language),
        _ => Linq.Expression.SymbolDocument(FileName)
    };
}

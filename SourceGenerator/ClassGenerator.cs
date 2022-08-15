using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SourceGenerator
{
    [Generator]
    public class ClassGenerator : ISourceGenerator
    {
        public void Execute(GeneratorExecutionContext context)
        {
            if (context.SyntaxContextReceiver is not SyntaxContextReceiver scr)
            {
                return;
            }

            foreach (var (classSyntax, classSymbol) in scr.GenClasses)
            {
                var attributeName = "TestAttAttribute";
                var attribute = classSymbol.GetAttributes().FirstOrDefault(a => a.AttributeClass.Name == attributeName);
                var attributeArgument = attribute?.ConstructorArguments[0].Value;
            }
        }

        public void Initialize(GeneratorInitializationContext context)
        {
            context.RegisterForSyntaxNotifications(() => new SyntaxContextReceiver());
        }
    }


    internal class SyntaxContextReceiver : ISyntaxContextReceiver
    {
        public IList<(ClassDeclarationSyntax, ITypeSymbol)> GenClasses { get; } = new List<(ClassDeclarationSyntax, ITypeSymbol)>();

        public void OnVisitSyntaxNode(GeneratorSyntaxContext context)
        {
            if (context.Node is not ClassDeclarationSyntax cds)
            {
                return;
            }

            var classSymbol = context.SemanticModel.GetDeclaredSymbol(cds) as ITypeSymbol;
            GenClasses.Add((cds, classSymbol));
        }
    }
}

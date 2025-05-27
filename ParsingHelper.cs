using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace StringLocalizer
{
    internal static class ParsingHelper
    {
        public static Dictionary<string, IEnumerable<string>> ExtractKeysFromCsFile(string filePath)
        {
            var map = new Dictionary<string, List<string>>();
            var code = File.ReadAllText(filePath);
            var tree = CSharpSyntaxTree.ParseText(code);
            var root = tree.GetRoot();

            var classes = root.DescendantNodes().OfType<ClassDeclarationSyntax>();
            foreach (var cls in classes)
            {
                var localizerNames = new HashSet<string>();
                var keys = new List<string>();

                var className = cls.Identifier.Text;

                var variables = cls.DescendantNodes().OfType<VariableDeclarationSyntax>();
                foreach (var decl in variables)
                {
                    if (decl.Type.ToString().StartsWith("IStringLocalizer"))
                    {
                        foreach (var variable in decl.Variables)
                        {
                            localizerNames.Add(variable.Identifier.Text);
                        }
                    }
                }

                var variableDeclarators = cls.DescendantNodes().OfType<VariableDeclaratorSyntax>();
                foreach (var declarator in variableDeclarators)
                {
                    var init = declarator.Initializer?.Value as InvocationExpressionSyntax;
                    if (init != null)
                    {
                        // typeof(IStringLocalizer<...>) detection
                        var arg = init.ArgumentList?.Arguments.FirstOrDefault()?.ToString();
                        if (arg != null && arg.Contains("typeof(IStringLocalizer"))
                        {
                            localizerNames.Add(declarator.Identifier.Text);
                        }

                        // Generic method GetRequiredService<IStringLocalizer<...>>() detection
                        if ((init.Expression as MemberAccessExpressionSyntax)?.Name is GenericNameSyntax genericName)
                        {
                            var typeName = genericName.TypeArgumentList.Arguments.FirstOrDefault()?.ToString();
                            if ((genericName.Identifier.Text.Contains("GetService") || genericName.Identifier.Text.Contains("GetRequiredService")) && typeName != null && typeName.StartsWith("IStringLocalizer"))
                            {
                                localizerNames.Add(declarator.Identifier.Text);
                            }
                        }
                    }
                }

                var parameters = cls.DescendantNodes().OfType<ParameterSyntax>();
                foreach (var param in parameters)
                {
                    if (param.Type?.ToString().StartsWith("IStringLocalizer") == true)
                    {
                        localizerNames.Add(param.Identifier.Text);
                    }
                }

                var indexers = cls.DescendantNodes().OfType<ElementAccessExpressionSyntax>();
                foreach (var indexer in indexers)
                {
                    var exprName = indexer.Expression.ToString();
                    if (localizerNames.Contains(exprName))
                    {
                        var arg = indexer.ArgumentList.Arguments.FirstOrDefault();
                        if (arg?.Expression is LiteralExpressionSyntax literal && literal.IsKind(SyntaxKind.StringLiteralExpression))
                        {
                            keys.Add(literal.Token.ValueText);
                        }
                    }
                }

                if (keys.Any())
                    map[className] = keys;
            }

            return map.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.AsEnumerable());
        }

        public static (string ClassName, IEnumerable<string> Keys) ExtractKeysFromRazorFile(string filePath)
        {
            var keys = new List<string>();
            var content = File.ReadAllText(filePath);

            //Remove comments
            content = Regex.Replace(content, "@\\*.*?\\*@", string.Empty, RegexOptions.Singleline);
            content = Regex.Replace(content, "<!--.*?-->", string.Empty, RegexOptions.Singleline);
            content = Regex.Replace(content, "//.*", string.Empty);
            content = Regex.Replace(content, "/\\*.*?\\*/", string.Empty, RegexOptions.Singleline);

            // Remove any C# class declarations and their bodies
            content = Regex.Replace(content, "class\\s+\\w+\\s*{[\\s\\S]*?}", string.Empty, RegexOptions.Multiline);

            var matches = Regex.Matches(content, "@?(\\w+)\\[\"(.*?)\"\\]");
            foreach (Match match in matches)
            {
                if (match.Groups.Count > 2)
                {
                    keys.Add(match.Groups[2].Value);
                }
            }

            // Use filename (without extension) as pseudo class name
            var fileName = Path.GetFileNameWithoutExtension(filePath);
            return (fileName, keys);
        }
    }
}

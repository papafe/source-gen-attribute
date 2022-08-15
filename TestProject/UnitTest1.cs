using Microsoft.CodeAnalysis.Text;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Text;
using VerifyCS = TestProject.CSharpSourceGeneratorVerifier<SourceGenerator.ClassGenerator>;

namespace TestProject
{
    public class Tests
    {
        string _classesPath;

        [SetUp]
        public void Setup()
        {
            var buildFolder = Path.GetDirectoryName(typeof(Program).Assembly.Location);
            var testFolder = buildFolder.Substring(0, buildFolder.IndexOf("TestProject", StringComparison.InvariantCulture));
            _classesPath = Path.Combine(testFolder, "Playground");
        }

        [Test]
        public async Task Test1()
        {
            var code = File.ReadAllText(Path.Combine(_classesPath, "Class1.cs"));
            var generated = "expected generated code";

            await new VerifyCS.Test
            {
                TestState =
                {
                    Sources = { code },
                    GeneratedSources =
                    {
                        (typeof(SourceGenerator.ClassGenerator), "GeneratedFileName", SourceText.From(generated, Encoding.UTF8, SourceHashAlgorithm.Sha256)),
                    },
                },
            }.RunAsync();
        }
    }

    
}
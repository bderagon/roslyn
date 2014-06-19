﻿using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.CSharp.FxCopAnalyzers.Performance;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.FxCopAnalyzers.Performance;
using Microsoft.CodeAnalysis.Test.Utilities;
using Microsoft.CodeAnalysis.VisualBasic.FxCopAnalyzers.Performance;
using Xunit;

namespace Microsoft.CodeAnalysis.UnitTests
{
    public partial class CA1821FixerTests : CodeFixTestBase
    {
        protected override IDiagnosticAnalyzer GetBasicDiagnosticAnalyzer()
        {
            return new BasicCA1821DiagnosticAnalyzer();
        }

        protected override ICodeFixProvider GetBasicCodeFixProvider()
        {
            return new CA1821CodeFixProvider();
        }

        protected override IDiagnosticAnalyzer GetCSharpDiagnosticAnalyzer()
        {
            return new CSharpCA1821DiagnosticAnalyzer();
        }

        protected override ICodeFixProvider GetCSharpCodeFixProvider()
        {
            return new CA1821CodeFixProvider();
        }

        [Fact, Trait(Traits.Feature, Traits.Features.Diagnostics)]
        public void CA1821CSharpCodeFixTestRemoveEmptyFinalizers()
        {
            VerifyCSharpFix(@"
public class Class1
{
    // Violation occurs because the finalizer is empty.
    ~Class1()
    {
    }
}
",
@"
public class Class1
{
}
");
        }

        [Fact(Skip = "Bug 902686"), Trait(Traits.Feature, Traits.Features.Diagnostics)]
        public void CA1821BasicCodeFixTestRemoveEmptyFinalizers()
        {
            VerifyBasicFix(@"
Imports System.Diagnostics

Public Class Class1
    '  Violation occurs because the finalizer is empty.
    Protected Overrides Sub Finalize()

    End Sub
End Class
",
@"
Imports System.Diagnostics

Public Class Class1
End Class
");
        }
    }
}

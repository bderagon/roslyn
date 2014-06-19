﻿using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Test.Utilities;
using Xunit;
using Microsoft.CodeAnalysis.FxCopAnalyzers.Design;

namespace Microsoft.CodeAnalysis.UnitTests
{
    public partial class CA1012FixerTests : CodeFixTestBase
    {
        protected override IDiagnosticAnalyzer GetBasicDiagnosticAnalyzer()
        {
            return new CA1012DiagnosticAnalyzer();
        }

        protected override ICodeFixProvider GetBasicCodeFixProvider()
        {
            return new CA1012CodeFixProvider();
        }

        protected override IDiagnosticAnalyzer GetCSharpDiagnosticAnalyzer()
        {
            return new CA1012DiagnosticAnalyzer();
        }

        protected override ICodeFixProvider GetCSharpCodeFixProvider()
        {
            return new CA1012CodeFixProvider();
        }

        [Fact(Skip = "Bug 823796"), Trait(Traits.Feature, Traits.Features.Diagnostics)]
        public void TestCSPublicAbstractClass()
        {
            var code = @"
public abstract class C
{
    public C()
    {
    }
}
";
            var fix = @"
public abstract class C
{
    protected C()
    {
    }
}
";
            VerifyCSharpFix(code, fix);
        }

        [Fact, Trait(Traits.Feature, Traits.Features.Diagnostics)]
        public void TestVBPublicAbstractClass()
        {
            var code = @"
Public MustInherit Class C
    Public Sub New()
    End Sub
End Class
";
            var fix = @"
Public MustInherit Class C
    Protected Sub New()
    End Sub
End Class
";
            VerifyBasicFix(code, fix);
        }

        [Fact(Skip = "Bug 823796"), Trait(Traits.Feature, Traits.Features.Diagnostics)]
        public void TestCSInternalAbstractClass()
        {
            var code = @"
abstract class C
{
    public C()
    {
    }
}
";
            var fix = @"
abstract class C
{
    protected C()
    {
    }
}
";
            VerifyCSharpFix(code, fix);
        }

        [Fact, Trait(Traits.Feature, Traits.Features.Diagnostics)]
        public void TestVBInternalAbstractClass()
        {
            var code = @"
MustInherit Class C
    Public Sub New()
    End Sub
End Class
";
            var fix = @"
MustInherit Class C
    Protected Sub New()
    End Sub
End Class
";
            VerifyBasicFix(code, fix);
        }

        [Fact, Trait(Traits.Feature, Traits.Features.Diagnostics)]
        public void TestCSNestedAbstractClassWithPublicConstructor1()
        {
            var code = @"
public struct C
{
    abstract class D
    {
        public D() { }
    }
}
";
            var fix = @"
public struct C
{
    abstract class D
    {
        protected D() { }
    }
}
";
            VerifyCSharpFix(code, fix);
        }

        [Fact, Trait(Traits.Feature, Traits.Features.Diagnostics)]
        public void TestVBNestedAbstractClassWithPublicConstructor1()
        {
            var code = @"
Public Structure C
    MustInherit Class D
        Public Sub New()
        End Sub
    End Class
End Class
";
            var fix = @"
Public Structure C
    MustInherit Class D
        Protected Sub New()
        End Sub
    End Class
End Class
";
            VerifyBasicFix(code, fix);
        }

        [Fact, Trait(Traits.Feature, Traits.Features.Diagnostics)]
        public void TestNestedAbstractClassWithPublicConstructor2()
        {
            var code = @"
public abstract class C
{
    public abstract class D
    {
        public D() { }
    }
}
";
            var fix = @"
public abstract class C
{
    public abstract class D
    {
        protected D() { }
    }
}
";
            VerifyCSharpFix(code, fix);
        }

        [Fact, Trait(Traits.Feature, Traits.Features.Diagnostics)]
        public void TestVBNestedAbstractClassWithPublicConstructor2()
        {
            var code = @"
Public MustInherit Class C
   Protected Friend MustInherit Class D
        Sub New()
        End Sub
    End Class
End Class
";
            var fix = @"
Public MustInherit Class C
   Protected Friend MustInherit Class D
        Protected Sub New()
        End Sub
    End Class
End Class
";
            VerifyBasicFix(code, fix);
        }

        [Fact, Trait(Traits.Feature, Traits.Features.Diagnostics)]
        public void TestNestedAbstractClassWithPublicConstructor3()
        {
            var code = @"
internal abstract class C
{
    public abstract class D
    {
        public D() { }
    }
}
";
            var fix = @"
internal abstract class C
{
    public abstract class D
    {
        protected D() { }
    }
}
";
            VerifyCSharpFix(code, fix);
        }

        [Fact, Trait(Traits.Feature, Traits.Features.Diagnostics)]
        public void TestVBNestedAbstractClassWithPublicConstructor3()
        {
            var code = @"
MustInherit Class C
   Public MustInherit Class D
        Sub New()
        End Sub
    End Class
End Class
";
            var fix = @"
MustInherit Class C
   Public MustInherit Class D
        Protected Sub New()
        End Sub
    End Class
End Class
";
            VerifyBasicFix(code, fix);
        }
    }
}

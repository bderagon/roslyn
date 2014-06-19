﻿using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.CSharp.FxCopAnalyzers.Usage;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Test.Utilities;
using Microsoft.CodeAnalysis.VisualBasic.FxCopAnalyzers.Usage;
using Roslyn.Test.Utilities;
using Xunit;
using Microsoft.CodeAnalysis.FxCopAnalyzers.Usage;

namespace Microsoft.CodeAnalysis.UnitTests
{
    public partial class CA2235FixerTests : CodeFixTestBase
    {
        protected override IDiagnosticAnalyzer GetBasicDiagnosticAnalyzer()
        {
            return new SerializationRulesDiagnosticAnalyzer();
        }

        [WorkItem(858655)]
        protected override ICodeFixProvider GetBasicCodeFixProvider()
        {
            return new CA2235BasicCodeFixProvider();
        }

        protected override IDiagnosticAnalyzer GetCSharpDiagnosticAnalyzer()
        {
            return new SerializationRulesDiagnosticAnalyzer();
        }

        [WorkItem(858655)]
        protected override ICodeFixProvider GetCSharpCodeFixProvider()
        {
            return new CA2235CSharpCodeFixProvider();
        }

        #region CA2235

        [Fact, Trait(Traits.Feature, Traits.Features.Diagnostics)]
        public void CA2235WithNonSerializableFieldsWithFix()
        {
            VerifyCSharpFix(@"
using System;
public class NonSerializableType { }

[Serializable]
public class CA2235WithNonPublicNonSerializableFields
{
    internal NonSerializableType s1;
}",
@"
using System;
public class NonSerializableType { }

[Serializable]
public class CA2235WithNonPublicNonSerializableFields
{
    [NonSerialized]
    internal NonSerializableType s1;
}",
codeFixIndex: 0);

            VerifyBasicFix(@"
Imports System
Public Class NonSerializableType
End Class

<Serializable>
Public Class CA2235WithNonPublicNonSerializableFields 
    Friend s1 As NonSerializableType
End Class",
@"
Imports System
Public Class NonSerializableType
End Class

<Serializable>
Public Class CA2235WithNonPublicNonSerializableFields
    <NonSerialized>
    Friend s1 As NonSerializableType
End Class",
codeFixIndex: 0);
        }

        [Fact, Trait(Traits.Feature, Traits.Features.Diagnostics)]
        public void CA2235WithNonSerializableFieldsWithFix1()
        {
            VerifyCSharpFix(@"
using System;
public class NonSerializableType { }

[Serializable]
public class CA2235WithNonPublicNonSerializableFields
{
    internal NonSerializableType s1;
}",
@"
using System;

[Serializable]
public class NonSerializableType { }

[Serializable]
public class CA2235WithNonPublicNonSerializableFields
{
    internal NonSerializableType s1;
}",
codeFixIndex: 1);

            VerifyBasicFix(@"
Imports System
Public Class NonSerializableType
End Class

<Serializable>
Public Class CA2235WithNonPublicNonSerializableFields
    Friend s1 As NonSerializableType
End Class",
@"
Imports System

<Serializable>
Public Class NonSerializableType
End Class

<Serializable>
Public Class CA2235WithNonPublicNonSerializableFields
    Friend s1 As NonSerializableType
End Class",
codeFixIndex: 1);
        }

        [Fact, Trait(Traits.Feature, Traits.Features.Diagnostics)]
        public void CA2235WithNonSerializableFieldsWithFix2()
        {
            VerifyCSharpFix(@"
using System;
public class NonSerializableType { }

[Serializable]
public class CA2235WithNonPublicNonSerializableFields
{
    internal NonSerializableType s1, s2 = new NonSerializableType(), s3;
}",
@"
using System;
public class NonSerializableType { }

[Serializable]
public class CA2235WithNonPublicNonSerializableFields
{
    [NonSerialized]
    internal NonSerializableType s1, s2 = new NonSerializableType(), s3;
}",
codeFixIndex: 0);

            VerifyBasicFix(@"
Imports System
Public Class NonSerializableType
End Class

<Serializable>
Public Class CA2235WithNonPublicNonSerializableFields 
    Friend s1, s2, s3 As NonSerializableType
End Class",
@"
Imports System
Public Class NonSerializableType
End Class

<Serializable>
Public Class CA2235WithNonPublicNonSerializableFields
    <NonSerialized>
    Friend s1, s2, s3 As NonSerializableType
End Class",
codeFixIndex: 0);
        }

        [Fact, Trait(Traits.Feature, Traits.Features.Diagnostics)]
        public void CA2235WithNonSerializableFieldsWithFix3()
        {
            VerifyCSharpFix(@"
using System;
public partial class NonSerializableType { }

public class NonSerializableType { public void baz() { } }

[Serializable]
public class CA2235WithNonPublicNonSerializableFields
{
    internal NonSerializableType s1;
}",
@"
using System;

[Serializable]
public partial class NonSerializableType { }

public class NonSerializableType { public void baz() { } }

[Serializable]
public class CA2235WithNonPublicNonSerializableFields
{
    internal NonSerializableType s1;
}",
codeFixIndex: 1);

            VerifyBasicFix(@"
Imports System
Public Partial Class NonSerializableType
End Class

Public Class NonSerializableType
    Sub foo()
    End Sub
End Class

<Serializable>
Public Class CA2235WithNonPublicNonSerializableFields
    Friend s1 As NonSerializableType
End Class",
@"
Imports System

<Serializable>
Public Partial Class NonSerializableType
End Class

Public Class NonSerializableType
    Sub foo()
    End Sub
End Class

<Serializable>
Public Class CA2235WithNonPublicNonSerializableFields
    Friend s1 As NonSerializableType
End Class",
codeFixIndex: 1);
        }

        #endregion
    }
}

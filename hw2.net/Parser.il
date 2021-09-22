// Type: homework1.Parser 
// Assembly: homework1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C658D23A-15E5-40E9-9F3A-8E9FBD6374A5
// Location: E:\Program Files\Github\hw1.net\homework1\homework1\bin\Debug\net5.0\homework1.dll
// Sequence point data from E:\Program Files\Github\hw1.net\homework1\homework1\bin\Debug\net5.0\homework1.pdb

.class public abstract sealed auto ansi beforefieldinit
  homework2.Parseril
    extends [System.Runtime]System.Object
{

  .field public static class [System.Runtime]System.Exception ParserilNumberFail

  .field public static class [System.Runtime]System.Exception ParserilOperationFail

  .method public hidebysig static int32
    ToParseNumber(
      string 'value'
    ) cil managed
  {
    .maxstack 2
    .locals init (
      [0] bool success,
      [1] int32 result,
      [2] bool V_2,
      [3] int32 V_3
    )

    // [11 13 - 11 14]
    IL_0000: nop

    // [12 17 - 12 67]
    IL_0001: ldarg.0      // 'value'
    IL_0002: ldloca.s     result
    IL_0004: call         bool [System.Runtime]System.Int32::TryParse(string, int32&)
    IL_0009: stloc.0      // success

    // [13 17 - 13 30]
    IL_000a: ldloc.0      // success
    IL_000b: ldc.i4.0
    IL_000c: ceq
    IL_000e: stloc.2      // V_2

    IL_000f: ldloc.2      // V_2
    IL_0010: brfalse.s    IL_0019

    // [14 17 - 14 18]
    IL_0012: nop

    // [15 21 - 15 44]
    IL_0013: ldsfld       class [System.Runtime]System.Exception homework2.Parseril::ParserilNumberFail
    IL_0018: throw

    // [18 17 - 18 31]
    IL_0019: ldloc.1      // result
    IL_001a: stloc.3      // V_3
    IL_001b: br.s         IL_001d

    // [19 13 - 19 14]
    IL_001d: ldloc.3      // V_3
    IL_001e: ret

  } // end of method Parseril::ToParseNumber

  .method public hidebysig static valuetype homework2.Calculatoril/Operation
    ParseOperation(
      string 'value'
    ) cil managed
  {
    .maxstack 2
    .locals init (
      [0] valuetype homework2.Calculatoril/Operation result,
      [1] valuetype homework2.Calculatoril/Operation V_1,
      [2] valuetype homework2.Calculatoril/Operation V_2
    )

    // [22 13 - 22 14]
    IL_0000: nop

    // [23 17 - 30 19]
    IL_0001: ldc.i4.1
    IL_0002: brtrue.s     IL_0005

    // [23 36 - 30 18]
    IL_0004: nop

    IL_0005: ldarg.0      // 'value'
    IL_0006: ldstr        "+"
    IL_000b: call         bool [System.Runtime]System.String::op_Equality(string, string)
    IL_0010: brtrue.s     IL_003b
    IL_0012: ldarg.0      // 'value'
    IL_0013: ldstr        "-"
    IL_0018: call         bool [System.Runtime]System.String::op_Equality(string, string)
    IL_001d: brtrue.s     IL_003f
    IL_001f: ldarg.0      // 'value'
    IL_0020: ldstr        "*"
    IL_0025: call         bool [System.Runtime]System.String::op_Equality(string, string)
    IL_002a: brtrue.s     IL_0043
    IL_002c: ldarg.0      // 'value'
    IL_002d: ldstr        "/"
    IL_0032: call         bool [System.Runtime]System.String::op_Equality(string, string)
    IL_0037: brtrue.s     IL_0047
    IL_0039: br.s         IL_004b

    // [25 28 - 25 53]
    IL_003b: ldc.i4.0
    IL_003c: stloc.1      // V_1
    IL_003d: br.s         IL_0051

    // [26 28 - 26 54]
    IL_003f: ldc.i4.1
    IL_0040: stloc.1      // V_1
    IL_0041: br.s         IL_0051

    // [27 28 - 27 57]
    IL_0043: ldc.i4.2
    IL_0044: stloc.1      // V_1
    IL_0045: br.s         IL_0051

    // [28 28 - 28 55]
    IL_0047: ldc.i4.3
    IL_0048: stloc.1      // V_1
    IL_0049: br.s         IL_0051

    // [29 26 - 29 51]
    IL_004b: ldsfld       class [System.Runtime]System.Exception homework2.Parseril::ParserilOperationFail
    IL_0050: throw

    IL_0051: ldc.i4.1
    IL_0052: brtrue.s     IL_0055

    // [23 17 - 30 19]
    IL_0054: nop

    IL_0055: ldloc.1      // V_1
    IL_0056: stloc.0      // result

    // [32 17 - 32 31]
    IL_0057: ldloc.0      // result
    IL_0058: stloc.2      // V_2
    IL_0059: br.s         IL_005b

    // [33 13 - 33 14]
    IL_005b: ldloc.2      // V_2
    IL_005c: ret

  } // end of method Parseril::ParseOperation

  .method private hidebysig static specialname rtspecialname void
    .cctor() cil managed
  {
    .maxstack 8

    // [6 13 - 6 94]
    IL_0000: ldstr        "Value is not integer"
    IL_0005: newobj       instance void [System.Runtime]System.Exception::.ctor(string)
    IL_000a: stsfld       class [System.Runtime]System.Exception homework2.Parseril::ParserilNumberFail

    // [8 13 - 8 101]
    IL_000f: ldstr        "Operation is not correct"
    IL_0014: newobj       instance void [System.Runtime]System.Exception::.ctor(string)
    IL_0019: stsfld       class [System.Runtime]System.Exception homework2.Parseril::ParserilOperationFail
    IL_001e: ret

  } // end of method Parseril::.cctor
} // end of class homework2.Parseril

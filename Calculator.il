.class public abstract sealed auto ansi beforefieldinit
  homework2.Calculatoril
    extends [System.Runtime]System.Object
{

  .class nested public sealed auto ansi
    Operation
      extends [System.Runtime]System.Enum
  {

    .field public specialname rtspecialname int32 value__

    .field public static literal valuetype homework2.Calculatoril/Operation Plus = int32(0) // 0x00000000

    .field public static literal valuetype homework2.Calculatoril/Operation Minus = int32(1) // 0x00000001

    .field public static literal valuetype homework2.Calculatoril/Operation Multiply = int32(2) // 0x00000002

    .field public static literal valuetype homework2.Calculatoril/Operation Divide = int32(3) // 0x00000003

    .field public static literal valuetype homework2.Calculatoril/Operation Unknown = int32(4) // 0x00000004
  } // end of class Operation

  .field public static class [System.Runtime]System.Exception WrongOperation

  .field public static class [System.Runtime]System.DivideByZeroException DivisionByZero

  .method public hidebysig static int32
    Calculate(
      int32 num1,
      int32 num2,
      valuetype homework2.Calculatoril/Operation operation
    ) cil managed
  {
    .maxstack 2
    .locals init (
      [0] int32 result,
      [1] valuetype homework2.Calculatoril/Operation V_1,
      [2] valuetype homework2.Calculatoril/Operation V_2,
      [3] bool V_3,
      [4] int32 V_4
    )

    // [21 9 - 21 10]
    IL_0000: nop

    // [22 13 - 22 28]
    IL_0001: ldc.i4.0
    IL_0002: stloc.0      // result

    // [23 13 - 23 31]
    IL_0003: ldarg.2      // operation
    IL_0004: stloc.2      // V_2

    IL_0005: ldloc.2      // V_2
    IL_0006: stloc.1      // V_1

    IL_0007: ldloc.1      // V_1
    IL_0008: switch       (IL_001f, IL_0025, IL_002b, IL_0031)
    IL_001d: br.s         IL_0046

    // [26 21 - 26 42]
    IL_001f: ldarg.0      // num1
    IL_0020: ldarg.1      // num2
    IL_0021: add
    IL_0022: stloc.0      // result

    // [27 21 - 27 27]
    IL_0023: br.s         IL_004c

    // [29 21 - 29 42]
    IL_0025: ldarg.0      // num1
    IL_0026: ldarg.1      // num2
    IL_0027: sub
    IL_0028: stloc.0      // result

    // [30 21 - 30 27]
    IL_0029: br.s         IL_004c

    // [32 21 - 32 42]
    IL_002b: ldarg.0      // num1
    IL_002c: ldarg.1      // num2
    IL_002d: mul
    IL_002e: stloc.0      // result

    // [33 21 - 33 27]
    IL_002f: br.s         IL_004c

    // [35 21 - 35 35]
    IL_0031: ldarg.1      // num2
    IL_0032: ldc.i4.0
    IL_0033: ceq
    IL_0035: stloc.3      // V_3

    IL_0036: ldloc.3      // V_3
    IL_0037: brfalse.s    IL_0040

    // [36 21 - 36 22]
    IL_0039: nop

    // [37 25 - 37 46]
    IL_003a: ldsfld       class [System.Runtime]System.DivideByZeroException homework2.Calculatoril::DivisionByZero
    IL_003f: throw

    // [39 21 - 39 42]
    IL_0040: ldarg.0      // num1
    IL_0041: ldarg.1      // num2
    IL_0042: div
    IL_0043: stloc.0      // result

    // [40 21 - 40 27]
    IL_0044: br.s         IL_004c

    // [42 21 - 42 42]
    IL_0046: ldsfld       class [System.Runtime]System.Exception homework2.Calculatoril::WrongOperation
    IL_004b: throw


    IL_004c: ldloc.0      
    IL_004d: stloc.s      V_4
    IL_004f: br.s         IL_0051
    IL_0051: ldloc.s      V_4
    IL_0053: ret
  }

  .method private hidebysig static specialname rtspecialname void
    .cctor() cil managed
  {
    .maxstack 8
    IL_0000: ldstr        "Operation is unknown"
    IL_0005: newobj       instance void [System.Runtime]System.Exception::.ctor(string)
    IL_000a: stsfld       class [System.Runtime]System.Exception homework2.Calculatoril::WrongOperation
    IL_000f: ldstr        "Num2 is zero"
    IL_0014: newobj       instance void [System.Runtime]System.DivideByZeroException::.ctor(string)
    IL_0019: stsfld       class [System.Runtime]System.DivideByZeroException homework2.Calculatoril::DivisionByZero
    IL_001e: ret

  }
}

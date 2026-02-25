using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;
using NetExtender.Utilities.Core;

namespace NetExtender.Harmony.Types.Interfaces
{
    public interface IHarmonyInstruction
    {
        public static IHarmonyInstruction New()
        {
            return Create(default, null);
        }

        public static IHarmonyInstruction Create(OpCode opcode)
        {
            return Create(opcode, null);
        }

        public static IHarmonyInstruction Create(OpCode opcode, Object? operand)
        {
            return HarmonySignatureUtilities.CreateHarmonyInstruction(opcode, operand);
        }

        public ref OpCode OpCode { get; }
        public ref Object? Operand { get; }
        public IList<Label> Labels { get; }
        public IList<HarmonyExceptionBlock> Blocks { get; }
        public Boolean IsValid { get; }

        public Boolean HasBlock(HarmonyExceptionBlockType type);
        public Int32 LocalIndex();
        public Int32 ArgumentIndex();
        public Boolean LoadsConstant();
        public Boolean LoadsConstant(Int64 value);
        public Boolean LoadsConstant(Double value);
        public Boolean LoadsConstant(Enum value);
        public Boolean LoadsConstant(String value);
        public Boolean LoadsField(FieldInfo field, Boolean addressable = false);
        public Boolean StoresField(FieldInfo field);
        public Boolean Is(OpCode opcode, Object? operand);
        public Boolean Is(OpCode opcode, MemberInfo operand);
        public Boolean OperandIs(Object? operand);
        public Boolean OperandIs(MemberInfo member);
        public Boolean IsLdarg(Int32? n = null);
        public Boolean IsLdarga(Int32? n = null);
        public Boolean IsStarg(Int32? n = null);
        public Boolean IsLdloc(LocalBuilder? variable = null);
        public Boolean IsStloc(LocalBuilder? variable = null);
        public Boolean Calls(MethodInfo method);
        public Boolean Branches(out Label? label);
        public IHarmonyInstruction WithLabels(params Label[] labels);
        public IHarmonyInstruction WithLabels(IEnumerable<Label> labels);
        public IHarmonyInstruction WithBlocks(params HarmonyExceptionBlock[] blocks);
        public IHarmonyInstruction WithBlocks(IEnumerable<HarmonyExceptionBlock> blocks);
        public IHarmonyInstruction MoveLabelsTo(IHarmonyInstruction other);
        public IHarmonyInstruction MoveLabelsFrom(IHarmonyInstruction other);
        public IHarmonyInstruction MoveBlocksTo(IHarmonyInstruction other);
        public IHarmonyInstruction MoveBlocksFrom(IHarmonyInstruction other);
        public IList<Label> ExtractLabels();
        public IList<HarmonyExceptionBlock> ExtractBlocks();

        public IHarmonyInstruction Call(LambdaExpression expression);
        public IHarmonyInstruction Call(Expression<Action> expression);
        public IHarmonyInstruction Call<T>(Expression<Action<T>> expression);
        public IHarmonyInstruction Call<T, TResult>(Expression<Func<T, TResult>> expression);
        public IHarmonyInstruction Call(Type type, String name, Type[]? parameters = null, Type[]? generics = null);
        public IHarmonyInstruction Call(String method, Type[]? parameters = null, Type[]? generics = null);
        public IHarmonyInstruction CallClosure<T>(T closure) where T : Delegate;
        public IHarmonyInstruction LoadArgument(Int32 index, Boolean addressable = false);
        public IHarmonyInstruction StoreArgument(Int32 index);
        public IHarmonyInstruction LoadLocal(Int32 index, Boolean addressable = false);
        public IHarmonyInstruction StoreLocal(Int32 index);
        public IHarmonyInstruction LoadField(Type type, String name, Boolean addressable = false);
        public IHarmonyInstruction StoreField(Type type, String name);

        public IHarmonyInstruction Clone();
        public IHarmonyInstruction Clone(OpCode opcode);
        public IHarmonyInstruction Clone(Object operand);
    }
}
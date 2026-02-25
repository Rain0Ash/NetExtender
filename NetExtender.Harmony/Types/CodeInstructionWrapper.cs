using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;
using NetExtender.Harmony.Types.Interfaces;
using NetExtender.Harmony.Utilities;
using NetExtender.Types.Lists;

namespace NetExtender.Harmony.Types
{
    public struct CodeInstructionWrapper : IEquatableStruct<CodeInstructionWrapper>, IUnsafeHarmonyInstruction, IEquatable<CodeInstruction>
    {
        public static implicit operator CodeInstructionWrapper(CodeInstruction value)
        {
            return new CodeInstructionWrapper(value);
        }

        public static implicit operator CodeInstruction(CodeInstructionWrapper value)
        {
            return value.Instruction;
        }

        public static Boolean operator ==(CodeInstructionWrapper first, CodeInstructionWrapper second)
        {
            return first.Instruction == second.Instruction;
        }

        public static Boolean operator !=(CodeInstructionWrapper first, CodeInstructionWrapper second)
        {
            return first.Instruction != second.Instruction;
        }

        private CodeInstruction _instruction;
        private CodeInstruction Instruction
        {
            get
            {
                return _instruction ??= new CodeInstruction(default, default);
            }
        }

        CodeInstruction IUnsafeHarmonyInstruction.Instruction
        {
            get
            {
                return Instruction;
            }
        }

        public ref OpCode OpCode
        {
            get
            {
                return ref Instruction.opcode;
            }
        }

        public ref Object? Operand
        {
            get
            {
                return ref Instruction.operand!;
            }
        }

        public IList<Label> Labels
        {
            get
            {
                return Instruction.labels;
            }
        }

        public IList<HarmonyExceptionBlock> Blocks
        {
            get
            {
                return new TwoWaySelectorListWrapper<ExceptionBlock, HarmonyExceptionBlock>(Instruction.blocks, HarmonyUtilities.ToHarmonyBlock, HarmonyUtilities.ToBlock);
            }
        }

        public Boolean IsValid
        {
            get
            {
                return Instruction.opcode.IsValid();
            }
        }

        public readonly Boolean IsEmpty
        {
            get
            {
                return _instruction is null;
            }
        }

        public CodeInstructionWrapper(OpCode opcode, Object? operand = null)
        {
            _instruction = new CodeInstruction(opcode, operand);
        }

        public CodeInstructionWrapper(CodeInstruction instruction)
        {
            _instruction = instruction;
        }

        public Boolean HasBlock(HarmonyExceptionBlockType type)
        {
            return Instruction.HasBlock(type.ToBlockType());
        }

        public Int32 LocalIndex()
        {
            return Instruction.LocalIndex();
        }

        public Int32 ArgumentIndex()
        {
            return Instruction.ArgumentIndex();
        }

        public Boolean LoadsConstant()
        {
            return Instruction.LoadsConstant();
        }

        public Boolean LoadsConstant(Int64 value)
        {
            return Instruction.LoadsConstant(value);
        }

        public Boolean LoadsConstant(Double value)
        {
            return Instruction.LoadsConstant(value);
        }

        public Boolean LoadsConstant(Enum value)
        {
            return Instruction.LoadsConstant(value);
        }

        public Boolean LoadsConstant(String value)
        {
            return Instruction.LoadsConstant(value);
        }

        public Boolean LoadsField(FieldInfo field, Boolean addressable = false)
        {
            return Instruction.LoadsField(field, addressable);
        }

        public Boolean StoresField(FieldInfo field)
        {
            return Instruction.StoresField(field);
        }

        public Boolean Is(OpCode opcode, Object? operand)
        {
            return Instruction.Is(opcode, operand);
        }

        public Boolean Is(OpCode opcode, MemberInfo operand)
        {
            return Instruction.Is(opcode, operand);
        }

        public Boolean OperandIs(Object? operand)
        {
            return Instruction.OperandIs(operand);
        }

        public Boolean OperandIs(MemberInfo member)
        {
            return Instruction.OperandIs(member);
        }

        public Boolean IsLdarg(Int32? n = null)
        {
            return Instruction.IsLdarg(n);
        }

        public Boolean IsLdarga(Int32? n = null)
        {
            return Instruction.IsLdarga(n);
        }

        public Boolean IsStarg(Int32? n = null)
        {
            return Instruction.IsStarg(n);
        }

        public Boolean IsLdloc(LocalBuilder? variable = null)
        {
            return Instruction.IsLdloc(variable);
        }

        public Boolean IsStloc(LocalBuilder? variable = null)
        {
            return Instruction.IsStloc(variable);
        }

        public Boolean Calls(MethodInfo method)
        {
            return Instruction.Calls(method);
        }

        public Boolean Branches(out Label? label)
        {
            return Instruction.Branches(out label);
        }

        public IHarmonyInstruction WithLabels(params Label[] labels)
        {
            return new CodeInstructionWrapper(Instruction.WithLabels(labels));
        }

        public IHarmonyInstruction WithLabels(IEnumerable<Label> labels)
        {
            return new CodeInstructionWrapper(Instruction.WithLabels(labels));
        }

        public IHarmonyInstruction WithBlocks(params HarmonyExceptionBlock[] blocks)
        {
            return WithBlocks((IEnumerable<HarmonyExceptionBlock>) blocks);
        }

        public IHarmonyInstruction WithBlocks(IEnumerable<HarmonyExceptionBlock> blocks)
        {
            if (blocks is null)
            {
                throw new ArgumentNullException(nameof(blocks));
            }

            return new CodeInstructionWrapper(Instruction.WithBlocks(blocks.Select(HarmonyUtilities.ToBlock)));
        }

        public IHarmonyInstruction MoveLabelsTo(IHarmonyInstruction other)
        {
            return other is CodeInstructionWrapper wrapper ? new CodeInstructionWrapper(Instruction.MoveLabelsTo(wrapper.Instruction)) : throw new NotSupportedException();
        }

        public IHarmonyInstruction MoveLabelsFrom(IHarmonyInstruction other)
        {
            return other is CodeInstructionWrapper wrapper ? new CodeInstructionWrapper(Instruction.MoveLabelsFrom(wrapper.Instruction)) : throw new NotSupportedException();
        }

        public IHarmonyInstruction MoveBlocksTo(IHarmonyInstruction other)
        {
            return other is CodeInstructionWrapper wrapper ? new CodeInstructionWrapper(Instruction.MoveBlocksTo(wrapper.Instruction)) : throw new NotSupportedException();
        }

        public IHarmonyInstruction MoveBlocksFrom(IHarmonyInstruction other)
        {
            return other is CodeInstructionWrapper wrapper ? new CodeInstructionWrapper(Instruction.MoveBlocksFrom(wrapper.Instruction)) : throw new NotSupportedException();
        }

        public IList<Label> ExtractLabels()
        {
            return Instruction.ExtractLabels();
        }

        public IList<HarmonyExceptionBlock> ExtractBlocks()
        {
            return new TwoWaySelectorListWrapper<ExceptionBlock, HarmonyExceptionBlock>(Instruction.ExtractBlocks(), HarmonyUtilities.ToHarmonyBlock, HarmonyUtilities.ToBlock);
        }

        public IHarmonyInstruction Call(LambdaExpression expression)
        {
            if (expression is null)
            {
                throw new ArgumentNullException(nameof(expression));
            }

            _instruction = CodeInstruction.Call(expression);
            return this;
        }

        public IHarmonyInstruction Call(Expression<Action> expression)
        {
            if (expression is null)
            {
                throw new ArgumentNullException(nameof(expression));
            }

            _instruction = CodeInstruction.Call(expression);
            return this;
        }

        public IHarmonyInstruction Call<T>(Expression<Action<T>> expression)
        {
            if (expression is null)
            {
                throw new ArgumentNullException(nameof(expression));
            }

            _instruction = CodeInstruction.Call(expression);
            return this;
        }

        public IHarmonyInstruction Call<T, TResult>(Expression<Func<T, TResult>> expression)
        {
            if (expression is null)
            {
                throw new ArgumentNullException(nameof(expression));
            }

            _instruction = CodeInstruction.Call(expression);
            return this;
        }

        public IHarmonyInstruction Call(Type type, String name, Type[]? parameters = null, Type[]? generics = null)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            _instruction = CodeInstruction.Call(type, name, parameters, generics);
            return this;
        }

        public IHarmonyInstruction Call(String method, Type[]? parameters = null, Type[]? generics = null)
        {
            if (method is null)
            {
                throw new ArgumentNullException(nameof(method));
            }

            _instruction = CodeInstruction.Call(method, parameters, generics);
            return this;
        }

        public IHarmonyInstruction CallClosure<T>(T closure) where T : Delegate
        {
            if (closure is null)
            {
                throw new ArgumentNullException(nameof(closure));
            }

            _instruction = CodeInstruction.CallClosure(closure);
            return this;
        }

        public IHarmonyInstruction LoadArgument(Int32 index, Boolean addressable = false)
        {
            _instruction = CodeInstruction.LoadArgument(index, addressable);
            return this;
        }

        public IHarmonyInstruction StoreArgument(Int32 index)
        {
            _instruction = CodeInstruction.StoreArgument(index);
            return this;
        }

        public IHarmonyInstruction LoadLocal(Int32 index, Boolean addressable = false)
        {
            _instruction = CodeInstruction.LoadLocal(index, addressable);
            return this;
        }

        public IHarmonyInstruction StoreLocal(Int32 index)
        {
            _instruction = CodeInstruction.StoreLocal(index);
            return this;
        }

        public IHarmonyInstruction LoadField(Type type, String name, Boolean addressable = false)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            _instruction = CodeInstruction.LoadField(type, name, addressable);
            return this;
        }

        public IHarmonyInstruction StoreField(Type type, String name)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            _instruction = CodeInstruction.StoreField(type, name);
            return this;
        }

        public IHarmonyInstruction Clone()
        {
            return new CodeInstructionWrapper(Instruction.Clone());
        }

        public IHarmonyInstruction Clone(OpCode opcode)
        {
            return new CodeInstructionWrapper(Instruction.Clone(opcode));
        }

        public IHarmonyInstruction Clone(Object operand)
        {
            return new CodeInstructionWrapper(Instruction.Clone(operand));
        }

        public override Int32 GetHashCode()
        {
            return Instruction.GetHashCode();
        }

        public override Boolean Equals([NotNullWhen(true)] Object? other)
        {
            return other switch
            {
                CodeInstruction value => Equals(value),
                CodeInstructionWrapper value => Equals(value),
                _ => false
            };
        }

        public Boolean Equals(CodeInstruction? other)
        {
            return Instruction.Equals(other);
        }

        public Boolean Equals(CodeInstructionWrapper other)
        {
            return Equals(other.Instruction);
        }

        public override String ToString()
        {
            return Instruction.ToString();
        }
    }
}
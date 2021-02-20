// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection;
using JetBrains.Annotations;

namespace NetExtender.Localizations.Resources
{
    public static class SystemResources
    {
        internal delegate String SystemResourceDelegate();

        internal static IImmutableDictionary<String, SystemResourceDelegate> Handlers { get; }

        static SystemResources()
        {
            Handlers = GetProperties();
        }

        private static IImmutableDictionary<String, SystemResourceDelegate> GetProperties()
        {
            Type type = Assembly.GetAssembly(typeof(String))?.GetType("System.SR") ?? throw new ArgumentNullException(nameof(type));

            PropertyInfo[] properties = type.GetProperties(BindingFlags.Static | BindingFlags.NonPublic);

            return properties.Where(info => info.PropertyType == typeof(String)).ToImmutableDictionary(info => info.Name, GetDelegate);
        }

        private static SystemResourceDelegate GetDelegate([NotNull] PropertyInfo info)
        {
            if (info is null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            return info.GetMethod?.CreateDelegate<SystemResourceDelegate>() ?? throw new ArgumentException(@"Property not contains get accessor", nameof(info));
        }
        
        public static String Acc_CreateAbstEx
        {
            get
            {
                return Handlers[nameof(Acc_CreateAbstEx)].Invoke();
            }
        }

        public static String Acc_CreateArgIterator
        {
            get
            {
                return Handlers[nameof(Acc_CreateArgIterator)].Invoke();
            }
        }

        public static String Acc_CreateGenericEx
        {
            get
            {
                return Handlers[nameof(Acc_CreateGenericEx)].Invoke();
            }
        }

        public static String Acc_CreateInterfaceEx
        {
            get
            {
                return Handlers[nameof(Acc_CreateInterfaceEx)].Invoke();
            }
        }

        public static String Acc_CreateVoid
        {
            get
            {
                return Handlers[nameof(Acc_CreateVoid)].Invoke();
            }
        }

        public static String Acc_NotClassInit
        {
            get
            {
                return Handlers[nameof(Acc_NotClassInit)].Invoke();
            }
        }

        public static String Acc_ReadOnly
        {
            get
            {
                return Handlers[nameof(Acc_ReadOnly)].Invoke();
            }
        }

        public static String Access_Void
        {
            get
            {
                return Handlers[nameof(Access_Void)].Invoke();
            }
        }

        public static String AggregateException_ctor_DefaultMessage
        {
            get
            {
                return Handlers[nameof(AggregateException_ctor_DefaultMessage)].Invoke();
            }
        }

        public static String AggregateException_ctor_InnerExceptionNull
        {
            get
            {
                return Handlers[nameof(AggregateException_ctor_InnerExceptionNull)].Invoke();
            }
        }

        public static String AggregateException_DeserializationFailure
        {
            get
            {
                return Handlers[nameof(AggregateException_DeserializationFailure)].Invoke();
            }
        }

        public static String AggregateException_InnerException
        {
            get
            {
                return Handlers[nameof(AggregateException_InnerException)].Invoke();
            }
        }

        public static String AppDomain_Name
        {
            get
            {
                return Handlers[nameof(AppDomain_Name)].Invoke();
            }
        }

        public static String AppDomain_NoContextPolicies
        {
            get
            {
                return Handlers[nameof(AppDomain_NoContextPolicies)].Invoke();
            }
        }

        public static String AppDomain_Policy_PrincipalTwice
        {
            get
            {
                return Handlers[nameof(AppDomain_Policy_PrincipalTwice)].Invoke();
            }
        }

        public static String AmbiguousImplementationException_NullMessage
        {
            get
            {
                return Handlers[nameof(AmbiguousImplementationException_NullMessage)].Invoke();
            }
        }

        public static String Arg_AccessException
        {
            get
            {
                return Handlers[nameof(Arg_AccessException)].Invoke();
            }
        }

        public static String Arg_AccessViolationException
        {
            get
            {
                return Handlers[nameof(Arg_AccessViolationException)].Invoke();
            }
        }

        public static String Arg_AmbiguousMatchException
        {
            get
            {
                return Handlers[nameof(Arg_AmbiguousMatchException)].Invoke();
            }
        }

        public static String Arg_ApplicationException
        {
            get
            {
                return Handlers[nameof(Arg_ApplicationException)].Invoke();
            }
        }

        public static String Arg_ArgumentException
        {
            get
            {
                return Handlers[nameof(Arg_ArgumentException)].Invoke();
            }
        }

        public static String Arg_ArgumentOutOfRangeException
        {
            get
            {
                return Handlers[nameof(Arg_ArgumentOutOfRangeException)].Invoke();
            }
        }

        public static String Arg_ArithmeticException
        {
            get
            {
                return Handlers[nameof(Arg_ArithmeticException)].Invoke();
            }
        }

        public static String Arg_ArrayLengthsDiffer
        {
            get
            {
                return Handlers[nameof(Arg_ArrayLengthsDiffer)].Invoke();
            }
        }

        public static String Arg_ArrayPlusOffTooSmall
        {
            get
            {
                return Handlers[nameof(Arg_ArrayPlusOffTooSmall)].Invoke();
            }
        }

        public static String Arg_ArrayTypeMismatchException
        {
            get
            {
                return Handlers[nameof(Arg_ArrayTypeMismatchException)].Invoke();
            }
        }

        public static String Arg_ArrayZeroError
        {
            get
            {
                return Handlers[nameof(Arg_ArrayZeroError)].Invoke();
            }
        }

        public static String Arg_BadDecimal
        {
            get
            {
                return Handlers[nameof(Arg_BadDecimal)].Invoke();
            }
        }

        public static String Arg_BadImageFormatException
        {
            get
            {
                return Handlers[nameof(Arg_BadImageFormatException)].Invoke();
            }
        }

        public static String Arg_BadLiteralFormat
        {
            get
            {
                return Handlers[nameof(Arg_BadLiteralFormat)].Invoke();
            }
        }

        public static String Arg_BogusIComparer
        {
            get
            {
                return Handlers[nameof(Arg_BogusIComparer)].Invoke();
            }
        }

        public static String Arg_BufferTooSmall
        {
            get
            {
                return Handlers[nameof(Arg_BufferTooSmall)].Invoke();
            }
        }

        public static String Arg_CannotBeNaN
        {
            get
            {
                return Handlers[nameof(Arg_CannotBeNaN)].Invoke();
            }
        }

        public static String Arg_CannotHaveNegativeValue
        {
            get
            {
                return Handlers[nameof(Arg_CannotHaveNegativeValue)].Invoke();
            }
        }

        public static String Arg_CannotMixComparisonInfrastructure
        {
            get
            {
                return Handlers[nameof(Arg_CannotMixComparisonInfrastructure)].Invoke();
            }
        }

        public static String Arg_CannotUnloadAppDomainException
        {
            get
            {
                return Handlers[nameof(Arg_CannotUnloadAppDomainException)].Invoke();
            }
        }

        public static String Arg_CATypeResolutionFailed
        {
            get
            {
                return Handlers[nameof(Arg_CATypeResolutionFailed)].Invoke();
            }
        }

        public static String Arg_COMAccess
        {
            get
            {
                return Handlers[nameof(Arg_COMAccess)].Invoke();
            }
        }

        public static String Arg_COMException
        {
            get
            {
                return Handlers[nameof(Arg_COMException)].Invoke();
            }
        }

        public static String Arg_COMPropSetPut
        {
            get
            {
                return Handlers[nameof(Arg_COMPropSetPut)].Invoke();
            }
        }

        public static String Arg_CreatInstAccess
        {
            get
            {
                return Handlers[nameof(Arg_CreatInstAccess)].Invoke();
            }
        }

        public static String Arg_CryptographyException
        {
            get
            {
                return Handlers[nameof(Arg_CryptographyException)].Invoke();
            }
        }

        public static String Arg_CustomAttributeFormatException
        {
            get
            {
                return Handlers[nameof(Arg_CustomAttributeFormatException)].Invoke();
            }
        }

        public static String Arg_DataMisalignedException
        {
            get
            {
                return Handlers[nameof(Arg_DataMisalignedException)].Invoke();
            }
        }

        public static String Arg_DateTimeRange
        {
            get
            {
                return Handlers[nameof(Arg_DateTimeRange)].Invoke();
            }
        }

        public static String Arg_DecBitCtor
        {
            get
            {
                return Handlers[nameof(Arg_DecBitCtor)].Invoke();
            }
        }

        public static String Arg_DirectoryNotFoundException
        {
            get
            {
                return Handlers[nameof(Arg_DirectoryNotFoundException)].Invoke();
            }
        }

        public static String Arg_DivideByZero
        {
            get
            {
                return Handlers[nameof(Arg_DivideByZero)].Invoke();
            }
        }

        public static String Arg_DlgtNullInst
        {
            get
            {
                return Handlers[nameof(Arg_DlgtNullInst)].Invoke();
            }
        }

        public static String Arg_DlgtTargMeth
        {
            get
            {
                return Handlers[nameof(Arg_DlgtTargMeth)].Invoke();
            }
        }

        public static String Arg_DlgtTypeMis
        {
            get
            {
                return Handlers[nameof(Arg_DlgtTypeMis)].Invoke();
            }
        }

        public static String Arg_DllNotFoundException
        {
            get
            {
                return Handlers[nameof(Arg_DllNotFoundException)].Invoke();
            }
        }

        public static String Arg_DuplicateWaitObjectException
        {
            get
            {
                return Handlers[nameof(Arg_DuplicateWaitObjectException)].Invoke();
            }
        }

        public static String Arg_EHClauseNotClause
        {
            get
            {
                return Handlers[nameof(Arg_EHClauseNotClause)].Invoke();
            }
        }

        public static String Arg_EHClauseNotFilter
        {
            get
            {
                return Handlers[nameof(Arg_EHClauseNotFilter)].Invoke();
            }
        }

        public static String Arg_EmptyArray
        {
            get
            {
                return Handlers[nameof(Arg_EmptyArray)].Invoke();
            }
        }

        public static String Arg_EndOfStreamException
        {
            get
            {
                return Handlers[nameof(Arg_EndOfStreamException)].Invoke();
            }
        }

        public static String Arg_EntryPointNotFoundException
        {
            get
            {
                return Handlers[nameof(Arg_EntryPointNotFoundException)].Invoke();
            }
        }

        public static String Arg_EnumAndObjectMustBeSameType
        {
            get
            {
                return Handlers[nameof(Arg_EnumAndObjectMustBeSameType)].Invoke();
            }
        }

        public static String Arg_EnumFormatUnderlyingTypeAndObjectMustBeSameType
        {
            get
            {
                return Handlers[nameof(Arg_EnumFormatUnderlyingTypeAndObjectMustBeSameType)].Invoke();
            }
        }

        public static String Arg_EnumIllegalVal
        {
            get
            {
                return Handlers[nameof(Arg_EnumIllegalVal)].Invoke();
            }
        }

        public static String Arg_EnumLitValueNotFound
        {
            get
            {
                return Handlers[nameof(Arg_EnumLitValueNotFound)].Invoke();
            }
        }

        public static String Arg_EnumUnderlyingTypeAndObjectMustBeSameType
        {
            get
            {
                return Handlers[nameof(Arg_EnumUnderlyingTypeAndObjectMustBeSameType)].Invoke();
            }
        }

        public static String Arg_EnumValueNotFound
        {
            get
            {
                return Handlers[nameof(Arg_EnumValueNotFound)].Invoke();
            }
        }

        public static String Arg_ExecutionEngineException
        {
            get
            {
                return Handlers[nameof(Arg_ExecutionEngineException)].Invoke();
            }
        }

        public static String Arg_ExternalException
        {
            get
            {
                return Handlers[nameof(Arg_ExternalException)].Invoke();
            }
        }

        public static String Arg_FieldAccessException
        {
            get
            {
                return Handlers[nameof(Arg_FieldAccessException)].Invoke();
            }
        }

        public static String Arg_FieldDeclTarget
        {
            get
            {
                return Handlers[nameof(Arg_FieldDeclTarget)].Invoke();
            }
        }

        public static String Arg_FldGetArgErr
        {
            get
            {
                return Handlers[nameof(Arg_FldGetArgErr)].Invoke();
            }
        }

        public static String Arg_FldGetPropSet
        {
            get
            {
                return Handlers[nameof(Arg_FldGetPropSet)].Invoke();
            }
        }

        public static String Arg_FldSetArgErr
        {
            get
            {
                return Handlers[nameof(Arg_FldSetArgErr)].Invoke();
            }
        }

        public static String Arg_FldSetGet
        {
            get
            {
                return Handlers[nameof(Arg_FldSetGet)].Invoke();
            }
        }

        public static String Arg_FldSetInvoke
        {
            get
            {
                return Handlers[nameof(Arg_FldSetInvoke)].Invoke();
            }
        }

        public static String Arg_FldSetPropGet
        {
            get
            {
                return Handlers[nameof(Arg_FldSetPropGet)].Invoke();
            }
        }

        public static String Arg_FormatException
        {
            get
            {
                return Handlers[nameof(Arg_FormatException)].Invoke();
            }
        }

        public static String Arg_GenericParameter
        {
            get
            {
                return Handlers[nameof(Arg_GenericParameter)].Invoke();
            }
        }

        public static String Arg_GetMethNotFnd
        {
            get
            {
                return Handlers[nameof(Arg_GetMethNotFnd)].Invoke();
            }
        }

        public static String Arg_GuidArrayCtor
        {
            get
            {
                return Handlers[nameof(Arg_GuidArrayCtor)].Invoke();
            }
        }

        public static String Arg_HandleNotAsync
        {
            get
            {
                return Handlers[nameof(Arg_HandleNotAsync)].Invoke();
            }
        }

        public static String Arg_HandleNotSync
        {
            get
            {
                return Handlers[nameof(Arg_HandleNotSync)].Invoke();
            }
        }

        public static String Arg_HexStyleNotSupported
        {
            get
            {
                return Handlers[nameof(Arg_HexStyleNotSupported)].Invoke();
            }
        }

        public static String Arg_HTCapacityOverflow
        {
            get
            {
                return Handlers[nameof(Arg_HTCapacityOverflow)].Invoke();
            }
        }

        public static String Arg_IndexMustBeInt
        {
            get
            {
                return Handlers[nameof(Arg_IndexMustBeInt)].Invoke();
            }
        }

        public static String Arg_IndexOutOfRangeException
        {
            get
            {
                return Handlers[nameof(Arg_IndexOutOfRangeException)].Invoke();
            }
        }

        public static String Arg_InsufficientExecutionStackException
        {
            get
            {
                return Handlers[nameof(Arg_InsufficientExecutionStackException)].Invoke();
            }
        }

        public static String Arg_InvalidANSIString
        {
            get
            {
                return Handlers[nameof(Arg_InvalidANSIString)].Invoke();
            }
        }

        public static String Arg_InvalidBase
        {
            get
            {
                return Handlers[nameof(Arg_InvalidBase)].Invoke();
            }
        }

        public static String Arg_InvalidCastException
        {
            get
            {
                return Handlers[nameof(Arg_InvalidCastException)].Invoke();
            }
        }

        public static String Arg_InvalidComObjectException
        {
            get
            {
                return Handlers[nameof(Arg_InvalidComObjectException)].Invoke();
            }
        }

        public static String Arg_InvalidFilterCriteriaException
        {
            get
            {
                return Handlers[nameof(Arg_InvalidFilterCriteriaException)].Invoke();
            }
        }

        public static String Arg_InvalidHandle
        {
            get
            {
                return Handlers[nameof(Arg_InvalidHandle)].Invoke();
            }
        }

        public static String Arg_InvalidHexStyle
        {
            get
            {
                return Handlers[nameof(Arg_InvalidHexStyle)].Invoke();
            }
        }

        public static String Arg_InvalidNeutralResourcesLanguage_Asm_Culture
        {
            get
            {
                return Handlers[nameof(Arg_InvalidNeutralResourcesLanguage_Asm_Culture)].Invoke();
            }
        }

        public static String Arg_InvalidNeutralResourcesLanguage_FallbackLoc
        {
            get
            {
                return Handlers[nameof(Arg_InvalidNeutralResourcesLanguage_FallbackLoc)].Invoke();
            }
        }

        public static String Arg_InvalidSatelliteContract_Asm_Ver
        {
            get
            {
                return Handlers[nameof(Arg_InvalidSatelliteContract_Asm_Ver)].Invoke();
            }
        }

        public static String Arg_InvalidOleVariantTypeException
        {
            get
            {
                return Handlers[nameof(Arg_InvalidOleVariantTypeException)].Invoke();
            }
        }

        public static String Arg_InvalidOperationException
        {
            get
            {
                return Handlers[nameof(Arg_InvalidOperationException)].Invoke();
            }
        }

        public static String Arg_InvalidTypeInRetType
        {
            get
            {
                return Handlers[nameof(Arg_InvalidTypeInRetType)].Invoke();
            }
        }

        public static String Arg_InvalidTypeInSignature
        {
            get
            {
                return Handlers[nameof(Arg_InvalidTypeInSignature)].Invoke();
            }
        }

        public static String Arg_IOException
        {
            get
            {
                return Handlers[nameof(Arg_IOException)].Invoke();
            }
        }

        public static String Arg_KeyNotFound
        {
            get
            {
                return Handlers[nameof(Arg_KeyNotFound)].Invoke();
            }
        }

        public static String Arg_KeyNotFoundWithKey
        {
            get
            {
                return Handlers[nameof(Arg_KeyNotFoundWithKey)].Invoke();
            }
        }

        public static String Arg_LongerThanDestArray
        {
            get
            {
                return Handlers[nameof(Arg_LongerThanDestArray)].Invoke();
            }
        }

        public static String Arg_LongerThanSrcArray
        {
            get
            {
                return Handlers[nameof(Arg_LongerThanSrcArray)].Invoke();
            }
        }

        public static String Arg_LongerThanSrcString
        {
            get
            {
                return Handlers[nameof(Arg_LongerThanSrcString)].Invoke();
            }
        }

        public static String Arg_LowerBoundsMustMatch
        {
            get
            {
                return Handlers[nameof(Arg_LowerBoundsMustMatch)].Invoke();
            }
        }

        public static String Arg_MarshalAsAnyRestriction
        {
            get
            {
                return Handlers[nameof(Arg_MarshalAsAnyRestriction)].Invoke();
            }
        }

        public static String Arg_MarshalDirectiveException
        {
            get
            {
                return Handlers[nameof(Arg_MarshalDirectiveException)].Invoke();
            }
        }

        public static String Arg_MethodAccessException
        {
            get
            {
                return Handlers[nameof(Arg_MethodAccessException)].Invoke();
            }
        }

        public static String Arg_MissingFieldException
        {
            get
            {
                return Handlers[nameof(Arg_MissingFieldException)].Invoke();
            }
        }

        public static String Arg_MissingManifestResourceException
        {
            get
            {
                return Handlers[nameof(Arg_MissingManifestResourceException)].Invoke();
            }
        }

        public static String Arg_MissingMemberException
        {
            get
            {
                return Handlers[nameof(Arg_MissingMemberException)].Invoke();
            }
        }

        public static String Arg_MissingMethodException
        {
            get
            {
                return Handlers[nameof(Arg_MissingMethodException)].Invoke();
            }
        }

        public static String Arg_MulticastNotSupportedException
        {
            get
            {
                return Handlers[nameof(Arg_MulticastNotSupportedException)].Invoke();
            }
        }

        public static String Arg_MustBeBoolean
        {
            get
            {
                return Handlers[nameof(Arg_MustBeBoolean)].Invoke();
            }
        }

        public static String Arg_MustBeByte
        {
            get
            {
                return Handlers[nameof(Arg_MustBeByte)].Invoke();
            }
        }

        public static String Arg_MustBeChar
        {
            get
            {
                return Handlers[nameof(Arg_MustBeChar)].Invoke();
            }
        }

        public static String Arg_MustBeDateTime
        {
            get
            {
                return Handlers[nameof(Arg_MustBeDateTime)].Invoke();
            }
        }

        public static String Arg_MustBeDateTimeOffset
        {
            get
            {
                return Handlers[nameof(Arg_MustBeDateTimeOffset)].Invoke();
            }
        }

        public static String Arg_MustBeDecimal
        {
            get
            {
                return Handlers[nameof(Arg_MustBeDecimal)].Invoke();
            }
        }

        public static String Arg_MustBeDelegate
        {
            get
            {
                return Handlers[nameof(Arg_MustBeDelegate)].Invoke();
            }
        }

        public static String Arg_MustBeDouble
        {
            get
            {
                return Handlers[nameof(Arg_MustBeDouble)].Invoke();
            }
        }

        public static String Arg_MustBeEnum
        {
            get
            {
                return Handlers[nameof(Arg_MustBeEnum)].Invoke();
            }
        }

        public static String Arg_MustBeEnumBaseTypeOrEnum
        {
            get
            {
                return Handlers[nameof(Arg_MustBeEnumBaseTypeOrEnum)].Invoke();
            }
        }

        public static String Arg_MustBeGuid
        {
            get
            {
                return Handlers[nameof(Arg_MustBeGuid)].Invoke();
            }
        }

        public static String Arg_MustBeInt16
        {
            get
            {
                return Handlers[nameof(Arg_MustBeInt16)].Invoke();
            }
        }

        public static String Arg_MustBeInt32
        {
            get
            {
                return Handlers[nameof(Arg_MustBeInt32)].Invoke();
            }
        }

        public static String Arg_MustBeInt64
        {
            get
            {
                return Handlers[nameof(Arg_MustBeInt64)].Invoke();
            }
        }

        public static String Arg_MustBeIntPtr
        {
            get
            {
                return Handlers[nameof(Arg_MustBeIntPtr)].Invoke();
            }
        }

        public static String Arg_MustBePointer
        {
            get
            {
                return Handlers[nameof(Arg_MustBePointer)].Invoke();
            }
        }

        public static String Arg_MustBePrimArray
        {
            get
            {
                return Handlers[nameof(Arg_MustBePrimArray)].Invoke();
            }
        }

        public static String Arg_MustBeRuntimeAssembly
        {
            get
            {
                return Handlers[nameof(Arg_MustBeRuntimeAssembly)].Invoke();
            }
        }

        public static String Arg_MustBeSByte
        {
            get
            {
                return Handlers[nameof(Arg_MustBeSByte)].Invoke();
            }
        }

        public static String Arg_MustBeSingle
        {
            get
            {
                return Handlers[nameof(Arg_MustBeSingle)].Invoke();
            }
        }

        public static String Arg_MustBeString
        {
            get
            {
                return Handlers[nameof(Arg_MustBeString)].Invoke();
            }
        }

        public static String Arg_MustBeTimeSpan
        {
            get
            {
                return Handlers[nameof(Arg_MustBeTimeSpan)].Invoke();
            }
        }

        public static String Arg_MustBeType
        {
            get
            {
                return Handlers[nameof(Arg_MustBeType)].Invoke();
            }
        }

        public static String Arg_MustBeTrue
        {
            get
            {
                return Handlers[nameof(Arg_MustBeTrue)].Invoke();
            }
        }

        public static String Arg_MustBeUInt16
        {
            get
            {
                return Handlers[nameof(Arg_MustBeUInt16)].Invoke();
            }
        }

        public static String Arg_MustBeUInt32
        {
            get
            {
                return Handlers[nameof(Arg_MustBeUInt32)].Invoke();
            }
        }

        public static String Arg_MustBeUInt64
        {
            get
            {
                return Handlers[nameof(Arg_MustBeUInt64)].Invoke();
            }
        }

        public static String Arg_MustBeUIntPtr
        {
            get
            {
                return Handlers[nameof(Arg_MustBeUIntPtr)].Invoke();
            }
        }

        public static String Arg_MustBeVersion
        {
            get
            {
                return Handlers[nameof(Arg_MustBeVersion)].Invoke();
            }
        }

        public static String Arg_MustContainEnumInfo
        {
            get
            {
                return Handlers[nameof(Arg_MustContainEnumInfo)].Invoke();
            }
        }

        public static String Arg_NamedParamNull
        {
            get
            {
                return Handlers[nameof(Arg_NamedParamNull)].Invoke();
            }
        }

        public static String Arg_NamedParamTooBig
        {
            get
            {
                return Handlers[nameof(Arg_NamedParamTooBig)].Invoke();
            }
        }

        public static String Arg_NDirectBadObject
        {
            get
            {
                return Handlers[nameof(Arg_NDirectBadObject)].Invoke();
            }
        }

        public static String Arg_Need1DArray
        {
            get
            {
                return Handlers[nameof(Arg_Need1DArray)].Invoke();
            }
        }

        public static String Arg_Need2DArray
        {
            get
            {
                return Handlers[nameof(Arg_Need2DArray)].Invoke();
            }
        }

        public static String Arg_Need3DArray
        {
            get
            {
                return Handlers[nameof(Arg_Need3DArray)].Invoke();
            }
        }

        public static String Arg_NeedAtLeast1Rank
        {
            get
            {
                return Handlers[nameof(Arg_NeedAtLeast1Rank)].Invoke();
            }
        }

        public static String Arg_NegativeArgCount
        {
            get
            {
                return Handlers[nameof(Arg_NegativeArgCount)].Invoke();
            }
        }

        public static String Arg_NoAccessSpec
        {
            get
            {
                return Handlers[nameof(Arg_NoAccessSpec)].Invoke();
            }
        }

        public static String Arg_NoDefCTor
        {
            get
            {
                return Handlers[nameof(Arg_NoDefCTor)].Invoke();
            }
        }

        public static String Arg_NonZeroLowerBound
        {
            get
            {
                return Handlers[nameof(Arg_NonZeroLowerBound)].Invoke();
            }
        }

        public static String Arg_NoStaticVirtual
        {
            get
            {
                return Handlers[nameof(Arg_NoStaticVirtual)].Invoke();
            }
        }

        public static String Arg_NotFiniteNumberException
        {
            get
            {
                return Handlers[nameof(Arg_NotFiniteNumberException)].Invoke();
            }
        }

        public static String Arg_NotGenericMethodDefinition
        {
            get
            {
                return Handlers[nameof(Arg_NotGenericMethodDefinition)].Invoke();
            }
        }

        public static String Arg_NotGenericParameter
        {
            get
            {
                return Handlers[nameof(Arg_NotGenericParameter)].Invoke();
            }
        }

        public static String Arg_NotGenericTypeDefinition
        {
            get
            {
                return Handlers[nameof(Arg_NotGenericTypeDefinition)].Invoke();
            }
        }

        public static String Arg_NotImplementedException
        {
            get
            {
                return Handlers[nameof(Arg_NotImplementedException)].Invoke();
            }
        }

        public static String Arg_NotSupportedException
        {
            get
            {
                return Handlers[nameof(Arg_NotSupportedException)].Invoke();
            }
        }

        public static String Arg_NullReferenceException
        {
            get
            {
                return Handlers[nameof(Arg_NullReferenceException)].Invoke();
            }
        }

        public static String Arg_ObjObjEx
        {
            get
            {
                return Handlers[nameof(Arg_ObjObjEx)].Invoke();
            }
        }

        public static String Arg_OleAutDateInvalid
        {
            get
            {
                return Handlers[nameof(Arg_OleAutDateInvalid)].Invoke();
            }
        }

        public static String Arg_OleAutDateScale
        {
            get
            {
                return Handlers[nameof(Arg_OleAutDateScale)].Invoke();
            }
        }

        public static String Arg_OverflowException
        {
            get
            {
                return Handlers[nameof(Arg_OverflowException)].Invoke();
            }
        }

        public static String Arg_ParamName_Name
        {
            get
            {
                return Handlers[nameof(Arg_ParamName_Name)].Invoke();
            }
        }

        public static String Arg_ParmArraySize
        {
            get
            {
                return Handlers[nameof(Arg_ParmArraySize)].Invoke();
            }
        }

        public static String Arg_ParmCnt
        {
            get
            {
                return Handlers[nameof(Arg_ParmCnt)].Invoke();
            }
        }

        public static String Arg_PathEmpty
        {
            get
            {
                return Handlers[nameof(Arg_PathEmpty)].Invoke();
            }
        }

        public static String Arg_PlatformNotSupported
        {
            get
            {
                return Handlers[nameof(Arg_PlatformNotSupported)].Invoke();
            }
        }

        public static String Arg_PropSetGet
        {
            get
            {
                return Handlers[nameof(Arg_PropSetGet)].Invoke();
            }
        }

        public static String Arg_PropSetInvoke
        {
            get
            {
                return Handlers[nameof(Arg_PropSetInvoke)].Invoke();
            }
        }

        public static String Arg_RankException
        {
            get
            {
                return Handlers[nameof(Arg_RankException)].Invoke();
            }
        }

        public static String Arg_RankIndices
        {
            get
            {
                return Handlers[nameof(Arg_RankIndices)].Invoke();
            }
        }

        public static String Arg_RankMultiDimNotSupported
        {
            get
            {
                return Handlers[nameof(Arg_RankMultiDimNotSupported)].Invoke();
            }
        }

        public static String Arg_RanksAndBounds
        {
            get
            {
                return Handlers[nameof(Arg_RanksAndBounds)].Invoke();
            }
        }

        public static String Arg_RegGetOverflowBug
        {
            get
            {
                return Handlers[nameof(Arg_RegGetOverflowBug)].Invoke();
            }
        }

        public static String Arg_RegKeyNotFound
        {
            get
            {
                return Handlers[nameof(Arg_RegKeyNotFound)].Invoke();
            }
        }

        public static String Arg_RegSubKeyValueAbsent
        {
            get
            {
                return Handlers[nameof(Arg_RegSubKeyValueAbsent)].Invoke();
            }
        }

        public static String Arg_RegValStrLenBug
        {
            get
            {
                return Handlers[nameof(Arg_RegValStrLenBug)].Invoke();
            }
        }

        public static String Arg_ResMgrNotResSet
        {
            get
            {
                return Handlers[nameof(Arg_ResMgrNotResSet)].Invoke();
            }
        }

        public static String Arg_ResourceFileUnsupportedVersion
        {
            get
            {
                return Handlers[nameof(Arg_ResourceFileUnsupportedVersion)].Invoke();
            }
        }

        public static String Arg_ResourceNameNotExist
        {
            get
            {
                return Handlers[nameof(Arg_ResourceNameNotExist)].Invoke();
            }
        }

        public static String Arg_SafeArrayRankMismatchException
        {
            get
            {
                return Handlers[nameof(Arg_SafeArrayRankMismatchException)].Invoke();
            }
        }

        public static String Arg_SafeArrayTypeMismatchException
        {
            get
            {
                return Handlers[nameof(Arg_SafeArrayTypeMismatchException)].Invoke();
            }
        }

        public static String Arg_SecurityException
        {
            get
            {
                return Handlers[nameof(Arg_SecurityException)].Invoke();
            }
        }

        public static String SerializationException
        {
            get
            {
                return Handlers[nameof(SerializationException)].Invoke();
            }
        }

        public static String Arg_SetMethNotFnd
        {
            get
            {
                return Handlers[nameof(Arg_SetMethNotFnd)].Invoke();
            }
        }

        public static String Arg_StackOverflowException
        {
            get
            {
                return Handlers[nameof(Arg_StackOverflowException)].Invoke();
            }
        }

        public static String Arg_SurrogatesNotAllowedAsSingleChar
        {
            get
            {
                return Handlers[nameof(Arg_SurrogatesNotAllowedAsSingleChar)].Invoke();
            }
        }

        public static String Arg_SynchronizationLockException
        {
            get
            {
                return Handlers[nameof(Arg_SynchronizationLockException)].Invoke();
            }
        }

        public static String Arg_SystemException
        {
            get
            {
                return Handlers[nameof(Arg_SystemException)].Invoke();
            }
        }

        public static String Arg_TargetInvocationException
        {
            get
            {
                return Handlers[nameof(Arg_TargetInvocationException)].Invoke();
            }
        }

        public static String Arg_TargetParameterCountException
        {
            get
            {
                return Handlers[nameof(Arg_TargetParameterCountException)].Invoke();
            }
        }

        public static String Arg_ThreadStartException
        {
            get
            {
                return Handlers[nameof(Arg_ThreadStartException)].Invoke();
            }
        }

        public static String Arg_ThreadStateException
        {
            get
            {
                return Handlers[nameof(Arg_ThreadStateException)].Invoke();
            }
        }

        public static String Arg_TimeoutException
        {
            get
            {
                return Handlers[nameof(Arg_TimeoutException)].Invoke();
            }
        }

        public static String Arg_TypeAccessException
        {
            get
            {
                return Handlers[nameof(Arg_TypeAccessException)].Invoke();
            }
        }

        public static String Arg_TypedReference_Null
        {
            get
            {
                return Handlers[nameof(Arg_TypedReference_Null)].Invoke();
            }
        }

        public static String Arg_TypeLoadException
        {
            get
            {
                return Handlers[nameof(Arg_TypeLoadException)].Invoke();
            }
        }

        public static String Arg_TypeLoadNullStr
        {
            get
            {
                return Handlers[nameof(Arg_TypeLoadNullStr)].Invoke();
            }
        }

        public static String Arg_TypeRefPrimitve
        {
            get
            {
                return Handlers[nameof(Arg_TypeRefPrimitve)].Invoke();
            }
        }

        public static String Arg_TypeUnloadedException
        {
            get
            {
                return Handlers[nameof(Arg_TypeUnloadedException)].Invoke();
            }
        }

        public static String Arg_UnauthorizedAccessException
        {
            get
            {
                return Handlers[nameof(Arg_UnauthorizedAccessException)].Invoke();
            }
        }

        public static String Arg_UnboundGenField
        {
            get
            {
                return Handlers[nameof(Arg_UnboundGenField)].Invoke();
            }
        }

        public static String Arg_UnboundGenParam
        {
            get
            {
                return Handlers[nameof(Arg_UnboundGenParam)].Invoke();
            }
        }

        public static String Arg_UnknownTypeCode
        {
            get
            {
                return Handlers[nameof(Arg_UnknownTypeCode)].Invoke();
            }
        }

        public static String Arg_VarMissNull
        {
            get
            {
                return Handlers[nameof(Arg_VarMissNull)].Invoke();
            }
        }

        public static String Arg_VersionString
        {
            get
            {
                return Handlers[nameof(Arg_VersionString)].Invoke();
            }
        }

        public static String Arg_WrongAsyncResult
        {
            get
            {
                return Handlers[nameof(Arg_WrongAsyncResult)].Invoke();
            }
        }

        public static String Arg_WrongType
        {
            get
            {
                return Handlers[nameof(Arg_WrongType)].Invoke();
            }
        }

        public static String Argument_AbsolutePathRequired
        {
            get
            {
                return Handlers[nameof(Argument_AbsolutePathRequired)].Invoke();
            }
        }

        public static String Argument_AddingDuplicate
        {
            get
            {
                return Handlers[nameof(Argument_AddingDuplicate)].Invoke();
            }
        }

        public static String Argument_AddingDuplicate__
        {
            get
            {
                return Handlers[nameof(Argument_AddingDuplicate__)].Invoke();
            }
        }

        public static String Argument_AddingDuplicateWithKey
        {
            get
            {
                return Handlers[nameof(Argument_AddingDuplicateWithKey)].Invoke();
            }
        }

        public static String Argument_AdjustmentRulesNoNulls
        {
            get
            {
                return Handlers[nameof(Argument_AdjustmentRulesNoNulls)].Invoke();
            }
        }

        public static String Argument_AdjustmentRulesOutOfOrder
        {
            get
            {
                return Handlers[nameof(Argument_AdjustmentRulesOutOfOrder)].Invoke();
            }
        }

        public static String Argument_AlreadyBoundOrSyncHandle
        {
            get
            {
                return Handlers[nameof(Argument_AlreadyBoundOrSyncHandle)].Invoke();
            }
        }

        public static String Argument_ArrayGetInterfaceMap
        {
            get
            {
                return Handlers[nameof(Argument_ArrayGetInterfaceMap)].Invoke();
            }
        }

        public static String Argument_ArraysInvalid
        {
            get
            {
                return Handlers[nameof(Argument_ArraysInvalid)].Invoke();
            }
        }

        public static String Argument_AttributeNamesMustBeUnique
        {
            get
            {
                return Handlers[nameof(Argument_AttributeNamesMustBeUnique)].Invoke();
            }
        }

        public static String Argument_BadConstructor
        {
            get
            {
                return Handlers[nameof(Argument_BadConstructor)].Invoke();
            }
        }

        public static String Argument_BadConstructorCallConv
        {
            get
            {
                return Handlers[nameof(Argument_BadConstructorCallConv)].Invoke();
            }
        }

        public static String Argument_BadExceptionCodeGen
        {
            get
            {
                return Handlers[nameof(Argument_BadExceptionCodeGen)].Invoke();
            }
        }

        public static String Argument_BadFieldForConstructorBuilder
        {
            get
            {
                return Handlers[nameof(Argument_BadFieldForConstructorBuilder)].Invoke();
            }
        }

        public static String Argument_BadFieldSig
        {
            get
            {
                return Handlers[nameof(Argument_BadFieldSig)].Invoke();
            }
        }

        public static String Argument_BadFieldType
        {
            get
            {
                return Handlers[nameof(Argument_BadFieldType)].Invoke();
            }
        }

        public static String Argument_BadFormatSpecifier
        {
            get
            {
                return Handlers[nameof(Argument_BadFormatSpecifier)].Invoke();
            }
        }

        public static String Argument_BadImageFormatExceptionResolve
        {
            get
            {
                return Handlers[nameof(Argument_BadImageFormatExceptionResolve)].Invoke();
            }
        }

        public static String Argument_BadLabel
        {
            get
            {
                return Handlers[nameof(Argument_BadLabel)].Invoke();
            }
        }

        public static String Argument_BadLabelContent
        {
            get
            {
                return Handlers[nameof(Argument_BadLabelContent)].Invoke();
            }
        }

        public static String Argument_BadNestedTypeFlags
        {
            get
            {
                return Handlers[nameof(Argument_BadNestedTypeFlags)].Invoke();
            }
        }

        public static String Argument_BadParameterCountsForConstructor
        {
            get
            {
                return Handlers[nameof(Argument_BadParameterCountsForConstructor)].Invoke();
            }
        }

        public static String Argument_BadParameterTypeForCAB
        {
            get
            {
                return Handlers[nameof(Argument_BadParameterTypeForCAB)].Invoke();
            }
        }

        public static String Argument_BadPropertyForConstructorBuilder
        {
            get
            {
                return Handlers[nameof(Argument_BadPropertyForConstructorBuilder)].Invoke();
            }
        }

        public static String Argument_BadSigFormat
        {
            get
            {
                return Handlers[nameof(Argument_BadSigFormat)].Invoke();
            }
        }

        public static String Argument_BadSizeForData
        {
            get
            {
                return Handlers[nameof(Argument_BadSizeForData)].Invoke();
            }
        }

        public static String Argument_BadTypeAttrInvalidLayout
        {
            get
            {
                return Handlers[nameof(Argument_BadTypeAttrInvalidLayout)].Invoke();
            }
        }

        public static String Argument_BadTypeAttrNestedVisibilityOnNonNestedType
        {
            get
            {
                return Handlers[nameof(Argument_BadTypeAttrNestedVisibilityOnNonNestedType)].Invoke();
            }
        }

        public static String Argument_BadTypeAttrNonNestedVisibilityNestedType
        {
            get
            {
                return Handlers[nameof(Argument_BadTypeAttrNonNestedVisibilityNestedType)].Invoke();
            }
        }

        public static String Argument_BadTypeAttrReservedBitsSet
        {
            get
            {
                return Handlers[nameof(Argument_BadTypeAttrReservedBitsSet)].Invoke();
            }
        }

        public static String Argument_BadTypeInCustomAttribute
        {
            get
            {
                return Handlers[nameof(Argument_BadTypeInCustomAttribute)].Invoke();
            }
        }

        public static String Argument_CannotGetTypeTokenForByRef
        {
            get
            {
                return Handlers[nameof(Argument_CannotGetTypeTokenForByRef)].Invoke();
            }
        }

        public static String Argument_CannotSetParentToInterface
        {
            get
            {
                return Handlers[nameof(Argument_CannotSetParentToInterface)].Invoke();
            }
        }

        public static String Argument_CodepageNotSupported
        {
            get
            {
                return Handlers[nameof(Argument_CodepageNotSupported)].Invoke();
            }
        }

        public static String Argument_CompareOptionOrdinal
        {
            get
            {
                return Handlers[nameof(Argument_CompareOptionOrdinal)].Invoke();
            }
        }

        public static String Argument_ConflictingDateTimeRoundtripStyles
        {
            get
            {
                return Handlers[nameof(Argument_ConflictingDateTimeRoundtripStyles)].Invoke();
            }
        }

        public static String Argument_ConflictingDateTimeStyles
        {
            get
            {
                return Handlers[nameof(Argument_ConflictingDateTimeStyles)].Invoke();
            }
        }

        public static String Argument_ConstantDoesntMatch
        {
            get
            {
                return Handlers[nameof(Argument_ConstantDoesntMatch)].Invoke();
            }
        }

        public static String Argument_ConstantNotSupported
        {
            get
            {
                return Handlers[nameof(Argument_ConstantNotSupported)].Invoke();
            }
        }

        public static String Argument_ConstantNull
        {
            get
            {
                return Handlers[nameof(Argument_ConstantNull)].Invoke();
            }
        }

        public static String Argument_ConstructorNeedGenericDeclaringType
        {
            get
            {
                return Handlers[nameof(Argument_ConstructorNeedGenericDeclaringType)].Invoke();
            }
        }

        public static String Argument_ConversionOverflow
        {
            get
            {
                return Handlers[nameof(Argument_ConversionOverflow)].Invoke();
            }
        }

        public static String Argument_ConvertMismatch
        {
            get
            {
                return Handlers[nameof(Argument_ConvertMismatch)].Invoke();
            }
        }

        public static String Argument_CultureIetfNotSupported
        {
            get
            {
                return Handlers[nameof(Argument_CultureIetfNotSupported)].Invoke();
            }
        }

        public static String Argument_CultureInvalidIdentifier
        {
            get
            {
                return Handlers[nameof(Argument_CultureInvalidIdentifier)].Invoke();
            }
        }

        public static String Argument_CultureIsNeutral
        {
            get
            {
                return Handlers[nameof(Argument_CultureIsNeutral)].Invoke();
            }
        }

        public static String Argument_CultureNotSupported
        {
            get
            {
                return Handlers[nameof(Argument_CultureNotSupported)].Invoke();
            }
        }

        public static String Argument_CustomAssemblyLoadContextRequestedNameMismatch
        {
            get
            {
                return Handlers[nameof(Argument_CustomAssemblyLoadContextRequestedNameMismatch)].Invoke();
            }
        }

        public static String Argument_CustomCultureCannotBePassedByNumber
        {
            get
            {
                return Handlers[nameof(Argument_CustomCultureCannotBePassedByNumber)].Invoke();
            }
        }

        public static String Argument_DateTimeBadBinaryData
        {
            get
            {
                return Handlers[nameof(Argument_DateTimeBadBinaryData)].Invoke();
            }
        }

        public static String Argument_DateTimeHasTicks
        {
            get
            {
                return Handlers[nameof(Argument_DateTimeHasTicks)].Invoke();
            }
        }

        public static String Argument_DateTimeHasTimeOfDay
        {
            get
            {
                return Handlers[nameof(Argument_DateTimeHasTimeOfDay)].Invoke();
            }
        }

        public static String Argument_DateTimeIsInvalid
        {
            get
            {
                return Handlers[nameof(Argument_DateTimeIsInvalid)].Invoke();
            }
        }

        public static String Argument_DateTimeIsNotAmbiguous
        {
            get
            {
                return Handlers[nameof(Argument_DateTimeIsNotAmbiguous)].Invoke();
            }
        }

        public static String Argument_DateTimeKindMustBeUnspecified
        {
            get
            {
                return Handlers[nameof(Argument_DateTimeKindMustBeUnspecified)].Invoke();
            }
        }

        public static String Argument_DateTimeKindMustBeUnspecifiedOrUtc
        {
            get
            {
                return Handlers[nameof(Argument_DateTimeKindMustBeUnspecifiedOrUtc)].Invoke();
            }
        }

        public static String Argument_DateTimeOffsetInvalidDateTimeStyles
        {
            get
            {
                return Handlers[nameof(Argument_DateTimeOffsetInvalidDateTimeStyles)].Invoke();
            }
        }

        public static String Argument_DateTimeOffsetIsNotAmbiguous
        {
            get
            {
                return Handlers[nameof(Argument_DateTimeOffsetIsNotAmbiguous)].Invoke();
            }
        }

        public static String Argument_DestinationTooShort
        {
            get
            {
                return Handlers[nameof(Argument_DestinationTooShort)].Invoke();
            }
        }

        public static String Argument_DuplicateTypeName
        {
            get
            {
                return Handlers[nameof(Argument_DuplicateTypeName)].Invoke();
            }
        }

        public static String Argument_EmitWriteLineType
        {
            get
            {
                return Handlers[nameof(Argument_EmitWriteLineType)].Invoke();
            }
        }

        public static String Argument_EmptyDecString
        {
            get
            {
                return Handlers[nameof(Argument_EmptyDecString)].Invoke();
            }
        }

        public static String Argument_EmptyName
        {
            get
            {
                return Handlers[nameof(Argument_EmptyName)].Invoke();
            }
        }

        public static String Argument_EmptyPath
        {
            get
            {
                return Handlers[nameof(Argument_EmptyPath)].Invoke();
            }
        }

        public static String Argument_EmptyWaithandleArray
        {
            get
            {
                return Handlers[nameof(Argument_EmptyWaithandleArray)].Invoke();
            }
        }

        public static String Argument_EncoderFallbackNotEmpty
        {
            get
            {
                return Handlers[nameof(Argument_EncoderFallbackNotEmpty)].Invoke();
            }
        }

        public static String Argument_EncodingConversionOverflowBytes
        {
            get
            {
                return Handlers[nameof(Argument_EncodingConversionOverflowBytes)].Invoke();
            }
        }

        public static String Argument_EncodingConversionOverflowChars
        {
            get
            {
                return Handlers[nameof(Argument_EncodingConversionOverflowChars)].Invoke();
            }
        }

        public static String Argument_EncodingNotSupported
        {
            get
            {
                return Handlers[nameof(Argument_EncodingNotSupported)].Invoke();
            }
        }

        public static String Argument_EnumTypeDoesNotMatch
        {
            get
            {
                return Handlers[nameof(Argument_EnumTypeDoesNotMatch)].Invoke();
            }
        }

        public static String Argument_FallbackBufferNotEmpty
        {
            get
            {
                return Handlers[nameof(Argument_FallbackBufferNotEmpty)].Invoke();
            }
        }

        public static String Argument_FieldDeclaringTypeGeneric
        {
            get
            {
                return Handlers[nameof(Argument_FieldDeclaringTypeGeneric)].Invoke();
            }
        }

        public static String Argument_FieldNeedGenericDeclaringType
        {
            get
            {
                return Handlers[nameof(Argument_FieldNeedGenericDeclaringType)].Invoke();
            }
        }

        public static String Argument_GenConstraintViolation
        {
            get
            {
                return Handlers[nameof(Argument_GenConstraintViolation)].Invoke();
            }
        }

        public static String Argument_GenericArgsCount
        {
            get
            {
                return Handlers[nameof(Argument_GenericArgsCount)].Invoke();
            }
        }

        public static String Argument_GenericsInvalid
        {
            get
            {
                return Handlers[nameof(Argument_GenericsInvalid)].Invoke();
            }
        }

        public static String Argument_GlobalFunctionHasToBeStatic
        {
            get
            {
                return Handlers[nameof(Argument_GlobalFunctionHasToBeStatic)].Invoke();
            }
        }

        public static String Argument_HasToBeArrayClass
        {
            get
            {
                return Handlers[nameof(Argument_HasToBeArrayClass)].Invoke();
            }
        }

        public static String Argument_IdnBadBidi
        {
            get
            {
                return Handlers[nameof(Argument_IdnBadBidi)].Invoke();
            }
        }

        public static String Argument_IdnBadLabelSize
        {
            get
            {
                return Handlers[nameof(Argument_IdnBadLabelSize)].Invoke();
            }
        }

        public static String Argument_IdnBadNameSize
        {
            get
            {
                return Handlers[nameof(Argument_IdnBadNameSize)].Invoke();
            }
        }

        public static String Argument_IdnBadPunycode
        {
            get
            {
                return Handlers[nameof(Argument_IdnBadPunycode)].Invoke();
            }
        }

        public static String Argument_IdnBadStd3
        {
            get
            {
                return Handlers[nameof(Argument_IdnBadStd3)].Invoke();
            }
        }

        public static String Argument_IdnIllegalName
        {
            get
            {
                return Handlers[nameof(Argument_IdnIllegalName)].Invoke();
            }
        }

        public static String Argument_IllegalEnvVarName
        {
            get
            {
                return Handlers[nameof(Argument_IllegalEnvVarName)].Invoke();
            }
        }

        public static String Argument_IllegalName
        {
            get
            {
                return Handlers[nameof(Argument_IllegalName)].Invoke();
            }
        }

        public static String Argument_ImplementIComparable
        {
            get
            {
                return Handlers[nameof(Argument_ImplementIComparable)].Invoke();
            }
        }

        public static String Argument_InvalidAppendMode
        {
            get
            {
                return Handlers[nameof(Argument_InvalidAppendMode)].Invoke();
            }
        }

        public static String Argument_InvalidArgumentForComparison
        {
            get
            {
                return Handlers[nameof(Argument_InvalidArgumentForComparison)].Invoke();
            }
        }

        public static String Argument_InvalidArrayLength
        {
            get
            {
                return Handlers[nameof(Argument_InvalidArrayLength)].Invoke();
            }
        }

        public static String Argument_InvalidArrayType
        {
            get
            {
                return Handlers[nameof(Argument_InvalidArrayType)].Invoke();
            }
        }

        public static String Argument_InvalidCalendar
        {
            get
            {
                return Handlers[nameof(Argument_InvalidCalendar)].Invoke();
            }
        }

        public static String Argument_InvalidCharSequence
        {
            get
            {
                return Handlers[nameof(Argument_InvalidCharSequence)].Invoke();
            }
        }

        public static String Argument_InvalidCharSequenceNoIndex
        {
            get
            {
                return Handlers[nameof(Argument_InvalidCharSequenceNoIndex)].Invoke();
            }
        }

        public static String Argument_InvalidCodePageBytesIndex
        {
            get
            {
                return Handlers[nameof(Argument_InvalidCodePageBytesIndex)].Invoke();
            }
        }

        public static String Argument_InvalidCodePageConversionIndex
        {
            get
            {
                return Handlers[nameof(Argument_InvalidCodePageConversionIndex)].Invoke();
            }
        }

        public static String Argument_InvalidConstructorDeclaringType
        {
            get
            {
                return Handlers[nameof(Argument_InvalidConstructorDeclaringType)].Invoke();
            }
        }

        public static String Argument_InvalidConstructorInfo
        {
            get
            {
                return Handlers[nameof(Argument_InvalidConstructorInfo)].Invoke();
            }
        }

        public static String Argument_InvalidCultureName
        {
            get
            {
                return Handlers[nameof(Argument_InvalidCultureName)].Invoke();
            }
        }

        public static String Argument_InvalidPredefinedCultureName
        {
            get
            {
                return Handlers[nameof(Argument_InvalidPredefinedCultureName)].Invoke();
            }
        }

        public static String Argument_InvalidDateTimeKind
        {
            get
            {
                return Handlers[nameof(Argument_InvalidDateTimeKind)].Invoke();
            }
        }

        public static String Argument_InvalidDateTimeStyles
        {
            get
            {
                return Handlers[nameof(Argument_InvalidDateTimeStyles)].Invoke();
            }
        }

        public static String Argument_InvalidDigitSubstitution
        {
            get
            {
                return Handlers[nameof(Argument_InvalidDigitSubstitution)].Invoke();
            }
        }

        public static String Argument_InvalidElementName
        {
            get
            {
                return Handlers[nameof(Argument_InvalidElementName)].Invoke();
            }
        }

        public static String Argument_InvalidElementTag
        {
            get
            {
                return Handlers[nameof(Argument_InvalidElementTag)].Invoke();
            }
        }

        public static String Argument_InvalidElementText
        {
            get
            {
                return Handlers[nameof(Argument_InvalidElementText)].Invoke();
            }
        }

        public static String Argument_InvalidElementValue
        {
            get
            {
                return Handlers[nameof(Argument_InvalidElementValue)].Invoke();
            }
        }

        public static String Argument_InvalidEnum
        {
            get
            {
                return Handlers[nameof(Argument_InvalidEnum)].Invoke();
            }
        }

        public static String Argument_InvalidEnumValue
        {
            get
            {
                return Handlers[nameof(Argument_InvalidEnumValue)].Invoke();
            }
        }

        public static String Argument_InvalidFieldDeclaringType
        {
            get
            {
                return Handlers[nameof(Argument_InvalidFieldDeclaringType)].Invoke();
            }
        }

        public static String Argument_InvalidFileModeAndAccessCombo
        {
            get
            {
                return Handlers[nameof(Argument_InvalidFileModeAndAccessCombo)].Invoke();
            }
        }

        public static String Argument_InvalidFlag
        {
            get
            {
                return Handlers[nameof(Argument_InvalidFlag)].Invoke();
            }
        }

        public static String Argument_InvalidGenericInstArray
        {
            get
            {
                return Handlers[nameof(Argument_InvalidGenericInstArray)].Invoke();
            }
        }

        public static String Argument_InvalidGroupSize
        {
            get
            {
                return Handlers[nameof(Argument_InvalidGroupSize)].Invoke();
            }
        }

        public static String Argument_InvalidHandle
        {
            get
            {
                return Handlers[nameof(Argument_InvalidHandle)].Invoke();
            }
        }

        public static String Argument_InvalidHighSurrogate
        {
            get
            {
                return Handlers[nameof(Argument_InvalidHighSurrogate)].Invoke();
            }
        }

        public static String Argument_InvalidId
        {
            get
            {
                return Handlers[nameof(Argument_InvalidId)].Invoke();
            }
        }

        public static String Argument_InvalidKindOfTypeForCA
        {
            get
            {
                return Handlers[nameof(Argument_InvalidKindOfTypeForCA)].Invoke();
            }
        }

        public static String Argument_InvalidLabel
        {
            get
            {
                return Handlers[nameof(Argument_InvalidLabel)].Invoke();
            }
        }

        public static String Argument_InvalidLowSurrogate
        {
            get
            {
                return Handlers[nameof(Argument_InvalidLowSurrogate)].Invoke();
            }
        }

        public static String Argument_InvalidMemberForNamedArgument
        {
            get
            {
                return Handlers[nameof(Argument_InvalidMemberForNamedArgument)].Invoke();
            }
        }

        public static String Argument_InvalidMethodDeclaringType
        {
            get
            {
                return Handlers[nameof(Argument_InvalidMethodDeclaringType)].Invoke();
            }
        }

        public static String Argument_InvalidName
        {
            get
            {
                return Handlers[nameof(Argument_InvalidName)].Invoke();
            }
        }

        public static String Argument_InvalidNativeDigitCount
        {
            get
            {
                return Handlers[nameof(Argument_InvalidNativeDigitCount)].Invoke();
            }
        }

        public static String Argument_InvalidNativeDigitValue
        {
            get
            {
                return Handlers[nameof(Argument_InvalidNativeDigitValue)].Invoke();
            }
        }

        public static String Argument_InvalidNeutralRegionName
        {
            get
            {
                return Handlers[nameof(Argument_InvalidNeutralRegionName)].Invoke();
            }
        }

        public static String Argument_InvalidNormalizationForm
        {
            get
            {
                return Handlers[nameof(Argument_InvalidNormalizationForm)].Invoke();
            }
        }

        public static String Argument_InvalidNumberStyles
        {
            get
            {
                return Handlers[nameof(Argument_InvalidNumberStyles)].Invoke();
            }
        }

        public static String Argument_InvalidOffLen
        {
            get
            {
                return Handlers[nameof(Argument_InvalidOffLen)].Invoke();
            }
        }

        public static String Argument_InvalidOpCodeOnDynamicMethod
        {
            get
            {
                return Handlers[nameof(Argument_InvalidOpCodeOnDynamicMethod)].Invoke();
            }
        }

        public static String Argument_InvalidParameterInfo
        {
            get
            {
                return Handlers[nameof(Argument_InvalidParameterInfo)].Invoke();
            }
        }

        public static String Argument_InvalidParamInfo
        {
            get
            {
                return Handlers[nameof(Argument_InvalidParamInfo)].Invoke();
            }
        }

        public static String Argument_InvalidPathChars
        {
            get
            {
                return Handlers[nameof(Argument_InvalidPathChars)].Invoke();
            }
        }

        public static String Argument_InvalidResourceCultureName
        {
            get
            {
                return Handlers[nameof(Argument_InvalidResourceCultureName)].Invoke();
            }
        }

        public static String Argument_InvalidSafeBufferOffLen
        {
            get
            {
                return Handlers[nameof(Argument_InvalidSafeBufferOffLen)].Invoke();
            }
        }

        public static String Argument_InvalidSeekOrigin
        {
            get
            {
                return Handlers[nameof(Argument_InvalidSeekOrigin)].Invoke();
            }
        }

        public static String Argument_InvalidSerializedString
        {
            get
            {
                return Handlers[nameof(Argument_InvalidSerializedString)].Invoke();
            }
        }

        public static String Argument_InvalidStartupHookSignature
        {
            get
            {
                return Handlers[nameof(Argument_InvalidStartupHookSignature)].Invoke();
            }
        }

        public static String Argument_InvalidTimeSpanStyles
        {
            get
            {
                return Handlers[nameof(Argument_InvalidTimeSpanStyles)].Invoke();
            }
        }

        public static String Argument_InvalidToken
        {
            get
            {
                return Handlers[nameof(Argument_InvalidToken)].Invoke();
            }
        }

        public static String Argument_InvalidTypeForCA
        {
            get
            {
                return Handlers[nameof(Argument_InvalidTypeForCA)].Invoke();
            }
        }

        public static String Argument_InvalidTypeForDynamicMethod
        {
            get
            {
                return Handlers[nameof(Argument_InvalidTypeForDynamicMethod)].Invoke();
            }
        }

        public static String Argument_InvalidTypeName
        {
            get
            {
                return Handlers[nameof(Argument_InvalidTypeName)].Invoke();
            }
        }

        public static String Argument_InvalidTypeWithPointersNotSupported
        {
            get
            {
                return Handlers[nameof(Argument_InvalidTypeWithPointersNotSupported)].Invoke();
            }
        }

        public static String Argument_InvalidUnity
        {
            get
            {
                return Handlers[nameof(Argument_InvalidUnity)].Invoke();
            }
        }

        public static String Argument_LargeInteger
        {
            get
            {
                return Handlers[nameof(Argument_LargeInteger)].Invoke();
            }
        }

        public static String Argument_LongEnvVarValue
        {
            get
            {
                return Handlers[nameof(Argument_LongEnvVarValue)].Invoke();
            }
        }

        public static String Argument_MethodDeclaringTypeGeneric
        {
            get
            {
                return Handlers[nameof(Argument_MethodDeclaringTypeGeneric)].Invoke();
            }
        }

        public static String Argument_MethodDeclaringTypeGenericLcg
        {
            get
            {
                return Handlers[nameof(Argument_MethodDeclaringTypeGenericLcg)].Invoke();
            }
        }

        public static String Argument_MethodNeedGenericDeclaringType
        {
            get
            {
                return Handlers[nameof(Argument_MethodNeedGenericDeclaringType)].Invoke();
            }
        }

        public static String Argument_MinMaxValue
        {
            get
            {
                return Handlers[nameof(Argument_MinMaxValue)].Invoke();
            }
        }

        public static String Argument_MismatchedArrays
        {
            get
            {
                return Handlers[nameof(Argument_MismatchedArrays)].Invoke();
            }
        }

        public static String Argument_MissingDefaultConstructor
        {
            get
            {
                return Handlers[nameof(Argument_MissingDefaultConstructor)].Invoke();
            }
        }

        public static String Argument_MustBeFalse
        {
            get
            {
                return Handlers[nameof(Argument_MustBeFalse)].Invoke();
            }
        }

        public static String Argument_MustBeRuntimeAssembly
        {
            get
            {
                return Handlers[nameof(Argument_MustBeRuntimeAssembly)].Invoke();
            }
        }

        public static String Argument_MustBeRuntimeFieldInfo
        {
            get
            {
                return Handlers[nameof(Argument_MustBeRuntimeFieldInfo)].Invoke();
            }
        }

        public static String Argument_MustBeRuntimeMethodInfo
        {
            get
            {
                return Handlers[nameof(Argument_MustBeRuntimeMethodInfo)].Invoke();
            }
        }

        public static String Argument_MustBeRuntimeReflectionObject
        {
            get
            {
                return Handlers[nameof(Argument_MustBeRuntimeReflectionObject)].Invoke();
            }
        }

        public static String Argument_MustBeRuntimeType
        {
            get
            {
                return Handlers[nameof(Argument_MustBeRuntimeType)].Invoke();
            }
        }

        public static String Argument_MustBeTypeBuilder
        {
            get
            {
                return Handlers[nameof(Argument_MustBeTypeBuilder)].Invoke();
            }
        }

        public static String Argument_MustHaveAttributeBaseClass
        {
            get
            {
                return Handlers[nameof(Argument_MustHaveAttributeBaseClass)].Invoke();
            }
        }

        public static String Argument_NativeOverlappedAlreadyFree
        {
            get
            {
                return Handlers[nameof(Argument_NativeOverlappedAlreadyFree)].Invoke();
            }
        }

        public static String Argument_NativeOverlappedWrongBoundHandle
        {
            get
            {
                return Handlers[nameof(Argument_NativeOverlappedWrongBoundHandle)].Invoke();
            }
        }

        public static String Argument_NeedGenericMethodDefinition
        {
            get
            {
                return Handlers[nameof(Argument_NeedGenericMethodDefinition)].Invoke();
            }
        }

        public static String Argument_NeedNonGenericType
        {
            get
            {
                return Handlers[nameof(Argument_NeedNonGenericType)].Invoke();
            }
        }

        public static String Argument_NeedStructWithNoRefs
        {
            get
            {
                return Handlers[nameof(Argument_NeedStructWithNoRefs)].Invoke();
            }
        }

        public static String Argument_NeverValidGenericArgument
        {
            get
            {
                return Handlers[nameof(Argument_NeverValidGenericArgument)].Invoke();
            }
        }

        public static String Argument_NoEra
        {
            get
            {
                return Handlers[nameof(Argument_NoEra)].Invoke();
            }
        }

        public static String Argument_NoRegionInvariantCulture
        {
            get
            {
                return Handlers[nameof(Argument_NoRegionInvariantCulture)].Invoke();
            }
        }

        public static String Argument_NotAWritableProperty
        {
            get
            {
                return Handlers[nameof(Argument_NotAWritableProperty)].Invoke();
            }
        }

        public static String Argument_NotEnoughBytesToRead
        {
            get
            {
                return Handlers[nameof(Argument_NotEnoughBytesToRead)].Invoke();
            }
        }

        public static String Argument_NotEnoughBytesToWrite
        {
            get
            {
                return Handlers[nameof(Argument_NotEnoughBytesToWrite)].Invoke();
            }
        }

        public static String Argument_NotEnoughGenArguments
        {
            get
            {
                return Handlers[nameof(Argument_NotEnoughGenArguments)].Invoke();
            }
        }

        public static String Argument_NotExceptionType
        {
            get
            {
                return Handlers[nameof(Argument_NotExceptionType)].Invoke();
            }
        }

        public static String Argument_NotInExceptionBlock
        {
            get
            {
                return Handlers[nameof(Argument_NotInExceptionBlock)].Invoke();
            }
        }

        public static String Argument_NotMethodCallOpcode
        {
            get
            {
                return Handlers[nameof(Argument_NotMethodCallOpcode)].Invoke();
            }
        }

        public static String Argument_NotSerializable
        {
            get
            {
                return Handlers[nameof(Argument_NotSerializable)].Invoke();
            }
        }

        public static String Argument_ObjNotComObject
        {
            get
            {
                return Handlers[nameof(Argument_ObjNotComObject)].Invoke();
            }
        }

        public static String Argument_OffsetAndCapacityOutOfBounds
        {
            get
            {
                return Handlers[nameof(Argument_OffsetAndCapacityOutOfBounds)].Invoke();
            }
        }

        public static String Argument_OffsetLocalMismatch
        {
            get
            {
                return Handlers[nameof(Argument_OffsetLocalMismatch)].Invoke();
            }
        }

        public static String Argument_OffsetOfFieldNotFound
        {
            get
            {
                return Handlers[nameof(Argument_OffsetOfFieldNotFound)].Invoke();
            }
        }

        public static String Argument_OffsetOutOfRange
        {
            get
            {
                return Handlers[nameof(Argument_OffsetOutOfRange)].Invoke();
            }
        }

        public static String Argument_OffsetPrecision
        {
            get
            {
                return Handlers[nameof(Argument_OffsetPrecision)].Invoke();
            }
        }

        public static String Argument_OffsetUtcMismatch
        {
            get
            {
                return Handlers[nameof(Argument_OffsetUtcMismatch)].Invoke();
            }
        }

        public static String Argument_OneOfCulturesNotSupported
        {
            get
            {
                return Handlers[nameof(Argument_OneOfCulturesNotSupported)].Invoke();
            }
        }

        public static String Argument_OnlyMscorlib
        {
            get
            {
                return Handlers[nameof(Argument_OnlyMscorlib)].Invoke();
            }
        }

        public static String Argument_OutOfOrderDateTimes
        {
            get
            {
                return Handlers[nameof(Argument_OutOfOrderDateTimes)].Invoke();
            }
        }

        public static String Argument_PathEmpty
        {
            get
            {
                return Handlers[nameof(Argument_PathEmpty)].Invoke();
            }
        }

        public static String Argument_PreAllocatedAlreadyAllocated
        {
            get
            {
                return Handlers[nameof(Argument_PreAllocatedAlreadyAllocated)].Invoke();
            }
        }

        public static String Argument_RecursiveFallback
        {
            get
            {
                return Handlers[nameof(Argument_RecursiveFallback)].Invoke();
            }
        }

        public static String Argument_RecursiveFallbackBytes
        {
            get
            {
                return Handlers[nameof(Argument_RecursiveFallbackBytes)].Invoke();
            }
        }

        public static String Argument_RedefinedLabel
        {
            get
            {
                return Handlers[nameof(Argument_RedefinedLabel)].Invoke();
            }
        }

        public static String Argument_ResolveField
        {
            get
            {
                return Handlers[nameof(Argument_ResolveField)].Invoke();
            }
        }

        public static String Argument_ResolveFieldHandle
        {
            get
            {
                return Handlers[nameof(Argument_ResolveFieldHandle)].Invoke();
            }
        }

        public static String Argument_ResolveMember
        {
            get
            {
                return Handlers[nameof(Argument_ResolveMember)].Invoke();
            }
        }

        public static String Argument_ResolveMethod
        {
            get
            {
                return Handlers[nameof(Argument_ResolveMethod)].Invoke();
            }
        }

        public static String Argument_ResolveMethodHandle
        {
            get
            {
                return Handlers[nameof(Argument_ResolveMethodHandle)].Invoke();
            }
        }

        public static String Argument_ResolveModuleType
        {
            get
            {
                return Handlers[nameof(Argument_ResolveModuleType)].Invoke();
            }
        }

        public static String Argument_ResolveString
        {
            get
            {
                return Handlers[nameof(Argument_ResolveString)].Invoke();
            }
        }

        public static String Argument_ResolveType
        {
            get
            {
                return Handlers[nameof(Argument_ResolveType)].Invoke();
            }
        }

        public static String Argument_ResultCalendarRange
        {
            get
            {
                return Handlers[nameof(Argument_ResultCalendarRange)].Invoke();
            }
        }

        public static String Argument_SemaphoreInitialMaximum
        {
            get
            {
                return Handlers[nameof(Argument_SemaphoreInitialMaximum)].Invoke();
            }
        }

        public static String Argument_ShouldNotSpecifyExceptionType
        {
            get
            {
                return Handlers[nameof(Argument_ShouldNotSpecifyExceptionType)].Invoke();
            }
        }

        public static String Argument_ShouldOnlySetVisibilityFlags
        {
            get
            {
                return Handlers[nameof(Argument_ShouldOnlySetVisibilityFlags)].Invoke();
            }
        }

        public static String Argument_SigIsFinalized
        {
            get
            {
                return Handlers[nameof(Argument_SigIsFinalized)].Invoke();
            }
        }

        public static String Argument_StreamNotReadable
        {
            get
            {
                return Handlers[nameof(Argument_StreamNotReadable)].Invoke();
            }
        }

        public static String Argument_StreamNotWritable
        {
            get
            {
                return Handlers[nameof(Argument_StreamNotWritable)].Invoke();
            }
        }

        public static String Argument_StringFirstCharIsZero
        {
            get
            {
                return Handlers[nameof(Argument_StringFirstCharIsZero)].Invoke();
            }
        }

        public static String Argument_StringZeroLength
        {
            get
            {
                return Handlers[nameof(Argument_StringZeroLength)].Invoke();
            }
        }

        public static String Argument_TimeSpanHasSeconds
        {
            get
            {
                return Handlers[nameof(Argument_TimeSpanHasSeconds)].Invoke();
            }
        }

        public static String Argument_ToExclusiveLessThanFromExclusive
        {
            get
            {
                return Handlers[nameof(Argument_ToExclusiveLessThanFromExclusive)].Invoke();
            }
        }

        public static String Argument_TooManyFinallyClause
        {
            get
            {
                return Handlers[nameof(Argument_TooManyFinallyClause)].Invoke();
            }
        }

        public static String Argument_TransitionTimesAreIdentical
        {
            get
            {
                return Handlers[nameof(Argument_TransitionTimesAreIdentical)].Invoke();
            }
        }

        public static String Argument_TypedReferenceInvalidField
        {
            get
            {
                return Handlers[nameof(Argument_TypedReferenceInvalidField)].Invoke();
            }
        }

        public static String Argument_TypeMustNotBeComImport
        {
            get
            {
                return Handlers[nameof(Argument_TypeMustNotBeComImport)].Invoke();
            }
        }

        public static String Argument_TypeNameTooLong
        {
            get
            {
                return Handlers[nameof(Argument_TypeNameTooLong)].Invoke();
            }
        }

        public static String Argument_TypeNotComObject
        {
            get
            {
                return Handlers[nameof(Argument_TypeNotComObject)].Invoke();
            }
        }

        public static String Argument_TypeNotValid
        {
            get
            {
                return Handlers[nameof(Argument_TypeNotValid)].Invoke();
            }
        }

        public static String Argument_UnclosedExceptionBlock
        {
            get
            {
                return Handlers[nameof(Argument_UnclosedExceptionBlock)].Invoke();
            }
        }

        public static String Argument_UnknownUnmanagedCallConv
        {
            get
            {
                return Handlers[nameof(Argument_UnknownUnmanagedCallConv)].Invoke();
            }
        }

        public static String Argument_UnmanagedMemAccessorWrapAround
        {
            get
            {
                return Handlers[nameof(Argument_UnmanagedMemAccessorWrapAround)].Invoke();
            }
        }

        public static String Argument_UnmatchedMethodForLocal
        {
            get
            {
                return Handlers[nameof(Argument_UnmatchedMethodForLocal)].Invoke();
            }
        }

        public static String Argument_UnmatchingSymScope
        {
            get
            {
                return Handlers[nameof(Argument_UnmatchingSymScope)].Invoke();
            }
        }

        public static String Argument_UTCOutOfRange
        {
            get
            {
                return Handlers[nameof(Argument_UTCOutOfRange)].Invoke();
            }
        }

        public static String ArgumentException_BadMethodImplBody
        {
            get
            {
                return Handlers[nameof(ArgumentException_BadMethodImplBody)].Invoke();
            }
        }

        public static String ArgumentException_BufferNotFromPool
        {
            get
            {
                return Handlers[nameof(ArgumentException_BufferNotFromPool)].Invoke();
            }
        }

        public static String ArgumentException_OtherNotArrayOfCorrectLength
        {
            get
            {
                return Handlers[nameof(ArgumentException_OtherNotArrayOfCorrectLength)].Invoke();
            }
        }

        public static String ArgumentException_NotIsomorphic
        {
            get
            {
                return Handlers[nameof(ArgumentException_NotIsomorphic)].Invoke();
            }
        }

        public static String ArgumentException_TupleIncorrectType
        {
            get
            {
                return Handlers[nameof(ArgumentException_TupleIncorrectType)].Invoke();
            }
        }

        public static String ArgumentException_TupleLastArgumentNotATuple
        {
            get
            {
                return Handlers[nameof(ArgumentException_TupleLastArgumentNotATuple)].Invoke();
            }
        }

        public static String ArgumentException_ValueTupleIncorrectType
        {
            get
            {
                return Handlers[nameof(ArgumentException_ValueTupleIncorrectType)].Invoke();
            }
        }

        public static String ArgumentException_ValueTupleLastArgumentNotAValueTuple
        {
            get
            {
                return Handlers[nameof(ArgumentException_ValueTupleLastArgumentNotAValueTuple)].Invoke();
            }
        }

        public static String ArgumentNull_Array
        {
            get
            {
                return Handlers[nameof(ArgumentNull_Array)].Invoke();
            }
        }

        public static String ArgumentNull_ArrayElement
        {
            get
            {
                return Handlers[nameof(ArgumentNull_ArrayElement)].Invoke();
            }
        }

        public static String ArgumentNull_ArrayValue
        {
            get
            {
                return Handlers[nameof(ArgumentNull_ArrayValue)].Invoke();
            }
        }

        public static String ArgumentNull_Assembly
        {
            get
            {
                return Handlers[nameof(ArgumentNull_Assembly)].Invoke();
            }
        }

        public static String ArgumentNull_AssemblyNameName
        {
            get
            {
                return Handlers[nameof(ArgumentNull_AssemblyNameName)].Invoke();
            }
        }

        public static String ArgumentNull_Buffer
        {
            get
            {
                return Handlers[nameof(ArgumentNull_Buffer)].Invoke();
            }
        }

        public static String ArgumentNull_Child
        {
            get
            {
                return Handlers[nameof(ArgumentNull_Child)].Invoke();
            }
        }

        public static String ArgumentNull_Collection
        {
            get
            {
                return Handlers[nameof(ArgumentNull_Collection)].Invoke();
            }
        }

        public static String ArgumentNull_Dictionary
        {
            get
            {
                return Handlers[nameof(ArgumentNull_Dictionary)].Invoke();
            }
        }

        public static String ArgumentNull_Generic
        {
            get
            {
                return Handlers[nameof(ArgumentNull_Generic)].Invoke();
            }
        }

        public static String ArgumentNull_Key
        {
            get
            {
                return Handlers[nameof(ArgumentNull_Key)].Invoke();
            }
        }

        public static String ArgumentNull_Path
        {
            get
            {
                return Handlers[nameof(ArgumentNull_Path)].Invoke();
            }
        }

        public static String ArgumentNull_SafeHandle
        {
            get
            {
                return Handlers[nameof(ArgumentNull_SafeHandle)].Invoke();
            }
        }

        public static String ArgumentNull_Stream
        {
            get
            {
                return Handlers[nameof(ArgumentNull_Stream)].Invoke();
            }
        }

        public static String ArgumentNull_String
        {
            get
            {
                return Handlers[nameof(ArgumentNull_String)].Invoke();
            }
        }

        public static String ArgumentNull_Type
        {
            get
            {
                return Handlers[nameof(ArgumentNull_Type)].Invoke();
            }
        }

        public static String ArgumentNull_Waithandles
        {
            get
            {
                return Handlers[nameof(ArgumentNull_Waithandles)].Invoke();
            }
        }

        public static String ArgumentOutOfRange_ActualValue
        {
            get
            {
                return Handlers[nameof(ArgumentOutOfRange_ActualValue)].Invoke();
            }
        }

        public static String ArgumentOutOfRange_AddValue
        {
            get
            {
                return Handlers[nameof(ArgumentOutOfRange_AddValue)].Invoke();
            }
        }

        public static String ArgumentOutOfRange_ArrayLB
        {
            get
            {
                return Handlers[nameof(ArgumentOutOfRange_ArrayLB)].Invoke();
            }
        }

        public static String ArgumentOutOfRange_BadHourMinuteSecond
        {
            get
            {
                return Handlers[nameof(ArgumentOutOfRange_BadHourMinuteSecond)].Invoke();
            }
        }

        public static String ArgumentOutOfRange_BadYearMonthDay
        {
            get
            {
                return Handlers[nameof(ArgumentOutOfRange_BadYearMonthDay)].Invoke();
            }
        }

        public static String ArgumentOutOfRange_BiggerThanCollection
        {
            get
            {
                return Handlers[nameof(ArgumentOutOfRange_BiggerThanCollection)].Invoke();
            }
        }

        public static String ArgumentOutOfRange_BinaryReaderFillBuffer
        {
            get
            {
                return Handlers[nameof(ArgumentOutOfRange_BinaryReaderFillBuffer)].Invoke();
            }
        }

        public static String ArgumentOutOfRange_Bounds_Lower_Upper
        {
            get
            {
                return Handlers[nameof(ArgumentOutOfRange_Bounds_Lower_Upper)].Invoke();
            }
        }

        public static String ArgumentOutOfRange_CalendarRange
        {
            get
            {
                return Handlers[nameof(ArgumentOutOfRange_CalendarRange)].Invoke();
            }
        }

        public static String ArgumentOutOfRange_Capacity
        {
            get
            {
                return Handlers[nameof(ArgumentOutOfRange_Capacity)].Invoke();
            }
        }

        public static String ArgumentOutOfRange_Count
        {
            get
            {
                return Handlers[nameof(ArgumentOutOfRange_Count)].Invoke();
            }
        }

        public static String ArgumentOutOfRange_DateArithmetic
        {
            get
            {
                return Handlers[nameof(ArgumentOutOfRange_DateArithmetic)].Invoke();
            }
        }

        public static String ArgumentOutOfRange_DateTimeBadMonths
        {
            get
            {
                return Handlers[nameof(ArgumentOutOfRange_DateTimeBadMonths)].Invoke();
            }
        }

        public static String ArgumentOutOfRange_DateTimeBadTicks
        {
            get
            {
                return Handlers[nameof(ArgumentOutOfRange_DateTimeBadTicks)].Invoke();
            }
        }

        public static String ArgumentOutOfRange_DateTimeBadYears
        {
            get
            {
                return Handlers[nameof(ArgumentOutOfRange_DateTimeBadYears)].Invoke();
            }
        }

        public static String ArgumentOutOfRange_Day
        {
            get
            {
                return Handlers[nameof(ArgumentOutOfRange_Day)].Invoke();
            }
        }

        public static String ArgumentOutOfRange_DayOfWeek
        {
            get
            {
                return Handlers[nameof(ArgumentOutOfRange_DayOfWeek)].Invoke();
            }
        }

        public static String ArgumentOutOfRange_DayParam
        {
            get
            {
                return Handlers[nameof(ArgumentOutOfRange_DayParam)].Invoke();
            }
        }

        public static String ArgumentOutOfRange_DecimalRound
        {
            get
            {
                return Handlers[nameof(ArgumentOutOfRange_DecimalRound)].Invoke();
            }
        }

        public static String ArgumentOutOfRange_DecimalScale
        {
            get
            {
                return Handlers[nameof(ArgumentOutOfRange_DecimalScale)].Invoke();
            }
        }

        public static String ArgumentOutOfRange_EndIndexStartIndex
        {
            get
            {
                return Handlers[nameof(ArgumentOutOfRange_EndIndexStartIndex)].Invoke();
            }
        }

        public static String ArgumentOutOfRange_Enum
        {
            get
            {
                return Handlers[nameof(ArgumentOutOfRange_Enum)].Invoke();
            }
        }

        public static String ArgumentOutOfRange_Era
        {
            get
            {
                return Handlers[nameof(ArgumentOutOfRange_Era)].Invoke();
            }
        }

        public static String ArgumentOutOfRange_FileLengthTooBig
        {
            get
            {
                return Handlers[nameof(ArgumentOutOfRange_FileLengthTooBig)].Invoke();
            }
        }

        public static String ArgumentOutOfRange_FileTimeInvalid
        {
            get
            {
                return Handlers[nameof(ArgumentOutOfRange_FileTimeInvalid)].Invoke();
            }
        }

        public static String ArgumentOutOfRange_GenericPositive
        {
            get
            {
                return Handlers[nameof(ArgumentOutOfRange_GenericPositive)].Invoke();
            }
        }

        public static String ArgumentOutOfRange_GetByteCountOverflow
        {
            get
            {
                return Handlers[nameof(ArgumentOutOfRange_GetByteCountOverflow)].Invoke();
            }
        }

        public static String ArgumentOutOfRange_GetCharCountOverflow
        {
            get
            {
                return Handlers[nameof(ArgumentOutOfRange_GetCharCountOverflow)].Invoke();
            }
        }

        public static String ArgumentOutOfRange_HashtableLoadFactor
        {
            get
            {
                return Handlers[nameof(ArgumentOutOfRange_HashtableLoadFactor)].Invoke();
            }
        }

        public static String ArgumentOutOfRange_HugeArrayNotSupported
        {
            get
            {
                return Handlers[nameof(ArgumentOutOfRange_HugeArrayNotSupported)].Invoke();
            }
        }

        public static String ArgumentOutOfRange_Index
        {
            get
            {
                return Handlers[nameof(ArgumentOutOfRange_Index)].Invoke();
            }
        }

        public static String ArgumentOutOfRange_IndexCount
        {
            get
            {
                return Handlers[nameof(ArgumentOutOfRange_IndexCount)].Invoke();
            }
        }

        public static String ArgumentOutOfRange_IndexCountBuffer
        {
            get
            {
                return Handlers[nameof(ArgumentOutOfRange_IndexCountBuffer)].Invoke();
            }
        }

        public static String ArgumentOutOfRange_IndexLength
        {
            get
            {
                return Handlers[nameof(ArgumentOutOfRange_IndexLength)].Invoke();
            }
        }

        public static String ArgumentOutOfRange_IndexString
        {
            get
            {
                return Handlers[nameof(ArgumentOutOfRange_IndexString)].Invoke();
            }
        }

        public static String ArgumentOutOfRange_InputTooLarge
        {
            get
            {
                return Handlers[nameof(ArgumentOutOfRange_InputTooLarge)].Invoke();
            }
        }

        public static String ArgumentOutOfRange_InvalidEraValue
        {
            get
            {
                return Handlers[nameof(ArgumentOutOfRange_InvalidEraValue)].Invoke();
            }
        }

        public static String ArgumentOutOfRange_InvalidHighSurrogate
        {
            get
            {
                return Handlers[nameof(ArgumentOutOfRange_InvalidHighSurrogate)].Invoke();
            }
        }

        public static String ArgumentOutOfRange_InvalidLowSurrogate
        {
            get
            {
                return Handlers[nameof(ArgumentOutOfRange_InvalidLowSurrogate)].Invoke();
            }
        }

        public static String ArgumentOutOfRange_InvalidUTF32
        {
            get
            {
                return Handlers[nameof(ArgumentOutOfRange_InvalidUTF32)].Invoke();
            }
        }

        public static String ArgumentOutOfRange_Length
        {
            get
            {
                return Handlers[nameof(ArgumentOutOfRange_Length)].Invoke();
            }
        }

        public static String ArgumentOutOfRange_LengthGreaterThanCapacity
        {
            get
            {
                return Handlers[nameof(ArgumentOutOfRange_LengthGreaterThanCapacity)].Invoke();
            }
        }

        public static String ArgumentOutOfRange_LengthTooLarge
        {
            get
            {
                return Handlers[nameof(ArgumentOutOfRange_LengthTooLarge)].Invoke();
            }
        }

        public static String ArgumentOutOfRange_LessEqualToIntegerMaxVal
        {
            get
            {
                return Handlers[nameof(ArgumentOutOfRange_LessEqualToIntegerMaxVal)].Invoke();
            }
        }

        public static String ArgumentOutOfRange_ListInsert
        {
            get
            {
                return Handlers[nameof(ArgumentOutOfRange_ListInsert)].Invoke();
            }
        }

        public static String ArgumentOutOfRange_Month
        {
            get
            {
                return Handlers[nameof(ArgumentOutOfRange_Month)].Invoke();
            }
        }

        public static String ArgumentOutOfRange_MonthParam
        {
            get
            {
                return Handlers[nameof(ArgumentOutOfRange_MonthParam)].Invoke();
            }
        }

        public static String ArgumentOutOfRange_MustBeNonNegInt32
        {
            get
            {
                return Handlers[nameof(ArgumentOutOfRange_MustBeNonNegInt32)].Invoke();
            }
        }

        public static String ArgumentOutOfRange_MustBeNonNegNum
        {
            get
            {
                return Handlers[nameof(ArgumentOutOfRange_MustBeNonNegNum)].Invoke();
            }
        }

        public static String ArgumentOutOfRange_MustBePositive
        {
            get
            {
                return Handlers[nameof(ArgumentOutOfRange_MustBePositive)].Invoke();
            }
        }

        public static String ArgumentOutOfRange_NeedNonNegNum
        {
            get
            {
                return Handlers[nameof(ArgumentOutOfRange_NeedNonNegNum)].Invoke();
            }
        }

        public static String ArgumentOutOfRange_NeedNonNegOrNegative1
        {
            get
            {
                return Handlers[nameof(ArgumentOutOfRange_NeedNonNegOrNegative1)].Invoke();
            }
        }

        public static String ArgumentOutOfRange_NeedPosNum
        {
            get
            {
                return Handlers[nameof(ArgumentOutOfRange_NeedPosNum)].Invoke();
            }
        }

        public static String ArgumentOutOfRange_NeedValidId
        {
            get
            {
                return Handlers[nameof(ArgumentOutOfRange_NeedValidId)].Invoke();
            }
        }

        public static String ArgumentOutOfRange_NegativeCapacity
        {
            get
            {
                return Handlers[nameof(ArgumentOutOfRange_NegativeCapacity)].Invoke();
            }
        }

        public static String ArgumentOutOfRange_NegativeCount
        {
            get
            {
                return Handlers[nameof(ArgumentOutOfRange_NegativeCount)].Invoke();
            }
        }

        public static String ArgumentOutOfRange_NegativeLength
        {
            get
            {
                return Handlers[nameof(ArgumentOutOfRange_NegativeLength)].Invoke();
            }
        }

        public static String ArgumentOutOfRange_OffsetLength
        {
            get
            {
                return Handlers[nameof(ArgumentOutOfRange_OffsetLength)].Invoke();
            }
        }

        public static String ArgumentOutOfRange_OffsetOut
        {
            get
            {
                return Handlers[nameof(ArgumentOutOfRange_OffsetOut)].Invoke();
            }
        }

        public static String ArgumentOutOfRange_ParamSequence
        {
            get
            {
                return Handlers[nameof(ArgumentOutOfRange_ParamSequence)].Invoke();
            }
        }

        public static String ArgumentOutOfRange_PartialWCHAR
        {
            get
            {
                return Handlers[nameof(ArgumentOutOfRange_PartialWCHAR)].Invoke();
            }
        }

        public static String ArgumentOutOfRange_PeriodTooLarge
        {
            get
            {
                return Handlers[nameof(ArgumentOutOfRange_PeriodTooLarge)].Invoke();
            }
        }

        public static String ArgumentOutOfRange_PositionLessThanCapacityRequired
        {
            get
            {
                return Handlers[nameof(ArgumentOutOfRange_PositionLessThanCapacityRequired)].Invoke();
            }
        }

        public static String ArgumentOutOfRange_Range
        {
            get
            {
                return Handlers[nameof(ArgumentOutOfRange_Range)].Invoke();
            }
        }

        public static String ArgumentOutOfRange_RoundingDigits
        {
            get
            {
                return Handlers[nameof(ArgumentOutOfRange_RoundingDigits)].Invoke();
            }
        }

        public static String ArgumentOutOfRange_SmallCapacity
        {
            get
            {
                return Handlers[nameof(ArgumentOutOfRange_SmallCapacity)].Invoke();
            }
        }

        public static String ArgumentOutOfRange_SmallMaxCapacity
        {
            get
            {
                return Handlers[nameof(ArgumentOutOfRange_SmallMaxCapacity)].Invoke();
            }
        }

        public static String ArgumentOutOfRange_StartIndex
        {
            get
            {
                return Handlers[nameof(ArgumentOutOfRange_StartIndex)].Invoke();
            }
        }

        public static String ArgumentOutOfRange_StartIndexLargerThanLength
        {
            get
            {
                return Handlers[nameof(ArgumentOutOfRange_StartIndexLargerThanLength)].Invoke();
            }
        }

        public static String ArgumentOutOfRange_StartIndexLessThanLength
        {
            get
            {
                return Handlers[nameof(ArgumentOutOfRange_StartIndexLessThanLength)].Invoke();
            }
        }

        public static String ArgumentOutOfRange_StreamLength
        {
            get
            {
                return Handlers[nameof(ArgumentOutOfRange_StreamLength)].Invoke();
            }
        }

        public static String ArgumentOutOfRange_TimeoutTooLarge
        {
            get
            {
                return Handlers[nameof(ArgumentOutOfRange_TimeoutTooLarge)].Invoke();
            }
        }

        public static String ArgumentOutOfRange_UIntPtrMax
        {
            get
            {
                return Handlers[nameof(ArgumentOutOfRange_UIntPtrMax)].Invoke();
            }
        }

        public static String ArgumentOutOfRange_UnmanagedMemStreamLength
        {
            get
            {
                return Handlers[nameof(ArgumentOutOfRange_UnmanagedMemStreamLength)].Invoke();
            }
        }

        public static String ArgumentOutOfRange_UnmanagedMemStreamWrapAround
        {
            get
            {
                return Handlers[nameof(ArgumentOutOfRange_UnmanagedMemStreamWrapAround)].Invoke();
            }
        }

        public static String ArgumentOutOfRange_UtcOffset
        {
            get
            {
                return Handlers[nameof(ArgumentOutOfRange_UtcOffset)].Invoke();
            }
        }

        public static String ArgumentOutOfRange_UtcOffsetAndDaylightDelta
        {
            get
            {
                return Handlers[nameof(ArgumentOutOfRange_UtcOffsetAndDaylightDelta)].Invoke();
            }
        }

        public static String ArgumentOutOfRange_Version
        {
            get
            {
                return Handlers[nameof(ArgumentOutOfRange_Version)].Invoke();
            }
        }

        public static String ArgumentOutOfRange_Week
        {
            get
            {
                return Handlers[nameof(ArgumentOutOfRange_Week)].Invoke();
            }
        }

        public static String ArgumentOutOfRange_Year
        {
            get
            {
                return Handlers[nameof(ArgumentOutOfRange_Year)].Invoke();
            }
        }

        public static String Arithmetic_NaN
        {
            get
            {
                return Handlers[nameof(Arithmetic_NaN)].Invoke();
            }
        }

        public static String ArrayTypeMismatch_ConstrainedCopy
        {
            get
            {
                return Handlers[nameof(ArrayTypeMismatch_ConstrainedCopy)].Invoke();
            }
        }

        public static String AssemblyLoadContext_Unload_CannotUnloadIfNotCollectible
        {
            get
            {
                return Handlers[nameof(AssemblyLoadContext_Unload_CannotUnloadIfNotCollectible)].Invoke();
            }
        }

        public static String AssemblyLoadContext_Verify_NotUnloading
        {
            get
            {
                return Handlers[nameof(AssemblyLoadContext_Verify_NotUnloading)].Invoke();
            }
        }

        public static String AssertionFailed
        {
            get
            {
                return Handlers[nameof(AssertionFailed)].Invoke();
            }
        }

        public static String AssertionFailed_Cnd
        {
            get
            {
                return Handlers[nameof(AssertionFailed_Cnd)].Invoke();
            }
        }

        public static String AssumptionFailed
        {
            get
            {
                return Handlers[nameof(AssumptionFailed)].Invoke();
            }
        }

        public static String AssumptionFailed_Cnd
        {
            get
            {
                return Handlers[nameof(AssumptionFailed_Cnd)].Invoke();
            }
        }

        public static String AsyncMethodBuilder_InstanceNotInitialized
        {
            get
            {
                return Handlers[nameof(AsyncMethodBuilder_InstanceNotInitialized)].Invoke();
            }
        }

        public static String BadImageFormat_BadILFormat
        {
            get
            {
                return Handlers[nameof(BadImageFormat_BadILFormat)].Invoke();
            }
        }

        public static String BadImageFormat_InvalidType
        {
            get
            {
                return Handlers[nameof(BadImageFormat_InvalidType)].Invoke();
            }
        }

        public static String BadImageFormat_NegativeStringLength
        {
            get
            {
                return Handlers[nameof(BadImageFormat_NegativeStringLength)].Invoke();
            }
        }

        public static String BadImageFormat_ParameterSignatureMismatch
        {
            get
            {
                return Handlers[nameof(BadImageFormat_ParameterSignatureMismatch)].Invoke();
            }
        }

        public static String BadImageFormat_ResType_SerBlobMismatch
        {
            get
            {
                return Handlers[nameof(BadImageFormat_ResType_SerBlobMismatch)].Invoke();
            }
        }

        public static String BadImageFormat_ResourceDataLengthInvalid
        {
            get
            {
                return Handlers[nameof(BadImageFormat_ResourceDataLengthInvalid)].Invoke();
            }
        }

        public static String BadImageFormat_ResourceNameCorrupted
        {
            get
            {
                return Handlers[nameof(BadImageFormat_ResourceNameCorrupted)].Invoke();
            }
        }

        public static String BadImageFormat_ResourceNameCorrupted_NameIndex
        {
            get
            {
                return Handlers[nameof(BadImageFormat_ResourceNameCorrupted_NameIndex)].Invoke();
            }
        }

        public static String BadImageFormat_ResourcesDataInvalidOffset
        {
            get
            {
                return Handlers[nameof(BadImageFormat_ResourcesDataInvalidOffset)].Invoke();
            }
        }

        public static String BadImageFormat_ResourcesHeaderCorrupted
        {
            get
            {
                return Handlers[nameof(BadImageFormat_ResourcesHeaderCorrupted)].Invoke();
            }
        }

        public static String BadImageFormat_ResourcesIndexTooLong
        {
            get
            {
                return Handlers[nameof(BadImageFormat_ResourcesIndexTooLong)].Invoke();
            }
        }

        public static String BadImageFormat_ResourcesNameInvalidOffset
        {
            get
            {
                return Handlers[nameof(BadImageFormat_ResourcesNameInvalidOffset)].Invoke();
            }
        }

        public static String BadImageFormat_ResourcesNameTooLong
        {
            get
            {
                return Handlers[nameof(BadImageFormat_ResourcesNameTooLong)].Invoke();
            }
        }

        public static String BadImageFormat_TypeMismatch
        {
            get
            {
                return Handlers[nameof(BadImageFormat_TypeMismatch)].Invoke();
            }
        }

        public static String CancellationToken_CreateLinkedToken_TokensIsEmpty
        {
            get
            {
                return Handlers[nameof(CancellationToken_CreateLinkedToken_TokensIsEmpty)].Invoke();
            }
        }

        public static String CancellationTokenSource_Disposed
        {
            get
            {
                return Handlers[nameof(CancellationTokenSource_Disposed)].Invoke();
            }
        }

        public static String ConcurrentCollection_SyncRoot_NotSupported
        {
            get
            {
                return Handlers[nameof(ConcurrentCollection_SyncRoot_NotSupported)].Invoke();
            }
        }

        public static String EventSource_AbstractMustNotDeclareEventMethods
        {
            get
            {
                return Handlers[nameof(EventSource_AbstractMustNotDeclareEventMethods)].Invoke();
            }
        }

        public static String EventSource_AbstractMustNotDeclareKTOC
        {
            get
            {
                return Handlers[nameof(EventSource_AbstractMustNotDeclareKTOC)].Invoke();
            }
        }

        public static String EventSource_AddScalarOutOfRange
        {
            get
            {
                return Handlers[nameof(EventSource_AddScalarOutOfRange)].Invoke();
            }
        }

        public static String EventSource_BadHexDigit
        {
            get
            {
                return Handlers[nameof(EventSource_BadHexDigit)].Invoke();
            }
        }

        public static String EventSource_ChannelTypeDoesNotMatchEventChannelValue
        {
            get
            {
                return Handlers[nameof(EventSource_ChannelTypeDoesNotMatchEventChannelValue)].Invoke();
            }
        }

        public static String EventSource_DataDescriptorsOutOfRange
        {
            get
            {
                return Handlers[nameof(EventSource_DataDescriptorsOutOfRange)].Invoke();
            }
        }

        public static String EventSource_DuplicateStringKey
        {
            get
            {
                return Handlers[nameof(EventSource_DuplicateStringKey)].Invoke();
            }
        }

        public static String EventSource_EnumKindMismatch
        {
            get
            {
                return Handlers[nameof(EventSource_EnumKindMismatch)].Invoke();
            }
        }

        public static String EventSource_EvenHexDigits
        {
            get
            {
                return Handlers[nameof(EventSource_EvenHexDigits)].Invoke();
            }
        }

        public static String EventSource_EventChannelOutOfRange
        {
            get
            {
                return Handlers[nameof(EventSource_EventChannelOutOfRange)].Invoke();
            }
        }

        public static String EventSource_EventIdReused
        {
            get
            {
                return Handlers[nameof(EventSource_EventIdReused)].Invoke();
            }
        }

        public static String EventSource_EventMustHaveTaskIfNonDefaultOpcode
        {
            get
            {
                return Handlers[nameof(EventSource_EventMustHaveTaskIfNonDefaultOpcode)].Invoke();
            }
        }

        public static String EventSource_EventMustNotBeExplicitImplementation
        {
            get
            {
                return Handlers[nameof(EventSource_EventMustNotBeExplicitImplementation)].Invoke();
            }
        }

        public static String EventSource_EventNameReused
        {
            get
            {
                return Handlers[nameof(EventSource_EventNameReused)].Invoke();
            }
        }

        public static String EventSource_EventParametersMismatch
        {
            get
            {
                return Handlers[nameof(EventSource_EventParametersMismatch)].Invoke();
            }
        }

        public static String EventSource_EventSourceGuidInUse
        {
            get
            {
                return Handlers[nameof(EventSource_EventSourceGuidInUse)].Invoke();
            }
        }

        public static String EventSource_EventTooBig
        {
            get
            {
                return Handlers[nameof(EventSource_EventTooBig)].Invoke();
            }
        }

        public static String EventSource_EventWithAdminChannelMustHaveMessage
        {
            get
            {
                return Handlers[nameof(EventSource_EventWithAdminChannelMustHaveMessage)].Invoke();
            }
        }

        public static String EventSource_IllegalKeywordsValue
        {
            get
            {
                return Handlers[nameof(EventSource_IllegalKeywordsValue)].Invoke();
            }
        }

        public static String EventSource_IllegalOpcodeValue
        {
            get
            {
                return Handlers[nameof(EventSource_IllegalOpcodeValue)].Invoke();
            }
        }

        public static String EventSource_IllegalTaskValue
        {
            get
            {
                return Handlers[nameof(EventSource_IllegalTaskValue)].Invoke();
            }
        }

        public static String EventSource_IllegalValue
        {
            get
            {
                return Handlers[nameof(EventSource_IllegalValue)].Invoke();
            }
        }

        public static String EventSource_IncorrentlyAuthoredTypeInfo
        {
            get
            {
                return Handlers[nameof(EventSource_IncorrentlyAuthoredTypeInfo)].Invoke();
            }
        }

        public static String EventSource_InvalidCommand
        {
            get
            {
                return Handlers[nameof(EventSource_InvalidCommand)].Invoke();
            }
        }

        public static String EventSource_InvalidEventFormat
        {
            get
            {
                return Handlers[nameof(EventSource_InvalidEventFormat)].Invoke();
            }
        }

        public static String EventSource_KeywordCollision
        {
            get
            {
                return Handlers[nameof(EventSource_KeywordCollision)].Invoke();
            }
        }

        public static String EventSource_KeywordNeedPowerOfTwo
        {
            get
            {
                return Handlers[nameof(EventSource_KeywordNeedPowerOfTwo)].Invoke();
            }
        }

        public static String EventSource_ListenerCreatedInsideCallback
        {
            get
            {
                return Handlers[nameof(EventSource_ListenerCreatedInsideCallback)].Invoke();
            }
        }

        public static String EventSource_ListenerNotFound
        {
            get
            {
                return Handlers[nameof(EventSource_ListenerNotFound)].Invoke();
            }
        }

        public static String EventSource_ListenerWriteFailure
        {
            get
            {
                return Handlers[nameof(EventSource_ListenerWriteFailure)].Invoke();
            }
        }

        public static String EventSource_MaxChannelExceeded
        {
            get
            {
                return Handlers[nameof(EventSource_MaxChannelExceeded)].Invoke();
            }
        }

        public static String EventSource_MismatchIdToWriteEvent
        {
            get
            {
                return Handlers[nameof(EventSource_MismatchIdToWriteEvent)].Invoke();
            }
        }

        public static String EventSource_NeedGuid
        {
            get
            {
                return Handlers[nameof(EventSource_NeedGuid)].Invoke();
            }
        }

        public static String EventSource_NeedName
        {
            get
            {
                return Handlers[nameof(EventSource_NeedName)].Invoke();
            }
        }

        public static String EventSource_NeedPositiveId
        {
            get
            {
                return Handlers[nameof(EventSource_NeedPositiveId)].Invoke();
            }
        }

        public static String EventSource_NoFreeBuffers
        {
            get
            {
                return Handlers[nameof(EventSource_NoFreeBuffers)].Invoke();
            }
        }

        public static String EventSource_NonCompliantTypeError
        {
            get
            {
                return Handlers[nameof(EventSource_NonCompliantTypeError)].Invoke();
            }
        }

        public static String EventSource_NoRelatedActivityId
        {
            get
            {
                return Handlers[nameof(EventSource_NoRelatedActivityId)].Invoke();
            }
        }

        public static String EventSource_NotSupportedArrayOfBinary
        {
            get
            {
                return Handlers[nameof(EventSource_NotSupportedArrayOfBinary)].Invoke();
            }
        }

        public static String EventSource_NotSupportedArrayOfNil
        {
            get
            {
                return Handlers[nameof(EventSource_NotSupportedArrayOfNil)].Invoke();
            }
        }

        public static String EventSource_NotSupportedArrayOfNullTerminatedString
        {
            get
            {
                return Handlers[nameof(EventSource_NotSupportedArrayOfNullTerminatedString)].Invoke();
            }
        }

        public static String EventSource_NotSupportedNestedArraysEnums
        {
            get
            {
                return Handlers[nameof(EventSource_NotSupportedNestedArraysEnums)].Invoke();
            }
        }

        public static String EventSource_NullInput
        {
            get
            {
                return Handlers[nameof(EventSource_NullInput)].Invoke();
            }
        }

        public static String EventSource_OpcodeCollision
        {
            get
            {
                return Handlers[nameof(EventSource_OpcodeCollision)].Invoke();
            }
        }

        public static String EventSource_PinArrayOutOfRange
        {
            get
            {
                return Handlers[nameof(EventSource_PinArrayOutOfRange)].Invoke();
            }
        }

        public static String EventSource_RecursiveTypeDefinition
        {
            get
            {
                return Handlers[nameof(EventSource_RecursiveTypeDefinition)].Invoke();
            }
        }

        public static String EventSource_StopsFollowStarts
        {
            get
            {
                return Handlers[nameof(EventSource_StopsFollowStarts)].Invoke();
            }
        }

        public static String EventSource_TaskCollision
        {
            get
            {
                return Handlers[nameof(EventSource_TaskCollision)].Invoke();
            }
        }

        public static String EventSource_TaskOpcodePairReused
        {
            get
            {
                return Handlers[nameof(EventSource_TaskOpcodePairReused)].Invoke();
            }
        }

        public static String EventSource_TooManyArgs
        {
            get
            {
                return Handlers[nameof(EventSource_TooManyArgs)].Invoke();
            }
        }

        public static String EventSource_TooManyFields
        {
            get
            {
                return Handlers[nameof(EventSource_TooManyFields)].Invoke();
            }
        }

        public static String EventSource_ToString
        {
            get
            {
                return Handlers[nameof(EventSource_ToString)].Invoke();
            }
        }

        public static String EventSource_TraitEven
        {
            get
            {
                return Handlers[nameof(EventSource_TraitEven)].Invoke();
            }
        }

        public static String EventSource_TypeMustBeSealedOrAbstract
        {
            get
            {
                return Handlers[nameof(EventSource_TypeMustBeSealedOrAbstract)].Invoke();
            }
        }

        public static String EventSource_TypeMustDeriveFromEventSource
        {
            get
            {
                return Handlers[nameof(EventSource_TypeMustDeriveFromEventSource)].Invoke();
            }
        }

        public static String EventSource_UndefinedChannel
        {
            get
            {
                return Handlers[nameof(EventSource_UndefinedChannel)].Invoke();
            }
        }

        public static String EventSource_UndefinedKeyword
        {
            get
            {
                return Handlers[nameof(EventSource_UndefinedKeyword)].Invoke();
            }
        }

        public static String EventSource_UndefinedOpcode
        {
            get
            {
                return Handlers[nameof(EventSource_UndefinedOpcode)].Invoke();
            }
        }

        public static String EventSource_UnknownEtwTrait
        {
            get
            {
                return Handlers[nameof(EventSource_UnknownEtwTrait)].Invoke();
            }
        }

        public static String EventSource_UnsupportedEventTypeInManifest
        {
            get
            {
                return Handlers[nameof(EventSource_UnsupportedEventTypeInManifest)].Invoke();
            }
        }

        public static String EventSource_UnsupportedMessageProperty
        {
            get
            {
                return Handlers[nameof(EventSource_UnsupportedMessageProperty)].Invoke();
            }
        }

        public static String EventSource_VarArgsParameterMismatch
        {
            get
            {
                return Handlers[nameof(EventSource_VarArgsParameterMismatch)].Invoke();
            }
        }

        public static String Exception_EndOfInnerExceptionStack
        {
            get
            {
                return Handlers[nameof(Exception_EndOfInnerExceptionStack)].Invoke();
            }
        }

        public static String Exception_EndStackTraceFromPreviousThrow
        {
            get
            {
                return Handlers[nameof(Exception_EndStackTraceFromPreviousThrow)].Invoke();
            }
        }

        public static String Exception_WasThrown
        {
            get
            {
                return Handlers[nameof(Exception_WasThrown)].Invoke();
            }
        }

        public static String ExecutionContext_ExceptionInAsyncLocalNotification
        {
            get
            {
                return Handlers[nameof(ExecutionContext_ExceptionInAsyncLocalNotification)].Invoke();
            }
        }

        public static String FileNotFound_ResolveAssembly
        {
            get
            {
                return Handlers[nameof(FileNotFound_ResolveAssembly)].Invoke();
            }
        }

        public static String Format_AttributeUsage
        {
            get
            {
                return Handlers[nameof(Format_AttributeUsage)].Invoke();
            }
        }

        public static String Format_Bad7BitInt
        {
            get
            {
                return Handlers[nameof(Format_Bad7BitInt)].Invoke();
            }
        }

        public static String Format_BadBase64Char
        {
            get
            {
                return Handlers[nameof(Format_BadBase64Char)].Invoke();
            }
        }

        public static String Format_BadBoolean
        {
            get
            {
                return Handlers[nameof(Format_BadBoolean)].Invoke();
            }
        }

        public static String Format_BadFormatSpecifier
        {
            get
            {
                return Handlers[nameof(Format_BadFormatSpecifier)].Invoke();
            }
        }

        public static String Format_NoFormatSpecifier
        {
            get
            {
                return Handlers[nameof(Format_NoFormatSpecifier)].Invoke();
            }
        }

        public static String Format_BadHexChar
        {
            get
            {
                return Handlers[nameof(Format_BadHexChar)].Invoke();
            }
        }

        public static String Format_BadHexLength
        {
            get
            {
                return Handlers[nameof(Format_BadHexLength)].Invoke();
            }
        }

        public static String Format_BadQuote
        {
            get
            {
                return Handlers[nameof(Format_BadQuote)].Invoke();
            }
        }

        public static String Format_BadTimeSpan
        {
            get
            {
                return Handlers[nameof(Format_BadTimeSpan)].Invoke();
            }
        }

        public static String Format_EmptyInputString
        {
            get
            {
                return Handlers[nameof(Format_EmptyInputString)].Invoke();
            }
        }

        public static String Format_ExtraJunkAtEnd
        {
            get
            {
                return Handlers[nameof(Format_ExtraJunkAtEnd)].Invoke();
            }
        }

        public static String Format_GuidUnrecognized
        {
            get
            {
                return Handlers[nameof(Format_GuidUnrecognized)].Invoke();
            }
        }

        public static String Format_IndexOutOfRange
        {
            get
            {
                return Handlers[nameof(Format_IndexOutOfRange)].Invoke();
            }
        }

        public static String Format_InvalidEnumFormatSpecification
        {
            get
            {
                return Handlers[nameof(Format_InvalidEnumFormatSpecification)].Invoke();
            }
        }

        public static String Format_InvalidGuidFormatSpecification
        {
            get
            {
                return Handlers[nameof(Format_InvalidGuidFormatSpecification)].Invoke();
            }
        }

        public static String Format_InvalidString
        {
            get
            {
                return Handlers[nameof(Format_InvalidString)].Invoke();
            }
        }

        public static String Format_NeedSingleChar
        {
            get
            {
                return Handlers[nameof(Format_NeedSingleChar)].Invoke();
            }
        }

        public static String Format_NoParsibleDigits
        {
            get
            {
                return Handlers[nameof(Format_NoParsibleDigits)].Invoke();
            }
        }

        public static String Format_StringZeroLength
        {
            get
            {
                return Handlers[nameof(Format_StringZeroLength)].Invoke();
            }
        }

        public static String IndexOutOfRange_ArrayRankIndex
        {
            get
            {
                return Handlers[nameof(IndexOutOfRange_ArrayRankIndex)].Invoke();
            }
        }

        public static String IndexOutOfRange_UMSPosition
        {
            get
            {
                return Handlers[nameof(IndexOutOfRange_UMSPosition)].Invoke();
            }
        }

        public static String InsufficientMemory_MemFailPoint
        {
            get
            {
                return Handlers[nameof(InsufficientMemory_MemFailPoint)].Invoke();
            }
        }

        public static String InsufficientMemory_MemFailPoint_TooBig
        {
            get
            {
                return Handlers[nameof(InsufficientMemory_MemFailPoint_TooBig)].Invoke();
            }
        }

        public static String InsufficientMemory_MemFailPoint_VAFrag
        {
            get
            {
                return Handlers[nameof(InsufficientMemory_MemFailPoint_VAFrag)].Invoke();
            }
        }

        public static String Interop_COM_TypeMismatch
        {
            get
            {
                return Handlers[nameof(Interop_COM_TypeMismatch)].Invoke();
            }
        }

        public static String Interop_Marshal_Unmappable_Char
        {
            get
            {
                return Handlers[nameof(Interop_Marshal_Unmappable_Char)].Invoke();
            }
        }

        public static String Interop_Marshal_SafeHandle_InvalidOperation
        {
            get
            {
                return Handlers[nameof(Interop_Marshal_SafeHandle_InvalidOperation)].Invoke();
            }
        }

        public static String Interop_Marshal_CannotCreateSafeHandleField
        {
            get
            {
                return Handlers[nameof(Interop_Marshal_CannotCreateSafeHandleField)].Invoke();
            }
        }

        public static String Interop_Marshal_CannotCreateCriticalHandleField
        {
            get
            {
                return Handlers[nameof(Interop_Marshal_CannotCreateCriticalHandleField)].Invoke();
            }
        }

        public static String InvalidCast_CannotCastNullToValueType
        {
            get
            {
                return Handlers[nameof(InvalidCast_CannotCastNullToValueType)].Invoke();
            }
        }

        public static String InvalidCast_CannotCoerceByRefVariant
        {
            get
            {
                return Handlers[nameof(InvalidCast_CannotCoerceByRefVariant)].Invoke();
            }
        }

        public static String InvalidCast_DBNull
        {
            get
            {
                return Handlers[nameof(InvalidCast_DBNull)].Invoke();
            }
        }

        public static String InvalidCast_Empty
        {
            get
            {
                return Handlers[nameof(InvalidCast_Empty)].Invoke();
            }
        }

        public static String InvalidCast_FromDBNull
        {
            get
            {
                return Handlers[nameof(InvalidCast_FromDBNull)].Invoke();
            }
        }

        public static String InvalidCast_FromTo
        {
            get
            {
                return Handlers[nameof(InvalidCast_FromTo)].Invoke();
            }
        }

        public static String InvalidCast_IConvertible
        {
            get
            {
                return Handlers[nameof(InvalidCast_IConvertible)].Invoke();
            }
        }

        public static String InvalidOperation_AsyncFlowCtrlCtxMismatch
        {
            get
            {
                return Handlers[nameof(InvalidOperation_AsyncFlowCtrlCtxMismatch)].Invoke();
            }
        }

        public static String InvalidOperation_AsyncIOInProgress
        {
            get
            {
                return Handlers[nameof(InvalidOperation_AsyncIOInProgress)].Invoke();
            }
        }

        public static String InvalidOperation_BadEmptyMethodBody
        {
            get
            {
                return Handlers[nameof(InvalidOperation_BadEmptyMethodBody)].Invoke();
            }
        }

        public static String InvalidOperation_BadILGeneratorUsage
        {
            get
            {
                return Handlers[nameof(InvalidOperation_BadILGeneratorUsage)].Invoke();
            }
        }

        public static String InvalidOperation_BadInstructionOrIndexOutOfBound
        {
            get
            {
                return Handlers[nameof(InvalidOperation_BadInstructionOrIndexOutOfBound)].Invoke();
            }
        }

        public static String InvalidOperation_BadInterfaceNotAbstract
        {
            get
            {
                return Handlers[nameof(InvalidOperation_BadInterfaceNotAbstract)].Invoke();
            }
        }

        public static String InvalidOperation_BadMethodBody
        {
            get
            {
                return Handlers[nameof(InvalidOperation_BadMethodBody)].Invoke();
            }
        }

        public static String InvalidOperation_BadTypeAttributesNotAbstract
        {
            get
            {
                return Handlers[nameof(InvalidOperation_BadTypeAttributesNotAbstract)].Invoke();
            }
        }

        public static String InvalidOperation_CalledTwice
        {
            get
            {
                return Handlers[nameof(InvalidOperation_CalledTwice)].Invoke();
            }
        }

        public static String InvalidOperation_CannotImportGlobalFromDifferentModule
        {
            get
            {
                return Handlers[nameof(InvalidOperation_CannotImportGlobalFromDifferentModule)].Invoke();
            }
        }

        public static String InvalidOperation_CannotRegisterSecondResolver
        {
            get
            {
                return Handlers[nameof(InvalidOperation_CannotRegisterSecondResolver)].Invoke();
            }
        }

        public static String InvalidOperation_CannotRestoreUnsupressedFlow
        {
            get
            {
                return Handlers[nameof(InvalidOperation_CannotRestoreUnsupressedFlow)].Invoke();
            }
        }

        public static String InvalidOperation_CannotSupressFlowMultipleTimes
        {
            get
            {
                return Handlers[nameof(InvalidOperation_CannotSupressFlowMultipleTimes)].Invoke();
            }
        }

        public static String InvalidOperation_CannotUseAFCMultiple
        {
            get
            {
                return Handlers[nameof(InvalidOperation_CannotUseAFCMultiple)].Invoke();
            }
        }

        public static String InvalidOperation_CannotUseAFCOtherThread
        {
            get
            {
                return Handlers[nameof(InvalidOperation_CannotUseAFCOtherThread)].Invoke();
            }
        }

        public static String InvalidOperation_CollectionCorrupted
        {
            get
            {
                return Handlers[nameof(InvalidOperation_CollectionCorrupted)].Invoke();
            }
        }

        public static String InvalidOperation_ComputerName
        {
            get
            {
                return Handlers[nameof(InvalidOperation_ComputerName)].Invoke();
            }
        }

        public static String InvalidOperation_ConcurrentOperationsNotSupported
        {
            get
            {
                return Handlers[nameof(InvalidOperation_ConcurrentOperationsNotSupported)].Invoke();
            }
        }

        public static String InvalidOperation_ConstructorNotAllowedOnInterface
        {
            get
            {
                return Handlers[nameof(InvalidOperation_ConstructorNotAllowedOnInterface)].Invoke();
            }
        }

        public static String InvalidOperation_DateTimeParsing
        {
            get
            {
                return Handlers[nameof(InvalidOperation_DateTimeParsing)].Invoke();
            }
        }

        public static String InvalidOperation_DefaultConstructorILGen
        {
            get
            {
                return Handlers[nameof(InvalidOperation_DefaultConstructorILGen)].Invoke();
            }
        }

        public static String InvalidOperation_EndReadCalledMultiple
        {
            get
            {
                return Handlers[nameof(InvalidOperation_EndReadCalledMultiple)].Invoke();
            }
        }

        public static String InvalidOperation_EndWriteCalledMultiple
        {
            get
            {
                return Handlers[nameof(InvalidOperation_EndWriteCalledMultiple)].Invoke();
            }
        }

        public static String InvalidOperation_EnumEnded
        {
            get
            {
                return Handlers[nameof(InvalidOperation_EnumEnded)].Invoke();
            }
        }

        public static String InvalidOperation_EnumFailedVersion
        {
            get
            {
                return Handlers[nameof(InvalidOperation_EnumFailedVersion)].Invoke();
            }
        }

        public static String InvalidOperation_EnumNotStarted
        {
            get
            {
                return Handlers[nameof(InvalidOperation_EnumNotStarted)].Invoke();
            }
        }

        public static String InvalidOperation_EnumOpCantHappen
        {
            get
            {
                return Handlers[nameof(InvalidOperation_EnumOpCantHappen)].Invoke();
            }
        }

        public static String InvalidOperation_EventInfoNotAvailable
        {
            get
            {
                return Handlers[nameof(InvalidOperation_EventInfoNotAvailable)].Invoke();
            }
        }

        public static String InvalidOperation_GenericParametersAlreadySet
        {
            get
            {
                return Handlers[nameof(InvalidOperation_GenericParametersAlreadySet)].Invoke();
            }
        }

        public static String InvalidOperation_GetVersion
        {
            get
            {
                return Handlers[nameof(InvalidOperation_GetVersion)].Invoke();
            }
        }

        public static String InvalidOperation_GlobalsHaveBeenCreated
        {
            get
            {
                return Handlers[nameof(InvalidOperation_GlobalsHaveBeenCreated)].Invoke();
            }
        }

        public static String InvalidOperation_HandleIsNotInitialized
        {
            get
            {
                return Handlers[nameof(InvalidOperation_HandleIsNotInitialized)].Invoke();
            }
        }

        public static String InvalidOperation_HandleIsNotPinned
        {
            get
            {
                return Handlers[nameof(InvalidOperation_HandleIsNotPinned)].Invoke();
            }
        }

        public static String InvalidOperation_HashInsertFailed
        {
            get
            {
                return Handlers[nameof(InvalidOperation_HashInsertFailed)].Invoke();
            }
        }

        public static String InvalidOperation_IComparerFailed
        {
            get
            {
                return Handlers[nameof(InvalidOperation_IComparerFailed)].Invoke();
            }
        }

        public static String InvalidOperation_MethodBaked
        {
            get
            {
                return Handlers[nameof(InvalidOperation_MethodBaked)].Invoke();
            }
        }

        public static String InvalidOperation_MethodBuilderBaked
        {
            get
            {
                return Handlers[nameof(InvalidOperation_MethodBuilderBaked)].Invoke();
            }
        }

        public static String InvalidOperation_MethodHasBody
        {
            get
            {
                return Handlers[nameof(InvalidOperation_MethodHasBody)].Invoke();
            }
        }

        public static String InvalidOperation_MustCallInitialize
        {
            get
            {
                return Handlers[nameof(InvalidOperation_MustCallInitialize)].Invoke();
            }
        }

        public static String InvalidOperation_NativeOverlappedReused
        {
            get
            {
                return Handlers[nameof(InvalidOperation_NativeOverlappedReused)].Invoke();
            }
        }

        public static String InvalidOperation_NoMultiModuleAssembly
        {
            get
            {
                return Handlers[nameof(InvalidOperation_NoMultiModuleAssembly)].Invoke();
            }
        }

        public static String InvalidOperation_NoPublicAddMethod
        {
            get
            {
                return Handlers[nameof(InvalidOperation_NoPublicAddMethod)].Invoke();
            }
        }

        public static String InvalidOperation_NoPublicRemoveMethod
        {
            get
            {
                return Handlers[nameof(InvalidOperation_NoPublicRemoveMethod)].Invoke();
            }
        }

        public static String InvalidOperation_NotADebugModule
        {
            get
            {
                return Handlers[nameof(InvalidOperation_NotADebugModule)].Invoke();
            }
        }

        public static String InvalidOperation_NotAllowedInDynamicMethod
        {
            get
            {
                return Handlers[nameof(InvalidOperation_NotAllowedInDynamicMethod)].Invoke();
            }
        }

        public static String InvalidOperation_NotAVarArgCallingConvention
        {
            get
            {
                return Handlers[nameof(InvalidOperation_NotAVarArgCallingConvention)].Invoke();
            }
        }

        public static String InvalidOperation_NotGenericType
        {
            get
            {
                return Handlers[nameof(InvalidOperation_NotGenericType)].Invoke();
            }
        }

        public static String InvalidOperation_NotWithConcurrentGC
        {
            get
            {
                return Handlers[nameof(InvalidOperation_NotWithConcurrentGC)].Invoke();
            }
        }

        public static String InvalidOperation_NoUnderlyingTypeOnEnum
        {
            get
            {
                return Handlers[nameof(InvalidOperation_NoUnderlyingTypeOnEnum)].Invoke();
            }
        }

        public static String InvalidOperation_NoValue
        {
            get
            {
                return Handlers[nameof(InvalidOperation_NoValue)].Invoke();
            }
        }

        public static String InvalidOperation_NullArray
        {
            get
            {
                return Handlers[nameof(InvalidOperation_NullArray)].Invoke();
            }
        }

        public static String InvalidOperation_NullContext
        {
            get
            {
                return Handlers[nameof(InvalidOperation_NullContext)].Invoke();
            }
        }

        public static String InvalidOperation_NullModuleHandle
        {
            get
            {
                return Handlers[nameof(InvalidOperation_NullModuleHandle)].Invoke();
            }
        }

        public static String InvalidOperation_OpenLocalVariableScope
        {
            get
            {
                return Handlers[nameof(InvalidOperation_OpenLocalVariableScope)].Invoke();
            }
        }

        public static String InvalidOperation_Overlapped_Pack
        {
            get
            {
                return Handlers[nameof(InvalidOperation_Overlapped_Pack)].Invoke();
            }
        }

        public static String InvalidOperation_PropertyInfoNotAvailable
        {
            get
            {
                return Handlers[nameof(InvalidOperation_PropertyInfoNotAvailable)].Invoke();
            }
        }

        public static String InvalidOperation_ReadOnly
        {
            get
            {
                return Handlers[nameof(InvalidOperation_ReadOnly)].Invoke();
            }
        }

        public static String InvalidOperation_ResMgrBadResSet_Type
        {
            get
            {
                return Handlers[nameof(InvalidOperation_ResMgrBadResSet_Type)].Invoke();
            }
        }

        public static String InvalidOperation_ResourceNotStream_Name
        {
            get
            {
                return Handlers[nameof(InvalidOperation_ResourceNotStream_Name)].Invoke();
            }
        }

        public static String InvalidOperation_ResourceNotString_Name
        {
            get
            {
                return Handlers[nameof(InvalidOperation_ResourceNotString_Name)].Invoke();
            }
        }

        public static String InvalidOperation_ResourceNotString_Type
        {
            get
            {
                return Handlers[nameof(InvalidOperation_ResourceNotString_Type)].Invoke();
            }
        }

        public static String InvalidOperation_SetLatencyModeNoGC
        {
            get
            {
                return Handlers[nameof(InvalidOperation_SetLatencyModeNoGC)].Invoke();
            }
        }

        public static String InvalidOperation_ShouldNotHaveMethodBody
        {
            get
            {
                return Handlers[nameof(InvalidOperation_ShouldNotHaveMethodBody)].Invoke();
            }
        }

        public static String InvalidOperation_ThreadWrongThreadStart
        {
            get
            {
                return Handlers[nameof(InvalidOperation_ThreadWrongThreadStart)].Invoke();
            }
        }

        public static String InvalidOperation_TimeoutsNotSupported
        {
            get
            {
                return Handlers[nameof(InvalidOperation_TimeoutsNotSupported)].Invoke();
            }
        }

        public static String InvalidOperation_TimerAlreadyClosed
        {
            get
            {
                return Handlers[nameof(InvalidOperation_TimerAlreadyClosed)].Invoke();
            }
        }

        public static String InvalidOperation_TypeHasBeenCreated
        {
            get
            {
                return Handlers[nameof(InvalidOperation_TypeHasBeenCreated)].Invoke();
            }
        }

        public static String InvalidOperation_TypeNotCreated
        {
            get
            {
                return Handlers[nameof(InvalidOperation_TypeNotCreated)].Invoke();
            }
        }

        public static String InvalidOperation_UnderlyingArrayListChanged
        {
            get
            {
                return Handlers[nameof(InvalidOperation_UnderlyingArrayListChanged)].Invoke();
            }
        }

        public static String InvalidOperation_UnknownEnumType
        {
            get
            {
                return Handlers[nameof(InvalidOperation_UnknownEnumType)].Invoke();
            }
        }

        public static String InvalidOperation_WriteOnce
        {
            get
            {
                return Handlers[nameof(InvalidOperation_WriteOnce)].Invoke();
            }
        }

        public static String InvalidOperation_WrongAsyncResultOrEndCalledMultiple
        {
            get
            {
                return Handlers[nameof(InvalidOperation_WrongAsyncResultOrEndCalledMultiple)].Invoke();
            }
        }

        public static String InvalidOperation_WrongAsyncResultOrEndReadCalledMultiple
        {
            get
            {
                return Handlers[nameof(InvalidOperation_WrongAsyncResultOrEndReadCalledMultiple)].Invoke();
            }
        }

        public static String InvalidOperation_WrongAsyncResultOrEndWriteCalledMultiple
        {
            get
            {
                return Handlers[nameof(InvalidOperation_WrongAsyncResultOrEndWriteCalledMultiple)].Invoke();
            }
        }

        public static String InvalidProgram_Default
        {
            get
            {
                return Handlers[nameof(InvalidProgram_Default)].Invoke();
            }
        }

        public static String InvalidTimeZone_InvalidRegistryData
        {
            get
            {
                return Handlers[nameof(InvalidTimeZone_InvalidRegistryData)].Invoke();
            }
        }

        public static String InvariantFailed
        {
            get
            {
                return Handlers[nameof(InvariantFailed)].Invoke();
            }
        }

        public static String InvariantFailed_Cnd
        {
            get
            {
                return Handlers[nameof(InvariantFailed_Cnd)].Invoke();
            }
        }

        public static String IO_NoFileTableInInMemoryAssemblies
        {
            get
            {
                return Handlers[nameof(IO_NoFileTableInInMemoryAssemblies)].Invoke();
            }
        }

        public static String IO_EOF_ReadBeyondEOF
        {
            get
            {
                return Handlers[nameof(IO_EOF_ReadBeyondEOF)].Invoke();
            }
        }

        public static String IO_FileLoad
        {
            get
            {
                return Handlers[nameof(IO_FileLoad)].Invoke();
            }
        }

        public static String IO_FileName_Name
        {
            get
            {
                return Handlers[nameof(IO_FileName_Name)].Invoke();
            }
        }

        public static String IO_FileNotFound
        {
            get
            {
                return Handlers[nameof(IO_FileNotFound)].Invoke();
            }
        }

        public static String IO_FileNotFound_FileName
        {
            get
            {
                return Handlers[nameof(IO_FileNotFound_FileName)].Invoke();
            }
        }

        public static String IO_AlreadyExists_Name
        {
            get
            {
                return Handlers[nameof(IO_AlreadyExists_Name)].Invoke();
            }
        }

        public static String IO_BindHandleFailed
        {
            get
            {
                return Handlers[nameof(IO_BindHandleFailed)].Invoke();
            }
        }

        public static String IO_FileExists_Name
        {
            get
            {
                return Handlers[nameof(IO_FileExists_Name)].Invoke();
            }
        }

        public static String IO_FileStreamHandlePosition
        {
            get
            {
                return Handlers[nameof(IO_FileStreamHandlePosition)].Invoke();
            }
        }

        public static String IO_FileTooLongOrHandleNotSync
        {
            get
            {
                return Handlers[nameof(IO_FileTooLongOrHandleNotSync)].Invoke();
            }
        }

        public static String IO_FixedCapacity
        {
            get
            {
                return Handlers[nameof(IO_FixedCapacity)].Invoke();
            }
        }

        public static String IO_InvalidStringLen_Len
        {
            get
            {
                return Handlers[nameof(IO_InvalidStringLen_Len)].Invoke();
            }
        }

        public static String IO_SeekAppendOverwrite
        {
            get
            {
                return Handlers[nameof(IO_SeekAppendOverwrite)].Invoke();
            }
        }

        public static String IO_SeekBeforeBegin
        {
            get
            {
                return Handlers[nameof(IO_SeekBeforeBegin)].Invoke();
            }
        }

        public static String IO_SetLengthAppendTruncate
        {
            get
            {
                return Handlers[nameof(IO_SetLengthAppendTruncate)].Invoke();
            }
        }

        public static String IO_SharingViolation_File
        {
            get
            {
                return Handlers[nameof(IO_SharingViolation_File)].Invoke();
            }
        }

        public static String IO_SharingViolation_NoFileName
        {
            get
            {
                return Handlers[nameof(IO_SharingViolation_NoFileName)].Invoke();
            }
        }

        public static String IO_StreamTooLong
        {
            get
            {
                return Handlers[nameof(IO_StreamTooLong)].Invoke();
            }
        }

        public static String IO_PathNotFound_NoPathName
        {
            get
            {
                return Handlers[nameof(IO_PathNotFound_NoPathName)].Invoke();
            }
        }

        public static String IO_PathNotFound_Path
        {
            get
            {
                return Handlers[nameof(IO_PathNotFound_Path)].Invoke();
            }
        }

        public static String IO_PathTooLong
        {
            get
            {
                return Handlers[nameof(IO_PathTooLong)].Invoke();
            }
        }

        public static String IO_PathTooLong_Path
        {
            get
            {
                return Handlers[nameof(IO_PathTooLong_Path)].Invoke();
            }
        }

        public static String IO_UnknownFileName
        {
            get
            {
                return Handlers[nameof(IO_UnknownFileName)].Invoke();
            }
        }

        public static String Lazy_CreateValue_NoParameterlessCtorForT
        {
            get
            {
                return Handlers[nameof(Lazy_CreateValue_NoParameterlessCtorForT)].Invoke();
            }
        }

        public static String Lazy_ctor_ModeInvalid
        {
            get
            {
                return Handlers[nameof(Lazy_ctor_ModeInvalid)].Invoke();
            }
        }

        public static String Lazy_StaticInit_InvalidOperation
        {
            get
            {
                return Handlers[nameof(Lazy_StaticInit_InvalidOperation)].Invoke();
            }
        }

        public static String Lazy_ToString_ValueNotCreated
        {
            get
            {
                return Handlers[nameof(Lazy_ToString_ValueNotCreated)].Invoke();
            }
        }

        public static String Lazy_Value_RecursiveCallsToValue
        {
            get
            {
                return Handlers[nameof(Lazy_Value_RecursiveCallsToValue)].Invoke();
            }
        }

        public static String ManualResetEventSlim_ctor_SpinCountOutOfRange
        {
            get
            {
                return Handlers[nameof(ManualResetEventSlim_ctor_SpinCountOutOfRange)].Invoke();
            }
        }

        public static String ManualResetEventSlim_ctor_TooManyWaiters
        {
            get
            {
                return Handlers[nameof(ManualResetEventSlim_ctor_TooManyWaiters)].Invoke();
            }
        }

        public static String ManualResetEventSlim_Disposed
        {
            get
            {
                return Handlers[nameof(ManualResetEventSlim_Disposed)].Invoke();
            }
        }

        public static String Marshaler_StringTooLong
        {
            get
            {
                return Handlers[nameof(Marshaler_StringTooLong)].Invoke();
            }
        }

        public static String MissingConstructor_Name
        {
            get
            {
                return Handlers[nameof(MissingConstructor_Name)].Invoke();
            }
        }

        public static String MissingField
        {
            get
            {
                return Handlers[nameof(MissingField)].Invoke();
            }
        }

        public static String MissingField_Name
        {
            get
            {
                return Handlers[nameof(MissingField_Name)].Invoke();
            }
        }

        public static String MissingManifestResource_MultipleBlobs
        {
            get
            {
                return Handlers[nameof(MissingManifestResource_MultipleBlobs)].Invoke();
            }
        }

        public static String MissingManifestResource_NoNeutralAsm
        {
            get
            {
                return Handlers[nameof(MissingManifestResource_NoNeutralAsm)].Invoke();
            }
        }

        public static String MissingManifestResource_NoNeutralDisk
        {
            get
            {
                return Handlers[nameof(MissingManifestResource_NoNeutralDisk)].Invoke();
            }
        }

        public static String MissingMember
        {
            get
            {
                return Handlers[nameof(MissingMember)].Invoke();
            }
        }

        public static String MissingMember_Name
        {
            get
            {
                return Handlers[nameof(MissingMember_Name)].Invoke();
            }
        }

        public static String MissingMemberNestErr
        {
            get
            {
                return Handlers[nameof(MissingMemberNestErr)].Invoke();
            }
        }

        public static String MissingMemberTypeRef
        {
            get
            {
                return Handlers[nameof(MissingMemberTypeRef)].Invoke();
            }
        }

        public static String MissingMethod_Name
        {
            get
            {
                return Handlers[nameof(MissingMethod_Name)].Invoke();
            }
        }

        public static String MissingSatelliteAssembly_Culture_Name
        {
            get
            {
                return Handlers[nameof(MissingSatelliteAssembly_Culture_Name)].Invoke();
            }
        }

        public static String MissingSatelliteAssembly_Default
        {
            get
            {
                return Handlers[nameof(MissingSatelliteAssembly_Default)].Invoke();
            }
        }

        public static String Multicast_Combine
        {
            get
            {
                return Handlers[nameof(Multicast_Combine)].Invoke();
            }
        }

        public static String MustUseCCRewrite
        {
            get
            {
                return Handlers[nameof(MustUseCCRewrite)].Invoke();
            }
        }

        public static String NotSupported_AbstractNonCLS
        {
            get
            {
                return Handlers[nameof(NotSupported_AbstractNonCLS)].Invoke();
            }
        }

        public static String NotSupported_ActivAttr
        {
            get
            {
                return Handlers[nameof(NotSupported_ActivAttr)].Invoke();
            }
        }

        public static String NotSupported_AssemblyLoadFromHash
        {
            get
            {
                return Handlers[nameof(NotSupported_AssemblyLoadFromHash)].Invoke();
            }
        }

        public static String NotSupported_ByRefToByRefLikeReturn
        {
            get
            {
                return Handlers[nameof(NotSupported_ByRefToByRefLikeReturn)].Invoke();
            }
        }

        public static String NotSupported_ByRefToVoidReturn
        {
            get
            {
                return Handlers[nameof(NotSupported_ByRefToVoidReturn)].Invoke();
            }
        }

        public static String NotSupported_CallToVarArg
        {
            get
            {
                return Handlers[nameof(NotSupported_CallToVarArg)].Invoke();
            }
        }

        public static String NotSupported_CannotCallEqualsOnSpan
        {
            get
            {
                return Handlers[nameof(NotSupported_CannotCallEqualsOnSpan)].Invoke();
            }
        }

        public static String NotSupported_CannotCallGetHashCodeOnSpan
        {
            get
            {
                return Handlers[nameof(NotSupported_CannotCallGetHashCodeOnSpan)].Invoke();
            }
        }

        public static String NotSupported_ChangeType
        {
            get
            {
                return Handlers[nameof(NotSupported_ChangeType)].Invoke();
            }
        }

        public static String NotSupported_CreateInstanceWithTypeBuilder
        {
            get
            {
                return Handlers[nameof(NotSupported_CreateInstanceWithTypeBuilder)].Invoke();
            }
        }

        public static String NotSupported_DBNullSerial
        {
            get
            {
                return Handlers[nameof(NotSupported_DBNullSerial)].Invoke();
            }
        }

        public static String NotSupported_DynamicAssembly
        {
            get
            {
                return Handlers[nameof(NotSupported_DynamicAssembly)].Invoke();
            }
        }

        public static String NotSupported_DynamicMethodFlags
        {
            get
            {
                return Handlers[nameof(NotSupported_DynamicMethodFlags)].Invoke();
            }
        }

        public static String NotSupported_DynamicModule
        {
            get
            {
                return Handlers[nameof(NotSupported_DynamicModule)].Invoke();
            }
        }

        public static String NotSupported_FileStreamOnNonFiles
        {
            get
            {
                return Handlers[nameof(NotSupported_FileStreamOnNonFiles)].Invoke();
            }
        }

        public static String NotSupported_FixedSizeCollection
        {
            get
            {
                return Handlers[nameof(NotSupported_FixedSizeCollection)].Invoke();
            }
        }

        public static String InvalidOperation_SpanOverlappedOperation
        {
            get
            {
                return Handlers[nameof(InvalidOperation_SpanOverlappedOperation)].Invoke();
            }
        }

        public static String NotSupported_IllegalOneByteBranch
        {
            get
            {
                return Handlers[nameof(NotSupported_IllegalOneByteBranch)].Invoke();
            }
        }

        public static String NotSupported_KeyCollectionSet
        {
            get
            {
                return Handlers[nameof(NotSupported_KeyCollectionSet)].Invoke();
            }
        }

        public static String NotSupported_MaxWaitHandles
        {
            get
            {
                return Handlers[nameof(NotSupported_MaxWaitHandles)].Invoke();
            }
        }

        public static String NotSupported_MemStreamNotExpandable
        {
            get
            {
                return Handlers[nameof(NotSupported_MemStreamNotExpandable)].Invoke();
            }
        }

        public static String NotSupported_MustBeModuleBuilder
        {
            get
            {
                return Handlers[nameof(NotSupported_MustBeModuleBuilder)].Invoke();
            }
        }

        public static String NotSupported_NoCodepageData
        {
            get
            {
                return Handlers[nameof(NotSupported_NoCodepageData)].Invoke();
            }
        }

        public static String InvalidOperation_FunctionMissingUnmanagedCallersOnly
        {
            get
            {
                return Handlers[nameof(InvalidOperation_FunctionMissingUnmanagedCallersOnly)].Invoke();
            }
        }

        public static String NotSupported_NonReflectedType
        {
            get
            {
                return Handlers[nameof(NotSupported_NonReflectedType)].Invoke();
            }
        }

        public static String NotSupported_NoParentDefaultConstructor
        {
            get
            {
                return Handlers[nameof(NotSupported_NoParentDefaultConstructor)].Invoke();
            }
        }

        public static String NotSupported_NoTypeInfo
        {
            get
            {
                return Handlers[nameof(NotSupported_NoTypeInfo)].Invoke();
            }
        }

        public static String NotSupported_NYI
        {
            get
            {
                return Handlers[nameof(NotSupported_NYI)].Invoke();
            }
        }

        public static String NotSupported_ObsoleteResourcesFile
        {
            get
            {
                return Handlers[nameof(NotSupported_ObsoleteResourcesFile)].Invoke();
            }
        }

        public static String NotSupported_OutputStreamUsingTypeBuilder
        {
            get
            {
                return Handlers[nameof(NotSupported_OutputStreamUsingTypeBuilder)].Invoke();
            }
        }

        public static String NotSupported_RangeCollection
        {
            get
            {
                return Handlers[nameof(NotSupported_RangeCollection)].Invoke();
            }
        }

        public static String NotSupported_Reading
        {
            get
            {
                return Handlers[nameof(NotSupported_Reading)].Invoke();
            }
        }

        public static String NotSupported_ReadOnlyCollection
        {
            get
            {
                return Handlers[nameof(NotSupported_ReadOnlyCollection)].Invoke();
            }
        }

        public static String NotSupported_ResourceObjectSerialization
        {
            get
            {
                return Handlers[nameof(NotSupported_ResourceObjectSerialization)].Invoke();
            }
        }

        public static String NotSupported_StringComparison
        {
            get
            {
                return Handlers[nameof(NotSupported_StringComparison)].Invoke();
            }
        }

        public static String NotSupported_SubclassOverride
        {
            get
            {
                return Handlers[nameof(NotSupported_SubclassOverride)].Invoke();
            }
        }

        public static String NotSupported_SymbolMethod
        {
            get
            {
                return Handlers[nameof(NotSupported_SymbolMethod)].Invoke();
            }
        }

        public static String NotSupported_Type
        {
            get
            {
                return Handlers[nameof(NotSupported_Type)].Invoke();
            }
        }

        public static String NotSupported_TypeNotYetCreated
        {
            get
            {
                return Handlers[nameof(NotSupported_TypeNotYetCreated)].Invoke();
            }
        }

        public static String NotSupported_UmsSafeBuffer
        {
            get
            {
                return Handlers[nameof(NotSupported_UmsSafeBuffer)].Invoke();
            }
        }

        public static String NotSupported_UnitySerHolder
        {
            get
            {
                return Handlers[nameof(NotSupported_UnitySerHolder)].Invoke();
            }
        }

        public static String NotSupported_UnknownTypeCode
        {
            get
            {
                return Handlers[nameof(NotSupported_UnknownTypeCode)].Invoke();
            }
        }

        public static String NotSupported_UnreadableStream
        {
            get
            {
                return Handlers[nameof(NotSupported_UnreadableStream)].Invoke();
            }
        }

        public static String NotSupported_UnseekableStream
        {
            get
            {
                return Handlers[nameof(NotSupported_UnseekableStream)].Invoke();
            }
        }

        public static String NotSupported_UnwritableStream
        {
            get
            {
                return Handlers[nameof(NotSupported_UnwritableStream)].Invoke();
            }
        }

        public static String NotSupported_ValueCollectionSet
        {
            get
            {
                return Handlers[nameof(NotSupported_ValueCollectionSet)].Invoke();
            }
        }

        public static String NotSupported_Writing
        {
            get
            {
                return Handlers[nameof(NotSupported_Writing)].Invoke();
            }
        }

        public static String NotSupported_WrongResourceReader_Type
        {
            get
            {
                return Handlers[nameof(NotSupported_WrongResourceReader_Type)].Invoke();
            }
        }

        public static String ObjectDisposed_FileClosed
        {
            get
            {
                return Handlers[nameof(ObjectDisposed_FileClosed)].Invoke();
            }
        }

        public static String ObjectDisposed_Generic
        {
            get
            {
                return Handlers[nameof(ObjectDisposed_Generic)].Invoke();
            }
        }

        public static String ObjectDisposed_ObjectName_Name
        {
            get
            {
                return Handlers[nameof(ObjectDisposed_ObjectName_Name)].Invoke();
            }
        }

        public static String ObjectDisposed_WriterClosed
        {
            get
            {
                return Handlers[nameof(ObjectDisposed_WriterClosed)].Invoke();
            }
        }

        public static String ObjectDisposed_ReaderClosed
        {
            get
            {
                return Handlers[nameof(ObjectDisposed_ReaderClosed)].Invoke();
            }
        }

        public static String ObjectDisposed_ResourceSet
        {
            get
            {
                return Handlers[nameof(ObjectDisposed_ResourceSet)].Invoke();
            }
        }

        public static String ObjectDisposed_StreamClosed
        {
            get
            {
                return Handlers[nameof(ObjectDisposed_StreamClosed)].Invoke();
            }
        }

        public static String ObjectDisposed_ViewAccessorClosed
        {
            get
            {
                return Handlers[nameof(ObjectDisposed_ViewAccessorClosed)].Invoke();
            }
        }

        public static String ObjectDisposed_SafeHandleClosed
        {
            get
            {
                return Handlers[nameof(ObjectDisposed_SafeHandleClosed)].Invoke();
            }
        }

        public static String OperationCanceled
        {
            get
            {
                return Handlers[nameof(OperationCanceled)].Invoke();
            }
        }

        public static String Overflow_Byte
        {
            get
            {
                return Handlers[nameof(Overflow_Byte)].Invoke();
            }
        }

        public static String Overflow_Char
        {
            get
            {
                return Handlers[nameof(Overflow_Char)].Invoke();
            }
        }

        public static String Overflow_Currency
        {
            get
            {
                return Handlers[nameof(Overflow_Currency)].Invoke();
            }
        }

        public static String Overflow_Decimal
        {
            get
            {
                return Handlers[nameof(Overflow_Decimal)].Invoke();
            }
        }

        public static String Overflow_Duration
        {
            get
            {
                return Handlers[nameof(Overflow_Duration)].Invoke();
            }
        }

        public static String Overflow_Int16
        {
            get
            {
                return Handlers[nameof(Overflow_Int16)].Invoke();
            }
        }

        public static String Overflow_Int32
        {
            get
            {
                return Handlers[nameof(Overflow_Int32)].Invoke();
            }
        }

        public static String Overflow_Int64
        {
            get
            {
                return Handlers[nameof(Overflow_Int64)].Invoke();
            }
        }

        public static String Overflow_NegateTwosCompNum
        {
            get
            {
                return Handlers[nameof(Overflow_NegateTwosCompNum)].Invoke();
            }
        }

        public static String Overflow_NegativeUnsigned
        {
            get
            {
                return Handlers[nameof(Overflow_NegativeUnsigned)].Invoke();
            }
        }

        public static String Overflow_SByte
        {
            get
            {
                return Handlers[nameof(Overflow_SByte)].Invoke();
            }
        }

        public static String Overflow_TimeSpanElementTooLarge
        {
            get
            {
                return Handlers[nameof(Overflow_TimeSpanElementTooLarge)].Invoke();
            }
        }

        public static String Overflow_TimeSpanTooLong
        {
            get
            {
                return Handlers[nameof(Overflow_TimeSpanTooLong)].Invoke();
            }
        }

        public static String Overflow_UInt16
        {
            get
            {
                return Handlers[nameof(Overflow_UInt16)].Invoke();
            }
        }

        public static String Overflow_UInt32
        {
            get
            {
                return Handlers[nameof(Overflow_UInt32)].Invoke();
            }
        }

        public static String Overflow_UInt64
        {
            get
            {
                return Handlers[nameof(Overflow_UInt64)].Invoke();
            }
        }

        public static String PlatformNotSupported_ReflectionOnly
        {
            get
            {
                return Handlers[nameof(PlatformNotSupported_ReflectionOnly)].Invoke();
            }
        }

        public static String PlatformNotSupported_Remoting
        {
            get
            {
                return Handlers[nameof(PlatformNotSupported_Remoting)].Invoke();
            }
        }

        public static String PlatformNotSupported_SecureBinarySerialization
        {
            get
            {
                return Handlers[nameof(PlatformNotSupported_SecureBinarySerialization)].Invoke();
            }
        }

        public static String PlatformNotSupported_StrongNameSigning
        {
            get
            {
                return Handlers[nameof(PlatformNotSupported_StrongNameSigning)].Invoke();
            }
        }

        public static String PlatformNotSupported_ITypeInfo
        {
            get
            {
                return Handlers[nameof(PlatformNotSupported_ITypeInfo)].Invoke();
            }
        }

        public static String PlatformNotSupported_IExpando
        {
            get
            {
                return Handlers[nameof(PlatformNotSupported_IExpando)].Invoke();
            }
        }

        public static String PlatformNotSupported_AppDomains
        {
            get
            {
                return Handlers[nameof(PlatformNotSupported_AppDomains)].Invoke();
            }
        }

        public static String PlatformNotSupported_CAS
        {
            get
            {
                return Handlers[nameof(PlatformNotSupported_CAS)].Invoke();
            }
        }

        public static String PlatformNotSupported_Principal
        {
            get
            {
                return Handlers[nameof(PlatformNotSupported_Principal)].Invoke();
            }
        }

        public static String PlatformNotSupported_ThreadAbort
        {
            get
            {
                return Handlers[nameof(PlatformNotSupported_ThreadAbort)].Invoke();
            }
        }

        public static String PlatformNotSupported_ThreadSuspend
        {
            get
            {
                return Handlers[nameof(PlatformNotSupported_ThreadSuspend)].Invoke();
            }
        }

        public static String PostconditionFailed
        {
            get
            {
                return Handlers[nameof(PostconditionFailed)].Invoke();
            }
        }

        public static String PostconditionFailed_Cnd
        {
            get
            {
                return Handlers[nameof(PostconditionFailed_Cnd)].Invoke();
            }
        }

        public static String PostconditionOnExceptionFailed
        {
            get
            {
                return Handlers[nameof(PostconditionOnExceptionFailed)].Invoke();
            }
        }

        public static String PostconditionOnExceptionFailed_Cnd
        {
            get
            {
                return Handlers[nameof(PostconditionOnExceptionFailed_Cnd)].Invoke();
            }
        }

        public static String PreconditionFailed
        {
            get
            {
                return Handlers[nameof(PreconditionFailed)].Invoke();
            }
        }

        public static String PreconditionFailed_Cnd
        {
            get
            {
                return Handlers[nameof(PreconditionFailed_Cnd)].Invoke();
            }
        }

        public static String Rank_MultiDimNotSupported
        {
            get
            {
                return Handlers[nameof(Rank_MultiDimNotSupported)].Invoke();
            }
        }

        public static String Rank_MustMatch
        {
            get
            {
                return Handlers[nameof(Rank_MustMatch)].Invoke();
            }
        }

        public static String ResourceReaderIsClosed
        {
            get
            {
                return Handlers[nameof(ResourceReaderIsClosed)].Invoke();
            }
        }

        public static String Resources_StreamNotValid
        {
            get
            {
                return Handlers[nameof(Resources_StreamNotValid)].Invoke();
            }
        }

        public static String RFLCT_AmbigCust
        {
            get
            {
                return Handlers[nameof(RFLCT_AmbigCust)].Invoke();
            }
        }

        public static String RFLCT_Ambiguous
        {
            get
            {
                return Handlers[nameof(RFLCT_Ambiguous)].Invoke();
            }
        }

        public static String InvalidFilterCriteriaException_CritInt
        {
            get
            {
                return Handlers[nameof(InvalidFilterCriteriaException_CritInt)].Invoke();
            }
        }

        public static String InvalidFilterCriteriaException_CritString
        {
            get
            {
                return Handlers[nameof(InvalidFilterCriteriaException_CritString)].Invoke();
            }
        }

        public static String RFLCT_InvalidFieldFail
        {
            get
            {
                return Handlers[nameof(RFLCT_InvalidFieldFail)].Invoke();
            }
        }

        public static String RFLCT_InvalidPropFail
        {
            get
            {
                return Handlers[nameof(RFLCT_InvalidPropFail)].Invoke();
            }
        }

        public static String RFLCT_Targ_ITargMismatch
        {
            get
            {
                return Handlers[nameof(RFLCT_Targ_ITargMismatch)].Invoke();
            }
        }

        public static String RFLCT_Targ_StatFldReqTarg
        {
            get
            {
                return Handlers[nameof(RFLCT_Targ_StatFldReqTarg)].Invoke();
            }
        }

        public static String RFLCT_Targ_StatMethReqTarg
        {
            get
            {
                return Handlers[nameof(RFLCT_Targ_StatMethReqTarg)].Invoke();
            }
        }

        public static String RuntimeWrappedException
        {
            get
            {
                return Handlers[nameof(RuntimeWrappedException)].Invoke();
            }
        }

        public static String StandardOleMarshalObjectGetMarshalerFailed
        {
            get
            {
                return Handlers[nameof(StandardOleMarshalObjectGetMarshalerFailed)].Invoke();
            }
        }

        public static String Security_CannotReadRegistryData
        {
            get
            {
                return Handlers[nameof(Security_CannotReadRegistryData)].Invoke();
            }
        }

        public static String Security_RegistryPermission
        {
            get
            {
                return Handlers[nameof(Security_RegistryPermission)].Invoke();
            }
        }

        public static String SemaphoreSlim_ctor_InitialCountWrong
        {
            get
            {
                return Handlers[nameof(SemaphoreSlim_ctor_InitialCountWrong)].Invoke();
            }
        }

        public static String SemaphoreSlim_ctor_MaxCountWrong
        {
            get
            {
                return Handlers[nameof(SemaphoreSlim_ctor_MaxCountWrong)].Invoke();
            }
        }

        public static String SemaphoreSlim_Disposed
        {
            get
            {
                return Handlers[nameof(SemaphoreSlim_Disposed)].Invoke();
            }
        }

        public static String SemaphoreSlim_Release_CountWrong
        {
            get
            {
                return Handlers[nameof(SemaphoreSlim_Release_CountWrong)].Invoke();
            }
        }

        public static String SemaphoreSlim_Wait_TimeoutWrong
        {
            get
            {
                return Handlers[nameof(SemaphoreSlim_Wait_TimeoutWrong)].Invoke();
            }
        }

        public static String Serialization_BadParameterInfo
        {
            get
            {
                return Handlers[nameof(Serialization_BadParameterInfo)].Invoke();
            }
        }

        public static String Serialization_CorruptField
        {
            get
            {
                return Handlers[nameof(Serialization_CorruptField)].Invoke();
            }
        }

        public static String Serialization_DateTimeTicksOutOfRange
        {
            get
            {
                return Handlers[nameof(Serialization_DateTimeTicksOutOfRange)].Invoke();
            }
        }

        public static String Serialization_DelegatesNotSupported
        {
            get
            {
                return Handlers[nameof(Serialization_DelegatesNotSupported)].Invoke();
            }
        }

        public static String Serialization_InsufficientState
        {
            get
            {
                return Handlers[nameof(Serialization_InsufficientState)].Invoke();
            }
        }

        public static String Serialization_InvalidData
        {
            get
            {
                return Handlers[nameof(Serialization_InvalidData)].Invoke();
            }
        }

        public static String Serialization_InvalidEscapeSequence
        {
            get
            {
                return Handlers[nameof(Serialization_InvalidEscapeSequence)].Invoke();
            }
        }

        public static String Serialization_InvalidOnDeser
        {
            get
            {
                return Handlers[nameof(Serialization_InvalidOnDeser)].Invoke();
            }
        }

        public static String Serialization_InvalidType
        {
            get
            {
                return Handlers[nameof(Serialization_InvalidType)].Invoke();
            }
        }

        public static String Serialization_KeyValueDifferentSizes
        {
            get
            {
                return Handlers[nameof(Serialization_KeyValueDifferentSizes)].Invoke();
            }
        }

        public static String Serialization_MissingDateTimeData
        {
            get
            {
                return Handlers[nameof(Serialization_MissingDateTimeData)].Invoke();
            }
        }

        public static String Serialization_MissingKeys
        {
            get
            {
                return Handlers[nameof(Serialization_MissingKeys)].Invoke();
            }
        }

        public static String Serialization_MissingValues
        {
            get
            {
                return Handlers[nameof(Serialization_MissingValues)].Invoke();
            }
        }

        public static String Serialization_NoParameterInfo
        {
            get
            {
                return Handlers[nameof(Serialization_NoParameterInfo)].Invoke();
            }
        }

        public static String Serialization_NotFound
        {
            get
            {
                return Handlers[nameof(Serialization_NotFound)].Invoke();
            }
        }

        public static String Serialization_NullKey
        {
            get
            {
                return Handlers[nameof(Serialization_NullKey)].Invoke();
            }
        }

        public static String Serialization_OptionalFieldVersionValue
        {
            get
            {
                return Handlers[nameof(Serialization_OptionalFieldVersionValue)].Invoke();
            }
        }

        public static String Serialization_SameNameTwice
        {
            get
            {
                return Handlers[nameof(Serialization_SameNameTwice)].Invoke();
            }
        }

        public static String Serialization_StringBuilderCapacity
        {
            get
            {
                return Handlers[nameof(Serialization_StringBuilderCapacity)].Invoke();
            }
        }

        public static String Serialization_StringBuilderMaxCapacity
        {
            get
            {
                return Handlers[nameof(Serialization_StringBuilderMaxCapacity)].Invoke();
            }
        }

        public static String SpinLock_Exit_SynchronizationLockException
        {
            get
            {
                return Handlers[nameof(SpinLock_Exit_SynchronizationLockException)].Invoke();
            }
        }

        public static String SpinLock_IsHeldByCurrentThread
        {
            get
            {
                return Handlers[nameof(SpinLock_IsHeldByCurrentThread)].Invoke();
            }
        }

        public static String SpinLock_TryEnter_ArgumentOutOfRange
        {
            get
            {
                return Handlers[nameof(SpinLock_TryEnter_ArgumentOutOfRange)].Invoke();
            }
        }

        public static String SpinLock_TryEnter_LockRecursionException
        {
            get
            {
                return Handlers[nameof(SpinLock_TryEnter_LockRecursionException)].Invoke();
            }
        }

        public static String SpinLock_TryReliableEnter_ArgumentException
        {
            get
            {
                return Handlers[nameof(SpinLock_TryReliableEnter_ArgumentException)].Invoke();
            }
        }

        public static String SpinWait_SpinUntil_ArgumentNull
        {
            get
            {
                return Handlers[nameof(SpinWait_SpinUntil_ArgumentNull)].Invoke();
            }
        }

        public static String SpinWait_SpinUntil_TimeoutWrong
        {
            get
            {
                return Handlers[nameof(SpinWait_SpinUntil_TimeoutWrong)].Invoke();
            }
        }

        public static String Task_ContinueWith_ESandLR
        {
            get
            {
                return Handlers[nameof(Task_ContinueWith_ESandLR)].Invoke();
            }
        }

        public static String Task_ContinueWith_NotOnAnything
        {
            get
            {
                return Handlers[nameof(Task_ContinueWith_NotOnAnything)].Invoke();
            }
        }

        public static String Task_Delay_InvalidDelay
        {
            get
            {
                return Handlers[nameof(Task_Delay_InvalidDelay)].Invoke();
            }
        }

        public static String Task_Delay_InvalidMillisecondsDelay
        {
            get
            {
                return Handlers[nameof(Task_Delay_InvalidMillisecondsDelay)].Invoke();
            }
        }

        public static String Task_Dispose_NotCompleted
        {
            get
            {
                return Handlers[nameof(Task_Dispose_NotCompleted)].Invoke();
            }
        }

        public static String Task_FromAsync_LongRunning
        {
            get
            {
                return Handlers[nameof(Task_FromAsync_LongRunning)].Invoke();
            }
        }

        public static String Task_FromAsync_PreferFairness
        {
            get
            {
                return Handlers[nameof(Task_FromAsync_PreferFairness)].Invoke();
            }
        }

        public static String Task_MultiTaskContinuation_EmptyTaskList
        {
            get
            {
                return Handlers[nameof(Task_MultiTaskContinuation_EmptyTaskList)].Invoke();
            }
        }

        public static String Task_MultiTaskContinuation_FireOptions
        {
            get
            {
                return Handlers[nameof(Task_MultiTaskContinuation_FireOptions)].Invoke();
            }
        }

        public static String Task_MultiTaskContinuation_NullTask
        {
            get
            {
                return Handlers[nameof(Task_MultiTaskContinuation_NullTask)].Invoke();
            }
        }

        public static String Task_RunSynchronously_AlreadyStarted
        {
            get
            {
                return Handlers[nameof(Task_RunSynchronously_AlreadyStarted)].Invoke();
            }
        }

        public static String Task_RunSynchronously_Continuation
        {
            get
            {
                return Handlers[nameof(Task_RunSynchronously_Continuation)].Invoke();
            }
        }

        public static String Task_RunSynchronously_Promise
        {
            get
            {
                return Handlers[nameof(Task_RunSynchronously_Promise)].Invoke();
            }
        }

        public static String Task_RunSynchronously_TaskCompleted
        {
            get
            {
                return Handlers[nameof(Task_RunSynchronously_TaskCompleted)].Invoke();
            }
        }

        public static String Task_Start_AlreadyStarted
        {
            get
            {
                return Handlers[nameof(Task_Start_AlreadyStarted)].Invoke();
            }
        }

        public static String Task_Start_ContinuationTask
        {
            get
            {
                return Handlers[nameof(Task_Start_ContinuationTask)].Invoke();
            }
        }

        public static String Task_Start_Promise
        {
            get
            {
                return Handlers[nameof(Task_Start_Promise)].Invoke();
            }
        }

        public static String Task_Start_TaskCompleted
        {
            get
            {
                return Handlers[nameof(Task_Start_TaskCompleted)].Invoke();
            }
        }

        public static String Task_ThrowIfDisposed
        {
            get
            {
                return Handlers[nameof(Task_ThrowIfDisposed)].Invoke();
            }
        }

        public static String Task_WaitMulti_NullTask
        {
            get
            {
                return Handlers[nameof(Task_WaitMulti_NullTask)].Invoke();
            }
        }

        public static String TaskCanceledException_ctor_DefaultMessage
        {
            get
            {
                return Handlers[nameof(TaskCanceledException_ctor_DefaultMessage)].Invoke();
            }
        }

        public static String TaskCompletionSourceT_TrySetException_NoExceptions
        {
            get
            {
                return Handlers[nameof(TaskCompletionSourceT_TrySetException_NoExceptions)].Invoke();
            }
        }

        public static String TaskCompletionSourceT_TrySetException_NullException
        {
            get
            {
                return Handlers[nameof(TaskCompletionSourceT_TrySetException_NullException)].Invoke();
            }
        }

        public static String TaskExceptionHolder_UnhandledException
        {
            get
            {
                return Handlers[nameof(TaskExceptionHolder_UnhandledException)].Invoke();
            }
        }

        public static String TaskExceptionHolder_UnknownExceptionType
        {
            get
            {
                return Handlers[nameof(TaskExceptionHolder_UnknownExceptionType)].Invoke();
            }
        }

        public static String TaskScheduler_ExecuteTask_WrongTaskScheduler
        {
            get
            {
                return Handlers[nameof(TaskScheduler_ExecuteTask_WrongTaskScheduler)].Invoke();
            }
        }

        public static String TaskScheduler_FromCurrentSynchronizationContext_NoCurrent
        {
            get
            {
                return Handlers[nameof(TaskScheduler_FromCurrentSynchronizationContext_NoCurrent)].Invoke();
            }
        }

        public static String TaskScheduler_InconsistentStateAfterTryExecuteTaskInline
        {
            get
            {
                return Handlers[nameof(TaskScheduler_InconsistentStateAfterTryExecuteTaskInline)].Invoke();
            }
        }

        public static String TaskSchedulerException_ctor_DefaultMessage
        {
            get
            {
                return Handlers[nameof(TaskSchedulerException_ctor_DefaultMessage)].Invoke();
            }
        }

        public static String TaskT_DebuggerNoResult
        {
            get
            {
                return Handlers[nameof(TaskT_DebuggerNoResult)].Invoke();
            }
        }

        public static String TaskT_TransitionToFinal_AlreadyCompleted
        {
            get
            {
                return Handlers[nameof(TaskT_TransitionToFinal_AlreadyCompleted)].Invoke();
            }
        }

        public static String Thread_ApartmentState_ChangeFailed
        {
            get
            {
                return Handlers[nameof(Thread_ApartmentState_ChangeFailed)].Invoke();
            }
        }

        public static String Thread_GetSetCompressedStack_NotSupported
        {
            get
            {
                return Handlers[nameof(Thread_GetSetCompressedStack_NotSupported)].Invoke();
            }
        }

        public static String Thread_Operation_RequiresCurrentThread
        {
            get
            {
                return Handlers[nameof(Thread_Operation_RequiresCurrentThread)].Invoke();
            }
        }

        public static String Threading_AbandonedMutexException
        {
            get
            {
                return Handlers[nameof(Threading_AbandonedMutexException)].Invoke();
            }
        }

        public static String Threading_WaitHandleCannotBeOpenedException
        {
            get
            {
                return Handlers[nameof(Threading_WaitHandleCannotBeOpenedException)].Invoke();
            }
        }

        public static String Threading_WaitHandleCannotBeOpenedException_InvalidHandle
        {
            get
            {
                return Handlers[nameof(Threading_WaitHandleCannotBeOpenedException_InvalidHandle)].Invoke();
            }
        }

        public static String Threading_WaitHandleTooManyPosts
        {
            get
            {
                return Handlers[nameof(Threading_WaitHandleTooManyPosts)].Invoke();
            }
        }

        public static String Threading_SemaphoreFullException
        {
            get
            {
                return Handlers[nameof(Threading_SemaphoreFullException)].Invoke();
            }
        }

        public static String ThreadLocal_Disposed
        {
            get
            {
                return Handlers[nameof(ThreadLocal_Disposed)].Invoke();
            }
        }

        public static String ThreadLocal_Value_RecursiveCallsToValue
        {
            get
            {
                return Handlers[nameof(ThreadLocal_Value_RecursiveCallsToValue)].Invoke();
            }
        }

        public static String ThreadLocal_ValuesNotAvailable
        {
            get
            {
                return Handlers[nameof(ThreadLocal_ValuesNotAvailable)].Invoke();
            }
        }

        public static String TimeZoneNotFound_MissingData
        {
            get
            {
                return Handlers[nameof(TimeZoneNotFound_MissingData)].Invoke();
            }
        }

        public static String TypeInitialization_Default
        {
            get
            {
                return Handlers[nameof(TypeInitialization_Default)].Invoke();
            }
        }

        public static String TypeInitialization_Type
        {
            get
            {
                return Handlers[nameof(TypeInitialization_Type)].Invoke();
            }
        }

        public static String TypeLoad_ResolveNestedType
        {
            get
            {
                return Handlers[nameof(TypeLoad_ResolveNestedType)].Invoke();
            }
        }

        public static String TypeLoad_ResolveType
        {
            get
            {
                return Handlers[nameof(TypeLoad_ResolveType)].Invoke();
            }
        }

        public static String TypeLoad_ResolveTypeFromAssembly
        {
            get
            {
                return Handlers[nameof(TypeLoad_ResolveTypeFromAssembly)].Invoke();
            }
        }

        public static String UnauthorizedAccess_IODenied_NoPathName
        {
            get
            {
                return Handlers[nameof(UnauthorizedAccess_IODenied_NoPathName)].Invoke();
            }
        }

        public static String UnauthorizedAccess_IODenied_Path
        {
            get
            {
                return Handlers[nameof(UnauthorizedAccess_IODenied_Path)].Invoke();
            }
        }

        public static String UnauthorizedAccess_MemStreamBuffer
        {
            get
            {
                return Handlers[nameof(UnauthorizedAccess_MemStreamBuffer)].Invoke();
            }
        }

        public static String UnauthorizedAccess_RegistryKeyGeneric_Key
        {
            get
            {
                return Handlers[nameof(UnauthorizedAccess_RegistryKeyGeneric_Key)].Invoke();
            }
        }

        public static String UnknownError_Num
        {
            get
            {
                return Handlers[nameof(UnknownError_Num)].Invoke();
            }
        }

        public static String Verification_Exception
        {
            get
            {
                return Handlers[nameof(Verification_Exception)].Invoke();
            }
        }

        public static String DebugAssertBanner
        {
            get
            {
                return Handlers[nameof(DebugAssertBanner)].Invoke();
            }
        }

        public static String DebugAssertLongMessage
        {
            get
            {
                return Handlers[nameof(DebugAssertLongMessage)].Invoke();
            }
        }

        public static String DebugAssertShortMessage
        {
            get
            {
                return Handlers[nameof(DebugAssertShortMessage)].Invoke();
            }
        }

        public static String LockRecursionException_ReadAfterWriteNotAllowed
        {
            get
            {
                return Handlers[nameof(LockRecursionException_ReadAfterWriteNotAllowed)].Invoke();
            }
        }

        public static String LockRecursionException_RecursiveReadNotAllowed
        {
            get
            {
                return Handlers[nameof(LockRecursionException_RecursiveReadNotAllowed)].Invoke();
            }
        }

        public static String LockRecursionException_RecursiveWriteNotAllowed
        {
            get
            {
                return Handlers[nameof(LockRecursionException_RecursiveWriteNotAllowed)].Invoke();
            }
        }

        public static String LockRecursionException_RecursiveUpgradeNotAllowed
        {
            get
            {
                return Handlers[nameof(LockRecursionException_RecursiveUpgradeNotAllowed)].Invoke();
            }
        }

        public static String LockRecursionException_WriteAfterReadNotAllowed
        {
            get
            {
                return Handlers[nameof(LockRecursionException_WriteAfterReadNotAllowed)].Invoke();
            }
        }

        public static String SynchronizationLockException_MisMatchedUpgrade
        {
            get
            {
                return Handlers[nameof(SynchronizationLockException_MisMatchedUpgrade)].Invoke();
            }
        }

        public static String SynchronizationLockException_MisMatchedRead
        {
            get
            {
                return Handlers[nameof(SynchronizationLockException_MisMatchedRead)].Invoke();
            }
        }

        public static String SynchronizationLockException_IncorrectDispose
        {
            get
            {
                return Handlers[nameof(SynchronizationLockException_IncorrectDispose)].Invoke();
            }
        }

        public static String LockRecursionException_UpgradeAfterReadNotAllowed
        {
            get
            {
                return Handlers[nameof(LockRecursionException_UpgradeAfterReadNotAllowed)].Invoke();
            }
        }

        public static String LockRecursionException_UpgradeAfterWriteNotAllowed
        {
            get
            {
                return Handlers[nameof(LockRecursionException_UpgradeAfterWriteNotAllowed)].Invoke();
            }
        }

        public static String SynchronizationLockException_MisMatchedWrite
        {
            get
            {
                return Handlers[nameof(SynchronizationLockException_MisMatchedWrite)].Invoke();
            }
        }

        public static String NotSupported_SignatureType
        {
            get
            {
                return Handlers[nameof(NotSupported_SignatureType)].Invoke();
            }
        }

        public static String HashCode_HashCodeNotSupported
        {
            get
            {
                return Handlers[nameof(HashCode_HashCodeNotSupported)].Invoke();
            }
        }

        public static String HashCode_EqualityNotSupported
        {
            get
            {
                return Handlers[nameof(HashCode_EqualityNotSupported)].Invoke();
            }
        }

        public static String Arg_TypeNotSupported
        {
            get
            {
                return Handlers[nameof(Arg_TypeNotSupported)].Invoke();
            }
        }

        public static String IO_InvalidReadLength
        {
            get
            {
                return Handlers[nameof(IO_InvalidReadLength)].Invoke();
            }
        }

        public static String Arg_BasePathNotFullyQualified
        {
            get
            {
                return Handlers[nameof(Arg_BasePathNotFullyQualified)].Invoke();
            }
        }

        public static String Arg_ElementsInSourceIsGreaterThanDestination
        {
            get
            {
                return Handlers[nameof(Arg_ElementsInSourceIsGreaterThanDestination)].Invoke();
            }
        }

        public static String Arg_NullArgumentNullRef
        {
            get
            {
                return Handlers[nameof(Arg_NullArgumentNullRef)].Invoke();
            }
        }

        public static String Argument_OverlapAlignmentMismatch
        {
            get
            {
                return Handlers[nameof(Argument_OverlapAlignmentMismatch)].Invoke();
            }
        }

        public static String Arg_InsufficientNumberOfElements
        {
            get
            {
                return Handlers[nameof(Arg_InsufficientNumberOfElements)].Invoke();
            }
        }

        public static String Arg_MustBeNullTerminatedString
        {
            get
            {
                return Handlers[nameof(Arg_MustBeNullTerminatedString)].Invoke();
            }
        }

        public static String ArgumentOutOfRange_Week_ISO
        {
            get
            {
                return Handlers[nameof(ArgumentOutOfRange_Week_ISO)].Invoke();
            }
        }

        public static String Argument_BadPInvokeMethod
        {
            get
            {
                return Handlers[nameof(Argument_BadPInvokeMethod)].Invoke();
            }
        }

        public static String Argument_BadPInvokeOnInterface
        {
            get
            {
                return Handlers[nameof(Argument_BadPInvokeOnInterface)].Invoke();
            }
        }

        public static String Argument_MethodRedefined
        {
            get
            {
                return Handlers[nameof(Argument_MethodRedefined)].Invoke();
            }
        }

        public static String Argument_CannotExtractScalar
        {
            get
            {
                return Handlers[nameof(Argument_CannotExtractScalar)].Invoke();
            }
        }

        public static String Argument_CannotParsePrecision
        {
            get
            {
                return Handlers[nameof(Argument_CannotParsePrecision)].Invoke();
            }
        }

        public static String Argument_GWithPrecisionNotSupported
        {
            get
            {
                return Handlers[nameof(Argument_GWithPrecisionNotSupported)].Invoke();
            }
        }

        public static String Argument_PrecisionTooLarge
        {
            get
            {
                return Handlers[nameof(Argument_PrecisionTooLarge)].Invoke();
            }
        }

        public static String AssemblyDependencyResolver_FailedToLoadHostpolicy
        {
            get
            {
                return Handlers[nameof(AssemblyDependencyResolver_FailedToLoadHostpolicy)].Invoke();
            }
        }

        public static String AssemblyDependencyResolver_FailedToResolveDependencies
        {
            get
            {
                return Handlers[nameof(AssemblyDependencyResolver_FailedToResolveDependencies)].Invoke();
            }
        }

        public static String Arg_EnumNotCloneable
        {
            get
            {
                return Handlers[nameof(Arg_EnumNotCloneable)].Invoke();
            }
        }

        public static String InvalidOp_InvalidNewEnumVariant
        {
            get
            {
                return Handlers[nameof(InvalidOp_InvalidNewEnumVariant)].Invoke();
            }
        }

        public static String Argument_StructArrayTooLarge
        {
            get
            {
                return Handlers[nameof(Argument_StructArrayTooLarge)].Invoke();
            }
        }

        public static String IndexOutOfRange_ArrayWithOffset
        {
            get
            {
                return Handlers[nameof(IndexOutOfRange_ArrayWithOffset)].Invoke();
            }
        }

        public static String Serialization_DangerousDeserialization
        {
            get
            {
                return Handlers[nameof(Serialization_DangerousDeserialization)].Invoke();
            }
        }

        public static String Serialization_DangerousDeserialization_Switch
        {
            get
            {
                return Handlers[nameof(Serialization_DangerousDeserialization_Switch)].Invoke();
            }
        }

        public static String Argument_InvalidStartupHookSimpleAssemblyName
        {
            get
            {
                return Handlers[nameof(Argument_InvalidStartupHookSimpleAssemblyName)].Invoke();
            }
        }

        public static String Argument_StartupHookAssemblyLoadFailed
        {
            get
            {
                return Handlers[nameof(Argument_StartupHookAssemblyLoadFailed)].Invoke();
            }
        }

        public static String InvalidOperation_NonStaticComRegFunction
        {
            get
            {
                return Handlers[nameof(InvalidOperation_NonStaticComRegFunction)].Invoke();
            }
        }

        public static String InvalidOperation_NonStaticComUnRegFunction
        {
            get
            {
                return Handlers[nameof(InvalidOperation_NonStaticComUnRegFunction)].Invoke();
            }
        }

        public static String InvalidOperation_InvalidComRegFunctionSig
        {
            get
            {
                return Handlers[nameof(InvalidOperation_InvalidComRegFunctionSig)].Invoke();
            }
        }

        public static String InvalidOperation_InvalidComUnRegFunctionSig
        {
            get
            {
                return Handlers[nameof(InvalidOperation_InvalidComUnRegFunctionSig)].Invoke();
            }
        }

        public static String InvalidOperation_MultipleComRegFunctions
        {
            get
            {
                return Handlers[nameof(InvalidOperation_MultipleComRegFunctions)].Invoke();
            }
        }

        public static String InvalidOperation_MultipleComUnRegFunctions
        {
            get
            {
                return Handlers[nameof(InvalidOperation_MultipleComUnRegFunctions)].Invoke();
            }
        }

        public static String InvalidOperation_ResetGlobalComWrappersInstance
        {
            get
            {
                return Handlers[nameof(InvalidOperation_ResetGlobalComWrappersInstance)].Invoke();
            }
        }

        public static String Argument_SpansMustHaveSameLength
        {
            get
            {
                return Handlers[nameof(Argument_SpansMustHaveSameLength)].Invoke();
            }
        }

        public static String NotSupported_CannotWriteToBufferedStreamIfReadBufferCannotBeFlushed
        {
            get
            {
                return Handlers[nameof(NotSupported_CannotWriteToBufferedStreamIfReadBufferCannotBeFlushed)].Invoke();
            }
        }

        public static String GenericInvalidData
        {
            get
            {
                return Handlers[nameof(GenericInvalidData)].Invoke();
            }
        }

        public static String Argument_ResourceScopeWrongDirection
        {
            get
            {
                return Handlers[nameof(Argument_ResourceScopeWrongDirection)].Invoke();
            }
        }

        public static String ArgumentNull_TypeRequiredByResourceScope
        {
            get
            {
                return Handlers[nameof(ArgumentNull_TypeRequiredByResourceScope)].Invoke();
            }
        }

        public static String Argument_BadResourceScopeTypeBits
        {
            get
            {
                return Handlers[nameof(Argument_BadResourceScopeTypeBits)].Invoke();
            }
        }

        public static String Argument_BadResourceScopeVisibilityBits
        {
            get
            {
                return Handlers[nameof(Argument_BadResourceScopeVisibilityBits)].Invoke();
            }
        }

        public static String net_emptystringcall
        {
            get
            {
                return Handlers[nameof(net_emptystringcall)].Invoke();
            }
        }

        public static String Argument_EmptyApplicationName
        {
            get
            {
                return Handlers[nameof(Argument_EmptyApplicationName)].Invoke();
            }
        }

        public static String Argument_FrameworkNameInvalid
        {
            get
            {
                return Handlers[nameof(Argument_FrameworkNameInvalid)].Invoke();
            }
        }

        public static String Argument_FrameworkNameInvalidVersion
        {
            get
            {
                return Handlers[nameof(Argument_FrameworkNameInvalidVersion)].Invoke();
            }
        }

        public static String Argument_FrameworkNameMissingVersion
        {
            get
            {
                return Handlers[nameof(Argument_FrameworkNameMissingVersion)].Invoke();
            }
        }

        public static String Argument_FrameworkNameTooShort
        {
            get
            {
                return Handlers[nameof(Argument_FrameworkNameTooShort)].Invoke();
            }
        }

        public static String Arg_SwitchExpressionException
        {
            get
            {
                return Handlers[nameof(Arg_SwitchExpressionException)].Invoke();
            }
        }

        public static String Arg_ContextMarshalException
        {
            get
            {
                return Handlers[nameof(Arg_ContextMarshalException)].Invoke();
            }
        }

        public static String Arg_AppDomainUnloadedException
        {
            get
            {
                return Handlers[nameof(Arg_AppDomainUnloadedException)].Invoke();
            }
        }

        public static String SwitchExpressionException_UnmatchedValue
        {
            get
            {
                return Handlers[nameof(SwitchExpressionException_UnmatchedValue)].Invoke();
            }
        }

        public static String Encoding_UTF7_Disabled
        {
            get
            {
                return Handlers[nameof(Encoding_UTF7_Disabled)].Invoke();
            }
        }

        public static String IDynamicInterfaceCastable_DoesNotImplementRequested
        {
            get
            {
                return Handlers[nameof(IDynamicInterfaceCastable_DoesNotImplementRequested)].Invoke();
            }
        }

        public static String IDynamicInterfaceCastable_MissingImplementationAttribute
        {
            get
            {
                return Handlers[nameof(IDynamicInterfaceCastable_MissingImplementationAttribute)].Invoke();
            }
        }

        public static String IDynamicInterfaceCastable_NotInterface
        {
            get
            {
                return Handlers[nameof(IDynamicInterfaceCastable_NotInterface)].Invoke();
            }
        }

        public static String Arg_MustBeHalf
        {
            get
            {
                return Handlers[nameof(Arg_MustBeHalf)].Invoke();
            }
        }

        public static String Arg_MustBeRune
        {
            get
            {
                return Handlers[nameof(Arg_MustBeRune)].Invoke();
            }
        }

        public static String BinaryFormatter_SerializationDisallowed
        {
            get
            {
                return Handlers[nameof(BinaryFormatter_SerializationDisallowed)].Invoke();
            }
        }

        public static String NotSupported_CodeBase
        {
            get
            {
                return Handlers[nameof(NotSupported_CodeBase)].Invoke();
            }
        }
    }
}
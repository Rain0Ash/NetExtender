// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection;
using NetExtender.Types.Strings;
using NetExtender.Types.Strings.Interfaces;
using NetExtender.Utils.Types;

namespace NetExtender.Resources
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("ReSharper", "UnassignedGetOnlyAutoProperty")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
    public static class SystemResources
    {
        internal delegate String SystemResourceDelegate();

        static SystemResources()
        {
            IImmutableDictionary<String, SystemResourceDelegate> handlers = GetProperties();

            IEnumerable<PropertyInfo> properties = typeof(SystemResources).GetProperties(BindingFlags.Static | BindingFlags.Public)
                .Where(info => info.PropertyType == typeof(IFormatString));
            
            foreach (PropertyInfo info in properties)
            {
                info.SetValue(null, new SystemResourceString(handlers[info.Name]));
            }
        }

        private static IImmutableDictionary<String, SystemResourceDelegate> GetProperties()
        {
            Type type = Assembly.GetAssembly(typeof(String))?.GetType("System.SR") ?? throw new ArgumentNullException(nameof(type));

            PropertyInfo[] properties = type.GetProperties(BindingFlags.Static | BindingFlags.NonPublic);

            return properties.Where(info => info.PropertyType == typeof(String)).ToImmutableDictionary(info => info.Name, GetDelegate);
        }

        private static SystemResourceDelegate GetDelegate(PropertyInfo info)
        {
            if (info is null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            return info.GetMethod?.CreateDelegate<SystemResourceDelegate>() ?? throw new ArgumentException(@"Property not contains get accessor", nameof(info));
        }

        private sealed class SystemResourceString : FormatStringBase
        {
            private SystemResourceDelegate Resource  { get; }

            public override Int32 Arguments  { get; }

            public override String Text
            {
                get
                {
                    return Resource.Invoke();
                }
                protected set
                {
                    throw new NotSupportedException();
                }
            }

            public SystemResourceString(SystemResourceDelegate value)
            {
                Resource = value ?? throw new ArgumentNullException(nameof(value));
                Arguments = Text.CountExpectedFormatArgs();
            }
        }

        public static IFormatString Acc_CreateAbstEx  { get; private set; } = null!;

        public static IFormatString Acc_CreateArgIterator  { get; private set; } = null!;

        public static IFormatString Acc_CreateGenericEx  { get; private set; } = null!;

        public static IFormatString Acc_CreateInterfaceEx  { get; private set; } = null!;

        public static IFormatString Acc_CreateVoid  { get; private set; } = null!;

        public static IFormatString Acc_NotClassInit  { get; private set; } = null!;

        public static IFormatString Acc_ReadOnly  { get; private set; } = null!;

        public static IFormatString Access_Void  { get; private set; } = null!;

        public static IFormatString AggregateException_ctor_DefaultMessage  { get; private set; } = null!;

        public static IFormatString AggregateException_ctor_InnerExceptionNull  { get; private set; } = null!;

        public static IFormatString AggregateException_DeserializationFailure  { get; private set; } = null!;

        public static IFormatString AggregateException_InnerException  { get; private set; } = null!;

        public static IFormatString AppDomain_Name  { get; private set; } = null!;

        public static IFormatString AppDomain_NoContextPolicies  { get; private set; } = null!;

        public static IFormatString AppDomain_Policy_PrincipalTwice  { get; private set; } = null!;

        public static IFormatString AmbiguousImplementationException_NullMessage  { get; private set; } = null!;

        public static IFormatString Arg_AccessException  { get; private set; } = null!;

        public static IFormatString Arg_AccessViolationException  { get; private set; } = null!;

        public static IFormatString Arg_AmbiguousMatchException  { get; private set; } = null!;

        public static IFormatString Arg_ApplicationException  { get; private set; } = null!;

        public static IFormatString Arg_ArgumentException  { get; private set; } = null!;

        public static IFormatString Arg_ArgumentOutOfRangeException  { get; private set; } = null!;

        public static IFormatString Arg_ArithmeticException  { get; private set; } = null!;

        public static IFormatString Arg_ArrayLengthsDiffer  { get; private set; } = null!;

        public static IFormatString Arg_ArrayPlusOffTooSmall  { get; private set; } = null!;

        public static IFormatString Arg_ArrayTypeMismatchException  { get; private set; } = null!;

        public static IFormatString Arg_ArrayZeroError  { get; private set; } = null!;

        public static IFormatString Arg_BadDecimal  { get; private set; } = null!;

        public static IFormatString Arg_BadImageFormatException  { get; private set; } = null!;

        public static IFormatString Arg_BadLiteralFormat  { get; private set; } = null!;

        public static IFormatString Arg_BogusIComparer  { get; private set; } = null!;

        public static IFormatString Arg_BufferTooSmall  { get; private set; } = null!;

        public static IFormatString Arg_CannotBeNaN  { get; private set; } = null!;

        public static IFormatString Arg_CannotHaveNegativeValue  { get; private set; } = null!;

        public static IFormatString Arg_CannotMixComparisonInfrastructure  { get; private set; } = null!;

        public static IFormatString Arg_CannotUnloadAppDomainException  { get; private set; } = null!;

        public static IFormatString Arg_CATypeResolutionFailed  { get; private set; } = null!;

        public static IFormatString Arg_COMAccess  { get; private set; } = null!;

        public static IFormatString Arg_COMException  { get; private set; } = null!;

        public static IFormatString Arg_COMPropSetPut  { get; private set; } = null!;

        public static IFormatString Arg_CreatInstAccess  { get; private set; } = null!;

        public static IFormatString Arg_CryptographyException  { get; private set; } = null!;

        public static IFormatString Arg_CustomAttributeFormatException  { get; private set; } = null!;

        public static IFormatString Arg_DataMisalignedException  { get; private set; } = null!;

        public static IFormatString Arg_DateTimeRange  { get; private set; } = null!;

        public static IFormatString Arg_DecBitCtor  { get; private set; } = null!;

        public static IFormatString Arg_DirectoryNotFoundException  { get; private set; } = null!;

        public static IFormatString Arg_DivideByZero  { get; private set; } = null!;

        public static IFormatString Arg_DlgtNullInst  { get; private set; } = null!;

        public static IFormatString Arg_DlgtTargMeth  { get; private set; } = null!;

        public static IFormatString Arg_DlgtTypeMis  { get; private set; } = null!;

        public static IFormatString Arg_DllNotFoundException  { get; private set; } = null!;

        public static IFormatString Arg_DuplicateWaitObjectException  { get; private set; } = null!;

        public static IFormatString Arg_EHClauseNotClause  { get; private set; } = null!;

        public static IFormatString Arg_EHClauseNotFilter  { get; private set; } = null!;

        public static IFormatString Arg_EmptyArray  { get; private set; } = null!;

        public static IFormatString Arg_EndOfStreamException  { get; private set; } = null!;

        public static IFormatString Arg_EntryPointNotFoundException  { get; private set; } = null!;

        public static IFormatString Arg_EnumAndObjectMustBeSameType  { get; private set; } = null!;

        public static IFormatString Arg_EnumFormatUnderlyingTypeAndObjectMustBeSameType  { get; private set; } = null!;

        public static IFormatString Arg_EnumIllegalVal  { get; private set; } = null!;

        public static IFormatString Arg_EnumLitValueNotFound  { get; private set; } = null!;

        public static IFormatString Arg_EnumUnderlyingTypeAndObjectMustBeSameType  { get; private set; } = null!;

        public static IFormatString Arg_EnumValueNotFound  { get; private set; } = null!;

        public static IFormatString Arg_ExecutionEngineException  { get; private set; } = null!;

        public static IFormatString Arg_ExternalException  { get; private set; } = null!;

        public static IFormatString Arg_FieldAccessException  { get; private set; } = null!;

        public static IFormatString Arg_FieldDeclTarget  { get; private set; } = null!;

        public static IFormatString Arg_FldGetArgErr  { get; private set; } = null!;

        public static IFormatString Arg_FldGetPropSet  { get; private set; } = null!;

        public static IFormatString Arg_FldSetArgErr  { get; private set; } = null!;

        public static IFormatString Arg_FldSetGet  { get; private set; } = null!;

        public static IFormatString Arg_FldSetInvoke  { get; private set; } = null!;

        public static IFormatString Arg_FldSetPropGet  { get; private set; } = null!;

        public static IFormatString Arg_FormatException  { get; private set; } = null!;

        public static IFormatString Arg_GenericParameter  { get; private set; } = null!;

        public static IFormatString Arg_GetMethNotFnd  { get; private set; } = null!;

        public static IFormatString Arg_GuidArrayCtor  { get; private set; } = null!;

        public static IFormatString Arg_HandleNotAsync  { get; private set; } = null!;

        public static IFormatString Arg_HandleNotSync  { get; private set; } = null!;

        public static IFormatString Arg_HexStyleNotSupported  { get; private set; } = null!;

        public static IFormatString Arg_HTCapacityOverflow  { get; private set; } = null!;

        public static IFormatString Arg_IndexMustBeInt  { get; private set; } = null!;

        public static IFormatString Arg_IndexOutOfRangeException  { get; private set; } = null!;

        public static IFormatString Arg_InsufficientExecutionStackException  { get; private set; } = null!;

        public static IFormatString Arg_InvalidANSIString  { get; private set; } = null!;

        public static IFormatString Arg_InvalidBase  { get; private set; } = null!;

        public static IFormatString Arg_InvalidCastException  { get; private set; } = null!;

        public static IFormatString Arg_InvalidComObjectException  { get; private set; } = null!;

        public static IFormatString Arg_InvalidFilterCriteriaException  { get; private set; } = null!;

        public static IFormatString Arg_InvalidHandle  { get; private set; } = null!;

        public static IFormatString Arg_InvalidHexStyle  { get; private set; } = null!;

        public static IFormatString Arg_InvalidNeutralResourcesLanguage_Asm_Culture  { get; private set; } = null!;

        public static IFormatString Arg_InvalidNeutralResourcesLanguage_FallbackLoc  { get; private set; } = null!;

        public static IFormatString Arg_InvalidSatelliteContract_Asm_Ver  { get; private set; } = null!;

        public static IFormatString Arg_InvalidOleVariantTypeException  { get; private set; } = null!;

        public static IFormatString Arg_InvalidOperationException  { get; private set; } = null!;

        public static IFormatString Arg_InvalidTypeInRetType  { get; private set; } = null!;

        public static IFormatString Arg_InvalidTypeInSignature  { get; private set; } = null!;

        public static IFormatString Arg_IOException  { get; private set; } = null!;

        public static IFormatString Arg_KeyNotFound  { get; private set; } = null!;

        public static IFormatString Arg_KeyNotFoundWithKey  { get; private set; } = null!;

        public static IFormatString Arg_LongerThanDestArray  { get; private set; } = null!;

        public static IFormatString Arg_LongerThanSrcArray  { get; private set; } = null!;

        public static IFormatString Arg_LongerThanSrcString  { get; private set; } = null!;

        public static IFormatString Arg_LowerBoundsMustMatch  { get; private set; } = null!;

        public static IFormatString Arg_MarshalAsAnyRestriction  { get; private set; } = null!;

        public static IFormatString Arg_MarshalDirectiveException  { get; private set; } = null!;

        public static IFormatString Arg_MethodAccessException  { get; private set; } = null!;

        public static IFormatString Arg_MissingFieldException  { get; private set; } = null!;

        public static IFormatString Arg_MissingManifestResourceException  { get; private set; } = null!;

        public static IFormatString Arg_MissingMemberException  { get; private set; } = null!;

        public static IFormatString Arg_MissingMethodException  { get; private set; } = null!;

        public static IFormatString Arg_MulticastNotSupportedException  { get; private set; } = null!;

        public static IFormatString Arg_MustBeBoolean  { get; private set; } = null!;

        public static IFormatString Arg_MustBeByte  { get; private set; } = null!;

        public static IFormatString Arg_MustBeChar  { get; private set; } = null!;

        public static IFormatString Arg_MustBeDateTime  { get; private set; } = null!;

        public static IFormatString Arg_MustBeDateTimeOffset  { get; private set; } = null!;

        public static IFormatString Arg_MustBeDecimal  { get; private set; } = null!;

        public static IFormatString Arg_MustBeDelegate  { get; private set; } = null!;

        public static IFormatString Arg_MustBeDouble  { get; private set; } = null!;

        public static IFormatString Arg_MustBeEnum  { get; private set; } = null!;

        public static IFormatString Arg_MustBeEnumBaseTypeOrEnum  { get; private set; } = null!;

        public static IFormatString Arg_MustBeGuid  { get; private set; } = null!;

        public static IFormatString Arg_MustBeInt16  { get; private set; } = null!;

        public static IFormatString Arg_MustBeInt32  { get; private set; } = null!;

        public static IFormatString Arg_MustBeInt64  { get; private set; } = null!;

        public static IFormatString Arg_MustBeIntPtr  { get; private set; } = null!;

        public static IFormatString Arg_MustBePointer  { get; private set; } = null!;

        public static IFormatString Arg_MustBePrimArray  { get; private set; } = null!;

        public static IFormatString Arg_MustBeRuntimeAssembly  { get; private set; } = null!;

        public static IFormatString Arg_MustBeSByte  { get; private set; } = null!;

        public static IFormatString Arg_MustBeSingle  { get; private set; } = null!;

        public static IFormatString Arg_MustBeString  { get; private set; } = null!;

        public static IFormatString Arg_MustBeTimeSpan  { get; private set; } = null!;

        public static IFormatString Arg_MustBeType  { get; private set; } = null!;

        public static IFormatString Arg_MustBeTrue  { get; private set; } = null!;

        public static IFormatString Arg_MustBeUInt16  { get; private set; } = null!;

        public static IFormatString Arg_MustBeUInt32  { get; private set; } = null!;

        public static IFormatString Arg_MustBeUInt64  { get; private set; } = null!;

        public static IFormatString Arg_MustBeUIntPtr  { get; private set; } = null!;

        public static IFormatString Arg_MustBeVersion  { get; private set; } = null!;

        public static IFormatString Arg_MustContainEnumInfo  { get; private set; } = null!;

        public static IFormatString Arg_NamedParamNull  { get; private set; } = null!;

        public static IFormatString Arg_NamedParamTooBig  { get; private set; } = null!;

        public static IFormatString Arg_NDirectBadObject  { get; private set; } = null!;

        public static IFormatString Arg_Need1DArray  { get; private set; } = null!;

        public static IFormatString Arg_Need2DArray  { get; private set; } = null!;

        public static IFormatString Arg_Need3DArray  { get; private set; } = null!;

        public static IFormatString Arg_NeedAtLeast1Rank  { get; private set; } = null!;

        public static IFormatString Arg_NegativeArgCount  { get; private set; } = null!;

        public static IFormatString Arg_NoAccessSpec  { get; private set; } = null!;

        public static IFormatString Arg_NoDefCTor  { get; private set; } = null!;

        public static IFormatString Arg_NonZeroLowerBound  { get; private set; } = null!;

        public static IFormatString Arg_NoStaticVirtual  { get; private set; } = null!;

        public static IFormatString Arg_NotFiniteNumberException  { get; private set; } = null!;

        public static IFormatString Arg_NotGenericMethodDefinition  { get; private set; } = null!;

        public static IFormatString Arg_NotGenericParameter  { get; private set; } = null!;

        public static IFormatString Arg_NotGenericTypeDefinition  { get; private set; } = null!;

        public static IFormatString Arg_NotImplementedException  { get; private set; } = null!;

        public static IFormatString Arg_NotSupportedException  { get; private set; } = null!;

        public static IFormatString Arg_NullReferenceException  { get; private set; } = null!;

        public static IFormatString Arg_ObjObjEx  { get; private set; } = null!;

        public static IFormatString Arg_OleAutDateInvalid  { get; private set; } = null!;

        public static IFormatString Arg_OleAutDateScale  { get; private set; } = null!;

        public static IFormatString Arg_OverflowException  { get; private set; } = null!;

        public static IFormatString Arg_ParamName_Name  { get; private set; } = null!;

        public static IFormatString Arg_ParmArraySize  { get; private set; } = null!;

        public static IFormatString Arg_ParmCnt  { get; private set; } = null!;

        public static IFormatString Arg_PathEmpty  { get; private set; } = null!;

        public static IFormatString Arg_PlatformNotSupported  { get; private set; } = null!;

        public static IFormatString Arg_PropSetGet  { get; private set; } = null!;

        public static IFormatString Arg_PropSetInvoke  { get; private set; } = null!;

        public static IFormatString Arg_RankException  { get; private set; } = null!;

        public static IFormatString Arg_RankIndices  { get; private set; } = null!;

        public static IFormatString Arg_RankMultiDimNotSupported  { get; private set; } = null!;

        public static IFormatString Arg_RanksAndBounds  { get; private set; } = null!;

        public static IFormatString Arg_RegGetOverflowBug  { get; private set; } = null!;

        public static IFormatString Arg_RegKeyNotFound  { get; private set; } = null!;

        public static IFormatString Arg_RegSubKeyValueAbsent  { get; private set; } = null!;

        public static IFormatString Arg_RegValStrLenBug  { get; private set; } = null!;

        public static IFormatString Arg_ResMgrNotResSet  { get; private set; } = null!;

        public static IFormatString Arg_ResourceFileUnsupportedVersion  { get; private set; } = null!;

        public static IFormatString Arg_ResourceNameNotExist  { get; private set; } = null!;

        public static IFormatString Arg_SafeArrayRankMismatchException  { get; private set; } = null!;

        public static IFormatString Arg_SafeArrayTypeMismatchException  { get; private set; } = null!;

        public static IFormatString Arg_SecurityException  { get; private set; } = null!;

        public static IFormatString SerializationException  { get; private set; } = null!;

        public static IFormatString Arg_SetMethNotFnd  { get; private set; } = null!;

        public static IFormatString Arg_StackOverflowException  { get; private set; } = null!;

        public static IFormatString Arg_SurrogatesNotAllowedAsSingleChar  { get; private set; } = null!;

        public static IFormatString Arg_SynchronizationLockException  { get; private set; } = null!;

        public static IFormatString Arg_SystemException  { get; private set; } = null!;

        public static IFormatString Arg_TargetInvocationException  { get; private set; } = null!;

        public static IFormatString Arg_TargetParameterCountException  { get; private set; } = null!;

        public static IFormatString Arg_ThreadStartException  { get; private set; } = null!;

        public static IFormatString Arg_ThreadStateException  { get; private set; } = null!;

        public static IFormatString Arg_TimeoutException  { get; private set; } = null!;

        public static IFormatString Arg_TypeAccessException  { get; private set; } = null!;

        public static IFormatString Arg_TypedReference_Null  { get; private set; } = null!;

        public static IFormatString Arg_TypeLoadException  { get; private set; } = null!;

        public static IFormatString Arg_TypeLoadNullStr  { get; private set; } = null!;

        public static IFormatString Arg_TypeRefPrimitve  { get; private set; } = null!;

        public static IFormatString Arg_TypeUnloadedException  { get; private set; } = null!;

        public static IFormatString Arg_UnauthorizedAccessException  { get; private set; } = null!;

        public static IFormatString Arg_UnboundGenField  { get; private set; } = null!;

        public static IFormatString Arg_UnboundGenParam  { get; private set; } = null!;

        public static IFormatString Arg_UnknownTypeCode  { get; private set; } = null!;

        public static IFormatString Arg_VarMissNull  { get; private set; } = null!;

        public static IFormatString Arg_VersionString  { get; private set; } = null!;

        public static IFormatString Arg_WrongAsyncResult  { get; private set; } = null!;

        public static IFormatString Arg_WrongType  { get; private set; } = null!;

        public static IFormatString Argument_AbsolutePathRequired  { get; private set; } = null!;

        public static IFormatString Argument_AddingDuplicate  { get; private set; } = null!;

        public static IFormatString Argument_AddingDuplicate__  { get; private set; } = null!;

        public static IFormatString Argument_AddingDuplicateWithKey  { get; private set; } = null!;

        public static IFormatString Argument_AdjustmentRulesNoNulls  { get; private set; } = null!;

        public static IFormatString Argument_AdjustmentRulesOutOfOrder  { get; private set; } = null!;

        public static IFormatString Argument_AlreadyBoundOrSyncHandle  { get; private set; } = null!;

        public static IFormatString Argument_ArrayGetInterfaceMap  { get; private set; } = null!;

        public static IFormatString Argument_ArraysInvalid  { get; private set; } = null!;

        public static IFormatString Argument_AttributeNamesMustBeUnique  { get; private set; } = null!;

        public static IFormatString Argument_BadConstructor  { get; private set; } = null!;

        public static IFormatString Argument_BadConstructorCallConv  { get; private set; } = null!;

        public static IFormatString Argument_BadExceptionCodeGen  { get; private set; } = null!;

        public static IFormatString Argument_BadFieldForConstructorBuilder  { get; private set; } = null!;

        public static IFormatString Argument_BadFieldSig  { get; private set; } = null!;

        public static IFormatString Argument_BadFieldType  { get; private set; } = null!;

        public static IFormatString Argument_BadFormatSpecifier  { get; private set; } = null!;

        public static IFormatString Argument_BadImageFormatExceptionResolve  { get; private set; } = null!;

        public static IFormatString Argument_BadLabel  { get; private set; } = null!;

        public static IFormatString Argument_BadLabelContent  { get; private set; } = null!;

        public static IFormatString Argument_BadNestedTypeFlags  { get; private set; } = null!;

        public static IFormatString Argument_BadParameterCountsForConstructor  { get; private set; } = null!;

        public static IFormatString Argument_BadParameterTypeForCAB  { get; private set; } = null!;

        public static IFormatString Argument_BadPropertyForConstructorBuilder  { get; private set; } = null!;

        public static IFormatString Argument_BadSigFormat  { get; private set; } = null!;

        public static IFormatString Argument_BadSizeForData  { get; private set; } = null!;

        public static IFormatString Argument_BadTypeAttrInvalidLayout  { get; private set; } = null!;

        public static IFormatString Argument_BadTypeAttrNestedVisibilityOnNonNestedType  { get; private set; } = null!;

        public static IFormatString Argument_BadTypeAttrNonNestedVisibilityNestedType  { get; private set; } = null!;

        public static IFormatString Argument_BadTypeAttrReservedBitsSet  { get; private set; } = null!;

        public static IFormatString Argument_BadTypeInCustomAttribute  { get; private set; } = null!;

        public static IFormatString Argument_CannotGetTypeTokenForByRef  { get; private set; } = null!;

        public static IFormatString Argument_CannotSetParentToInterface  { get; private set; } = null!;

        public static IFormatString Argument_CodepageNotSupported  { get; private set; } = null!;

        public static IFormatString Argument_CompareOptionOrdinal  { get; private set; } = null!;

        public static IFormatString Argument_ConflictingDateTimeRoundtripStyles  { get; private set; } = null!;

        public static IFormatString Argument_ConflictingDateTimeStyles  { get; private set; } = null!;

        public static IFormatString Argument_ConstantDoesntMatch  { get; private set; } = null!;

        public static IFormatString Argument_ConstantNotSupported  { get; private set; } = null!;

        public static IFormatString Argument_ConstantNull  { get; private set; } = null!;

        public static IFormatString Argument_ConstructorNeedGenericDeclaringType  { get; private set; } = null!;

        public static IFormatString Argument_ConversionOverflow  { get; private set; } = null!;

        public static IFormatString Argument_ConvertMismatch  { get; private set; } = null!;

        public static IFormatString Argument_CultureIetfNotSupported  { get; private set; } = null!;

        public static IFormatString Argument_CultureInvalidIdentifier  { get; private set; } = null!;

        public static IFormatString Argument_CultureIsNeutral  { get; private set; } = null!;

        public static IFormatString Argument_CultureNotSupported  { get; private set; } = null!;

        public static IFormatString Argument_CustomAssemblyLoadContextRequestedNameMismatch  { get; private set; } = null!;

        public static IFormatString Argument_CustomCultureCannotBePassedByNumber  { get; private set; } = null!;

        public static IFormatString Argument_DateTimeBadBinaryData  { get; private set; } = null!;

        public static IFormatString Argument_DateTimeHasTicks  { get; private set; } = null!;

        public static IFormatString Argument_DateTimeHasTimeOfDay  { get; private set; } = null!;

        public static IFormatString Argument_DateTimeIsInvalid  { get; private set; } = null!;

        public static IFormatString Argument_DateTimeIsNotAmbiguous  { get; private set; } = null!;

        public static IFormatString Argument_DateTimeKindMustBeUnspecified  { get; private set; } = null!;

        public static IFormatString Argument_DateTimeKindMustBeUnspecifiedOrUtc  { get; private set; } = null!;

        public static IFormatString Argument_DateTimeOffsetInvalidDateTimeStyles  { get; private set; } = null!;

        public static IFormatString Argument_DateTimeOffsetIsNotAmbiguous  { get; private set; } = null!;

        public static IFormatString Argument_DestinationTooShort  { get; private set; } = null!;

        public static IFormatString Argument_DuplicateTypeName  { get; private set; } = null!;

        public static IFormatString Argument_EmitWriteLineType  { get; private set; } = null!;

        public static IFormatString Argument_EmptyDecString  { get; private set; } = null!;

        public static IFormatString Argument_EmptyName  { get; private set; } = null!;

        public static IFormatString Argument_EmptyPath  { get; private set; } = null!;

        public static IFormatString Argument_EmptyWaithandleArray  { get; private set; } = null!;

        public static IFormatString Argument_EncoderFallbackNotEmpty  { get; private set; } = null!;

        public static IFormatString Argument_EncodingConversionOverflowBytes  { get; private set; } = null!;

        public static IFormatString Argument_EncodingConversionOverflowChars  { get; private set; } = null!;

        public static IFormatString Argument_EncodingNotSupported  { get; private set; } = null!;

        public static IFormatString Argument_EnumTypeDoesNotMatch  { get; private set; } = null!;

        public static IFormatString Argument_FallbackBufferNotEmpty  { get; private set; } = null!;

        public static IFormatString Argument_FieldDeclaringTypeGeneric  { get; private set; } = null!;

        public static IFormatString Argument_FieldNeedGenericDeclaringType  { get; private set; } = null!;

        public static IFormatString Argument_GenConstraintViolation  { get; private set; } = null!;

        public static IFormatString Argument_GenericArgsCount  { get; private set; } = null!;

        public static IFormatString Argument_GenericsInvalid  { get; private set; } = null!;

        public static IFormatString Argument_GlobalFunctionHasToBeStatic  { get; private set; } = null!;

        public static IFormatString Argument_HasToBeArrayClass  { get; private set; } = null!;

        public static IFormatString Argument_IdnBadBidi  { get; private set; } = null!;

        public static IFormatString Argument_IdnBadLabelSize  { get; private set; } = null!;

        public static IFormatString Argument_IdnBadNameSize  { get; private set; } = null!;

        public static IFormatString Argument_IdnBadPunycode  { get; private set; } = null!;

        public static IFormatString Argument_IdnBadStd3  { get; private set; } = null!;

        public static IFormatString Argument_IdnIllegalName  { get; private set; } = null!;

        public static IFormatString Argument_IllegalEnvVarName  { get; private set; } = null!;

        public static IFormatString Argument_IllegalName  { get; private set; } = null!;

        public static IFormatString Argument_ImplementIComparable  { get; private set; } = null!;

        public static IFormatString Argument_InvalidAppendMode  { get; private set; } = null!;

        public static IFormatString Argument_InvalidArgumentForComparison  { get; private set; } = null!;

        public static IFormatString Argument_InvalidArrayLength  { get; private set; } = null!;

        public static IFormatString Argument_InvalidArrayType  { get; private set; } = null!;

        public static IFormatString Argument_InvalidCalendar  { get; private set; } = null!;

        public static IFormatString Argument_InvalidCharSequence  { get; private set; } = null!;

        public static IFormatString Argument_InvalidCharSequenceNoIndex  { get; private set; } = null!;

        public static IFormatString Argument_InvalidCodePageBytesIndex  { get; private set; } = null!;

        public static IFormatString Argument_InvalidCodePageConversionIndex  { get; private set; } = null!;

        public static IFormatString Argument_InvalidConstructorDeclaringType  { get; private set; } = null!;

        public static IFormatString Argument_InvalidConstructorInfo  { get; private set; } = null!;

        public static IFormatString Argument_InvalidCultureName  { get; private set; } = null!;

        public static IFormatString Argument_InvalidPredefinedCultureName  { get; private set; } = null!;

        public static IFormatString Argument_InvalidDateTimeKind  { get; private set; } = null!;

        public static IFormatString Argument_InvalidDateTimeStyles  { get; private set; } = null!;

        public static IFormatString Argument_InvalidDigitSubstitution  { get; private set; } = null!;

        public static IFormatString Argument_InvalidElementName  { get; private set; } = null!;

        public static IFormatString Argument_InvalidElementTag  { get; private set; } = null!;

        public static IFormatString Argument_InvalidElementText  { get; private set; } = null!;

        public static IFormatString Argument_InvalidElementValue  { get; private set; } = null!;

        public static IFormatString Argument_InvalidEnum  { get; private set; } = null!;

        public static IFormatString Argument_InvalidEnumValue  { get; private set; } = null!;

        public static IFormatString Argument_InvalidFieldDeclaringType  { get; private set; } = null!;

        public static IFormatString Argument_InvalidFileModeAndAccessCombo  { get; private set; } = null!;

        public static IFormatString Argument_InvalidFlag  { get; private set; } = null!;

        public static IFormatString Argument_InvalidGenericInstArray  { get; private set; } = null!;

        public static IFormatString Argument_InvalidGroupSize  { get; private set; } = null!;

        public static IFormatString Argument_InvalidHandle  { get; private set; } = null!;

        public static IFormatString Argument_InvalidHighSurrogate  { get; private set; } = null!;

        public static IFormatString Argument_InvalidId  { get; private set; } = null!;

        public static IFormatString Argument_InvalidKindOfTypeForCA  { get; private set; } = null!;

        public static IFormatString Argument_InvalidLabel  { get; private set; } = null!;

        public static IFormatString Argument_InvalidLowSurrogate  { get; private set; } = null!;

        public static IFormatString Argument_InvalidMemberForNamedArgument  { get; private set; } = null!;

        public static IFormatString Argument_InvalidMethodDeclaringType  { get; private set; } = null!;

        public static IFormatString Argument_InvalidName  { get; private set; } = null!;

        public static IFormatString Argument_InvalidNativeDigitCount  { get; private set; } = null!;

        public static IFormatString Argument_InvalidNativeDigitValue  { get; private set; } = null!;

        public static IFormatString Argument_InvalidNeutralRegionName  { get; private set; } = null!;

        public static IFormatString Argument_InvalidNormalizationForm  { get; private set; } = null!;

        public static IFormatString Argument_InvalidNumberStyles  { get; private set; } = null!;

        public static IFormatString Argument_InvalidOffLen  { get; private set; } = null!;

        public static IFormatString Argument_InvalidOpCodeOnDynamicMethod  { get; private set; } = null!;

        public static IFormatString Argument_InvalidParameterInfo  { get; private set; } = null!;

        public static IFormatString Argument_InvalidParamInfo  { get; private set; } = null!;

        public static IFormatString Argument_InvalidPathChars  { get; private set; } = null!;

        public static IFormatString Argument_InvalidResourceCultureName  { get; private set; } = null!;

        public static IFormatString Argument_InvalidSafeBufferOffLen  { get; private set; } = null!;

        public static IFormatString Argument_InvalidSeekOrigin  { get; private set; } = null!;

        public static IFormatString Argument_InvalidSerializedString  { get; private set; } = null!;

        public static IFormatString Argument_InvalidStartupHookSignature  { get; private set; } = null!;

        public static IFormatString Argument_InvalidTimeSpanStyles  { get; private set; } = null!;

        public static IFormatString Argument_InvalidToken  { get; private set; } = null!;

        public static IFormatString Argument_InvalidTypeForCA  { get; private set; } = null!;

        public static IFormatString Argument_InvalidTypeForDynamicMethod  { get; private set; } = null!;

        public static IFormatString Argument_InvalidTypeName  { get; private set; } = null!;

        public static IFormatString Argument_InvalidTypeWithPointersNotSupported  { get; private set; } = null!;

        public static IFormatString Argument_InvalidUnity  { get; private set; } = null!;

        public static IFormatString Argument_LargeInteger  { get; private set; } = null!;

        public static IFormatString Argument_LongEnvVarValue  { get; private set; } = null!;

        public static IFormatString Argument_MethodDeclaringTypeGeneric  { get; private set; } = null!;

        public static IFormatString Argument_MethodDeclaringTypeGenericLcg  { get; private set; } = null!;

        public static IFormatString Argument_MethodNeedGenericDeclaringType  { get; private set; } = null!;

        public static IFormatString Argument_MinMaxValue  { get; private set; } = null!;

        public static IFormatString Argument_MismatchedArrays  { get; private set; } = null!;

        public static IFormatString Argument_MissingDefaultConstructor  { get; private set; } = null!;

        public static IFormatString Argument_MustBeFalse  { get; private set; } = null!;

        public static IFormatString Argument_MustBeRuntimeAssembly  { get; private set; } = null!;

        public static IFormatString Argument_MustBeRuntimeFieldInfo  { get; private set; } = null!;

        public static IFormatString Argument_MustBeRuntimeMethodInfo  { get; private set; } = null!;

        public static IFormatString Argument_MustBeRuntimeReflectionObject  { get; private set; } = null!;

        public static IFormatString Argument_MustBeRuntimeType  { get; private set; } = null!;

        public static IFormatString Argument_MustBeTypeBuilder  { get; private set; } = null!;

        public static IFormatString Argument_MustHaveAttributeBaseClass  { get; private set; } = null!;

        public static IFormatString Argument_NativeOverlappedAlreadyFree  { get; private set; } = null!;

        public static IFormatString Argument_NativeOverlappedWrongBoundHandle  { get; private set; } = null!;

        public static IFormatString Argument_NeedGenericMethodDefinition  { get; private set; } = null!;

        public static IFormatString Argument_NeedNonGenericType  { get; private set; } = null!;

        public static IFormatString Argument_NeedStructWithNoRefs  { get; private set; } = null!;

        public static IFormatString Argument_NeverValidGenericArgument  { get; private set; } = null!;

        public static IFormatString Argument_NoEra  { get; private set; } = null!;

        public static IFormatString Argument_NoRegionInvariantCulture  { get; private set; } = null!;

        public static IFormatString Argument_NotAWritableProperty  { get; private set; } = null!;

        public static IFormatString Argument_NotEnoughBytesToRead  { get; private set; } = null!;

        public static IFormatString Argument_NotEnoughBytesToWrite  { get; private set; } = null!;

        public static IFormatString Argument_NotEnoughGenArguments  { get; private set; } = null!;

        public static IFormatString Argument_NotExceptionType  { get; private set; } = null!;

        public static IFormatString Argument_NotInExceptionBlock  { get; private set; } = null!;

        public static IFormatString Argument_NotMethodCallOpcode  { get; private set; } = null!;

        public static IFormatString Argument_NotSerializable  { get; private set; } = null!;

        public static IFormatString Argument_ObjNotComObject  { get; private set; } = null!;

        public static IFormatString Argument_OffsetAndCapacityOutOfBounds  { get; private set; } = null!;

        public static IFormatString Argument_OffsetLocalMismatch  { get; private set; } = null!;

        public static IFormatString Argument_OffsetOfFieldNotFound  { get; private set; } = null!;

        public static IFormatString Argument_OffsetOutOfRange  { get; private set; } = null!;

        public static IFormatString Argument_OffsetPrecision  { get; private set; } = null!;

        public static IFormatString Argument_OffsetUtcMismatch  { get; private set; } = null!;

        public static IFormatString Argument_OneOfCulturesNotSupported  { get; private set; } = null!;

        public static IFormatString Argument_OnlyMscorlib  { get; private set; } = null!;

        public static IFormatString Argument_OutOfOrderDateTimes  { get; private set; } = null!;

        public static IFormatString Argument_PathEmpty  { get; private set; } = null!;

        public static IFormatString Argument_PreAllocatedAlreadyAllocated  { get; private set; } = null!;

        public static IFormatString Argument_RecursiveFallback  { get; private set; } = null!;

        public static IFormatString Argument_RecursiveFallbackBytes  { get; private set; } = null!;

        public static IFormatString Argument_RedefinedLabel  { get; private set; } = null!;

        public static IFormatString Argument_ResolveField  { get; private set; } = null!;

        public static IFormatString Argument_ResolveFieldHandle  { get; private set; } = null!;

        public static IFormatString Argument_ResolveMember  { get; private set; } = null!;

        public static IFormatString Argument_ResolveMethod  { get; private set; } = null!;

        public static IFormatString Argument_ResolveMethodHandle  { get; private set; } = null!;

        public static IFormatString Argument_ResolveModuleType  { get; private set; } = null!;

        public static IFormatString Argument_ResolveString  { get; private set; } = null!;

        public static IFormatString Argument_ResolveType  { get; private set; } = null!;

        public static IFormatString Argument_ResultCalendarRange  { get; private set; } = null!;

        public static IFormatString Argument_SemaphoreInitialMaximum  { get; private set; } = null!;

        public static IFormatString Argument_ShouldNotSpecifyExceptionType  { get; private set; } = null!;

        public static IFormatString Argument_ShouldOnlySetVisibilityFlags  { get; private set; } = null!;

        public static IFormatString Argument_SigIsFinalized  { get; private set; } = null!;

        public static IFormatString Argument_StreamNotReadable  { get; private set; } = null!;

        public static IFormatString Argument_StreamNotWritable  { get; private set; } = null!;

        public static IFormatString Argument_StringFirstCharIsZero  { get; private set; } = null!;

        public static IFormatString Argument_StringZeroLength  { get; private set; } = null!;

        public static IFormatString Argument_TimeSpanHasSeconds  { get; private set; } = null!;

        public static IFormatString Argument_ToExclusiveLessThanFromExclusive  { get; private set; } = null!;

        public static IFormatString Argument_TooManyFinallyClause  { get; private set; } = null!;

        public static IFormatString Argument_TransitionTimesAreIdentical  { get; private set; } = null!;

        public static IFormatString Argument_TypedReferenceInvalidField  { get; private set; } = null!;

        public static IFormatString Argument_TypeMustNotBeComImport  { get; private set; } = null!;

        public static IFormatString Argument_TypeNameTooLong  { get; private set; } = null!;

        public static IFormatString Argument_TypeNotComObject  { get; private set; } = null!;

        public static IFormatString Argument_TypeNotValid  { get; private set; } = null!;

        public static IFormatString Argument_UnclosedExceptionBlock  { get; private set; } = null!;

        public static IFormatString Argument_UnknownUnmanagedCallConv  { get; private set; } = null!;

        public static IFormatString Argument_UnmanagedMemAccessorWrapAround  { get; private set; } = null!;

        public static IFormatString Argument_UnmatchedMethodForLocal  { get; private set; } = null!;

        public static IFormatString Argument_UnmatchingSymScope  { get; private set; } = null!;

        public static IFormatString Argument_UTCOutOfRange  { get; private set; } = null!;

        public static IFormatString ArgumentException_BadMethodImplBody  { get; private set; } = null!;

        public static IFormatString ArgumentException_BufferNotFromPool  { get; private set; } = null!;

        public static IFormatString ArgumentException_OtherNotArrayOfCorrectLength  { get; private set; } = null!;

        public static IFormatString ArgumentException_NotIsomorphic  { get; private set; } = null!;

        public static IFormatString ArgumentException_TupleIncorrectType  { get; private set; } = null!;

        public static IFormatString ArgumentException_TupleLastArgumentNotATuple  { get; private set; } = null!;

        public static IFormatString ArgumentException_ValueTupleIncorrectType  { get; private set; } = null!;

        public static IFormatString ArgumentException_ValueTupleLastArgumentNotAValueTuple  { get; private set; } = null!;

        public static IFormatString ArgumentNull_Array  { get; private set; } = null!;

        public static IFormatString ArgumentNull_ArrayElement  { get; private set; } = null!;

        public static IFormatString ArgumentNull_ArrayValue  { get; private set; } = null!;

        public static IFormatString ArgumentNull_Assembly  { get; private set; } = null!;

        public static IFormatString ArgumentNull_AssemblyNameName  { get; private set; } = null!;

        public static IFormatString ArgumentNull_Buffer  { get; private set; } = null!;

        public static IFormatString ArgumentNull_Child  { get; private set; } = null!;

        public static IFormatString ArgumentNull_Collection  { get; private set; } = null!;

        public static IFormatString ArgumentNull_Dictionary  { get; private set; } = null!;

        public static IFormatString ArgumentNull_Generic  { get; private set; } = null!;

        public static IFormatString ArgumentNull_Key  { get; private set; } = null!;

        public static IFormatString ArgumentNull_Path  { get; private set; } = null!;

        public static IFormatString ArgumentNull_SafeHandle  { get; private set; } = null!;

        public static IFormatString ArgumentNull_Stream  { get; private set; } = null!;

        public static IFormatString ArgumentNull_String  { get; private set; } = null!;

        public static IFormatString ArgumentNull_Type  { get; private set; } = null!;

        public static IFormatString ArgumentNull_Waithandles  { get; private set; } = null!;

        public static IFormatString ArgumentOutOfRange_ActualValue  { get; private set; } = null!;

        public static IFormatString ArgumentOutOfRange_AddValue  { get; private set; } = null!;

        public static IFormatString ArgumentOutOfRange_ArrayLB  { get; private set; } = null!;

        public static IFormatString ArgumentOutOfRange_BadHourMinuteSecond  { get; private set; } = null!;

        public static IFormatString ArgumentOutOfRange_BadYearMonthDay  { get; private set; } = null!;

        public static IFormatString ArgumentOutOfRange_BiggerThanCollection  { get; private set; } = null!;

        public static IFormatString ArgumentOutOfRange_BinaryReaderFillBuffer  { get; private set; } = null!;

        public static IFormatString ArgumentOutOfRange_Bounds_Lower_Upper  { get; private set; } = null!;

        public static IFormatString ArgumentOutOfRange_CalendarRange  { get; private set; } = null!;

        public static IFormatString ArgumentOutOfRange_Capacity  { get; private set; } = null!;

        public static IFormatString ArgumentOutOfRange_Count  { get; private set; } = null!;

        public static IFormatString ArgumentOutOfRange_DateArithmetic  { get; private set; } = null!;

        public static IFormatString ArgumentOutOfRange_DateTimeBadMonths  { get; private set; } = null!;

        public static IFormatString ArgumentOutOfRange_DateTimeBadTicks  { get; private set; } = null!;

        public static IFormatString ArgumentOutOfRange_DateTimeBadYears  { get; private set; } = null!;

        public static IFormatString ArgumentOutOfRange_Day  { get; private set; } = null!;

        public static IFormatString ArgumentOutOfRange_DayOfWeek  { get; private set; } = null!;

        public static IFormatString ArgumentOutOfRange_DayParam  { get; private set; } = null!;

        public static IFormatString ArgumentOutOfRange_DecimalRound  { get; private set; } = null!;

        public static IFormatString ArgumentOutOfRange_DecimalScale  { get; private set; } = null!;

        public static IFormatString ArgumentOutOfRange_EndIndexStartIndex  { get; private set; } = null!;

        public static IFormatString ArgumentOutOfRange_Enum  { get; private set; } = null!;

        public static IFormatString ArgumentOutOfRange_Era  { get; private set; } = null!;

        public static IFormatString ArgumentOutOfRange_FileLengthTooBig  { get; private set; } = null!;

        public static IFormatString ArgumentOutOfRange_FileTimeInvalid  { get; private set; } = null!;

        public static IFormatString ArgumentOutOfRange_GenericPositive  { get; private set; } = null!;

        public static IFormatString ArgumentOutOfRange_GetByteCountOverflow  { get; private set; } = null!;

        public static IFormatString ArgumentOutOfRange_GetCharCountOverflow  { get; private set; } = null!;

        public static IFormatString ArgumentOutOfRange_HashtableLoadFactor  { get; private set; } = null!;

        public static IFormatString ArgumentOutOfRange_HugeArrayNotSupported  { get; private set; } = null!;

        public static IFormatString ArgumentOutOfRange_Index  { get; private set; } = null!;

        public static IFormatString ArgumentOutOfRange_IndexCount  { get; private set; } = null!;

        public static IFormatString ArgumentOutOfRange_IndexCountBuffer  { get; private set; } = null!;

        public static IFormatString ArgumentOutOfRange_IndexLength  { get; private set; } = null!;

        public static IFormatString ArgumentOutOfRange_IndexString  { get; private set; } = null!;

        public static IFormatString ArgumentOutOfRange_InputTooLarge  { get; private set; } = null!;

        public static IFormatString ArgumentOutOfRange_InvalidEraValue  { get; private set; } = null!;

        public static IFormatString ArgumentOutOfRange_InvalidHighSurrogate  { get; private set; } = null!;

        public static IFormatString ArgumentOutOfRange_InvalidLowSurrogate  { get; private set; } = null!;

        public static IFormatString ArgumentOutOfRange_InvalidUTF32  { get; private set; } = null!;

        public static IFormatString ArgumentOutOfRange_Length  { get; private set; } = null!;

        public static IFormatString ArgumentOutOfRange_LengthGreaterThanCapacity  { get; private set; } = null!;

        public static IFormatString ArgumentOutOfRange_LengthTooLarge  { get; private set; } = null!;

        public static IFormatString ArgumentOutOfRange_LessEqualToIntegerMaxVal  { get; private set; } = null!;

        public static IFormatString ArgumentOutOfRange_ListInsert  { get; private set; } = null!;

        public static IFormatString ArgumentOutOfRange_Month  { get; private set; } = null!;

        public static IFormatString ArgumentOutOfRange_MonthParam  { get; private set; } = null!;

        public static IFormatString ArgumentOutOfRange_MustBeNonNegInt32  { get; private set; } = null!;

        public static IFormatString ArgumentOutOfRange_MustBeNonNegNum  { get; private set; } = null!;

        public static IFormatString ArgumentOutOfRange_MustBePositive  { get; private set; } = null!;

        public static IFormatString ArgumentOutOfRange_NeedNonNegNum  { get; private set; } = null!;

        public static IFormatString ArgumentOutOfRange_NeedNonNegOrNegative1  { get; private set; } = null!;

        public static IFormatString ArgumentOutOfRange_NeedPosNum  { get; private set; } = null!;

        public static IFormatString ArgumentOutOfRange_NeedValidId  { get; private set; } = null!;

        public static IFormatString ArgumentOutOfRange_NegativeCapacity  { get; private set; } = null!;

        public static IFormatString ArgumentOutOfRange_NegativeCount  { get; private set; } = null!;

        public static IFormatString ArgumentOutOfRange_NegativeLength  { get; private set; } = null!;

        public static IFormatString ArgumentOutOfRange_OffsetLength  { get; private set; } = null!;

        public static IFormatString ArgumentOutOfRange_OffsetOut  { get; private set; } = null!;

        public static IFormatString ArgumentOutOfRange_ParamSequence  { get; private set; } = null!;

        public static IFormatString ArgumentOutOfRange_PartialWCHAR  { get; private set; } = null!;

        public static IFormatString ArgumentOutOfRange_PeriodTooLarge  { get; private set; } = null!;

        public static IFormatString ArgumentOutOfRange_PositionLessThanCapacityRequired  { get; private set; } = null!;

        public static IFormatString ArgumentOutOfRange_Range  { get; private set; } = null!;

        public static IFormatString ArgumentOutOfRange_RoundingDigits  { get; private set; } = null!;

        public static IFormatString ArgumentOutOfRange_SmallCapacity  { get; private set; } = null!;

        public static IFormatString ArgumentOutOfRange_SmallMaxCapacity  { get; private set; } = null!;

        public static IFormatString ArgumentOutOfRange_StartIndex  { get; private set; } = null!;

        public static IFormatString ArgumentOutOfRange_StartIndexLargerThanLength  { get; private set; } = null!;

        public static IFormatString ArgumentOutOfRange_StartIndexLessThanLength  { get; private set; } = null!;

        public static IFormatString ArgumentOutOfRange_StreamLength  { get; private set; } = null!;

        public static IFormatString ArgumentOutOfRange_TimeoutTooLarge  { get; private set; } = null!;

        public static IFormatString ArgumentOutOfRange_UIntPtrMax  { get; private set; } = null!;

        public static IFormatString ArgumentOutOfRange_UnmanagedMemStreamLength  { get; private set; } = null!;

        public static IFormatString ArgumentOutOfRange_UnmanagedMemStreamWrapAround  { get; private set; } = null!;

        public static IFormatString ArgumentOutOfRange_UtcOffset  { get; private set; } = null!;

        public static IFormatString ArgumentOutOfRange_UtcOffsetAndDaylightDelta  { get; private set; } = null!;

        public static IFormatString ArgumentOutOfRange_Version  { get; private set; } = null!;

        public static IFormatString ArgumentOutOfRange_Week  { get; private set; } = null!;

        public static IFormatString ArgumentOutOfRange_Year  { get; private set; } = null!;

        public static IFormatString Arithmetic_NaN  { get; private set; } = null!;

        public static IFormatString ArrayTypeMismatch_ConstrainedCopy  { get; private set; } = null!;

        public static IFormatString AssemblyLoadContext_Unload_CannotUnloadIfNotCollectible  { get; private set; } = null!;

        public static IFormatString AssemblyLoadContext_Verify_NotUnloading  { get; private set; } = null!;

        public static IFormatString AssertionFailed  { get; private set; } = null!;

        public static IFormatString AssertionFailed_Cnd  { get; private set; } = null!;

        public static IFormatString AssumptionFailed  { get; private set; } = null!;

        public static IFormatString AssumptionFailed_Cnd  { get; private set; } = null!;

        public static IFormatString AsyncMethodBuilder_InstanceNotInitialized  { get; private set; } = null!;

        public static IFormatString BadImageFormat_BadILFormat  { get; private set; } = null!;

        public static IFormatString BadImageFormat_InvalidType  { get; private set; } = null!;

        public static IFormatString BadImageFormat_NegativeStringLength  { get; private set; } = null!;

        public static IFormatString BadImageFormat_ParameterSignatureMismatch  { get; private set; } = null!;

        public static IFormatString BadImageFormat_ResType_SerBlobMismatch  { get; private set; } = null!;

        public static IFormatString BadImageFormat_ResourceDataLengthInvalid  { get; private set; } = null!;

        public static IFormatString BadImageFormat_ResourceNameCorrupted  { get; private set; } = null!;

        public static IFormatString BadImageFormat_ResourceNameCorrupted_NameIndex  { get; private set; } = null!;

        public static IFormatString BadImageFormat_ResourcesDataInvalidOffset  { get; private set; } = null!;

        public static IFormatString BadImageFormat_ResourcesHeaderCorrupted  { get; private set; } = null!;

        public static IFormatString BadImageFormat_ResourcesIndexTooLong  { get; private set; } = null!;

        public static IFormatString BadImageFormat_ResourcesNameInvalidOffset  { get; private set; } = null!;

        public static IFormatString BadImageFormat_ResourcesNameTooLong  { get; private set; } = null!;

        public static IFormatString BadImageFormat_TypeMismatch  { get; private set; } = null!;

        public static IFormatString CancellationToken_CreateLinkedToken_TokensIsEmpty  { get; private set; } = null!;

        public static IFormatString CancellationTokenSource_Disposed  { get; private set; } = null!;

        public static IFormatString ConcurrentCollection_SyncRoot_NotSupported  { get; private set; } = null!;

        public static IFormatString EventSource_AbstractMustNotDeclareEventMethods  { get; private set; } = null!;

        public static IFormatString EventSource_AbstractMustNotDeclareKTOC  { get; private set; } = null!;

        public static IFormatString EventSource_AddScalarOutOfRange  { get; private set; } = null!;

        public static IFormatString EventSource_BadHexDigit  { get; private set; } = null!;

        public static IFormatString EventSource_ChannelTypeDoesNotMatchEventChannelValue  { get; private set; } = null!;

        public static IFormatString EventSource_DataDescriptorsOutOfRange  { get; private set; } = null!;

        public static IFormatString EventSource_DuplicateStringKey  { get; private set; } = null!;

        public static IFormatString EventSource_EnumKindMismatch  { get; private set; } = null!;

        public static IFormatString EventSource_EvenHexDigits  { get; private set; } = null!;

        public static IFormatString EventSource_EventChannelOutOfRange  { get; private set; } = null!;

        public static IFormatString EventSource_EventIdReused  { get; private set; } = null!;

        public static IFormatString EventSource_EventMustHaveTaskIfNonDefaultOpcode  { get; private set; } = null!;

        public static IFormatString EventSource_EventMustNotBeExplicitImplementation  { get; private set; } = null!;

        public static IFormatString EventSource_EventNameReused  { get; private set; } = null!;

        public static IFormatString EventSource_EventParametersMismatch  { get; private set; } = null!;

        public static IFormatString EventSource_EventSourceGuidInUse  { get; private set; } = null!;

        public static IFormatString EventSource_EventTooBig  { get; private set; } = null!;

        public static IFormatString EventSource_EventWithAdminChannelMustHaveMessage  { get; private set; } = null!;

        public static IFormatString EventSource_IllegalKeywordsValue  { get; private set; } = null!;

        public static IFormatString EventSource_IllegalOpcodeValue  { get; private set; } = null!;

        public static IFormatString EventSource_IllegalTaskValue  { get; private set; } = null!;

        public static IFormatString EventSource_IllegalValue  { get; private set; } = null!;

        public static IFormatString EventSource_IncorrentlyAuthoredTypeInfo  { get; private set; } = null!;

        public static IFormatString EventSource_InvalidCommand  { get; private set; } = null!;

        public static IFormatString EventSource_InvalidEventFormat  { get; private set; } = null!;

        public static IFormatString EventSource_KeywordCollision  { get; private set; } = null!;

        public static IFormatString EventSource_KeywordNeedPowerOfTwo  { get; private set; } = null!;

        public static IFormatString EventSource_ListenerCreatedInsideCallback  { get; private set; } = null!;

        public static IFormatString EventSource_ListenerNotFound  { get; private set; } = null!;

        public static IFormatString EventSource_ListenerWriteFailure  { get; private set; } = null!;

        public static IFormatString EventSource_MaxChannelExceeded  { get; private set; } = null!;

        public static IFormatString EventSource_MismatchIdToWriteEvent  { get; private set; } = null!;

        public static IFormatString EventSource_NeedGuid  { get; private set; } = null!;

        public static IFormatString EventSource_NeedName  { get; private set; } = null!;

        public static IFormatString EventSource_NeedPositiveId  { get; private set; } = null!;

        public static IFormatString EventSource_NoFreeBuffers  { get; private set; } = null!;

        public static IFormatString EventSource_NonCompliantTypeError  { get; private set; } = null!;

        public static IFormatString EventSource_NoRelatedActivityId  { get; private set; } = null!;

        public static IFormatString EventSource_NotSupportedArrayOfBinary  { get; private set; } = null!;

        public static IFormatString EventSource_NotSupportedArrayOfNil  { get; private set; } = null!;

        public static IFormatString EventSource_NotSupportedArrayOfNullTerminatedString  { get; private set; } = null!;

        public static IFormatString EventSource_NotSupportedNestedArraysEnums  { get; private set; } = null!;

        public static IFormatString EventSource_NullInput  { get; private set; } = null!;

        public static IFormatString EventSource_OpcodeCollision  { get; private set; } = null!;

        public static IFormatString EventSource_PinArrayOutOfRange  { get; private set; } = null!;

        public static IFormatString EventSource_RecursiveTypeDefinition  { get; private set; } = null!;

        public static IFormatString EventSource_StopsFollowStarts  { get; private set; } = null!;

        public static IFormatString EventSource_TaskCollision  { get; private set; } = null!;

        public static IFormatString EventSource_TaskOpcodePairReused  { get; private set; } = null!;

        public static IFormatString EventSource_TooManyArgs  { get; private set; } = null!;

        public static IFormatString EventSource_TooManyFields  { get; private set; } = null!;

        public static IFormatString EventSource_ToString  { get; private set; } = null!;

        public static IFormatString EventSource_TraitEven  { get; private set; } = null!;

        public static IFormatString EventSource_TypeMustBeSealedOrAbstract  { get; private set; } = null!;

        public static IFormatString EventSource_TypeMustDeriveFromEventSource  { get; private set; } = null!;

        public static IFormatString EventSource_UndefinedChannel  { get; private set; } = null!;

        public static IFormatString EventSource_UndefinedKeyword  { get; private set; } = null!;

        public static IFormatString EventSource_UndefinedOpcode  { get; private set; } = null!;

        public static IFormatString EventSource_UnknownEtwTrait  { get; private set; } = null!;

        public static IFormatString EventSource_UnsupportedEventTypeInManifest  { get; private set; } = null!;

        public static IFormatString EventSource_UnsupportedMessageProperty  { get; private set; } = null!;

        public static IFormatString EventSource_VarArgsParameterMismatch  { get; private set; } = null!;

        public static IFormatString Exception_EndOfInnerExceptionStack  { get; private set; } = null!;

        public static IFormatString Exception_EndStackTraceFromPreviousThrow  { get; private set; } = null!;

        public static IFormatString Exception_WasThrown  { get; private set; } = null!;

        public static IFormatString ExecutionContext_ExceptionInAsyncLocalNotification  { get; private set; } = null!;

        public static IFormatString FileNotFound_ResolveAssembly  { get; private set; } = null!;

        public static IFormatString Format_AttributeUsage  { get; private set; } = null!;

        public static IFormatString Format_Bad7BitInt  { get; private set; } = null!;

        public static IFormatString Format_BadBase64Char  { get; private set; } = null!;

        public static IFormatString Format_BadBoolean  { get; private set; } = null!;

        public static IFormatString Format_BadFormatSpecifier  { get; private set; } = null!;

        public static IFormatString Format_NoFormatSpecifier  { get; private set; } = null!;

        public static IFormatString Format_BadHexChar  { get; private set; } = null!;

        public static IFormatString Format_BadHexLength  { get; private set; } = null!;

        public static IFormatString Format_BadQuote  { get; private set; } = null!;

        public static IFormatString Format_BadTimeSpan  { get; private set; } = null!;

        public static IFormatString Format_EmptyInputString  { get; private set; } = null!;

        public static IFormatString Format_ExtraJunkAtEnd  { get; private set; } = null!;

        public static IFormatString Format_GuidUnrecognized  { get; private set; } = null!;

        public static IFormatString Format_IndexOutOfRange  { get; private set; } = null!;

        public static IFormatString Format_InvalidEnumFormatSpecification  { get; private set; } = null!;

        public static IFormatString Format_InvalidGuidFormatSpecification  { get; private set; } = null!;

        public static IFormatString Format_InvalidString  { get; private set; } = null!;

        public static IFormatString Format_NeedSingleChar  { get; private set; } = null!;

        public static IFormatString Format_NoParsibleDigits  { get; private set; } = null!;

        public static IFormatString Format_StringZeroLength  { get; private set; } = null!;

        public static IFormatString IndexOutOfRange_ArrayRankIndex  { get; private set; } = null!;

        public static IFormatString IndexOutOfRange_UMSPosition  { get; private set; } = null!;

        public static IFormatString InsufficientMemory_MemFailPoint  { get; private set; } = null!;

        public static IFormatString InsufficientMemory_MemFailPoint_TooBig  { get; private set; } = null!;

        public static IFormatString InsufficientMemory_MemFailPoint_VAFrag  { get; private set; } = null!;

        public static IFormatString Interop_COM_TypeMismatch  { get; private set; } = null!;

        public static IFormatString Interop_Marshal_Unmappable_Char  { get; private set; } = null!;

        public static IFormatString Interop_Marshal_SafeHandle_InvalidOperation  { get; private set; } = null!;

        public static IFormatString Interop_Marshal_CannotCreateSafeHandleField  { get; private set; } = null!;

        public static IFormatString Interop_Marshal_CannotCreateCriticalHandleField  { get; private set; } = null!;

        public static IFormatString InvalidCast_CannotCastNullToValueType  { get; private set; } = null!;

        public static IFormatString InvalidCast_CannotCoerceByRefVariant  { get; private set; } = null!;

        public static IFormatString InvalidCast_DBNull  { get; private set; } = null!;

        public static IFormatString InvalidCast_Empty  { get; private set; } = null!;

        public static IFormatString InvalidCast_FromDBNull  { get; private set; } = null!;

        public static IFormatString InvalidCast_FromTo  { get; private set; } = null!;

        public static IFormatString InvalidCast_IConvertible  { get; private set; } = null!;

        public static IFormatString InvalidOperation_AsyncFlowCtrlCtxMismatch  { get; private set; } = null!;

        public static IFormatString InvalidOperation_AsyncIOInProgress  { get; private set; } = null!;

        public static IFormatString InvalidOperation_BadEmptyMethodBody  { get; private set; } = null!;

        public static IFormatString InvalidOperation_BadILGeneratorUsage  { get; private set; } = null!;

        public static IFormatString InvalidOperation_BadInstructionOrIndexOutOfBound  { get; private set; } = null!;

        public static IFormatString InvalidOperation_BadInterfaceNotAbstract  { get; private set; } = null!;

        public static IFormatString InvalidOperation_BadMethodBody  { get; private set; } = null!;

        public static IFormatString InvalidOperation_BadTypeAttributesNotAbstract  { get; private set; } = null!;

        public static IFormatString InvalidOperation_CalledTwice  { get; private set; } = null!;

        public static IFormatString InvalidOperation_CannotImportGlobalFromDifferentModule  { get; private set; } = null!;

        public static IFormatString InvalidOperation_CannotRegisterSecondResolver  { get; private set; } = null!;

        public static IFormatString InvalidOperation_CannotRestoreUnsupressedFlow  { get; private set; } = null!;

        public static IFormatString InvalidOperation_CannotSupressFlowMultipleTimes  { get; private set; } = null!;

        public static IFormatString InvalidOperation_CannotUseAFCMultiple  { get; private set; } = null!;

        public static IFormatString InvalidOperation_CannotUseAFCOtherThread  { get; private set; } = null!;

        public static IFormatString InvalidOperation_CollectionCorrupted  { get; private set; } = null!;

        public static IFormatString InvalidOperation_ComputerName  { get; private set; } = null!;

        public static IFormatString InvalidOperation_ConcurrentOperationsNotSupported  { get; private set; } = null!;

        public static IFormatString InvalidOperation_ConstructorNotAllowedOnInterface  { get; private set; } = null!;

        public static IFormatString InvalidOperation_DateTimeParsing  { get; private set; } = null!;

        public static IFormatString InvalidOperation_DefaultConstructorILGen  { get; private set; } = null!;

        public static IFormatString InvalidOperation_EndReadCalledMultiple  { get; private set; } = null!;

        public static IFormatString InvalidOperation_EndWriteCalledMultiple  { get; private set; } = null!;

        public static IFormatString InvalidOperation_EnumEnded  { get; private set; } = null!;

        public static IFormatString InvalidOperation_EnumFailedVersion  { get; private set; } = null!;

        public static IFormatString InvalidOperation_EnumNotStarted  { get; private set; } = null!;

        public static IFormatString InvalidOperation_EnumOpCantHappen  { get; private set; } = null!;

        public static IFormatString InvalidOperation_EventInfoNotAvailable  { get; private set; } = null!;

        public static IFormatString InvalidOperation_GenericParametersAlreadySet  { get; private set; } = null!;

        public static IFormatString InvalidOperation_GetVersion  { get; private set; } = null!;

        public static IFormatString InvalidOperation_GlobalsHaveBeenCreated  { get; private set; } = null!;

        public static IFormatString InvalidOperation_HandleIsNotInitialized  { get; private set; } = null!;

        public static IFormatString InvalidOperation_HandleIsNotPinned  { get; private set; } = null!;

        public static IFormatString InvalidOperation_HashInsertFailed  { get; private set; } = null!;

        public static IFormatString InvalidOperation_IComparerFailed  { get; private set; } = null!;

        public static IFormatString InvalidOperation_MethodBaked  { get; private set; } = null!;

        public static IFormatString InvalidOperation_MethodBuilderBaked  { get; private set; } = null!;

        public static IFormatString InvalidOperation_MethodHasBody  { get; private set; } = null!;

        public static IFormatString InvalidOperation_MustCallInitialize  { get; private set; } = null!;

        public static IFormatString InvalidOperation_NativeOverlappedReused  { get; private set; } = null!;

        public static IFormatString InvalidOperation_NoMultiModuleAssembly  { get; private set; } = null!;

        public static IFormatString InvalidOperation_NoPublicAddMethod  { get; private set; } = null!;

        public static IFormatString InvalidOperation_NoPublicRemoveMethod  { get; private set; } = null!;

        public static IFormatString InvalidOperation_NotADebugModule  { get; private set; } = null!;

        public static IFormatString InvalidOperation_NotAllowedInDynamicMethod  { get; private set; } = null!;

        public static IFormatString InvalidOperation_NotAVarArgCallingConvention  { get; private set; } = null!;

        public static IFormatString InvalidOperation_NotGenericType  { get; private set; } = null!;

        public static IFormatString InvalidOperation_NotWithConcurrentGC  { get; private set; } = null!;

        public static IFormatString InvalidOperation_NoUnderlyingTypeOnEnum  { get; private set; } = null!;

        public static IFormatString InvalidOperation_NoValue  { get; private set; } = null!;

        public static IFormatString InvalidOperation_NullArray  { get; private set; } = null!;

        public static IFormatString InvalidOperation_NullContext  { get; private set; } = null!;

        public static IFormatString InvalidOperation_NullModuleHandle  { get; private set; } = null!;

        public static IFormatString InvalidOperation_OpenLocalVariableScope  { get; private set; } = null!;

        public static IFormatString InvalidOperation_Overlapped_Pack  { get; private set; } = null!;

        public static IFormatString InvalidOperation_PropertyInfoNotAvailable  { get; private set; } = null!;

        public static IFormatString InvalidOperation_ReadOnly  { get; private set; } = null!;

        public static IFormatString InvalidOperation_ResMgrBadResSet_Type  { get; private set; } = null!;

        public static IFormatString InvalidOperation_ResourceNotStream_Name  { get; private set; } = null!;

        public static IFormatString InvalidOperation_ResourceNotString_Name  { get; private set; } = null!;

        public static IFormatString InvalidOperation_ResourceNotString_Type  { get; private set; } = null!;

        public static IFormatString InvalidOperation_SetLatencyModeNoGC  { get; private set; } = null!;

        public static IFormatString InvalidOperation_ShouldNotHaveMethodBody  { get; private set; } = null!;

        public static IFormatString InvalidOperation_ThreadWrongThreadStart  { get; private set; } = null!;

        public static IFormatString InvalidOperation_TimeoutsNotSupported  { get; private set; } = null!;

        public static IFormatString InvalidOperation_TimerAlreadyClosed  { get; private set; } = null!;

        public static IFormatString InvalidOperation_TypeHasBeenCreated  { get; private set; } = null!;

        public static IFormatString InvalidOperation_TypeNotCreated  { get; private set; } = null!;

        public static IFormatString InvalidOperation_UnderlyingArrayListChanged  { get; private set; } = null!;

        public static IFormatString InvalidOperation_UnknownEnumType  { get; private set; } = null!;

        public static IFormatString InvalidOperation_WriteOnce  { get; private set; } = null!;

        public static IFormatString InvalidOperation_WrongAsyncResultOrEndCalledMultiple  { get; private set; } = null!;

        public static IFormatString InvalidOperation_WrongAsyncResultOrEndReadCalledMultiple  { get; private set; } = null!;

        public static IFormatString InvalidOperation_WrongAsyncResultOrEndWriteCalledMultiple  { get; private set; } = null!;

        public static IFormatString InvalidProgram_Default  { get; private set; } = null!;

        public static IFormatString InvalidTimeZone_InvalidRegistryData  { get; private set; } = null!;

        public static IFormatString InvariantFailed  { get; private set; } = null!;

        public static IFormatString InvariantFailed_Cnd  { get; private set; } = null!;

        public static IFormatString IO_NoFileTableInInMemoryAssemblies  { get; private set; } = null!;

        public static IFormatString IO_EOF_ReadBeyondEOF  { get; private set; } = null!;

        public static IFormatString IO_FileLoad  { get; private set; } = null!;

        public static IFormatString IO_FileName_Name  { get; private set; } = null!;

        public static IFormatString IO_FileNotFound  { get; private set; } = null!;

        public static IFormatString IO_FileNotFound_FileName  { get; private set; } = null!;

        public static IFormatString IO_AlreadyExists_Name  { get; private set; } = null!;

        public static IFormatString IO_BindHandleFailed  { get; private set; } = null!;

        public static IFormatString IO_FileExists_Name  { get; private set; } = null!;

        public static IFormatString IO_FileStreamHandlePosition  { get; private set; } = null!;

        public static IFormatString IO_FileTooLongOrHandleNotSync  { get; private set; } = null!;

        public static IFormatString IO_FixedCapacity  { get; private set; } = null!;

        public static IFormatString IO_InvalidStringLen_Len  { get; private set; } = null!;

        public static IFormatString IO_SeekAppendOverwrite  { get; private set; } = null!;

        public static IFormatString IO_SeekBeforeBegin  { get; private set; } = null!;

        public static IFormatString IO_SetLengthAppendTruncate  { get; private set; } = null!;

        public static IFormatString IO_SharingViolation_File  { get; private set; } = null!;

        public static IFormatString IO_SharingViolation_NoFileName  { get; private set; } = null!;

        public static IFormatString IO_StreamTooLong  { get; private set; } = null!;

        public static IFormatString IO_PathNotFound_NoPathName  { get; private set; } = null!;

        public static IFormatString IO_PathNotFound_Path  { get; private set; } = null!;

        public static IFormatString IO_PathTooLong  { get; private set; } = null!;

        public static IFormatString IO_PathTooLong_Path  { get; private set; } = null!;

        public static IFormatString IO_UnknownFileName  { get; private set; } = null!;

        public static IFormatString Lazy_CreateValue_NoParameterlessCtorForT  { get; private set; } = null!;

        public static IFormatString Lazy_ctor_ModeInvalid  { get; private set; } = null!;

        public static IFormatString Lazy_StaticInit_InvalidOperation  { get; private set; } = null!;

        public static IFormatString Lazy_ToString_ValueNotCreated  { get; private set; } = null!;

        public static IFormatString Lazy_Value_RecursiveCallsToValue  { get; private set; } = null!;

        public static IFormatString ManualResetEventSlim_ctor_SpinCountOutOfRange  { get; private set; } = null!;

        public static IFormatString ManualResetEventSlim_ctor_TooManyWaiters  { get; private set; } = null!;

        public static IFormatString ManualResetEventSlim_Disposed  { get; private set; } = null!;

        public static IFormatString Marshaler_StringTooLong  { get; private set; } = null!;

        public static IFormatString MissingConstructor_Name  { get; private set; } = null!;

        public static IFormatString MissingField  { get; private set; } = null!;

        public static IFormatString MissingField_Name  { get; private set; } = null!;

        public static IFormatString MissingManifestResource_MultipleBlobs  { get; private set; } = null!;

        public static IFormatString MissingManifestResource_NoNeutralAsm  { get; private set; } = null!;

        public static IFormatString MissingManifestResource_NoNeutralDisk  { get; private set; } = null!;

        public static IFormatString MissingMember  { get; private set; } = null!;

        public static IFormatString MissingMember_Name  { get; private set; } = null!;

        public static IFormatString MissingMemberNestErr  { get; private set; } = null!;

        public static IFormatString MissingMemberTypeRef  { get; private set; } = null!;

        public static IFormatString MissingMethod_Name  { get; private set; } = null!;

        public static IFormatString MissingSatelliteAssembly_Culture_Name  { get; private set; } = null!;

        public static IFormatString MissingSatelliteAssembly_Default  { get; private set; } = null!;

        public static IFormatString Multicast_Combine  { get; private set; } = null!;

        public static IFormatString MustUseCCRewrite  { get; private set; } = null!;

        public static IFormatString NotSupported_AbstractNonCLS  { get; private set; } = null!;

        public static IFormatString NotSupported_ActivAttr  { get; private set; } = null!;

        public static IFormatString NotSupported_AssemblyLoadFromHash  { get; private set; } = null!;

        public static IFormatString NotSupported_ByRefToByRefLikeReturn  { get; private set; } = null!;

        public static IFormatString NotSupported_ByRefToVoidReturn  { get; private set; } = null!;

        public static IFormatString NotSupported_CallToVarArg  { get; private set; } = null!;

        public static IFormatString NotSupported_CannotCallEqualsOnSpan  { get; private set; } = null!;

        public static IFormatString NotSupported_CannotCallGetHashCodeOnSpan  { get; private set; } = null!;

        public static IFormatString NotSupported_ChangeType  { get; private set; } = null!;

        public static IFormatString NotSupported_CreateInstanceWithTypeBuilder  { get; private set; } = null!;

        public static IFormatString NotSupported_DBNullSerial  { get; private set; } = null!;

        public static IFormatString NotSupported_DynamicAssembly  { get; private set; } = null!;

        public static IFormatString NotSupported_DynamicMethodFlags  { get; private set; } = null!;

        public static IFormatString NotSupported_DynamicModule  { get; private set; } = null!;

        public static IFormatString NotSupported_FileStreamOnNonFiles  { get; private set; } = null!;

        public static IFormatString NotSupported_FixedSizeCollection  { get; private set; } = null!;

        public static IFormatString InvalidOperation_SpanOverlappedOperation  { get; private set; } = null!;

        public static IFormatString NotSupported_IllegalOneByteBranch  { get; private set; } = null!;

        public static IFormatString NotSupported_KeyCollectionSet  { get; private set; } = null!;

        public static IFormatString NotSupported_MaxWaitHandles  { get; private set; } = null!;

        public static IFormatString NotSupported_MemStreamNotExpandable  { get; private set; } = null!;

        public static IFormatString NotSupported_MustBeModuleBuilder  { get; private set; } = null!;

        public static IFormatString NotSupported_NoCodepageData  { get; private set; } = null!;

        public static IFormatString InvalidOperation_FunctionMissingUnmanagedCallersOnly  { get; private set; } = null!;

        public static IFormatString NotSupported_NonReflectedType  { get; private set; } = null!;

        public static IFormatString NotSupported_NoParentDefaultConstructor  { get; private set; } = null!;

        public static IFormatString NotSupported_NoTypeInfo  { get; private set; } = null!;

        public static IFormatString NotSupported_NYI  { get; private set; } = null!;

        public static IFormatString NotSupported_ObsoleteResourcesFile  { get; private set; } = null!;

        public static IFormatString NotSupported_OutputStreamUsingTypeBuilder  { get; private set; } = null!;

        public static IFormatString NotSupported_RangeCollection  { get; private set; } = null!;

        public static IFormatString NotSupported_Reading  { get; private set; } = null!;

        public static IFormatString NotSupported_ReadOnlyCollection  { get; private set; } = null!;

        public static IFormatString NotSupported_ResourceObjectSerialization  { get; private set; } = null!;

        public static IFormatString NotSupported_StringComparison  { get; private set; } = null!;

        public static IFormatString NotSupported_SubclassOverride  { get; private set; } = null!;

        public static IFormatString NotSupported_SymbolMethod  { get; private set; } = null!;

        public static IFormatString NotSupported_Type  { get; private set; } = null!;

        public static IFormatString NotSupported_TypeNotYetCreated  { get; private set; } = null!;

        public static IFormatString NotSupported_UmsSafeBuffer  { get; private set; } = null!;

        public static IFormatString NotSupported_UnitySerHolder  { get; private set; } = null!;

        public static IFormatString NotSupported_UnknownTypeCode  { get; private set; } = null!;

        public static IFormatString NotSupported_UnreadableStream  { get; private set; } = null!;

        public static IFormatString NotSupported_UnseekableStream  { get; private set; } = null!;

        public static IFormatString NotSupported_UnwritableStream  { get; private set; } = null!;

        public static IFormatString NotSupported_ValueCollectionSet  { get; private set; } = null!;

        public static IFormatString NotSupported_Writing  { get; private set; } = null!;

        public static IFormatString NotSupported_WrongResourceReader_Type  { get; private set; } = null!;

        public static IFormatString ObjectDisposed_FileClosed  { get; private set; } = null!;

        public static IFormatString ObjectDisposed_Generic  { get; private set; } = null!;

        public static IFormatString ObjectDisposed_ObjectName_Name  { get; private set; } = null!;

        public static IFormatString ObjectDisposed_WriterClosed  { get; private set; } = null!;

        public static IFormatString ObjectDisposed_ReaderClosed  { get; private set; } = null!;

        public static IFormatString ObjectDisposed_ResourceSet  { get; private set; } = null!;

        public static IFormatString ObjectDisposed_StreamClosed  { get; private set; } = null!;

        public static IFormatString ObjectDisposed_ViewAccessorClosed  { get; private set; } = null!;

        public static IFormatString ObjectDisposed_SafeHandleClosed  { get; private set; } = null!;

        public static IFormatString OperationCanceled  { get; private set; } = null!;

        public static IFormatString Overflow_Byte  { get; private set; } = null!;

        public static IFormatString Overflow_Char  { get; private set; } = null!;

        public static IFormatString Overflow_Currency  { get; private set; } = null!;

        public static IFormatString Overflow_Decimal  { get; private set; } = null!;

        public static IFormatString Overflow_Duration  { get; private set; } = null!;

        public static IFormatString Overflow_Int16  { get; private set; } = null!;

        public static IFormatString Overflow_Int32  { get; private set; } = null!;

        public static IFormatString Overflow_Int64  { get; private set; } = null!;

        public static IFormatString Overflow_NegateTwosCompNum  { get; private set; } = null!;

        public static IFormatString Overflow_NegativeUnsigned  { get; private set; } = null!;

        public static IFormatString Overflow_SByte  { get; private set; } = null!;

        public static IFormatString Overflow_TimeSpanElementTooLarge  { get; private set; } = null!;

        public static IFormatString Overflow_TimeSpanTooLong  { get; private set; } = null!;

        public static IFormatString Overflow_UInt16  { get; private set; } = null!;

        public static IFormatString Overflow_UInt32  { get; private set; } = null!;

        public static IFormatString Overflow_UInt64  { get; private set; } = null!;

        public static IFormatString PlatformNotSupported_ReflectionOnly  { get; private set; } = null!;

        public static IFormatString PlatformNotSupported_Remoting  { get; private set; } = null!;

        public static IFormatString PlatformNotSupported_SecureBinarySerialization  { get; private set; } = null!;

        public static IFormatString PlatformNotSupported_StrongNameSigning  { get; private set; } = null!;

        public static IFormatString PlatformNotSupported_ITypeInfo  { get; private set; } = null!;

        public static IFormatString PlatformNotSupported_IExpando  { get; private set; } = null!;

        public static IFormatString PlatformNotSupported_AppDomains  { get; private set; } = null!;

        public static IFormatString PlatformNotSupported_CAS  { get; private set; } = null!;

        public static IFormatString PlatformNotSupported_Principal  { get; private set; } = null!;

        public static IFormatString PlatformNotSupported_ThreadAbort  { get; private set; } = null!;

        public static IFormatString PlatformNotSupported_ThreadSuspend  { get; private set; } = null!;

        public static IFormatString PostconditionFailed  { get; private set; } = null!;

        public static IFormatString PostconditionFailed_Cnd  { get; private set; } = null!;

        public static IFormatString PostconditionOnExceptionFailed  { get; private set; } = null!;

        public static IFormatString PostconditionOnExceptionFailed_Cnd  { get; private set; } = null!;

        public static IFormatString PreconditionFailed  { get; private set; } = null!;

        public static IFormatString PreconditionFailed_Cnd  { get; private set; } = null!;

        public static IFormatString Rank_MultiDimNotSupported  { get; private set; } = null!;

        public static IFormatString Rank_MustMatch  { get; private set; } = null!;

        public static IFormatString ResourceReaderIsClosed  { get; private set; } = null!;

        public static IFormatString Resources_StreamNotValid  { get; private set; } = null!;

        public static IFormatString RFLCT_AmbigCust  { get; private set; } = null!;

        public static IFormatString RFLCT_Ambiguous  { get; private set; } = null!;

        public static IFormatString InvalidFilterCriteriaException_CritInt  { get; private set; } = null!;

        public static IFormatString InvalidFilterCriteriaException_CritString  { get; private set; } = null!;

        public static IFormatString RFLCT_InvalidFieldFail  { get; private set; } = null!;

        public static IFormatString RFLCT_InvalidPropFail  { get; private set; } = null!;

        public static IFormatString RFLCT_Targ_ITargMismatch  { get; private set; } = null!;

        public static IFormatString RFLCT_Targ_StatFldReqTarg  { get; private set; } = null!;

        public static IFormatString RFLCT_Targ_StatMethReqTarg  { get; private set; } = null!;

        public static IFormatString RuntimeWrappedException  { get; private set; } = null!;

        public static IFormatString StandardOleMarshalObjectGetMarshalerFailed  { get; private set; } = null!;

        public static IFormatString Security_CannotReadRegistryData  { get; private set; } = null!;

        public static IFormatString Security_RegistryPermission  { get; private set; } = null!;

        public static IFormatString SemaphoreSlim_ctor_InitialCountWrong  { get; private set; } = null!;

        public static IFormatString SemaphoreSlim_ctor_MaxCountWrong  { get; private set; } = null!;

        public static IFormatString SemaphoreSlim_Disposed  { get; private set; } = null!;

        public static IFormatString SemaphoreSlim_Release_CountWrong  { get; private set; } = null!;

        public static IFormatString SemaphoreSlim_Wait_TimeoutWrong  { get; private set; } = null!;

        public static IFormatString Serialization_BadParameterInfo  { get; private set; } = null!;

        public static IFormatString Serialization_CorruptField  { get; private set; } = null!;

        public static IFormatString Serialization_DateTimeTicksOutOfRange  { get; private set; } = null!;

        public static IFormatString Serialization_DelegatesNotSupported  { get; private set; } = null!;

        public static IFormatString Serialization_InsufficientState  { get; private set; } = null!;

        public static IFormatString Serialization_InvalidData  { get; private set; } = null!;

        public static IFormatString Serialization_InvalidEscapeSequence  { get; private set; } = null!;

        public static IFormatString Serialization_InvalidOnDeser  { get; private set; } = null!;

        public static IFormatString Serialization_InvalidType  { get; private set; } = null!;

        public static IFormatString Serialization_KeyValueDifferentSizes  { get; private set; } = null!;

        public static IFormatString Serialization_MissingDateTimeData  { get; private set; } = null!;

        public static IFormatString Serialization_MissingKeys  { get; private set; } = null!;

        public static IFormatString Serialization_MissingValues  { get; private set; } = null!;

        public static IFormatString Serialization_NoParameterInfo  { get; private set; } = null!;

        public static IFormatString Serialization_NotFound  { get; private set; } = null!;

        public static IFormatString Serialization_NullKey  { get; private set; } = null!;

        public static IFormatString Serialization_OptionalFieldVersionValue  { get; private set; } = null!;

        public static IFormatString Serialization_SameNameTwice  { get; private set; } = null!;

        public static IFormatString Serialization_StringBuilderCapacity  { get; private set; } = null!;

        public static IFormatString Serialization_StringBuilderMaxCapacity  { get; private set; } = null!;

        public static IFormatString SpinLock_Exit_SynchronizationLockException  { get; private set; } = null!;

        public static IFormatString SpinLock_IsHeldByCurrentThread  { get; private set; } = null!;

        public static IFormatString SpinLock_TryEnter_ArgumentOutOfRange  { get; private set; } = null!;

        public static IFormatString SpinLock_TryEnter_LockRecursionException  { get; private set; } = null!;

        public static IFormatString SpinLock_TryReliableEnter_ArgumentException  { get; private set; } = null!;

        public static IFormatString SpinWait_SpinUntil_ArgumentNull  { get; private set; } = null!;

        public static IFormatString SpinWait_SpinUntil_TimeoutWrong  { get; private set; } = null!;

        public static IFormatString Task_ContinueWith_ESandLR  { get; private set; } = null!;

        public static IFormatString Task_ContinueWith_NotOnAnything  { get; private set; } = null!;

        public static IFormatString Task_Delay_InvalidDelay  { get; private set; } = null!;

        public static IFormatString Task_Delay_InvalidMillisecondsDelay  { get; private set; } = null!;

        public static IFormatString Task_Dispose_NotCompleted  { get; private set; } = null!;

        public static IFormatString Task_FromAsync_LongRunning  { get; private set; } = null!;

        public static IFormatString Task_FromAsync_PreferFairness  { get; private set; } = null!;

        public static IFormatString Task_MultiTaskContinuation_EmptyTaskList  { get; private set; } = null!;

        public static IFormatString Task_MultiTaskContinuation_FireOptions  { get; private set; } = null!;

        public static IFormatString Task_MultiTaskContinuation_NullTask  { get; private set; } = null!;

        public static IFormatString Task_RunSynchronously_AlreadyStarted  { get; private set; } = null!;

        public static IFormatString Task_RunSynchronously_Continuation  { get; private set; } = null!;

        public static IFormatString Task_RunSynchronously_Promise  { get; private set; } = null!;

        public static IFormatString Task_RunSynchronously_TaskCompleted  { get; private set; } = null!;

        public static IFormatString Task_Start_AlreadyStarted  { get; private set; } = null!;

        public static IFormatString Task_Start_ContinuationTask  { get; private set; } = null!;

        public static IFormatString Task_Start_Promise  { get; private set; } = null!;

        public static IFormatString Task_Start_TaskCompleted  { get; private set; } = null!;

        public static IFormatString Task_ThrowIfDisposed  { get; private set; } = null!;

        public static IFormatString Task_WaitMulti_NullTask  { get; private set; } = null!;

        public static IFormatString TaskCanceledException_ctor_DefaultMessage  { get; private set; } = null!;

        public static IFormatString TaskCompletionSourceT_TrySetException_NoExceptions  { get; private set; } = null!;

        public static IFormatString TaskCompletionSourceT_TrySetException_NullException  { get; private set; } = null!;

        public static IFormatString TaskExceptionHolder_UnhandledException  { get; private set; } = null!;

        public static IFormatString TaskExceptionHolder_UnknownExceptionType  { get; private set; } = null!;

        public static IFormatString TaskScheduler_ExecuteTask_WrongTaskScheduler  { get; private set; } = null!;

        public static IFormatString TaskScheduler_FromCurrentSynchronizationContext_NoCurrent  { get; private set; } = null!;

        public static IFormatString TaskScheduler_InconsistentStateAfterTryExecuteTaskInline  { get; private set; } = null!;

        public static IFormatString TaskSchedulerException_ctor_DefaultMessage  { get; private set; } = null!;

        public static IFormatString TaskT_DebuggerNoResult  { get; private set; } = null!;

        public static IFormatString TaskT_TransitionToFinal_AlreadyCompleted  { get; private set; } = null!;

        public static IFormatString Thread_ApartmentState_ChangeFailed  { get; private set; } = null!;

        public static IFormatString Thread_GetSetCompressedStack_NotSupported  { get; private set; } = null!;

        public static IFormatString Thread_Operation_RequiresCurrentThread  { get; private set; } = null!;

        public static IFormatString Threading_AbandonedMutexException  { get; private set; } = null!;

        public static IFormatString Threading_WaitHandleCannotBeOpenedException  { get; private set; } = null!;

        public static IFormatString Threading_WaitHandleCannotBeOpenedException_InvalidHandle  { get; private set; } = null!;

        public static IFormatString Threading_WaitHandleTooManyPosts  { get; private set; } = null!;

        public static IFormatString Threading_SemaphoreFullException  { get; private set; } = null!;

        public static IFormatString ThreadLocal_Disposed  { get; private set; } = null!;

        public static IFormatString ThreadLocal_Value_RecursiveCallsToValue  { get; private set; } = null!;

        public static IFormatString ThreadLocal_ValuesNotAvailable  { get; private set; } = null!;

        public static IFormatString TimeZoneNotFound_MissingData  { get; private set; } = null!;

        public static IFormatString TypeInitialization_Default  { get; private set; } = null!;

        public static IFormatString TypeInitialization_Type  { get; private set; } = null!;

        public static IFormatString TypeLoad_ResolveNestedType  { get; private set; } = null!;

        public static IFormatString TypeLoad_ResolveType  { get; private set; } = null!;

        public static IFormatString TypeLoad_ResolveTypeFromAssembly  { get; private set; } = null!;

        public static IFormatString UnauthorizedAccess_IODenied_NoPathName  { get; private set; } = null!;

        public static IFormatString UnauthorizedAccess_IODenied_Path  { get; private set; } = null!;

        public static IFormatString UnauthorizedAccess_MemStreamBuffer  { get; private set; } = null!;

        public static IFormatString UnauthorizedAccess_RegistryKeyGeneric_Key  { get; private set; } = null!;

        public static IFormatString UnknownError_Num  { get; private set; } = null!;

        public static IFormatString Verification_Exception  { get; private set; } = null!;

        public static IFormatString DebugAssertBanner  { get; private set; } = null!;

        public static IFormatString DebugAssertLongMessage  { get; private set; } = null!;

        public static IFormatString DebugAssertShortMessage  { get; private set; } = null!;

        public static IFormatString LockRecursionException_ReadAfterWriteNotAllowed  { get; private set; } = null!;

        public static IFormatString LockRecursionException_RecursiveReadNotAllowed  { get; private set; } = null!;

        public static IFormatString LockRecursionException_RecursiveWriteNotAllowed  { get; private set; } = null!;

        public static IFormatString LockRecursionException_RecursiveUpgradeNotAllowed  { get; private set; } = null!;

        public static IFormatString LockRecursionException_WriteAfterReadNotAllowed  { get; private set; } = null!;

        public static IFormatString SynchronizationLockException_MisMatchedUpgrade  { get; private set; } = null!;

        public static IFormatString SynchronizationLockException_MisMatchedRead  { get; private set; } = null!;

        public static IFormatString SynchronizationLockException_IncorrectDispose  { get; private set; } = null!;

        public static IFormatString LockRecursionException_UpgradeAfterReadNotAllowed  { get; private set; } = null!;

        public static IFormatString LockRecursionException_UpgradeAfterWriteNotAllowed  { get; private set; } = null!;

        public static IFormatString SynchronizationLockException_MisMatchedWrite  { get; private set; } = null!;

        public static IFormatString NotSupported_SignatureType  { get; private set; } = null!;

        public static IFormatString HashCode_HashCodeNotSupported  { get; private set; } = null!;

        public static IFormatString HashCode_EqualityNotSupported  { get; private set; } = null!;

        public static IFormatString Arg_TypeNotSupported  { get; private set; } = null!;

        public static IFormatString IO_InvalidReadLength  { get; private set; } = null!;

        public static IFormatString Arg_BasePathNotFullyQualified  { get; private set; } = null!;

        public static IFormatString Arg_ElementsInSourceIsGreaterThanDestination  { get; private set; } = null!;

        public static IFormatString Arg_NullArgumentNullRef  { get; private set; } = null!;

        public static IFormatString Argument_OverlapAlignmentMismatch  { get; private set; } = null!;

        public static IFormatString Arg_InsufficientNumberOfElements  { get; private set; } = null!;

        public static IFormatString Arg_MustBeNullTerminatedString  { get; private set; } = null!;

        public static IFormatString ArgumentOutOfRange_Week_ISO  { get; private set; } = null!;

        public static IFormatString Argument_BadPInvokeMethod  { get; private set; } = null!;

        public static IFormatString Argument_BadPInvokeOnInterface  { get; private set; } = null!;

        public static IFormatString Argument_MethodRedefined  { get; private set; } = null!;

        public static IFormatString Argument_CannotExtractScalar  { get; private set; } = null!;

        public static IFormatString Argument_CannotParsePrecision  { get; private set; } = null!;

        public static IFormatString Argument_GWithPrecisionNotSupported  { get; private set; } = null!;

        public static IFormatString Argument_PrecisionTooLarge  { get; private set; } = null!;

        public static IFormatString AssemblyDependencyResolver_FailedToLoadHostpolicy  { get; private set; } = null!;

        public static IFormatString AssemblyDependencyResolver_FailedToResolveDependencies  { get; private set; } = null!;

        public static IFormatString Arg_EnumNotCloneable  { get; private set; } = null!;

        public static IFormatString InvalidOp_InvalidNewEnumVariant  { get; private set; } = null!;

        public static IFormatString Argument_StructArrayTooLarge  { get; private set; } = null!;

        public static IFormatString IndexOutOfRange_ArrayWithOffset  { get; private set; } = null!;

        public static IFormatString Serialization_DangerousDeserialization  { get; private set; } = null!;

        public static IFormatString Serialization_DangerousDeserialization_Switch  { get; private set; } = null!;

        public static IFormatString Argument_InvalidStartupHookSimpleAssemblyName  { get; private set; } = null!;

        public static IFormatString Argument_StartupHookAssemblyLoadFailed  { get; private set; } = null!;

        public static IFormatString InvalidOperation_NonStaticComRegFunction  { get; private set; } = null!;

        public static IFormatString InvalidOperation_NonStaticComUnRegFunction  { get; private set; } = null!;

        public static IFormatString InvalidOperation_InvalidComRegFunctionSig  { get; private set; } = null!;

        public static IFormatString InvalidOperation_InvalidComUnRegFunctionSig  { get; private set; } = null!;

        public static IFormatString InvalidOperation_MultipleComRegFunctions  { get; private set; } = null!;

        public static IFormatString InvalidOperation_MultipleComUnRegFunctions  { get; private set; } = null!;

        public static IFormatString InvalidOperation_ResetGlobalComWrappersInstance  { get; private set; } = null!;

        public static IFormatString Argument_SpansMustHaveSameLength  { get; private set; } = null!;

        public static IFormatString NotSupported_CannotWriteToBufferedStreamIfReadBufferCannotBeFlushed  { get; private set; } = null!;

        public static IFormatString GenericInvalidData  { get; private set; } = null!;

        public static IFormatString Argument_ResourceScopeWrongDirection  { get; private set; } = null!;

        public static IFormatString ArgumentNull_TypeRequiredByResourceScope  { get; private set; } = null!;

        public static IFormatString Argument_BadResourceScopeTypeBits  { get; private set; } = null!;

        public static IFormatString Argument_BadResourceScopeVisibilityBits  { get; private set; } = null!;

        public static IFormatString net_emptystringcall  { get; private set; } = null!;

        public static IFormatString Argument_EmptyApplicationName  { get; private set; } = null!;

        public static IFormatString Argument_FrameworkNameInvalid  { get; private set; } = null!;

        public static IFormatString Argument_FrameworkNameInvalidVersion  { get; private set; } = null!;

        public static IFormatString Argument_FrameworkNameMissingVersion  { get; private set; } = null!;

        public static IFormatString Argument_FrameworkNameTooShort  { get; private set; } = null!;

        public static IFormatString Arg_SwitchExpressionException  { get; private set; } = null!;

        public static IFormatString Arg_ContextMarshalException  { get; private set; } = null!;

        public static IFormatString Arg_AppDomainUnloadedException  { get; private set; } = null!;

        public static IFormatString SwitchExpressionException_UnmatchedValue  { get; private set; } = null!;

        public static IFormatString Encoding_UTF7_Disabled  { get; private set; } = null!;

        public static IFormatString IDynamicInterfaceCastable_DoesNotImplementRequested  { get; private set; } = null!;

        public static IFormatString IDynamicInterfaceCastable_MissingImplementationAttribute  { get; private set; } = null!;

        public static IFormatString IDynamicInterfaceCastable_NotInterface  { get; private set; } = null!;

        public static IFormatString Arg_MustBeHalf  { get; private set; } = null!;

        public static IFormatString Arg_MustBeRune  { get; private set; } = null!;

        public static IFormatString BinaryFormatter_SerializationDisallowed  { get; private set; } = null!;

        public static IFormatString NotSupported_CodeBase  { get; private set; } = null!;
    }
}
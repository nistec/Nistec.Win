using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using System.Globalization;

namespace MControl.Win
{
    public enum ExceptionArgument
    {
        obj,
        dictionary,
        array,
        info,
        key,
        collection,
        match,
        converter,
        queue,
        stack,
        capacity,
        index,
        startIndex,
        value,
        count,
        arrayIndex
    }

    public enum ExceptionResource
    {
        Argument_ImplementIComparable,
        ArgumentOutOfRange_NeedNonNegNum,
        ArgumentOutOfRange_NeedNonNegNumRequired,
        Arg_ArrayPlusOffTooSmall,
        Argument_AddingDuplicate,
        Serialization_InvalidOnDeser,
        Serialization_MismatchedCount,
        Serialization_MissingValues,
        Arg_RankMultiDimNotSupported,
        Arg_NonZeroLowerBound,
        Argument_InvalidArrayType,
        NotSupported_KeyCollectionSet,
        ArgumentOutOfRange_SmallCapacity,
        ArgumentOutOfRange_Index,
        Argument_InvalidOffLen,
        NotSupported_ReadOnlyCollection,
        InvalidOperation_CannotRemoveFromStackOrQueue,
        InvalidOperation_EmptyCollection,
        InvalidOperation_EmptyQueue,
        InvalidOperation_EnumOpCantHappen,
        InvalidOperation_EnumFailedVersion,
        InvalidOperation_EmptyStack,
        InvalidOperation_EnumNotStarted,
        InvalidOperation_EnumEnded,
        NotSupported_GenericListNestedWrite,
        NotSupported_ValueCollectionSet
    }


    public static class ExceptionHelper
    {
        // Methods
        internal static string GetArgumentName(ExceptionArgument argument)
        {
            switch (argument)
            {
                case ExceptionArgument.obj:
                    return "obj";

                case ExceptionArgument.dictionary:
                    return "dictionary";

                case ExceptionArgument.array:
                    return "array";

                case ExceptionArgument.info:
                    return "info";

                case ExceptionArgument.key:
                    return "key";

                case ExceptionArgument.collection:
                    return "collection";

                case ExceptionArgument.match:
                    return "match";

                case ExceptionArgument.converter:
                    return "converter";

                case ExceptionArgument.queue:
                    return "queue";

                case ExceptionArgument.stack:
                    return "stack";

                case ExceptionArgument.capacity:
                    return "capacity";

                case ExceptionArgument.index:
                    return "index";

                case ExceptionArgument.startIndex:
                    return "startIndex";

                case ExceptionArgument.value:
                    return "value";

                case ExceptionArgument.count:
                    return "count";

                case ExceptionArgument.arrayIndex:
                    return "arrayIndex";
            }
            return string.Empty;
        }

        internal static string GetResourceName(ExceptionResource resource)
        {
            switch (resource)
            {
                case ExceptionResource.Argument_ImplementIComparable:
                    return "Argument_ImplementIComparable";

                case ExceptionResource.ArgumentOutOfRange_NeedNonNegNum:
                    return "ArgumentOutOfRange_NeedNonNegNum";

                case ExceptionResource.ArgumentOutOfRange_NeedNonNegNumRequired:
                    return "ArgumentOutOfRange_NeedNonNegNumRequired";

                case ExceptionResource.Arg_ArrayPlusOffTooSmall:
                    return "Arg_ArrayPlusOffTooSmall";

                case ExceptionResource.Argument_AddingDuplicate:
                    return "Argument_AddingDuplicate";

                case ExceptionResource.Serialization_InvalidOnDeser:
                    return "Serialization_InvalidOnDeser";

                case ExceptionResource.Serialization_MismatchedCount:
                    return "Serialization_MismatchedCount";

                case ExceptionResource.Serialization_MissingValues:
                    return "Serialization_MissingValues";

                case ExceptionResource.Arg_RankMultiDimNotSupported:
                    return "Arg_MultiRank";

                case ExceptionResource.Arg_NonZeroLowerBound:
                    return "Arg_NonZeroLowerBound";

                case ExceptionResource.Argument_InvalidArrayType:
                    return "Invalid_Array_Type";

                case ExceptionResource.NotSupported_KeyCollectionSet:
                    return "NotSupported_KeyCollectionSet";

                case ExceptionResource.ArgumentOutOfRange_SmallCapacity:
                    return "ArgumentOutOfRange_SmallCapacity";

                case ExceptionResource.ArgumentOutOfRange_Index:
                    return "ArgumentOutOfRange_Index";

                case ExceptionResource.Argument_InvalidOffLen:
                    return "Argument_InvalidOffLen";

                case ExceptionResource.InvalidOperation_CannotRemoveFromStackOrQueue:
                    return "InvalidOperation_CannotRemoveFromStackOrQueue";

                case ExceptionResource.InvalidOperation_EmptyCollection:
                    return "InvalidOperation_EmptyCollection";

                case ExceptionResource.InvalidOperation_EmptyQueue:
                    return "InvalidOperation_EmptyQueue";

                case ExceptionResource.InvalidOperation_EnumOpCantHappen:
                    return "InvalidOperation_EnumOpCantHappen";

                case ExceptionResource.InvalidOperation_EnumFailedVersion:
                    return "InvalidOperation_EnumFailedVersion";

                case ExceptionResource.InvalidOperation_EmptyStack:
                    return "InvalidOperation_EmptyStack";

                case ExceptionResource.InvalidOperation_EnumNotStarted:
                    return "InvalidOperation_EnumNotStarted";

                case ExceptionResource.InvalidOperation_EnumEnded:
                    return "InvalidOperation_EnumEnded";

                case ExceptionResource.NotSupported_GenericListNestedWrite:
                    return "NotSupported_GenericListNestedWrite";

                case ExceptionResource.NotSupported_ValueCollectionSet:
                    return "NotSupported_ValueCollectionSet";
            }
            return string.Empty;
        }

        public static void ArgumentException(ExceptionResource resource)
        {
            throw new ArgumentException(RM.GetString(GetResourceName(resource)));
        }

        public static void ArgumentNullException(ExceptionArgument argument)
        {
            throw new ArgumentNullException(GetArgumentName(argument));
        }

        public static void ArgumentOutOfRangeException(ExceptionArgument argument)
        {
            throw new ArgumentOutOfRangeException(GetArgumentName(argument));
        }

        public static void ArgumentOutOfRangeException(ExceptionArgument argument, ExceptionResource resource)
        {
            throw new ArgumentOutOfRangeException(GetArgumentName(argument), RM.GetString(GetResourceName(resource)));
        }

        public static void InvalidOperationException(ExceptionResource resource)
        {
            throw new InvalidOperationException(RM.GetString(GetResourceName(resource)));
        }

        public static void KeyNotFoundException()
        {
            throw new KeyNotFoundException();
        }

        public static void NotSupportedException(ExceptionResource resource)
        {
            throw new NotSupportedException(RM.GetString(GetResourceName(resource)));
        }

        public static void SerializationException(ExceptionResource resource)
        {
            throw new SerializationException(RM.GetString(GetResourceName(resource)));
        }

        public static void WrongKeyTypeArgumentException(object key, Type targetType)
        {
            throw new ArgumentException(RM.GetString("Arg_WrongType", new object[] { key, targetType }), "key");
        }

        public static void WrongValueTypeArgumentException(object value, Type targetType)
        {
            throw new ArgumentException(RM.GetString("Arg_WrongType", new object[] { value, targetType }), "value");
        }

        public static Exception ArgumentNull(string paramName)
        {
            return new ArgumentNullException(DataResx.GetString("Data_ArgumentNull", new object[] { paramName }));
        }

        public static Exception ArgumentOutOfRange(string paramName)
        {
            return new ArgumentOutOfRangeException(DataResx.GetString("Data_ArgumentOutOfRange", new object[] { paramName }));
        }

        public static Exception RangeArgument(int min, int max)
        {
            return new ArgumentException(DataResx.GetString("Range_Argument", new object[] { min.ToString(CultureInfo.InvariantCulture), max.ToString(CultureInfo.InvariantCulture) }));
        }
        public static Exception NullValues(string column)
        {
            return new ArgumentNullException(DataResx.GetString("DataColumn_NullValues", new object[] { column }));
        }

        public static Exception NullRange()
        {
            return new ArgumentNullException(DataResx.GetString("Range_NullRange"));
        }

        public static Exception TypeNotSupportedException(Type pType)
        {
            return new Exception("Type " + pType.ToString() + " not supported exception");
        }

        public static Exception InvalidFormatException(string parameter)
        {
            return new Exception("Invalid Format Exception in MControl.BindControl , " + parameter + " is not the correct format.");

        }
    }

}

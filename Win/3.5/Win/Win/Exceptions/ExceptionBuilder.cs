using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.ComponentModel;
using System.Globalization;
//using MControl.Data;

namespace MControl.Util
{
    public static class ExceptionBuilder
    {
        // Methods
        public static ArgumentException _Argument(string error)
        {
            ArgumentException e = new ArgumentException(error);
            TraceExceptionAsReturnValue(e);
            return e;
        }

        public static ArgumentException _Argument(string error, Exception innerException)
        {
            ArgumentException e = new ArgumentException(error, innerException);
            TraceExceptionAsReturnValue(e);
            return e;
        }

        public static ArgumentException _Argument(string paramName, string error)
        {
            ArgumentException e = new ArgumentException(error);
            TraceExceptionAsReturnValue(e);
            return e;
        }

        private static ArgumentNullException _ArgumentNull(string paramName, string msg)
        {
            ArgumentNullException e = new ArgumentNullException(paramName, msg);
            TraceExceptionAsReturnValue(e);
            return e;
        }

        public static ArgumentOutOfRangeException _ArgumentOutOfRange(string paramName, string msg)
        {
            ArgumentOutOfRangeException e = new ArgumentOutOfRangeException(paramName, msg);
            TraceExceptionAsReturnValue(e);
            return e;
        }

        private static ConstraintException _Constraint(string error)
        {
            ConstraintException e = new ConstraintException(error);
            TraceExceptionAsReturnValue(e);
            return e;
        }

        private static DataException _Data(string error)
        {
            DataException e = new DataException(error);
            TraceExceptionAsReturnValue(e);
            return e;
        }

        private static DataException _Data(string error, Exception innerException)
        {
            DataException e = new DataException(error);
            TraceExceptionAsReturnValue(e);
            return e;
        }

        private static DeletedRowInaccessibleException _DeletedRowInaccessible(string error)
        {
            DeletedRowInaccessibleException e = new DeletedRowInaccessibleException(error);
            TraceExceptionAsReturnValue(e);
            return e;
        }

        private static DuplicateNameException _DuplicateName(string error)
        {
            DuplicateNameException e = new DuplicateNameException(error);
            TraceExceptionAsReturnValue(e);
            return e;
        }

        private static IndexOutOfRangeException _IndexOutOfRange(string error)
        {
            IndexOutOfRangeException e = new IndexOutOfRangeException(error);
            TraceExceptionAsReturnValue(e);
            return e;
        }

        private static InRowChangingEventException _InRowChangingEvent(string error)
        {
            InRowChangingEventException e = new InRowChangingEventException(error);
            TraceExceptionAsReturnValue(e);
            return e;
        }

        private static InvalidConstraintException _InvalidConstraint(string error)
        {
            InvalidConstraintException e = new InvalidConstraintException(error);
            TraceExceptionAsReturnValue(e);
            return e;
        }

        private static InvalidEnumArgumentException _InvalidEnumArgumentException<T>(T value)
        {
            return _InvalidEnumArgumentException(DataResx.GetString("ADP_InvalidEnumerationValue", new object[] { typeof(T).Name, value.ToString() }));
        }

        private static InvalidEnumArgumentException _InvalidEnumArgumentException(string error)
        {
            InvalidEnumArgumentException e = new InvalidEnumArgumentException(error);
            TraceExceptionAsReturnValue(e);
            return e;
        }

        private static InvalidOperationException _InvalidOperation(string error)
        {
            InvalidOperationException e = new InvalidOperationException(error);
            TraceExceptionAsReturnValue(e);
            return e;
        }

        private static MissingPrimaryKeyException _MissingPrimaryKey(string error)
        {
            MissingPrimaryKeyException e = new MissingPrimaryKeyException(error);
            TraceExceptionAsReturnValue(e);
            return e;
        }

        private static NoNullAllowedException _NoNullAllowed(string error)
        {
            NoNullAllowedException e = new NoNullAllowedException(error);
            TraceExceptionAsReturnValue(e);
            return e;
        }

        private static ReadOnlyException _ReadOnly(string error)
        {
            ReadOnlyException e = new ReadOnlyException(error);
            TraceExceptionAsReturnValue(e);
            return e;
        }

        private static RowNotInTableException _RowNotInTable(string error)
        {
            RowNotInTableException e = new RowNotInTableException(error);
            TraceExceptionAsReturnValue(e);
            return e;
        }

        private static VersionNotFoundException _VersionNotFound(string error)
        {
            VersionNotFoundException e = new VersionNotFoundException(error);
            TraceExceptionAsReturnValue(e);
            return e;
        }

        public static Exception AddExternalObject()
        {
            return _Argument(DataResx.GetString("DataView_AddExternalObject"));
        }

        public static Exception AddNewNotAllowNull()
        {
            return _Data(DataResx.GetString("DataView_AddNewNotAllowNull"));
        }

        public static Exception AddPrimaryKeyConstraint()
        {
            return _Argument(DataResx.GetString("DataConstraint_AddPrimaryKeyConstraint"));
        }

        //public static Exception AggregateException(AggregateType aggregateType, Type type)
        //{
        //    return _Data(DataResx.GetString("DataStorage_AggregateException", new object[] { aggregateType.ToString(), type.Name }));
        //}

        public static Exception ArgumentContainsNull(string paramName)
        {
            return _Argument(paramName, DataResx.GetString("Data_ArgumentContainsNull", new object[] { paramName }));
        }

        public static Exception ArgumentContainsNullValue()
        {
            return _Argument(DataResx.GetString("DataTableReader_ArgumentContainsNullValue"));
        }

        public static Exception ArgumentNull(string paramName)
        {
            return _ArgumentNull(paramName, DataResx.GetString("Data_ArgumentNull", new object[] { paramName }));
        }

        public static Exception ArgumentOutOfRange(string paramName)
        {
            return _ArgumentOutOfRange(paramName, DataResx.GetString("Data_ArgumentOutOfRange", new object[] { paramName }));
        }

        public static Exception AutoIncrementAndDefaultValue()
        {
            return _Argument(DataResx.GetString("DataColumn_AutoIncrementAndDefaultValue"));
        }

        public static Exception AutoIncrementAndExpression()
        {
            return _Argument(DataResx.GetString("DataColumn_AutoIncrementAndExpression"));
        }

        public static Exception AutoIncrementCannotSetIfHasData(string typeName)
        {
            return _Argument(DataResx.GetString("DataColumn_AutoIncrementCannotSetIfHasData", new object[] { typeName }));
        }

        public static Exception AutoIncrementSeed()
        {
            return _Argument(DataResx.GetString("DataColumn_AutoIncrementSeed"));
        }

        public static Exception BadObjectPropertyAccess(string error)
        {
            return _InvalidOperation(DataResx.GetString("DataConstraint_BadObjectPropertyAccess", new object[] { error }));
        }

        public static Exception BeginEditInRowChanging()
        {
            return _InRowChangingEvent(DataResx.GetString("DataRow_BeginEditInRowChanging"));
        }

        public static Exception CancelEditInRowChanging()
        {
            return _InRowChangingEvent(DataResx.GetString("DataRow_CancelEditInRowChanging"));
        }

        public static Exception CannotAddColumn1(string column)
        {
            return _Argument(DataResx.GetString("DataColumns_Add1", new object[] { column }));
        }

        public static Exception CannotAddColumn2(string column)
        {
            return _Argument(DataResx.GetString("DataColumns_Add2", new object[] { column }));
        }

        public static Exception CannotAddColumn3()
        {
            return _Argument(DataResx.GetString("DataColumns_Add3"));
        }

        public static Exception CannotAddColumn4(string column)
        {
            return _Argument(DataResx.GetString("DataColumns_Add4", new object[] { column }));
        }

        public static Exception CannotAddDuplicate(string column)
        {
            return _DuplicateName(DataResx.GetString("DataColumns_AddDuplicate", new object[] { column }));
        }

        public static Exception CannotAddDuplicate2(string table)
        {
            return _DuplicateName(DataResx.GetString("DataColumns_AddDuplicate2", new object[] { table }));
        }

        public static Exception CannotAddDuplicate3(string table)
        {
            return _DuplicateName(DataResx.GetString("DataColumns_AddDuplicate3", new object[] { table }));
        }

        public static Exception CanNotBindTable()
        {
            return _Data(DataResx.GetString("DataView_CanNotBindTable"));
        }

        public static Exception CannotChangeCaseLocale()
        {
            return CannotChangeCaseLocale(null);
        }

        public static Exception CannotChangeCaseLocale(Exception innerException)
        {
            return _Argument(DataResx.GetString("DataSet_CannotChangeCaseLocale"), innerException);
        }

        public static Exception CannotChangeNamespace(string columnName)
        {
            return _Argument(DataResx.GetString("DataColumn_CannotChangeNamespace", new object[] { columnName }));
        }

        public static Exception CannotChangeSchemaSerializationMode()
        {
            return _InvalidOperation(DataResx.GetString("DataSet_CannotChangeSchemaSerializationMode"));
        }

        public static Exception CanNotClear()
        {
            return _Argument(DataResx.GetString("DataView_CanNotClear"));
        }

        public static Exception CannotCreateDataReaderOnEmptyDataSet()
        {
            return _Argument(DataResx.GetString("DataTableReader_CannotCreateDataReaderOnEmptyDataSet"));
        }

        public static Exception CanNotDelete()
        {
            return _Data(DataResx.GetString("DataView_CanNotDelete"));
        }

        public static Exception CanNotEdit()
        {
            return _Data(DataResx.GetString("DataView_CanNotEdit"));
        }

        public static Exception CannotModifyCollection()
        {
            return _Argument(DataResx.GetString("Data_CannotModifyCollection"));
        }

        public static Exception CanNotRemoteDataTable()
        {
            return _InvalidOperation(DataResx.GetString("DataTable_CanNotRemoteDataTable"));
        }

        public static Exception CannotRemoveChildKey(string relation)
        {
            return _Argument(DataResx.GetString("DataColumns_RemoveChildKey", new object[] { relation }));
        }

        public static Exception CannotRemoveColumn()
        {
            return _Argument(DataResx.GetString("DataColumns_Remove"));
        }

        public static Exception CannotRemoveConstraint(string constraint, string table)
        {
            return _Argument(DataResx.GetString("DataColumns_RemoveConstraint", new object[] { constraint, table }));
        }

        public static Exception CannotRemoveExpression(string column, string expression)
        {
            return _Argument(DataResx.GetString("DataColumns_RemoveExpression", new object[] { column, expression }));
        }

        public static Exception CannotRemovePrimaryKey()
        {
            return _Argument(DataResx.GetString("DataColumns_RemovePrimaryKey"));
        }

        public static Exception CanNotSerializeDataTableHierarchy()
        {
            return _InvalidOperation(DataResx.GetString("DataTable_CanNotSerializeDataTableHierarchy"));
        }

        public static Exception CanNotSerializeDataTableWithEmptyName()
        {
            return _InvalidOperation(DataResx.GetString("DataTable_CanNotSerializeDataTableWithEmptyName"));
        }

        public static Exception CanNotSetDataSet()
        {
            return _Data(DataResx.GetString("DataView_CanNotSetDataSet"));
        }

        public static Exception CannotSetDateTimeModeForNonDateTimeColumns()
        {
            return _InvalidOperation(DataResx.GetString("DataColumn_CannotSetDateTimeModeForNonDateTimeColumns"));
        }

        public static Exception CannotSetMaxLength(DataColumn column, int value)
        {
            return _Argument(DataResx.GetString("DataColumn_CannotSetMaxLength", new object[] { column.ColumnName, value.ToString(CultureInfo.InvariantCulture) }));
        }

        public static Exception CannotSetMaxLength2(DataColumn column)
        {
            return _Argument(DataResx.GetString("DataColumn_CannotSetMaxLength2", new object[] { column.ColumnName }));
        }

        public static Exception CanNotSetRemotingFormat()
        {
            return _Argument(DataResx.GetString("DataTable_CanNotSetRemotingFormat"));
        }

        public static Exception CannotSetSimpleContent(string columnName, Type type)
        {
            return _Argument(DataResx.GetString("DataColumn_CannotSimpleContent", new object[] { columnName, type }));
        }

        public static Exception CannotSetSimpleContentType(string columnName, Type type)
        {
            return _Argument(DataResx.GetString("DataColumn_CannotSimpleContentType", new object[] { columnName, type }));
        }

        public static Exception CanNotSetTable()
        {
            return _Data(DataResx.GetString("DataView_CanNotSetTable"));
        }

        public static Exception CannotSetToNull(DataColumn column)
        {
            return _Argument(DataResx.GetString("DataColumn_CannotSetToNull", new object[] { column.ColumnName }));
        }

        public static Exception CanNotUse()
        {
            return _Data(DataResx.GetString("DataView_CanNotUse"));
        }

        public static Exception CanNotUseDataViewManager()
        {
            return _Data(DataResx.GetString("DataView_CanNotUseDataViewManager"));
        }

        public static Exception CantAddConstraintToMultipleNestedTable(string tableName)
        {
            return _Argument(DataResx.GetString("DataConstraint_CantAddConstraintToMultipleNestedTable", new object[] { tableName }));
        }

        public static Exception CantChangeDataType()
        {
            return _Argument(DataResx.GetString("DataColumn_ChangeDataType"));
        }

        public static Exception CantChangeDateTimeMode(DataSetDateTime oldValue, DataSetDateTime newValue)
        {
            return _InvalidOperation(DataResx.GetString("DataColumn_DateTimeMode", new object[] { oldValue.ToString(), newValue.ToString() }));
        }

        public static Exception CaseInsensitiveNameConflict(string name)
        {
            return _Argument(DataResx.GetString("Data_CaseInsensitiveNameConflict", new object[] { name }));
        }

        public static Exception CaseLocaleMismatch()
        {
            return _Argument(DataResx.GetString("DataRelation_CaseLocaleMismatch"));
        }

        public static Exception ChildTableMismatch()
        {
            return _Argument(DataResx.GetString("DataRelation_ChildTableMismatch"));
        }
        public static Exception ColumnNameRequired()
        {
            return _Argument(DataResx.GetString("DataColumn_NameRequired"));
        }

        public static Exception ColumnNotInAnyTable()
        {
            return _Argument(DataResx.GetString("DataColumn_NotInAnyTable"));
        }

        public static Exception ColumnNotInTheTable(string column, string table)
        {
            return _Argument(DataResx.GetString("DataColumn_NotInTheTable", new object[] { column, table }));
        }

        public static Exception ColumnNotInTheUnderlyingTable(string column, string table)
        {
            return _Argument(DataResx.GetString("DataColumn_NotInTheUnderlyingTable", new object[] { column, table }));
        }

        public static Exception ColumnOutOfRange(int index)
        {
            return _IndexOutOfRange(DataResx.GetString("DataColumns_OutOfRange", new object[] { index.ToString(CultureInfo.InvariantCulture) }));
        }

        public static Exception ColumnOutOfRange(string column)
        {
            return _IndexOutOfRange(DataResx.GetString("DataColumns_OutOfRange", new object[] { column }));
        }

        public static Exception ColumnsTypeMismatch()
        {
            return _InvalidConstraint(DataResx.GetString("DataRelation_ColumnsTypeMismatch"));
        }

        public static Exception ColumnToSortIsOutOfRange(string column)
        {
            return _Argument(DataResx.GetString("DataColumns_OutOfRange", new object[] { column }));
        }

        public static Exception ColumnTypeNotSupported()
        {
            return /*ADP.NotSupported(*/_Data(DataResx.GetString("DataColumn_NullableTypesNotSupported"));
        }

        public static Exception ConstraintAddFailed(DataTable table)
        {
            return _InvalidConstraint(DataResx.GetString("DataConstraint_AddFailed", new object[] { table.TableName }));
        }

        public static Exception ConstraintForeignTable()
        {
            return _Argument(DataResx.GetString("DataConstraint_ForeignTable"));
        }

        public static Exception ConstraintNotInTheTable(string constraint)
        {
            return _Argument(DataResx.GetString("DataConstraint_NotInTheTable", new object[] { constraint }));
        }

        public static Exception ConstraintOutOfRange(int index)
        {
            return _IndexOutOfRange(DataResx.GetString("DataConstraint_OutOfRange", new object[] { index.ToString(CultureInfo.InvariantCulture) }));
        }

        public static Exception ConstraintParentValues()
        {
            return _Argument(DataResx.GetString("DataConstraint_ParentValues"));
        }

        public static Exception ConstraintRemoveFailed()
        {
            return _Argument(DataResx.GetString("DataConstraint_RemoveFailed"));
        }

        public static Exception ConstraintViolation(string constraint)
        {
            return _Constraint(DataResx.GetString("DataConstraint_Violation", new object[] { constraint }));
        }

        public static Exception ConstraintViolation(DataColumn[] columns, object[] values)
        {
            return _Constraint(UniqueConstraintViolationText(columns, values));
        }

        public static DataException ConvertFailed(Type type1, Type type2)
        {
            return ConvertFailed(type1, type2, null);
        }

        public static DataException ConvertFailed(Type type1, Type type2, Exception innerExeption)
        {
            return _Data(DataResx.GetString("SqlConvert_ConvertFailed", new object[] { type1.FullName, type2.FullName }), innerExeption);
        }

        public static Exception CreateChildView()
        {
            return _Argument(DataResx.GetString("DataView_CreateChildView"));
        }

        public static Exception DatasetConflictingName(string table)
        {
            return _DuplicateName(DataResx.GetString("DataTable_DatasetConflictingName", new object[] { table }));
        }

        public static Exception DataSetUnsupportedSchema(string ns)
        {
            return _Argument(DataResx.GetString("DataSet_UnsupportedSchema", new object[] { ns }));
        }

        public static Exception DataTableReaderArgumentIsEmpty()
        {
            return _Argument(DataResx.GetString("DataTableReader_DataTableReaderArgumentIsEmpty"));
        }

        public static Exception DataTableReaderSchemaIsInvalid(string tableName)
        {
            return _InvalidOperation(DataResx.GetString("DataTableReader_SchemaInvalidDataTableReader", new object[] { tableName }));
        }


        public static Exception DefaultValueAndAutoIncrement()
        {
            return _Argument(DataResx.GetString("DataColumn_DefaultValueAndAutoIncrement"));
        }

        public static Exception DefaultValueColumnDataType(string column, Type defaultType, Type columnType)
        {
            return _Argument(DataResx.GetString("DataColumn_DefaultValueColumnDataType", new object[] { column, defaultType.FullName, columnType.FullName }));
        }

        public static Exception DefaultValueDataType(string column, Type defaultType, Type columnType)
        {
            if (column.Length == 0)
            {
                return _Argument(DataResx.GetString("DataColumn_DefaultValueDataType1", new object[] { defaultType.FullName, columnType.FullName }));
            }
            return _Argument(DataResx.GetString("DataColumn_DefaultValueDataType", new object[] { column, defaultType.FullName, columnType.FullName }));
        }

        public static Exception DeletedRowInaccessible()
        {
            return _DeletedRowInaccessible(DataResx.GetString("DataRow_DeletedRowInaccessible"));
        }

        public static Exception DeleteInRowDeleting()
        {
            return _InRowChangingEvent(DataResx.GetString("DataRow_DeleteInRowDeleting"));
        }

        public static Exception DuplicateConstraint(string constraint)
        {
            return _Data(DataResx.GetString("DataConstraint_Duplicate", new object[] { constraint }));
        }

        public static Exception DuplicateConstraintName(string constraint)
        {
            return _DuplicateName(DataResx.GetString("DataConstraint_DuplicateName", new object[] { constraint }));
        }


        public static Exception DuplicateRelation(string relation)
        {
            return _DuplicateName(DataResx.GetString("DataRelation_DuplicateName", new object[] { relation }));
        }

        public static Exception DuplicateTableName(string table)
        {
            return _DuplicateName(DataResx.GetString("DataTable_DuplicateName", new object[] { table }));
        }

        public static Exception DuplicateTableName2(string table, string ns)
        {
            return _DuplicateName(DataResx.GetString("DataTable_DuplicateName2", new object[] { table, ns }));
        }

        public static Exception EditInRowChanging()
        {
            return _InRowChangingEvent(DataResx.GetString("DataRow_EditInRowChanging"));
        }


        public static Exception EmptyDataTableReader(string tableName)
        {
            return _DeletedRowInaccessible(DataResx.GetString("DataTableReader_DataTableCleared", new object[] { tableName }));
        }

        public static Exception EndEditInRowChanging()
        {
            return _InRowChangingEvent(DataResx.GetString("DataRow_EndEditInRowChanging"));
        }

        public static Exception EnforceConstraint()
        {
            return _Constraint(DataResx.GetString("Data_EnforceConstraints"));
        }

        public static Exception EnumeratorModified()
        {
            return _InvalidOperation(DataResx.GetString("RbTree_EnumerationBroken"));
        }

        public static Exception ExpressionAndConstraint(DataColumn column, Constraint constraint)
        {
            return _Argument(DataResx.GetString("DataColumn_ExpressionAndConstraint", new object[] { column.ColumnName, constraint.ConstraintName }));
        }

        public static Exception ExpressionAndReadOnly()
        {
            return _Argument(DataResx.GetString("DataColumn_ExpressionAndReadOnly"));
        }

        public static Exception ExpressionAndUnique()
        {
            return _Argument(DataResx.GetString("DataColumn_ExpressionAndUnique"));
        }

        public static Exception ExpressionCircular()
        {
            return _Argument(DataResx.GetString("DataColumn_ExpressionCircular"));
        }

        public static Exception ExpressionInConstraint(DataColumn column)
        {
            return _Argument(DataResx.GetString("DataColumn_ExpressionInConstraint", new object[] { column.ColumnName }));
        }

        public static Exception FailedCascadeDelete(string constraint)
        {
            return _InvalidConstraint(DataResx.GetString("DataConstraint_CascadeDelete", new object[] { constraint }));
        }

        public static Exception FailedCascadeUpdate(string constraint)
        {
            return _InvalidConstraint(DataResx.GetString("DataConstraint_CascadeUpdate", new object[] { constraint }));
        }

        public static Exception FailedClearParentTable(string table, string constraint, string childTable)
        {
            return _InvalidConstraint(DataResx.GetString("DataConstraint_ClearParentTable", new object[] { table, constraint, childTable }));
        }

        public static Exception ForeignKeyViolation(string constraint, object[] keys)
        {
            return _InvalidConstraint(DataResx.GetString("DataConstraint_ForeignKeyViolation", new object[] { constraint, KeysToString(keys) }));
        }

        public static Exception ForeignRelation()
        {
            return _Argument(DataResx.GetString("DataRelation_ForeignDataSet"));
        }

        public static Exception GetElementIndex(int index)
        {
            return _IndexOutOfRange(DataResx.GetString("DataView_GetElementIndex", new object[] { index.ToString(CultureInfo.InvariantCulture) }));
        }

        public static Exception GetParentRowTableMismatch(string t1, string t2)
        {
            return _InvalidConstraint(DataResx.GetString("DataRelation_GetParentRowTableMismatch", new object[] { t1, t2 }));
        }

        public static Exception HasToBeStringType(DataColumn column)
        {
            return _Argument(DataResx.GetString("DataColumn_HasToBeStringType", new object[] { column.ColumnName }));
        }

        public static Exception IComparableNotImplemented(string typeName)
        {
            return _Data(DataResx.GetString("DataStorage_IComparableNotDefined", new object[] { typeName }));
        }

        public static Exception IndexKeyLength(int length, int keyLength)
        {
            if (length == 0)
            {
                return _Argument(DataResx.GetString("DataIndex_FindWithoutSortOrder"));
            }
            return _Argument(DataResx.GetString("DataIndex_KeyLength", new object[] { length.ToString(CultureInfo.InvariantCulture), keyLength.ToString(CultureInfo.InvariantCulture) }));
        }

        public static Exception InsertExternalObject()
        {
            return _Argument(DataResx.GetString("DataView_InsertExternalObject"));
        }

        //public static Exception InternalRBTreeError(RBTreeError internalError)
        //{
        //    return _InvalidOperation(DataResx.GetString("RbTree_InvalidState", new object[] { (int)internalError }));
        //}

        public static Exception INullableUDTwithoutStaticNull(string typeName)
        {
            return _Argument(DataResx.GetString("DataColumn_INullableUDTwithoutStaticNull", new object[] { typeName }));
        }

        public static Exception InvalidCurrentRowInDataTableReader()
        {
            return _DeletedRowInaccessible(DataResx.GetString("DataTableReader_InvalidRowInDataTableReader"));
        }

        public static Exception InvalidDataColumnMapping(Type type)
        {
            return _Argument(DataResx.GetString("DataColumn_InvalidDataColumnMapping", new object[] { type.AssemblyQualifiedName }));
        }

        public static Exception InvalidDataTableReader(string tableName)
        {
            return _InvalidOperation(DataResx.GetString("DataTableReader_InvalidDataTableReader", new object[] { tableName }));
        }

        public static Exception InvalidDateTimeMode(DataSetDateTime mode)
        {
            return _InvalidEnumArgumentException<DataSetDateTime>(mode);
        }

        public static Exception InvalidDuplicateNamedSimpleTypeDelaration(string stName, string errorStr)
        {
            return _Argument(DataResx.GetString("NamedSimpleType_InvalidDuplicateNamedSimpleTypeDelaration", new object[] { stName, errorStr }));
        }

        public static Exception InValidNestedRelation(string childTableName)
        {
            return _InvalidOperation(DataResx.GetString("DataRelation_InValidNestedRelation", new object[] { childTableName }));
        }

        public static Exception InvalidOffsetLength()
        {
            return _Argument(DataResx.GetString("Data_InvalidOffsetLength"));
        }

        public static Exception InvalidOrdinal(string name, int ordinal)
        {
            return _ArgumentOutOfRange(name, DataResx.GetString("DataColumn_OrdinalExceedMaximun", new object[] { ordinal.ToString(CultureInfo.InvariantCulture) }));
        }

        public static Exception InvalidParentNamespaceinNestedRelation(string childTableName)
        {
            return _InvalidOperation(DataResx.GetString("DataRelation_InValidNamespaceInNestedRelation", new object[] { childTableName }));
        }

        public static Exception InvalidRemotingFormat(SerializationFormat mode)
        {
            return _InvalidEnumArgumentException<SerializationFormat>(mode);
        }

        public static Exception InvalidRowBitPattern()
        {
            return _Argument(DataResx.GetString("DataRow_InvalidRowBitPattern"));
        }

        public static Exception InvalidRowState(DataRowState state)
        {
            return _InvalidEnumArgumentException<DataRowState>(state);
        }

        public static Exception InvalidRowVersion()
        {
            return _Data(DataResx.GetString("DataRow_InvalidVersion"));
        }

        public static Exception InvalidSchemaSerializationMode(Type enumType, string mode)
        {
            return _InvalidEnumArgumentException(DataResx.GetString("ADP_InvalidEnumerationValue", new object[] { enumType.Name, mode }));
        }

        public static Exception InvalidSortString(string sort)
        {
            return _Argument(DataResx.GetString("DataTable_InvalidSortString", new object[] { sort }));
        }

        public static Exception InvalidStorageType(TypeCode typecode)
        {
            return _Data(DataResx.GetString("DataStorage_InvalidStorageType", new object[] { typecode.ToString() }));
        }

        public static Exception KeyColumnsIdentical()
        {
            return _InvalidConstraint(DataResx.GetString("DataRelation_KeyColumnsIdentical"));
        }

        public static Exception KeyDuplicateColumns(string columnName)
        {
            return _InvalidConstraint(DataResx.GetString("DataKey_DuplicateColumns", new object[] { columnName }));
        }

        public static Exception KeyLengthMismatch()
        {
            return _Argument(DataResx.GetString("DataRelation_KeyLengthMismatch"));
        }

        public static Exception KeyLengthZero()
        {
            return _Argument(DataResx.GetString("DataRelation_KeyZeroLength"));
        }

        public static Exception KeyNoColumns()
        {
            return _InvalidConstraint(DataResx.GetString("DataKey_NoColumns"));
        }

        public static string KeysToString(object[] keys)
        {
            string text = string.Empty;
            for (int i = 0; i < keys.Length; i++)
            {
                text = text + Convert.ToString(keys[i], null) + ((i < (keys.Length - 1)) ? ", " : string.Empty);
            }
            return text;
        }

        public static Exception KeyTableMismatch()
        {
            return _InvalidConstraint(DataResx.GetString("DataKey_TableMismatch"));
        }

        public static Exception KeyTooManyColumns(int cols)
        {
            return _InvalidConstraint(DataResx.GetString("DataKey_TooManyColumns", new object[] { cols.ToString(CultureInfo.InvariantCulture) }));
        }

        public static Exception LongerThanMaxLength(DataColumn column)
        {
            return _Argument(DataResx.GetString("DataColumn_LongerThanMaxLength", new object[] { column.ColumnName }));
        }

        public static Exception LoopInNestedRelations(string tableName)
        {
            return _Argument(DataResx.GetString("DataRelation_LoopInNestedRelations", new object[] { tableName }));
        }

        public static string MaxLengthViolationText(string columnName)
        {
            return DataResx.GetString("DataColumn_ExceedMaxLength", new object[] { columnName });
        }

        public static Exception MergeFailed(string name)
        {
            return _Data(name);
        }

        public static Exception MergeMissingDefinition(string obj)
        {
            return _Argument(DataResx.GetString("DataMerge_MissingDefinition", new object[] { obj }));
        }


        public static Exception MultipleParents()
        {
            return _Data(DataResx.GetString("DataRow_MultipleParents"));
        }

        public static Exception MultipleTextOnlyColumns()
        {
            return _Argument(DataResx.GetString("DataTable_MultipleSimpleContentColumns"));
        }

        public static Exception NamespaceNameConflict(string name)
        {
            return _Argument(DataResx.GetString("Data_NamespaceNameConflict", new object[] { name }));
        }

        public static Exception NeededForForeignKeyConstraint(UniqueConstraint key, ForeignKeyConstraint fk)
        {
            return _Argument(DataResx.GetString("DataConstraint_NeededForForeignKeyConstraint", new object[] { key.ConstraintName, fk.ConstraintName }));
        }

        public static Exception NegativeMinimumCapacity()
        {
            return _Argument(DataResx.GetString("RecordManager_MinimumCapacity"));
        }

        public static Exception NoConstraintName()
        {
            return _Argument(DataResx.GetString("DataConstraint_NoName"));
        }

        public static Exception NoCurrentData()
        {
            return _VersionNotFound(DataResx.GetString("DataRow_NoCurrentData"));
        }

        public static Exception NonUniqueValues(string column)
        {
            return _InvalidConstraint(DataResx.GetString("DataColumn_NonUniqueValues", new object[] { column }));
        }

        public static Exception NoOriginalData()
        {
            return _VersionNotFound(DataResx.GetString("DataRow_NoOriginalData"));
        }

        public static Exception NoProposedData()
        {
            return _VersionNotFound(DataResx.GetString("DataRow_NoProposedData"));
        }

        public static Exception NoRelationName()
        {
            return _Argument(DataResx.GetString("DataRelation_NoName"));
        }

        public static Exception NoTableName()
        {
            return _Argument(DataResx.GetString("DataTable_NoName"));
        }

        public static string NotAllowDBNullViolationText(string columnName)
        {
            return DataResx.GetString("DataColumn_NotAllowDBNull", new object[] { columnName });
        }

        public static Exception NotOpen()
        {
            return _Data(DataResx.GetString("DataView_NotOpen"));
        }

        public static Exception NullDataType()
        {
            return _Argument(DataResx.GetString("DataColumn_NullDataType"));
        }

        public static Exception NullKeyValues(string column)
        {
            return _Data(DataResx.GetString("DataColumn_NullKeyValues", new object[] { column }));
        }

        public static Exception NullRange()
        {
            return _Data(DataResx.GetString("Range_NullRange"));
        }

        public static Exception NullValues(string column)
        {
            return _NoNullAllowed(DataResx.GetString("DataColumn_NullValues", new object[] { column }));
        }

        public static Exception ParentOrChildColumnsDoNotHaveDataSet()
        {
            return _InvalidConstraint(DataResx.GetString("DataRelation_ParentOrChildColumnsDoNotHaveDataSet"));
        }

        public static Exception ParentRowNotInTheDataSet()
        {
            return _Argument(DataResx.GetString("DataRow_ParentRowNotInTheDataSet"));
        }

        public static Exception ParentTableMismatch()
        {
            return _Argument(DataResx.GetString("DataRelation_ParentTableMismatch"));
        }

        public static Exception ProblematicChars(char charValue)
        {
            string text = "0x" + ((ushort)charValue).ToString("X", CultureInfo.InvariantCulture);
            return _Argument(DataResx.GetString("DataStorage_ProblematicChars", new object[] { text }));
        }

        public static Exception PropertyNotFound(string property, string table)
        {
            return _Argument(DataResx.GetString("DataROWView_PropertyNotFound", new object[] { property, table }));
        }

        public static Exception RangeArgument(int min, int max)
        {
            return _Argument(DataResx.GetString("Range_Argument", new object[] { min.ToString(CultureInfo.InvariantCulture), max.ToString(CultureInfo.InvariantCulture) }));
        }

        public static Exception ReadOnly(string column)
        {
            return _ReadOnly(DataResx.GetString("DataColumn_ReadOnly", new object[] { column }));
        }

        public static Exception ReadOnlyAndExpression()
        {
            return _ReadOnly(DataResx.GetString("DataColumn_ReadOnlyAndExpression"));
        }

        public static Exception RecordStateRange()
        {
            return _Argument(DataResx.GetString("DataIndex_RecordStateRange"));
        }

        public static Exception RelationAlreadyExists()
        {
            return _Argument(DataResx.GetString("DataRelation_AlreadyExists"));
        }

        public static Exception RelationAlreadyInOtherDataSet()
        {
            return _Argument(DataResx.GetString("DataRelation_AlreadyInOtherDataSet"));
        }

        public static Exception RelationAlreadyInTheDataSet()
        {
            return _Argument(DataResx.GetString("DataRelation_AlreadyInTheDataSet"));
        }
        public static Exception RelationDataSetMismatch()
        {
            return _InvalidConstraint(DataResx.GetString("DataRelation_DataSetMismatch"));
        }

        public static Exception RelationDataSetNull()
        {
            return _Argument(DataResx.GetString("DataRelation_TableNull"));
        }

        public static Exception RelationDoesNotExist()
        {
            return _Argument(DataResx.GetString("DataRelation_DoesNotExist"));
        }

        public static Exception RelationForeignRow()
        {
            return _Argument(DataResx.GetString("DataRelation_ForeignRow"));
        }

        public static Exception RelationForeignTable(string t1, string t2)
        {
            return _InvalidConstraint(DataResx.GetString("DataRelation_ForeignTable", new object[] { t1, t2 }));
        }

        public static Exception RelationNestedReadOnly()
        {
            return _Argument(DataResx.GetString("DataRelation_RelationNestedReadOnly"));
        }

        public static Exception RelationNotInTheDataSet(string relation)
        {
            return _Argument(DataResx.GetString("DataRelation_NotInTheDataSet", new object[] { relation }));
        }

        public static Exception RelationOutOfRange(object index)
        {
            return _IndexOutOfRange(DataResx.GetString("DataRelation_OutOfRange", new object[] { Convert.ToString(index, null) }));
        }


        public static Exception RelationTableNull()
        {
            return _Argument(DataResx.GetString("DataRelation_TableNull"));
        }

        public static Exception RelationTableWasRemoved()
        {
            return _Argument(DataResx.GetString("DataRelation_TableWasRemoved"));
        }

        public static Exception RemoveExternalObject()
        {
            return _Argument(DataResx.GetString("DataView_RemoveExternalObject"));
        }

        public static Exception RemoveParentRow(ForeignKeyConstraint constraint)
        {
            return _InvalidConstraint(DataResx.GetString("DataConstraint_RemoveParentRow", new object[] { constraint.ConstraintName }));
        }

        public static Exception RemovePrimaryKey(DataTable table)
        {
            if (table.TableName.Length == 0)
            {
                return _Argument(DataResx.GetString("DataKey_RemovePrimaryKey"));
            }
            return _Argument(DataResx.GetString("DataKey_RemovePrimaryKey1", new object[] { table.TableName }));
        }

        public static Exception RowAlreadyDeleted()
        {
            return _DeletedRowInaccessible(DataResx.GetString("DataRow_AlreadyDeleted"));
        }

        public static Exception RowAlreadyInOtherCollection()
        {
            return _Argument(DataResx.GetString("DataRow_AlreadyInOtherCollection"));
        }

        public static Exception RowAlreadyInTheCollection()
        {
            return _Argument(DataResx.GetString("DataRow_AlreadyInTheCollection"));
        }

        public static Exception RowAlreadyRemoved()
        {
            return _Data(DataResx.GetString("DataRow_AlreadyRemoved"));
        }

        public static Exception RowEmpty()
        {
            return _Argument(DataResx.GetString("DataRow_Empty"));
        }

        public static Exception RowInsertMissing(string tableName)
        {
            return _IndexOutOfRange(DataResx.GetString("DataRow_RowInsertMissing", new object[] { tableName }));
        }

        public static Exception RowInsertOutOfRange(int index)
        {
            return _IndexOutOfRange(DataResx.GetString("DataRow_RowInsertOutOfRange", new object[] { index.ToString(CultureInfo.InvariantCulture) }));
        }

        public static Exception RowInsertTwice(int index, string tableName)
        {
            return _IndexOutOfRange(DataResx.GetString("DataRow_RowInsertTwice", new object[] { index.ToString(CultureInfo.InvariantCulture), tableName }));
        }

        public static Exception RowNotInTheDataSet()
        {
            return _Argument(DataResx.GetString("DataRow_NotInTheDataSet"));
        }

        public static Exception RowNotInTheTable()
        {
            return _RowNotInTable(DataResx.GetString("DataRow_NotInTheTable"));
        }

        public static Exception RowOutOfRange()
        {
            return _IndexOutOfRange(DataResx.GetString("DataRow_RowOutOfRange"));
        }

        public static Exception RowOutOfRange(int index)
        {
            return _IndexOutOfRange(DataResx.GetString("DataRow_OutOfRange", new object[] { index.ToString(CultureInfo.InvariantCulture) }));
        }

        public static Exception RowRemovedFromTheTable()
        {
            return _RowNotInTable(DataResx.GetString("DataRow_RemovedFromTheTable"));
        }

        public static Exception SelfnestedDatasetConflictingName(string table)
        {
            return _DuplicateName(DataResx.GetString("DataTable_SelfnestedDatasetConflictingName", new object[] { table }));
        }

        public static Exception SetAddedAndModifiedCalledOnnonUnchanged()
        {
            return _InvalidOperation(DataResx.GetString("DataColumn_SetAddedAndModifiedCalledOnNonUnchanged"));
        }

        public static Exception SetDataSetFailed()
        {
            return _Data(DataResx.GetString("DataView_SetDataSetFailed"));
        }

        public static Exception SetDataSetNameConflicting(string name)
        {
            return _Argument(DataResx.GetString("DataSet_SetDataSetNameConflicting", new object[] { name }));
        }

        public static Exception SetDataSetNameToEmpty()
        {
            return _Argument(DataResx.GetString("DataSet_SetNameToEmpty"));
        }

        public static Exception SetFailed(string name)
        {
            return _Data(DataResx.GetString("DataView_SetFailed", new object[] { name }));
        }

        public static Exception SetFailed(object value, DataColumn column, Type type, Exception innerException)
        {
            return _Argument(innerException.Message + DataResx.GetString("DataColumn_SetFailed", new object[] { value.ToString(), column.ColumnName, type.Name }), innerException);
        }

        public static Exception SetIListObject()
        {
            return _Argument(DataResx.GetString("DataView_SetIListObject"));
        }

        public static Exception SetParentRowTableMismatch(string t1, string t2)
        {
            return _InvalidConstraint(DataResx.GetString("DataRelation_SetParentRowTableMismatch", new object[] { t1, t2 }));
        }

        public static Exception SetRowStateFilter()
        {
            return _Data(DataResx.GetString("DataView_SetRowStateFilter"));
        }

        public static Exception SetTable()
        {
            return _Data(DataResx.GetString("DataView_SetTable"));
        }


        public static Exception StorageSetFailed()
        {
            return _Argument(DataResx.GetString("DataStorage_SetInvalidDataType"));
        }

        public static Exception TableAlreadyInOtherDataSet()
        {
            return _Argument(DataResx.GetString("DataTable_AlreadyInOtherDataSet"));
        }

        public static Exception TableAlreadyInTheDataSet()
        {
            return _Argument(DataResx.GetString("DataTable_AlreadyInTheDataSet"));
        }

        public static Exception TableCannotAddToSimpleContent()
        {
            return _Argument(DataResx.GetString("DataTable_CannotAddToSimpleContent"));
        }

        public static Exception TableCantBeNestedInTwoTables(string tableName)
        {
            return _Argument(DataResx.GetString("DataRelation_TableCantBeNestedInTwoTables", new object[] { tableName }));
        }

        public static Exception TableForeignPrimaryKey()
        {
            return _Argument(DataResx.GetString("DataTable_ForeignPrimaryKey"));
        }

        public static Exception TableInConstraint(DataTable table, Constraint constraint)
        {
            return _Argument(DataResx.GetString("DataTable_InConstraint", new object[] { table.TableName, constraint.ConstraintName }));
        }

        public static Exception TableInRelation()
        {
            return _Argument(DataResx.GetString("DataTable_InRelation"));
        }

        public static Exception TableMissingPrimaryKey()
        {
            return _MissingPrimaryKey(DataResx.GetString("DataTable_MissingPrimaryKey"));
        }

        public static Exception TableNotFound(string tableName)
        {
            return _Argument(DataResx.GetString("DataTable_TableNotFound", new object[] { tableName }));
        }

        public static Exception TableNotInTheDataSet(string table)
        {
            return _Argument(DataResx.GetString("DataTable_NotInTheDataSet", new object[] { table }));
        }

        public static Exception TableOutOfRange(int index)
        {
            return _IndexOutOfRange(DataResx.GetString("DataTable_OutOfRange", new object[] { index.ToString(CultureInfo.InvariantCulture) }));
        }

        public static Exception TablesInDifferentSets()
        {
            return _Argument(DataResx.GetString("DataRelation_TablesInDifferentSets"));
        }


        private static void TraceException(string trace, Exception e)
        {
            //if (e != null)
            //{
            //    Bid.Trace(trace, e.Message);
            //    if (Bid.AdvancedOn)
            //    {
            //        try
            //        {
            //            Bid.Trace(", StackTrace='%ls'", Environment.StackTrace);
            //        }
            //        catch (SecurityException)
            //        {
            //        }
            //    }
            //    Bid.Trace("\n");
            //}
        }

        public static void TraceExceptionAsReturnValue(Exception e)
        {
            TraceException("<comm.ADP.TraceException|ERR|THROW> Message='%ls'", e);
        }

        public static void TraceExceptionForCapture(Exception e)
        {
            TraceException("<comm.ADP.TraceException|ERR|CATCH> Message='%ls'", e);
        }

        public static void TraceExceptionWithoutRethrow(Exception e)
        {
            TraceException("<comm.ADP.TraceException|ERR|CATCH> Message='%ls'", e);
        }

        public static Exception UDTImplementsIChangeTrackingButnotIRevertible(string typeName)
        {
            return _InvalidOperation(DataResx.GetString("DataColumn_UDTImplementsIChangeTrackingButnotIRevertible", new object[] { typeName }));
        }

        public static Exception UndefinedDatatype(string name)
        {
            return _Data(DataResx.GetString("Xml_UndefinedDatatype", new object[] { name }));
        }

        public static Exception UniqueAndExpression()
        {
            return _Argument(DataResx.GetString("DataColumn_UniqueAndExpression"));
        }

        public static Exception UniqueConstraintViolation()
        {
            return _Argument(DataResx.GetString("DataConstraint_UniqueViolation"));
        }

        public static string UniqueConstraintViolationText(DataColumn[] columns, object[] values)
        {
            if (columns.Length > 1)
            {
                string text = string.Empty;
                for (int i = 0; i < columns.Length; i++)
                {
                    text = text + columns[i].ColumnName + ((i < (columns.Length - 1)) ? ", " : "");
                }
                return DataResx.GetString("DataConstraint_ViolationValue", new object[] { text, KeysToString(values) });
            }
            return DataResx.GetString("DataConstraint_ViolationValue", new object[] { columns[0].ColumnName, Convert.ToString(values[0], null) });
        }

        public static Exception ValueArrayLength()
        {
            return _Argument(DataResx.GetString("DataRow_ValuesArrayLength"));
        }


        #region xml exception

        //public static class XmlExceptionBuilder
        //{
        public static Exception CircularComplexType(string name)
        {
            return _Data(DataResx.GetString("Xml_CircularComplexType", new object[] { name }));
        }
        public static Exception AttributeValues(string name, string value1, string value2)
        {
            return _Data(DataResx.GetString("Xml_AttributeValues", new object[] { name, value1, value2 }));
        }

        public static Exception CannotConvert(string name, string type)
        {
            return _Data(DataResx.GetString("Xml_CannotConvert", new object[] { name, type }));
        }

        public static Exception CanNotDeserializeObjectType()
        {
            return _InvalidOperation(DataResx.GetString("Xml_CanNotDeserializeObjectType"));
        }

        public static Exception CannotInstantiateAbstract(string name)
        {
            return _Data(DataResx.GetString("Xml_CannotInstantiateAbstract", new object[] { name }));
        }

        public static Exception ColumnTypeConflict(string name)
        {
            return _Data(DataResx.GetString("Xml_ColumnConflict", new object[] { name }));
        }

        public static Exception DataTableInferenceNotSupported()
        {
            return _InvalidOperation(DataResx.GetString("Xml_DataTableInferenceNotSupported"));
        }

        public static Exception DatatypeNotDefined()
        {
            return _Data(DataResx.GetString("Xml_DatatypeNotDefined"));
        }
        public static Exception DiffgramMissingSQL()
        {
            return _Data(DataResx.GetString("Xml_MissingSQL"));
        }

        public static Exception DiffgramMissingTable(string name)
        {
            return _Data(DataResx.GetString("Xml_MissingTable", new object[] { name }));
        }

        public static Exception DuplicateConstraintRead(string str)
        {
            return _Data(DataResx.GetString("Xml_DuplicateConstraint", new object[] { str }));
        }

        public static Exception DuplicateDeclaration(string name)
        {
            return _Data(DataResx.GetString("Xml_MergeDuplicateDeclaration", new object[] { name }));
        }

        public static Exception ElementTypeNotFound(string name)
        {
            return _Data(DataResx.GetString("Xml_ElementTypeNotFound", new object[] { name }));
        }
        public static Exception InvalidField(string name)
        {
            return _Data(DataResx.GetString("Xml_InvalidField", new object[] { name }));
        }
        public static Exception InvalidAttributeValue(string name, string value)
        {
            return _Data(DataResx.GetString("Xml_ValueOutOfRange", new object[] { name, value }));
        }
        public static Exception FoundEntity()
        {
            return _Data(DataResx.GetString("Xml_FoundEntity"));
        }

        public static Exception InvalidKey(string name)
        {
            return _Data(DataResx.GetString("Xml_InvalidKey", new object[] { name }));
        }

        public static Exception InvalidPrefix(string name)
        {
            return _Data(DataResx.GetString("Xml_InvalidPrefix", new object[] { name }));
        }
        public static Exception MismatchKeyLength()
        {
            return _Data(DataResx.GetString("Xml_MismatchKeyLength"));
        }
        public static Exception InvalidSelector(string name)
        {
            return _Data(DataResx.GetString("Xml_InvalidSelector", new object[] { name }));
        }

        public static Exception IsDataSetAttributeMissingInSchema()
        {
            return _Data(DataResx.GetString("Xml_IsDataSetAttributeMissingInSchema"));
        }

        public static Exception MissingAttribute(string attribute)
        {
            return MissingAttribute(string.Empty, attribute);
        }

        public static Exception MissingAttribute(string element, string attribute)
        {
            return _Data(DataResx.GetString("Xml_MissingAttribute", new object[] { element, attribute }));
        }

        public static Exception MissingRefer(string name)
        {
            return _Data(DataResx.GetString("Xml_MissingRefer", new object[] { "refer", "keyref", name }));
        }

        public static Exception MultipleParentRows(string tableQName)
        {
            return _Data(DataResx.GetString("Xml_MultipleParentRows", new object[] { tableQName }));
        }
        public static Exception TooManyIsDataSetAtributeInSchema()
        {
            return _Data(DataResx.GetString("Xml_TooManyIsDataSetAtributeInSchema"));
        }
        public static Exception SimpleTypeNotSupported()
        {
            return _Data(DataResx.GetString("Xml_SimpleTypeNotSupported"));
        }
        public static Exception RelationParentNameMissing(string rel)
        {
            return _Data(DataResx.GetString("Xml_RelationParentNameMissing", new object[] { rel }));
        }
        public static Exception PolymorphismNotSupported(string typeName)
        {
            return _InvalidOperation(DataResx.GetString("Xml_PolymorphismNotSupported", new object[] { typeName }));
        }
        public static Exception NestedCircular(string name)
        {
            return _Data(DataResx.GetString("Xml_NestedCircular", new object[] { name }));
        }

        public static Exception RelationChildKeyMissing(string rel)
        {
            return _Data(DataResx.GetString("Xml_RelationChildKeyMissing", new object[] { rel }));
        }

        public static Exception RelationChildNameMissing(string rel)
        {
            return _Data(DataResx.GetString("Xml_RelationChildNameMissing", new object[] { rel }));
        }

        public static Exception RelationTableKeyMissing(string rel)
        {
            return _Data(DataResx.GetString("Xml_RelationTableKeyMissing", new object[] { rel }));
        }

        //}

        #endregion

        #region Data exception

        #endregion
    }
}
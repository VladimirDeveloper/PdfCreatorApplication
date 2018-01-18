using System;
using System.Data;

namespace PdfCreatorApplication.Core.Utils.Database
{
    /// <summary>
    /// Contains methods helping to work with DataRow
    /// </summary>
    public static class DataRowHelper
    {
        /// <summary>
        /// Gets the value of specified row.
        /// This method is extention of System.Data.DataRow. 
        /// </summary>
        /// <typeparam name="T">Desired type of value which will be returned</typeparam>
        /// <param name="dataRow">The data row.</param>
        /// <param name="columnName">Name of the column.</param>
        /// <param name="defaultValue">The default value, in case column's value will be null.</param>
        /// <returns>Value of specified column, if value is not null. If value is null, return specified default value</returns>
        public static T Get<T>(this DataRow dataRow, string columnName, T defaultValue) where T : IConvertible
        {
            if (dataRow != null && dataRow.Table.Columns.Contains(columnName))
            {
                object value = dataRow[columnName];
                if (value != null && value != DBNull.Value)
                {
                    return (T)Convert.ChangeType(value, typeof(T));
                }
            }
            return defaultValue;
        }

        /// <summary>
        /// Gets the value of specified row.
        /// This method is extention of System.Data.DataRow. 
        /// </summary>
        /// <typeparam name="T">Desired type of value which will be returned</typeparam>
        /// <param name="dataRow">The data row.</param>
        /// <param name="columnName">Name of the column.</param>
        /// <returns>Value of specified column, if value is not null. If value is null, return default (Null or "0") depends on desired type T</returns>
        public static T Get<T>(this DataRow dataRow, string columnName)
        {
            if (dataRow != null && dataRow.Table.Columns.Contains(columnName))
            {
                object value = dataRow[columnName];
                if (value != null && value != DBNull.Value)
                {
                    Type type = typeof(T);
                    if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
                    {
                        Type t = typeof(T);
                        t = Nullable.GetUnderlyingType(t) ?? t;
                        return (T)Convert.ChangeType(value, t);
                    }
                    return (T)Convert.ChangeType(value, typeof(T));
                }
            }
            return default(T);
        }


        /// <summary>
        /// Gets the nullable.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataRow">The data row.</param>
        /// <param name="columnName">Name of the column.</param>
        /// <returns></returns>
        public static T? GetNullable<T>(this DataRow dataRow, string columnName) where T : struct
        {
            if (dataRow != null && dataRow.Table.Columns.Contains(columnName))
            {
                object value = dataRow[columnName];
                if (value != null && value != DBNull.Value)
                {
                    return Get<T>(dataRow, columnName);
                }
            }
            return null;
        }

        /// <summary>
        /// Gets the specified column value.
        /// </summary>
        /// <param name="columnValue">The column value.</param>
        /// <returns>If column value is null will return DBNull.Value, else returns original column value</returns>
        public static object Get(object columnValue)
        {
            if (columnValue == null)
            {
                columnValue = DBNull.Value;
            }
            return columnValue;
        }

    }
}

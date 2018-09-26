using System;
using System.Collections.Generic;
using System.Data;

namespace System.Data
{
    public static class DataReaderExtensions
    {
        private static T GetValueWith<T>(IDataReader subject, Func<int, T> getClosure, string columnName)
        {
            var ordinal = subject.GetOrdinal(columnName);
            var value = getClosure(ordinal);
            return value;
        }

        private static T? GetNullableValueWith<T>(IDataReader subject, string columnName)
            where T : struct
        {
            var ordinal = subject.GetOrdinal(columnName);
            var value = subject.GetValue(ordinal);

            if (DBNull.Value.Equals(value))
                return null;

            return (T)value;
        }

        public static byte[] GetBytes(this IDataReader subject, string columnName)
        {
            var ordinal = subject.GetOrdinal(columnName);
            if (subject.IsDBNull(ordinal))
                return default(byte[]);

            long dataLength = subject.GetBytes(ordinal, 0, null, 0, 0);
            var bytes = new byte[dataLength];
            var bufferSize = 1024;
            var bytesRead = 0L;
            var curPos = 0;
            while (bytesRead < dataLength)
            {
                bytesRead += subject.GetBytes(ordinal, curPos, bytes, curPos, bufferSize);
                curPos += bufferSize;
            }

            return bytes;
        }

        public static string GetString(this IDataReader subject, string columnName)
        {
            var ordinal = subject.GetOrdinal(columnName);
            if (subject.IsDBNull(ordinal))
                return null;

            return GetValueWith(subject, subject.GetString, columnName);
        }

        public static bool GetBoolean(this IDataReader subject, string columnName)
        {
            return GetValueWith(subject, subject.GetBoolean, columnName);
        }

        public static bool? GetBooleanNullable(this IDataReader subject, string columnName)
        {
            return GetNullableValueWith<bool>(subject, columnName);
        }

        public static byte GetByte(this IDataReader subject, string columnName)
        {
            return GetValueWith(subject, subject.GetByte, columnName);
        }

        public static byte? GetByteNullable(this IDataReader subject, string columnName)
        {
            return GetNullableValueWith<byte>(subject, columnName);
        }

        public static decimal GetDecimal(this IDataReader subject, string columnName)
        {
            return GetValueWith(subject, subject.GetDecimal, columnName);
        }

        public static decimal? GetDecimalNullable(this IDataReader subject, string columnName)
        {
            return GetNullableValueWith<decimal>(subject, columnName);
        }

        public static double GetDouble(this IDataReader subject, string columnName)
        {
            return GetValueWith(subject, subject.GetDouble, columnName);
        }

        public static double? GetDoubleNullable(this IDataReader subject, string columnName)
        {
            return GetNullableValueWith<double>(subject, columnName);
        }

        public static float GetFloat(this IDataReader subject, string columnName)
        {
            return GetValueWith(subject, subject.GetFloat, columnName);
        }

        public static float? GetFloatNullable(this IDataReader subject, string columnName)
        {
            return GetNullableValueWith<float>(subject, columnName);
        }

        public static short GetInt16(this IDataReader subject, string columnName)
        {
            return GetValueWith(subject, subject.GetInt16, columnName);
        }

        public static short? GetInt16Nullable(this IDataReader subject, string columnName)
        {
            return GetNullableValueWith<short>(subject, columnName);
        }

        public static int GetInt32(this IDataReader subject, string columnName)
        {
            return GetValueWith(subject, subject.GetInt32, columnName);
        }

        public static int? GetInt32Nullable(this IDataReader subject, string columnName)
        {
            return GetNullableValueWith<int>(subject, columnName);
        }

        public static long GetInt64(this IDataReader subject, string columnName)
        {
            return GetValueWith(subject, subject.GetInt64, columnName);
        }

        public static long? GetInt64Nullable(this IDataReader subject, string columnName)
        {
            return GetNullableValueWith<long>(subject, columnName);
        }

        public static DateTime GetDateTime(this IDataReader subject, string columnName)
        {
            return GetValueWith(subject, subject.GetDateTime, columnName);
        }

        public static DateTime? GetDateTimeNullable(this IDataReader subject, string columnName)
        {
            return GetNullableValueWith<DateTime>(subject, columnName);
        }
    }
}

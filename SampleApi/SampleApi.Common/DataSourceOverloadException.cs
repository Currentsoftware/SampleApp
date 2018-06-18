using System;

namespace SampleApi.Common
{
    /// <summary>
    /// Thrown when the data source is getting too many requests
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2237:MarkISerializableTypesWithSerializable", Justification = "Keep it simple for now")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1032:ImplementStandardExceptionConstructors", Justification = "Keep it simple for now")]
    public class DataSourceOverloadException : Exception
    {
    }
}

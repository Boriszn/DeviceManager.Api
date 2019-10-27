using System;

namespace DeviceManager.Api.Attributes.Dapper
{
    /// <summary>
    /// This attribute is used to indicate Dapper to select property in the Insert query
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class DapperInsertAttribute : Attribute
    {
    }
}

using System;

namespace DeviceManager.Api.Attributes.Dapper
{
    /// <summary>
    /// This attribute is used to indicate Dapper to select property in the Update query
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class DapperUpdateAttribute : Attribute
    {
    }
}

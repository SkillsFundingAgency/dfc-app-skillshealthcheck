using System;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using System.Text.Json;
using DfE.SkillsCentral.Api.Domain.Models;

public class DictionaryTypeHandler : SqlMapper.TypeHandler<Dictionary<string, object>?>
{
    public override void SetValue(IDbDataParameter parameter, Dictionary<string, object>? value)
    {
        parameter.Value = (value == null) ? (object)DBNull.Value : JsonSerializer.Serialize(value);
    }

    public override Dictionary<string,object>? Parse(object value)
    {
        return value is DBNull ? null : JsonSerializer.Deserialize<Dictionary<string, object>?>((string)value);
    }
}
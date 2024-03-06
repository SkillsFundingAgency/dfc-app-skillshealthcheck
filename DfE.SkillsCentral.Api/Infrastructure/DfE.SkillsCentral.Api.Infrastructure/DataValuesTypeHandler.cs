using System;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using System.Text.Json;
using DfE.SkillsCentral.Api.Domain.Models;

public class DataValuesTypeHandler : SqlMapper.TypeHandler<DataValues>
{
    public override void SetValue(IDbDataParameter parameter, DataValues value)
    {
        parameter.Value = (value == null) ? (object)DBNull.Value : JsonSerializer.Serialize(value);
    }

    public override DataValues Parse(object value)
    {
        if (value is DBNull || value == null)
        {
            return null;
        }

        return JsonSerializer.Deserialize<DataValues>((string)value);
    }
}
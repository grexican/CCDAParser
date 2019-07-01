using System.Collections.Generic;
using System.Linq;
using System.Text;
using CqmSolution.Models;
using CqmSolution.Models.Extensions;

namespace CqmSolution.XmlExtensions
{
    public static class StoredProcStagingDataPrep
    {
        public static void UpdateClientStagingData(Client client, string stagingTableName = "StagingTable")
        {
            //ClearStagingTableForClient(client, stagingTableName);
        }

        public static void ClearStagingTableForClient(Client client, string stagingTableName = "StagingTable")
        {
            // DELETE all staging table for that client...
        }

        public static string GenerateStagingTableInserts(List<CqmSolutionEntity> entities, string stagingTableName="StagingTable")
        {
            var sql = new StringBuilder();

            foreach (var entity in entities)
            {
                string clientIdentifier = null;

                var entityType = entity.GetType();

                if (entityType == typeof(Client))
                {
                    var client = entity as Client;
                    clientIdentifier = client?.ClientIdentifier;
                }
                else if (typeof(CqmSolutionClientEntity).IsAssignableFrom(entityType))
                {
                    var clientEntity = entity as CqmSolutionClientEntity;
                    clientIdentifier = clientEntity?.Client?.ClientIdentifier;
                }

                var dynamicValues = new List<string>();

                var dynamicParameters = entity.GetDynamicParameters();

                var range = Enumerable.Range(1, 50).Select(i =>
                {
                    var idx = ("00" + i);

                    return $"D{idx.Substring(idx.Length - 3)}";
                }).ToList();

                foreach(var idx in range)
                {
                    dynamicValues.Add(SqlSafeString(dynamicParameters.TryGetValue(idx, out var value) ? value : null));
                }

                var insert = $"INSERT INTO {stagingTableName} (CLIENT_ID, REC_TYPE, SEC_TYPE, {string.Join(", ", range)}, " +
                             $"ValueSetOID, ACCOUNT_NUMBER, IDRoot, IDExtension) VALUES ({SqlSafeString(clientIdentifier)}, {SqlSafeString(entity.RecordType)}, " +
                             $"{SqlSafeString(entity.SectionType)}, {string.Join(", ", dynamicValues)}, {SqlSafeString(entity.ValueSetOid?.Value)}, {SqlSafeString(entity.AccountNumber)}, {SqlSafeString(entity.IdRoot?.Value)}, {SqlSafeString(entity.IdExtension)})";

                sql.AppendLine(insert);
            }

            return sql.ToString();
        }

        public static string SqlSafeString(string input)
        {
            var ret = input?.Replace("'", "''");

            return ret == null ? "NULL" : $"'{ret}'";
        }
    }
}

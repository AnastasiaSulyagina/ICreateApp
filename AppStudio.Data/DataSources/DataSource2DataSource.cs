using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppStudio.Data
{
    public class DataSource2DataSource : DataSourceBase<Schema1Schema>
    {
        private const string _file = "/Assets/Data/DataSource2DataSource.json";

        protected override string CacheKey
        {
            get { return "DataSource2DataSource"; }
        }

        public override bool HasStaticData
        {
            get { return false; }
        }

        public async override Task<IEnumerable<Schema1Schema>> LoadDataAsync()
        {
            try
            {
                var serviceDataProvider = new StaticDataProvider(_file);
                return await serviceDataProvider.Load<Schema1Schema>();
            }
            catch (Exception ex)
            {
                AppLogs.WriteError("DataSource2DataSource.LoadData", ex.ToString());
                return new Schema1Schema[0];
            }
        }
    }
}

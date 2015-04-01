using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppStudio.Data
{
    public class DessertsDataSource : DataSourceBase<DessertsSchema>
    {
        private const string _file = "/Assets/Data/DessertsDataSource.json";

        protected override string CacheKey
        {
            get { return "DessertsDataSource"; }
        }

        public override bool HasStaticData
        {
            get { return false; }
        }

        public async override Task<IEnumerable<DessertsSchema>> LoadDataAsync()
        {
            try
            {
                var serviceDataProvider = new StaticDataProvider(_file);
                return await serviceDataProvider.Load<DessertsSchema>();
            }
            catch (Exception ex)
            {
                AppLogs.WriteError("DessertsDataSource.LoadData", ex.ToString());
                return new DessertsSchema[0];
            }
        }
    }
}

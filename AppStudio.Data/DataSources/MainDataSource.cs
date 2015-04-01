using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppStudio.Data
{
    public class MainDataSource : DataSourceBase<MainSchema>
    {
        private const string _file = "/Assets/Data/MainDataSource.json";

        protected override string CacheKey
        {
            get { return "MainDataSource"; }
        }

        public override bool HasStaticData
        {
            get { return false; }
        }

        public async override Task<IEnumerable<MainSchema>> LoadDataAsync()
        {
            try
            {
                var serviceDataProvider = new StaticDataProvider(_file);
                return await serviceDataProvider.Load<MainSchema>();
            }
            catch (Exception ex)
            {
                AppLogs.WriteError("MainDataSource.LoadData", ex.ToString());
                return new MainSchema[0];
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppStudio.Data
{
    public class StartersDataSource : DataSourceBase<StartersSchema>
    {
        private const string _file = "/Assets/Data/StartersDataSource.json";

        protected override string CacheKey
        {
            get { return "StartersDataSource"; }
        }

        public override bool HasStaticData
        {
            get { return false; }
        }

        public async override Task<IEnumerable<StartersSchema>> LoadDataAsync()
        {
            try
            {
                var serviceDataProvider = new StaticDataProvider(_file);
                return await serviceDataProvider.Load<StartersSchema>();
            }
            catch (Exception ex)
            {
                AppLogs.WriteError("StartersDataSource.LoadData", ex.ToString());
                return new StartersSchema[0];
            }
        }
    }
}

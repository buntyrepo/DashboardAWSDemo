using DevExpress.DashboardCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard.Module.Helpers
{
	public class AWSFillService : IObjectDataSourceCustomFillService
	{
		public AWSFillService(AWSDashboardDataProvider dataProvider, IObjectDataSourceCustomFillService defaultFillService)
		{
			DataProvider = dataProvider;
			DefaultFillService = defaultFillService;
		}

		private AWSDashboardDataProvider DataProvider
		{
			get;
		}
		private IObjectDataSourceCustomFillService DefaultFillService
		{
			get;
		}

		public object GetData(DashboardObjectDataSource dataSource, ObjectDataSourceFillParameters fillParameters)
		{

			if (fillParameters.CtorParameters != null && fillParameters.CtorParameters.Count > 0)
			{
				AWSDataSource DataSource = new AWSDataSource((string)fillParameters.CtorParameters[0].Value);
				DataSource.Application = DataProvider.ContextApplication;


				var data = fillParameters.Parameters.ToDictionary(p => p.Name, p => p.Value);

				return DataSource.GetData(data, (string)fillParameters.CtorParameters[1].Value);
			}
			else
			{
				var data = DefaultFillService.GetData(dataSource, fillParameters);
				return data;
			}

		}



	}
}

using System;
using DevExpress.DashboardCommon;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Dashboards;
using DevExpress.Persistent.Base;

namespace Dashboard.Module.Helpers
{
public class AWSDashboardDataProvider : DashboardDataProvider
	{

		protected override IObjectDataSourceCustomFillService CreateViewService(IDashboardData dashboardData)
		{
			return new AWSFillService(this, base.CreateViewService(dashboardData));
		}

		protected override IObjectDataSourceCustomFillService CreateService(IDashboardData dashboardData)
		{
			return new AWSFillService(this, base.CreateService(dashboardData));
		}

		public override IObjectSpace CreateObjectSpace(Type type)
		{
			return ContextApplication.CreateObjectSpace();
		}
	}
}

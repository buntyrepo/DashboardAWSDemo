using System;
using Dashboard.Win.Module.EnterPages;
using DevExpress.DashboardCommon;
using DevExpress.DashboardCommon.DataSourceWizard;
using DevExpress.DataAccess.Wizard;
using DevExpress.DataAccess.Wizard.Model;
using DevExpress.DataAccess.Wizard.Services;
using DevExpress.DataAccess.Wizard.Views;
using DevExpress.Entity.ProjectModel;
using DevExpress.ExpressApp.Dashboards.Win;

namespace Dashboard.Win.Module.DataSourceWizard
{

	public class XafDashboardChooseDataSourceTypePage<TModel> : DashboardChooseDataSourceTypePage<TModel> where TModel : class, IDashboardDataSourceModel
	{
		public XafDashboardChooseDataSourceTypePage(IChooseDataSourceTypePageView view, IWizardRunnerContext context, IConnectionStorageService connectionStorageService, IJsonConnectionStorageService jsonConnectionStorageService, ISolutionTypesProvider solutionTypesProvider, SqlWizardOptions options) 
			: base(view, context, connectionStorageService, jsonConnectionStorageService, solutionTypesProvider, options)
		{
		}

		public override void Commit()
		{
			base.Commit();
			if (IsXafDataSource())
			{
				Model.DataSourceType = DataSourceType.Object;
			}

			
		}
		public override Type GetNextPageType()
		{
			if (View.DataSourceType == XAFDataSourceType.XAF)
				return typeof(ChooseXafTypePage<DashboardDataSourceModel>);
			if (View.DataSourceType == AWSDataSourceType.Data)
				return typeof(EnterAWSPage<DashboardDataSourceModel>);

			var test = View.DataSourceType;

			return base.GetNextPageType();
		}

		private bool IsXafDataSource()
		{
			return View.DataSourceType == XAFDataSourceType.XAF || View.DataSourceType == AWSDataSourceType.Data;
		}
	}
}

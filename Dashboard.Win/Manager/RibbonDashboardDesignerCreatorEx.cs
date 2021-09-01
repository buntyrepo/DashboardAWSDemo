using Dashboard.Win.Module.DataSourceWizard;
using Dashboard.Win.Module.XAFDataSource;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Dashboards.Win;

namespace Dashboard.Win.Module.Manager
{

	public class RibbonDashboardDesignerCreatorEx : RibbonDashboardDesignerCreator
	{
		public RibbonDashboardDesignerCreatorEx(XafApplication application) : base(application)
		{
			Application = application;
		}

		private XafApplication Application
		{
			get;
		}

		protected override XafDashboardDataSourceWizardCustomization CreateXafDataSourceWizardCustomization()
		{
			var parameters = new AWSDashboardWizardParameters();			

			IObjectSpace objectSpace = Application.CreateObjectSpace();
			return new DashboardCustomization(parameters, objectSpace, Application);
		}
	}
}

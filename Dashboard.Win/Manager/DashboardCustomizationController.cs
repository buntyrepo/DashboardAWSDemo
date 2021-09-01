using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Dashboards.Win;

namespace Dashboard.Win.Module.Manager
{
	public class DashboardCustomizationController : ViewController
	{

		protected override void OnActivated()
		{
			base.OnActivated();
			var dashboardDesignerController = Frame.GetController<WinShowDashboardDesignerController>();
			dashboardDesignerController.DashboardDesignerManager = new DashboardDesignerManagerEx(Application);
		}
	}

}

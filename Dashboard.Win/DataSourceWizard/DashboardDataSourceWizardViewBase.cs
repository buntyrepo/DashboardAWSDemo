using DevExpress.DataAccess.UI.Wizard.Views;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Win.Layout;

namespace Dashboard.Win.Module.DataSourceWizard
{

	public abstract class DashboardDataSourceWizardViewBase : WizardViewBase
	{
		private DetailView paramsView;

		public DashboardDataSourceWizardViewBase(AWSDashboardWizardParameters parameters, IObjectSpace objectSpace, XafApplication application)
		{
			ObjectSpace = objectSpace;
			WizardParameters = parameters;
			Application = application;

			ParamsObjectSpace = application.CreateObjectSpace();
			paramsView = CreateDetailView(ParamsObjectSpace);
			paramsView.CreateControls();
			paramsView.LayoutManager.CustomizationEnabled = false;
			panelBaseContent.Controls.Add((XafLayoutControl)paramsView.LayoutManager.Container);
		}


		public AWSDashboardWizardParameters WizardParameters
		{
			get; set;
		}
		public IObjectSpace ObjectSpace
		{
			get; set;
		}
		public XafApplication Application
		{
			get;
		}
		public IObjectSpace ParamsObjectSpace
		{
			get; private set;
		}

		protected abstract DetailView CreateDetailView(IObjectSpace objectSpace);

		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			if (disposing && paramsView != null)
			{
				Controls.Remove(((XafLayoutControl)paramsView.LayoutManager.Container));
				paramsView.Dispose();
				paramsView = null;
			}
			if (ParamsObjectSpace != null)
			{
				ParamsObjectSpace.Dispose();
				ParamsObjectSpace = null;
			}
		}
	}
}

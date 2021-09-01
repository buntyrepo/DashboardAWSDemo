using Dashboard.Win.Module.DataSourceWizard;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Dashboards.Win;
using DevExpress.ExpressApp.Utils;

namespace Dashboard.Win.Module.XAFDefault
{
	public class DefaultXafChooseTypePageView : DashboardDataSourceWizardViewBase, IChooseXafTypePageView
	{
		public DefaultXafChooseTypePageView(AWSDashboardWizardParameters parameters, IObjectSpace objectSpace, XafApplication application)
			: base(parameters, objectSpace, application)
		{
		}

		public override string HeaderDescription => CaptionHelper.GetLocalizedText("Captions", "ChooseXafType");

		protected override DetailView CreateDetailView(IObjectSpace objectSpace) => Application.CreateDetailView(objectSpace, "XafWizardParameters_DetailView", true, WizardParameters);
		XafWizardParameters IChooseXafTypePageView.WizardParameters
		{
			get => WizardParameters;
			set => WizardParameters = (AWSDashboardWizardParameters)value;
		}

	}

}

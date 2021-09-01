using Dashboard.Win.Module.DataSourceWizard;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Utils;

namespace Dashboard.Win.Module.EnterPages
{
	public class EnterAWSPageView : DashboardDataSourceWizardViewBase, IEnterPageView
	{
		public EnterAWSPageView(AWSDashboardWizardParameters parameters, IObjectSpace objectSpace, XafApplication application) : base(parameters, objectSpace, application)
		{
		}

		protected override DetailView CreateDetailView(IObjectSpace objectSpace)
		{
			return Application.CreateDetailView(objectSpace, WizardParameters);
		}
		public override string HeaderDescription
		{
			get
			{
				return CaptionHelper.GetLocalizedText("Captions", "EnterData");
			}
		}
	}

	public interface IEnterPageView
	{
		AWSDashboardWizardParameters WizardParameters
		{
			get; set;
		}
		IObjectSpace ObjectSpace
		{
			get; set;
		}

		IObjectSpace ParamsObjectSpace
		{
			get;
		}
	}
}

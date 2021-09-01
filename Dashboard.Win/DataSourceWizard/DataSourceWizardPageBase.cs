using System.Linq;
using Dashboard.Win.Module.EnterPages;
using DevExpress.DashboardCommon;
using DevExpress.Data.WizardFramework;

namespace Dashboard.Win.Module.DataSourceWizard
{
	public abstract class DataSourceWizardPageBase<TModel> : WizardPageBase<IEnterPageView, TModel> where TModel : DashboardDataSourceModel
	{
		protected DataSourceWizardPageBase(IEnterPageView view) : base(view)
		{
		}

		protected T GetConstructorParameterValueByName<T>(string parameterName) => (T)Model?.CtorParameters?.Single(p => p.Name == parameterName)?.Value;

		public override void Begin()
		{
			View.WizardParameters.PropertyChanged += (s, e) => RaiseChanged();
		}
	}
}

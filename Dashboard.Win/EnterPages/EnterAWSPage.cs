using System;
using Dashboard.Module.Helpers;
using Dashboard.Win.Module.DataSourceWizard;
using DevExpress.DashboardCommon;
using DevExpress.Entity.ProjectModel;

namespace Dashboard.Win.Module.EnterPages
{

	public class EnterAWSPage<TModel> : DataSourceWizardPageBase<TModel> where TModel : DashboardDataSourceModel
	{
		public EnterAWSPage(IEnterPageView view) : base(view)
		{
		}
		public override void Begin()
		{
			base.Begin();
			string ParameterValue = GetConstructorParameterValueByName<string>("url");
			if (ParameterValue != null)
				View.WizardParameters.AWS = ParameterValue;
		}
		public override void Commit()
		{
			Type type = typeof(AWSDataSource);
			Model.Assembly = new DXAssemblyInfo(type.Assembly, false, true, null);			
			Model.ObjectType = new DXTypeInfo(type);
			Model.ObjectConstructor = type.GetConstructor(new[] { typeof(string) });
			Model.CtorParameters = new[] { new DevExpress.DataAccess.ObjectBinding.Parameter("url", typeof(string), View.WizardParameters.AWS), new DevExpress.DataAccess.ObjectBinding.Parameter("worksheetIndex", typeof(string), View.WizardParameters.WorksheetIndex) };
		}
		public override bool FinishEnabled => !string.IsNullOrWhiteSpace(View.WizardParameters.AWS);
		public override bool MoveNextEnabled => false;
	}
}

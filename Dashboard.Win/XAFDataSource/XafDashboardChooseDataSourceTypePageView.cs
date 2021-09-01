using System.Linq;
using DevExpress.DashboardWin.DataSourceWizard;
using DevExpress.DashboardWin.ServiceModel;
using DevExpress.DataAccess.Wizard.Services;
using DevExpress.ExpressApp.Dashboards.Win;
using DevExpress.ExpressApp.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;

namespace Dashboard.Win.Module.XAFDataSource
{

	public class XafDashboardChooseDataSourceTypePageView : DashboardChooseDataSourceTypePageView
	{
		public XafDashboardChooseDataSourceTypePageView(ISupportedDataSourceTypesService dataSourceTypesService, DataSourceTypes dataSourceTypes)
		  : base(dataSourceTypesService, dataSourceTypes)
		{
			//TODO: Localize
			if (Controls.Find("dataSourceTypesListBox", true).FirstOrDefault() is ListBoxControl listbox)
			{
				listbox.Items.Add(new CheckedListBoxItem
				{
					Value = XAFDataSourceType.XAF,
					Description = CaptionHelper.GetLocalizedText("Captions", "XafObjectDataSource"),
					Tag = CaptionHelper.GetLocalizedText("Captions", "ObjectDataSourceWithXafPersistentTypes")
				});

				listbox.Items.Add(new CheckedListBoxItem
				{
					Value = Dashboard.Win.Module.DataSourceWizard.SenDevDataSourceType.Data,
					Description = "AWS S3 Excel data source",  //CaptionHelper.GetLocalizedText("Captions", "XafObjectDataSource"),
					Tag = "Add the AWS S3 link" //CaptionHelper.GetLocalizedText("Captions", "ObjectDataSourceWithXafPersistentTypes")
				});


				

			}
		}
	}
}

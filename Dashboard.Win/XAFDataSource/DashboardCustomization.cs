using Dashboard.Win.Module.DataSourceWizard;
using Dashboard.Win.Module.EnterPages;
using Dashboard.Win.Module.XAFDefault;
using DevExpress.DashboardCommon;
using DevExpress.DashboardWin.ServiceModel;
using DevExpress.DataAccess.UI.Wizard;
using DevExpress.DataAccess.Wizard.Model;
using DevExpress.DataAccess.Wizard.Presenters;
using DevExpress.DataAccess.Wizard.Views;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Dashboards.Win;
using System;

namespace Dashboard.Win.Module.XAFDataSource
{

    public class DashboardCustomization : XafDashboardDataSourceWizardCustomization, IDashboardDataSourceWizardCustomization
    {
        public DashboardCustomization(AWSDashboardWizardParameters wizardParameters, IObjectSpace objectSpace, XafApplication application) : base(wizardParameters, objectSpace, application)
        {
            WizardParameters = wizardParameters;
            ObjectSpace = objectSpace;
            Application = application;
        }

        public XafApplication Application
        {
            get;
        }
        private IObjectSpace ObjectSpace
        {
            get;
        }
        private AWSDashboardWizardParameters WizardParameters
        {
            get;
        }

        public new void CustomizeDataSourceWizard(IWizardCustomization<DashboardDataSourceModel> customization)
        {

            AddDefaultCustomizations(customization);
            if (!IsFederationDataSource(customization))
            {

                if (customization.Model.ObjectType != null)
                {
                    customization.StartPage = typeof(EnterAWSPage<DashboardDataSourceModel>);
                }
                var connectionModel = customization.Model as IDataComponentModelWithConnection;
                bool hasDataConnection = connectionModel?.DataConnection != null;

                if (customization.Model.DataSchema == null && !hasDataConnection)
                {
                    customization.StartPage = typeof(Dashboard.Win.Module.DataSourceWizard.XafDashboardChooseDataSourceTypePage<DashboardDataSourceModel>);
                }
            }
            customization.RegisterPage<Dashboard.Win.Module.DataSourceWizard.XafDashboardChooseDataSourceTypePage<DashboardDataSourceModel>, Dashboard.Win.Module.DataSourceWizard.XafDashboardChooseDataSourceTypePage<DashboardDataSourceModel>>();
            customization.RegisterPageView<IChooseDataSourceTypePageView, XafDashboardChooseDataSourceTypePageView>();
            customization.RegisterPage<EnterAWSPage<DashboardDataSourceModel>, EnterAWSPage<DashboardDataSourceModel>>();
            customization.RegisterPageView<IEnterPageView, EnterAWSPageView>();
            customization.RegisterInstance(WizardParameters);
            customization.RegisterInstance(ObjectSpace);
            customization.RegisterInstance(Application);
        }

        private bool IsFederationDataSource(IWizardCustomization<DashboardDataSourceModel> customization)
        {
            Type startPageType = customization.StartPage;
            return startPageType.IsGenericType && startPageType.GetGenericTypeDefinition() == typeof(ConfigureFederatedQueryPage<>);
        }

        private void AddDefaultCustomizations(IWizardCustomization<DashboardDataSourceModel> customization)
        {
            customization.RegisterPage<Dashboard.Win.Module.DataSourceWizard.XafDashboardChooseDataSourceTypePage<DashboardDataSourceModel>, Dashboard.Win.Module.DataSourceWizard.XafDashboardChooseDataSourceTypePage<DashboardDataSourceModel>>();
            customization.RegisterPageView<IChooseDataSourceTypePageView, XafDashboardChooseDataSourceTypePageView>();
            customization.RegisterPage<ChooseXafTypePage<DashboardDataSourceModel>, ChooseXafTypePage<DashboardDataSourceModel>>();
            customization.RegisterPageView<IChooseXafTypePageView, DefaultXafChooseTypePageView>();
        }
    }
}

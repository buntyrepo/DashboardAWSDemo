using System.ComponentModel;
using DevExpress.ExpressApp.Dashboards.Win;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;


namespace Dashboard.Win.Module.DataSourceWizard
{

	[DomainComponent]
	public class AWSDashboardWizardParameters : XafWizardParameters, INotifyPropertyChanged
	{

		public event PropertyChangedEventHandler PropertyChanged;


		public AWSDashboardWizardParameters()
		{
		}

		private void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		private string aws;

		[FieldSize(FieldSizeAttribute.Unlimited)]
		[ImmediatePostData]
		public string AWS
		{
			get => aws;
			set => SetPropertyValue(nameof(AWS), ref aws, value);
		}

		private string worksheetIndex;

        [ImmediatePostData]
        public string WorksheetIndex
        {
            get => worksheetIndex;
            set => SetPropertyValue(nameof(WorksheetIndex), ref worksheetIndex, value);
        }
        protected bool SetPropertyValue<T>(string propertyName, ref T propertyHolder, T value)
		{
			if (!Equals(value, propertyHolder))
			{
				var oldValue = propertyHolder;
				propertyHolder = value;
				OnChanged(propertyName, oldValue, value);
				return true;
			}

			return false;
		}

		protected virtual void OnChanged(string propertyName, object oldValue, object newValue)
		{
			OnPropertyChanged(propertyName);
		}

	}
}

using DashboardAWSDemo.Module.BusinessObjects;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Win.Editors;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DashboardAWSDemo.Module.Win.Editors
{
    [PropertyEditor(typeof(object), false)]
    public class URLPropertyEditor : DXPropertyEditor, IComplexViewItem
    {
        XafApplication xafApplication;
        IObjectSpace objectSpace;
        public URLPropertyEditor(Type objectType, DevExpress.ExpressApp.Model.IModelMemberViewItem model)
            : base(objectType, model)
        {
        }

        public void Setup(IObjectSpace _objectSpace, XafApplication _application)
        {
            xafApplication = _application;
            objectSpace = _objectSpace;
        }

        protected override object CreateControlCore()
        {

            ComboBoxEdit combo = new DevExpress.XtraEditors.ComboBoxEdit();
            combo.Properties.ImmediatePopup = true;
            combo.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            return combo;
        }
        protected override RepositoryItem CreateRepositoryItem()
        {
            return new RepositoryItemComboBox();
        }

        protected override void SetupRepositoryItem(RepositoryItem item)
        {
            base.SetupRepositoryItem(item);
            ((RepositoryItemComboBox)item).Items.Clear();

            var files = objectSpace.GetObjects<Files>();

            foreach (var item1 in files)
            {
                //GetPreSignedUrlRequest request = new GetPreSignedUrlRequest();
                //request.BucketName = "test23720214";
                //request.Key = "B2C.txt";
                //request.Expires = DateTime.Now.AddHours(1);
                //request.Protocol = Protocol.HTTP;
                //string url = s3Client.GetPreSignedURL(request);
                // Submit the request
                ((RepositoryItemComboBox)item).Items.Add(item1.URL);
            }
            ((RepositoryItemComboBox)item).EditValueChanged += ClientCustomLookupPropertyEditor_EditValueChanged;
        }

        private void ClientCustomLookupPropertyEditor_EditValueChanged(object sender, EventArgs e)
        {

            if (((DevExpress.XtraEditors.Controls.ChangingEventArgs)e).NewValue != null)
            {
                object newvalue = ((DevExpress.XtraEditors.Controls.ChangingEventArgs)e).NewValue;
                // ValueManagers.BucketName = newvalue.ToString();
                //SetupRepositoryItemExtracted(((ComboBoxEdit)Control).Properties, newvalue.ToString());
            }
        }

        //private void SetupRepositoryItemExtracted(RepositoryItem item, string searchText)
        //{
        //    ((RepositoryItemComboBox)item).Items.Clear();

        //    //string url = string.Format("https://maps.googleapis.com/maps/api/place/autocomplete/json?input={0}&types=address&language=en&key={1}", searchText, "AIzaSyC9h1101eEN644FuRVMgWsQyWvWk92-NLc");
        //    //var result = new System.Net.WebClient().DownloadString(url);
        //    //var Jsonobject = JsonConvert.DeserializeObject<Root>(result);
        //    //List<Prediction> list = Jsonobject.predictions;
        //    BasicAWSCredentials basicCredentials =
        //    new BasicAWSCredentials("AKIA2GSUQAMJRJ7KNM4G",
        //    "+s26IaA0d2VaCRGA7RsvrHN2679J+VbFNH6M0aD3");
        //    AmazonS3Client s3Client = new AmazonS3Client(basicCredentials);

        //    // Display all S3 buckets
        //    ListBucketsResponse buckets = s3Client.ListBuckets();

        //    foreach (var bucket in buckets.Buckets)
        //    {
        //        ((RepositoryItemComboBox)item).Items.Add(bucket.BucketName);
        //    }
        //}
    }
}

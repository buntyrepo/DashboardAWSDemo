using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using AWS;
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
    public class BucketPropertyEditor : DXPropertyEditor, IComplexViewItem
    {
        XafApplication xafApplication;
        IObjectSpace objectSpace;
        public BucketPropertyEditor(Type objectType, DevExpress.ExpressApp.Model.IModelMemberViewItem model)
            : base(objectType, model)
        {
        }

        public void Setup(IObjectSpace objectSpace, XafApplication application)
        {

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

            AmazonS3Client s3Client = new AmazonS3Client("AKIA2GSUQAMJRJ7KNM4G",
            "+s26IaA0d2VaCRGA7RsvrHN2679J+VbFNH6M0aD3", Amazon.RegionEndpoint.USEast1);

            ListBucketsResponse buckets = s3Client.ListBuckets();

            foreach (var bucket in buckets.Buckets)
            {
                //GetPreSignedUrlRequest request = new GetPreSignedUrlRequest();
                //request.BucketName = "test23720214";
                //request.Key = "B2C.txt";
                //request.Expires = DateTime.Now.AddHours(1);
                //request.Protocol = Protocol.HTTP;
                //string url = s3Client.GetPreSignedURL(request);
                // Submit the request
                ((RepositoryItemComboBox)item).Items.Add(bucket.BucketName);
            }
            ((RepositoryItemComboBox)item).EditValueChanged += ClientCustomLookupPropertyEditor_EditValueChanged;
        }

        private void ClientCustomLookupPropertyEditor_EditValueChanged(object sender, EventArgs e)
        {

            if (((DevExpress.XtraEditors.Controls.ChangingEventArgs)e).NewValue != null)
            {
                object newvalue = ((DevExpress.XtraEditors.Controls.ChangingEventArgs)e).NewValue;
                ValueManagers.BucketName = newvalue.ToString();
                //SetupRepositoryItemExtracted(((ComboBoxEdit)Control).Properties, newvalue.ToString());
            }
        }

        private void SetupRepositoryItemExtracted(RepositoryItem item, string searchText)
        {
            ((RepositoryItemComboBox)item).Items.Clear();

            //string url = string.Format("https://maps.googleapis.com/maps/api/place/autocomplete/json?input={0}&types=address&language=en&key={1}", searchText, "AIzaSyC9h1101eEN644FuRVMgWsQyWvWk92-NLc");
            //var result = new System.Net.WebClient().DownloadString(url);
            //var Jsonobject = JsonConvert.DeserializeObject<Root>(result);
            //List<Prediction> list = Jsonobject.predictions;
            BasicAWSCredentials basicCredentials =
            new BasicAWSCredentials("AKIA2GSUQAMJRJ7KNM4G",
            "+s26IaA0d2VaCRGA7RsvrHN2679J+VbFNH6M0aD3");
            AmazonS3Client s3Client = new AmazonS3Client(basicCredentials);

            // Display all S3 buckets
            ListBucketsResponse buckets = s3Client.ListBuckets();

            foreach (var bucket in buckets.Buckets)
            {
                ((RepositoryItemComboBox)item).Items.Add(bucket.BucketName);
            }
        }
    }
}

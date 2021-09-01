using AWS;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using System.ComponentModel;

namespace DashboardAWSDemo.Module.BusinessObjects
{
    [DefaultClassOptions]
    [DefaultProperty("Name")]
    public class Files : BaseObject
    {
        public Files(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        //[Bucket]
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Name
        {
            get => name;
            set => SetPropertyValue(nameof(Name), ref name, value);
        }

        string uRL;
        string bucket;
        string name;
        AmazonS3FileData file;
        [ImmediatePostData]
        [Appearance("EnableFile", "IsNullOrEmpty([Bucket])", Enabled = false)]
        public AmazonS3FileData File
        {
            get
            {
                if (!IsLoading && file != null && string.IsNullOrEmpty(uRL) && !string.IsNullOrEmpty(file.FileName))
                {
                    uRL = AmazonS3Helper.UploadedFileUrl(Bucket, file.FileName);
                }
                return file;
            }
            set
            {

                SetPropertyValue(nameof(File), ref file, value);

            }
        }

        [ImmediatePostData]
        [XafDisplayName("Select a container:")]
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Bucket
        {
            get => bucket;
            set => SetPropertyValue(nameof(Bucket), ref bucket, value);
        }
        
        [Size(200)]
        public string URL
        {
            get => uRL;
            set => SetPropertyValue(nameof(URL), ref uRL, value);
        }
    }
}

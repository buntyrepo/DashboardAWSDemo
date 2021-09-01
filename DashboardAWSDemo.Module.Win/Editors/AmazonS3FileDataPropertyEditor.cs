using AWS;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.FileAttachments.Win;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DashboardAWSDemo.Module.Win.Editors
{
    [PropertyEditor(typeof(AmazonS3FileData), true)]
    public class AmazonS3FileDataPropertyEditor : FileDataPropertyEditor
    {
        public AmazonS3FileDataPropertyEditor(Type objectType, DevExpress.ExpressApp.Model.IModelMemberViewItem model)
            : base(objectType, model)
        {

        }
        protected override object CreateControlCore()
        {

            var themember = this.MemberInfo.FindAttribute<BucketAttribute>(true);
            return new AmazonS3FileDataEdit();
        }

    }
}

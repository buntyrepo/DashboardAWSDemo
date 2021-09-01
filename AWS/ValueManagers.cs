using DevExpress.Persistent.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWS
{
    public class ValueManagers
    {
        private const string bucketName = "Bucket";
        public static string BucketName
        {
            get
            {
                var valueManager = ValueManager.GetValueManager<string>(bucketName);
                return valueManager.Value;
            }
            set
            {
                var valueManager = ValueManager.GetValueManager<string>(bucketName);
                if (valueManager.CanManageValue)
                    valueManager.Value = value;
            }
        }
    }
}

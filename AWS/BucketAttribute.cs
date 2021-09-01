using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWS
{
    [AttributeUsage(AttributeTargets.Property)]
    public class BucketAttribute : Attribute
    {
        // Fields...
        private string _BucketName;

        public string BucketName
        {
            get { return _BucketName; }
            set
            {
                _BucketName = value;
            }
        }

    }
}

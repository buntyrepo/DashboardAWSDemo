using DevExpress.ExpressApp.FileAttachments.Win;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DashboardAWSDemo.Module.Win.Editors
{
    public class AmazonS3FileDataEdit : FileDataEdit
    {
        public AmazonS3FileDataEdit()
        {

        }
        public new void FileSelected(string fileName)
        {
            if (!string.IsNullOrEmpty(fileName))
            {
                Focus();
                if (ContainsFocus)
                {
                    using (FileStream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    {

                        if (FileData == null)
                        {
                            EditValue = OnCreateCustomFileDataObject();
                            IsModified = true;
                            DoValidate();
                        }
                        if (FileData != null)
                        {
                            LoadFromStream(FileData, Path.GetFileName(fileName), stream, fileName);
                            this.UpdateDisplayText();
                            IsModified = true;
                        }
                    }
                }
            }
        }
        public static bool IsFileDataEmpty(IFileData fileData)
        {
            return (fileData == null) || (string.IsNullOrEmpty(fileData.FileName));
        }
        public static void LoadFromStream(IFileData fileData, string fileName, Stream stream, string fullName)
        {
            Guard.ArgumentNotNull(fileData, "fileData");
            if (fileData is ISupportFullName)
            {
                ((ISupportFullName)fileData).FullName = fullName;
            }
            fileData.LoadFromStream(fileName, stream);
        }

    }
}

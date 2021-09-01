using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System;
using System.ComponentModel;
using System.IO;

namespace AWS
{
    [DefaultProperty("FileName")]
    public class AmazonS3FileData : BaseObject, IFileData, IEmptyCheckable
    {
        private string fileName = "";
#if MediumTrust
		private int size;
		public int Size {
			get { return size; }
			set { SetPropertyValue("Size", ref size, value); }
		}
#else
        [Persistent]
        private int size;
        public int Size
        {
            get { return size; }
        }
#endif
        public AmazonS3FileData(Session session) : base(session) { }

        public virtual void LoadFromStream(string fileName, Stream stream)
        {

            // System.Threading.Tasks.Task task = new System.Threading.Tasks.Task(Upload);
            //task.Start();
            //task.Wait();

            Guard.ArgumentNotNull(stream, "stream");
            FileName = fileName;
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            Upload(FileName, stream);



        }
        protected virtual void Upload(string file, Stream stream)
        {

            AmazonS3Helper.Upload(stream, file);

        }

        public virtual void SaveToStream(Stream stream)
        {
            AmazonS3Helper.DownloadFile(this.fileName, stream);
        }
        public void Clear()
        {
            AmazonS3Helper.DeleteFile(this.fileName);
            this.size = 0;
            FileName = String.Empty;
        }
        public override string ToString()
        {
            return FileName;
        }
        [Size(260)]
        public string FileName
        {
            get { return fileName; }
            set { SetPropertyValue("FileName", ref fileName, value); }
        }

        #region IEmptyCheckable Members
        [NonPersistent, MemberDesignTimeVisibility(false)]
        public bool IsEmpty
        {
            get { return string.IsNullOrEmpty(FileName); }
        }
        #endregion


    }
}

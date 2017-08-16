using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Drive.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RaviLib.Utils
{
    public class GoogleDriveUtil
    {
        private const string GOOGLE_CLIENT_ID = "1352944710-6ob455b71qun9ij48ou7lit2inkvldnd.apps.googleusercontent.com";
        private const string GOOGLE_CLIENT_SECRET = "BQUmZFvGLYlRd_EQZk8x0m-3";
        private static DriveService service = null;


        public GoogleDriveUtil()
        {
            if(service == null)
                Authenticate();
        }
        #region public methods
        public File CreateDirectory(string title, string description, string parent)
        {
            // Create metaData for a new Directory
            File body = new File();
            body.Name = title;
            body.Description = description;
            body.MimeType = "application/vnd.google-apps.folder";
            if (!string.IsNullOrEmpty(parent))
            {
                body.Parents = new List<string> { parent };
            }
             

            FilesResource.CreateRequest request = service.Files.Create(body);
            request.Fields = "id";
            return request.Execute();            
        }

        public File UploadFile(string uploadFile, string description, string parent, string fields)
        {
            File body = new File();
            body.Name = System.IO.Path.GetFileName(uploadFile);
            body.Description = description;
            body.MimeType = GetMimeType(uploadFile);
           
            if (!string.IsNullOrEmpty(parent))
            {
                body.Parents = new List<string> { parent };
            }

            // File's content.
            byte[] byteArray = System.IO.File.ReadAllBytes(uploadFile);
            System.IO.MemoryStream stream = new System.IO.MemoryStream(byteArray);
             
            FilesResource.CreateMediaUpload request = service.Files.Create(body, stream, GetMimeType(uploadFile));
            request.Fields = fields;
            request.Upload();
            return request.ResponseBody;
            
        }

        public File UpdateFile(string _uploadFile, string description, string parent, string _fileId)
        {
            File body = new File();
            body.Name = System.IO.Path.GetFileName(_uploadFile);
            body.Description = description;
            body.MimeType = GetMimeType(_uploadFile);
            if (!string.IsNullOrEmpty(parent))
            {
                body.Parents = new List<string> { parent };
            }

            // File's content.
            byte[] byteArray = System.IO.File.ReadAllBytes(_uploadFile);
            System.IO.MemoryStream stream = new System.IO.MemoryStream(byteArray);
            FilesResource.UpdateMediaUpload request = service.Files.Update(body, _fileId, stream, GetMimeType(_uploadFile));
            request.Upload();
            return request.ResponseBody;
            
        }

        public Boolean DownloadFile( File _fileResource, string _saveTo)
        {

            //if (!String.IsNullOrEmpty(_fileResource.WebContentLink))
            {
                //try
                //{
                    var x = service.HttpClient.GetByteArrayAsync(_fileResource.WebContentLink);
                    byte[] arrBytes = x.Result;
                    System.IO.File.WriteAllBytes(_saveTo, arrBytes);
                    return true;
                //}
                //catch (Exception e)
                //{
                //    Console.WriteLine("An error occurred: " + e.Message);
                //    return false;
                //}
            }
            //else
            //{
            //    // The file doesn't have any content stored on Drive.
            //    return false;
            //}
        }


        public IList<File> GetFiles(string search)
        {
            IList<File> Files = new List<File>();
            
            FilesResource.ListRequest list = service.Files.List();
            list.PageSize = 1000;
            if (search != null)
            {
                list.Q = search;
            }
            FileList filesFeed = list.Execute();

            //// Loop through until we arrive at an empty page
            while (filesFeed.Files != null)
            {
                // Adding each item  to the list.
                foreach (File item in filesFeed.Files)
                {
                    Files.Add(item);
                }

                // We will know we are on the last page when the next page token is
                // null.
                // If this is the case, break.
                if (filesFeed.NextPageToken == null)
                {
                    break;
                }

                // Prepare the next page of results
                list.PageToken = filesFeed.NextPageToken;

                // Execute and process the next page request
                filesFeed = list.Execute();
            }
            return Files;
        }

        public File GetDirectory(string directoryName, string parent, string fields = "nextPageToken, files(id, name)")
        {
            File file = null;

            FilesResource.ListRequest list = service.Files.List();
            list.PageSize = 1000;
            list.Fields = fields;
            if(!string.IsNullOrEmpty(parent))
                list.Q = "mimeType = 'application/vnd.google-apps.folder' and '"+parent+"' in parents and trashed = false";
            else
                list.Q = "mimeType = 'application/vnd.google-apps.folder' and trashed = false";
            FileList filesFeed = list.Execute();

            //// Loop through until we arrive at an empty page
            while (filesFeed.Files != null)
            {
                // Adding each item  to the list.
                foreach (File item in filesFeed.Files)
                {
                    // Files.Add(item);
                    if (item.Name == directoryName)
                    {
                        return item;
                    }
                }

                // We will know we are on the last page when the next page token is
                // null.
                // If this is the case, break.
                if (filesFeed.NextPageToken == null)
                {
                    break;
                }

                // Prepare the next page of results
                list.PageToken = filesFeed.NextPageToken;

                // Execute and process the next page request
                filesFeed = list.Execute();
            }
            return file;
        }


        public File UpsertDirectory(string folderName, string parent)
        {
            File file = null;

            file = GetDirectory(folderName, parent);
            if(file == null)
            {
                file = CreateDirectory(folderName, "", parent);
            }

            return file;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName">ex: ravi.png</param>
        /// <param name="fileType">ex: image/png</param>
        /// <param name="parentId"></param>
        /// <param name="fields"></param>
        /// <returns></returns>
        public File GetFileInFolder(string fileName, string fileType, string parentId, string fields = "nextPageToken, files(id, name, thumbnailLink)")
        {
            File file = null;

            FilesResource.ListRequest list = service.Files.List();
            list.PageSize = 1000;
            list.Fields = fields;
            if (!string.IsNullOrEmpty(parentId))
            {
                list.Q = "mimeType = '"+ fileType + "' and '" + parentId + "' in parents and trashed = false";
            } else
            {
                list.Q = "mimeType = '"+ fileType + "'";
            }
           
            FileList filesFeed = list.Execute();

            //// Loop through until we arrive at an empty page
            while (filesFeed.Files != null)
            {
                // Adding each item  to the list.
                foreach (File item in filesFeed.Files)
                {
                   // Files.Add(item);
                   if(item.Name == fileName)
                    {
                        return item;
                    }
                }

                // We will know we are on the last page when the next page token is
                // null.
                // If this is the case, break.
                if (filesFeed.NextPageToken == null)
                {
                    break;
                }

                // Prepare the next page of results
                list.PageToken = filesFeed.NextPageToken;

                // Execute and process the next page request
                filesFeed = list.Execute();
            }
            return file;
        }


        public File GetFile(string fileName, string search, string fields = "nextPageToken, files(id, name, thumbnailLink)")
        {
            File file = null;

            FilesResource.ListRequest list = service.Files.List();
            list.PageSize = 1000;
            list.Fields = fields;
            if (search != null)
            {
                list.Q = search;
            }
            FileList filesFeed = list.Execute();

            //// Loop through until we arrive at an empty page
            while (filesFeed.Files != null)
            {
                // Adding each item  to the list.
                foreach (File item in filesFeed.Files)
                {
                    // Files.Add(item);
                    if (item.Name == fileName)
                    {
                        return item;
                    }
                }

                // We will know we are on the last page when the next page token is
                // null.
                // If this is the case, break.
                if (filesFeed.NextPageToken == null)
                {
                    break;
                }

                // Prepare the next page of results
                list.PageToken = filesFeed.NextPageToken;

                // Execute and process the next page request
                filesFeed = list.Execute();
            }
            return file;
        }

        public File UpsertFile(string filePath, string parent, string fields = "id, name, thumbnailLink")
        {
            File file = null;
            file = GetFile(System.IO.Path.GetFileName(filePath), "mimeType = 'image/png' and '" + parent + "' in parents and trashed = false");
            if(file == null || file.ThumbnailLink == null)
            {
                file = UploadFile(filePath, "", parent, fields);
                InsertPermission(file.Id, null, "anyone", "reader");
            }
            return file;
        }
        /// <summary>
        /// Insert a new permission.
        /// </summary>
        /// <param name="service">Drive API service instance.</param>
        /// <param name="fileId">ID of the file to insert permission for.</param>
        /// <param name="who">
        /// User or group e-mail address, domain name or null for "default" type.
        /// </param>
        /// <param name="type">The value "user", "group", "domain" or "default".</param>
        /// <param name="role">The value "owner", "writer" or "reader".</param>
        /// <returns>The inserted permission, null is returned if an API error occurred</returns>
        public Permission InsertPermission(String fileId, String who, String type, String role)
        {
            Permission newPermission = new Permission();
           // newPermission.Domain = value;
            
            newPermission.Type = type;
            newPermission.Role = role;
            return service.Permissions.Create(newPermission, fileId).Execute();
        }

        #endregion


        #region private methods

        private string GetMimeType(string fileName)
        {
            string mimeType = "application/unknown";
            string ext = System.IO.Path.GetExtension(fileName).ToLower();
            Microsoft.Win32.RegistryKey regKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(ext);
            if (regKey != null && regKey.GetValue("Content Type") != null)
                mimeType = regKey.GetValue("Content Type").ToString();
            return mimeType;
        }
        private void Authenticate()
        {
            //Scopes for use with the Google Drive API
            string[] scopes = new string[] { DriveService.Scope.Drive,
                                 DriveService.Scope.DriveFile};

            UserCredential credential;

            using (var stream =
                new System.IO.FileStream(System.IO.Directory.GetCurrentDirectory() + "\\credential.json", System.IO.FileMode.Open,
                System.IO.FileAccess.Read))
            {
                string credPath = System.Environment.GetFolderPath(
                    System.Environment.SpecialFolder.Personal);
                credPath = System.IO.Path.Combine(credPath, System.IO.Directory.GetCurrentDirectory() + "\\result_credential.json");

                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    scopes,
                    "hcm.tuanlm@vtca.edu.vn",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
                Console.WriteLine("Credential file saved to: " + credPath);
            }

            // Create Drive API service.
            service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "sf uploader",
            });
        }

        #endregion


    }
}

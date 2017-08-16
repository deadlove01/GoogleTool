using GPlusTool.Models;
using LiteDB;
using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPlusTool.Controllers
{
    public class LiteDBController: IDisposable
    {
        #region create thread safe instance
        private static readonly Lazy<LiteDBController> lazy =
        new Lazy<LiteDBController>(() => new LiteDBController());

        public static LiteDBController Instance { get { return lazy.Value; } }

        private LiteDBController()
        {
        }

        #endregion


        private static readonly ILog logger = LogManager.GetLogger(typeof(LiteDBController));
        private LiteDatabase database = null;
        public void CreateDBWithEmail(string email)
        {
            string dbRootPath = Directory.GetCurrentDirectory() + "\\DB\\";
            if(!Directory.Exists(dbRootPath))
            {
                Directory.CreateDirectory(dbRootPath);
            }

            database = new LiteDatabase(dbRootPath + email + ".db");
            
            var comments = database.GetCollection<LiteComment>("comments");

            // Index document using a document property
            comments.EnsureIndex(x => x.PostId);
            
        }

        public void InsertPostId(string email, string postId)
        {
            try
            {
                if(database == null)
                {
                    CreateDBWithEmail(email);
                }

                var comments = database.GetCollection<LiteComment>("comments");
                comments.Insert(new LiteComment
                {
                    PostId = postId
                });
            }
            catch (Exception ex)
            {
                logger.ErrorFormat(ex.Message + "|" + ex.StackTrace);
            }
        }

        public bool ExistsPostId(string email, string postId)
        {
            try
            {
                if (database == null)
                {
                    CreateDBWithEmail(email);
                }

                var comments = database.GetCollection<LiteComment>("comments");
                return comments.Exists(cm => cm.PostId == postId);             
            }
            catch (Exception ex)
            {
                logger.ErrorFormat(ex.Message + "|" + ex.StackTrace);
            }

            return false;
        }

        public void Test(string email)
        {
            if (database == null)
            {
                CreateDBWithEmail(email);
            }

            var comments = database.GetCollection<LiteComment>("comments");
            var result = comments.FindAll().ToList();
            for (int i = 0; i < result.Count; i++)
            {
                Console.WriteLine(result[i].PostId);
            }
        }

        public void Dispose()
        {
            if(database != null)
            {
                database.Dispose();
            }
        }
    }
}

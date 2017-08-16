using GPlusTool.Controllers;
using GPlusTool.Models;
using log4net;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GPlusTool.Controller
{
    public class DriverController: IDisposable
    {
        public delegate void UpdateText(string text);
        public event UpdateText UpdateTextEvent; 


        private static readonly ILog logger = LogManager.GetLogger(typeof(DriverController));

        private ChromeDriver driver = null;
        
        private string emailInfo = string.Empty;
        public bool IsStarted { get; set; }
        public List<string> FollowedIds { get; set; }
        public List<string> CommunitiesIds { get; set; }


        #region logic
        

        public DriverController()
        {
            IsStarted = false;

            FollowedIds = new List<string>();
            CommunitiesIds = new List<string>();
        }

        
        public bool Start(Gmail gmail, string pass)
        {
            emailInfo = gmail.Email;
            //if (driver == null)
                Init();

            driver.Navigate().GoToUrl("https://plus.google.com");
            Thread.Sleep(5000);
            bool result =  Login(gmail.Email, pass);
            Thread.Sleep(1500);
            CheckPointFillRecovery(gmail);

            DisplayToEnglish();

            //CheckPointAddTrustedDevices();
            CreateProfile();

            IsStarted = result;
            return result;
        }




        public bool Login(string email, string pass)
        {
            try
            {
                // login url
                string loginUrl = "https://accounts.google.com/ServiceLogin";
                driver.Navigate().GoToUrl(loginUrl);

                Thread.Sleep(2000);

                var emailInput = GetElement(driver, By.Id("identifierId"));
                emailInput.Clear();
                emailInput.SendKeys(email);
                Thread.Sleep(1500);

                //identifierNext
                var divNext = driver.FindElementById("identifierNext");
                divNext.Click();
                Thread.Sleep(5000);

                var passInput = GetElement(driver, By.Name("password")); //driver.FindElementByName("password");
                passInput.Clear();
                passInput.SendKeys(pass);
                Thread.Sleep(1500);

                divNext = driver.FindElementById("passwordNext");

                divNext.Click();
                Thread.Sleep(3000);

                string result = CheckLoginFailed();
               
                if (!string.IsNullOrEmpty(result))
                {
                    if (UpdateTextEvent != null)
                    {
                        UpdateTextEvent(emailInfo+" - "+"Login result: " + result);
                    }
                    return false;
                }else
                {
                    if (UpdateTextEvent != null)
                    {
                        UpdateTextEvent(emailInfo + " - "+"Login success!");
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                if (UpdateTextEvent != null)
                {
                    UpdateTextEvent(emailInfo + " - "+string.Format("Error: {0}, Details: {1}", ex.Message, ex.StackTrace));
                }
                logger.ErrorFormat("Error: {0}, Details: {1}", ex.Message, ex.StackTrace);
            }

            return false;
        }


        private bool DisplayToEnglish()
        {
            try
            {
                // login url
                string loginUrl = "https://plus.google.com/";
                driver.Navigate().GoToUrl(loginUrl);

                Thread.Sleep(2000);

                var doc = new HtmlAgilityPack.HtmlDocument();
                doc.LoadHtml(driver.PageSource);
                var html = doc.DocumentNode.SelectSingleNode("//html");
                var lang = html.GetAttributeValue("lang", string.Empty);
                if(!string.IsNullOrEmpty(lang) && lang != "en")
                {
                    string editUrl = "https://myaccount.google.com/language";
                    driver.Navigate().GoToUrl(editUrl);
                    Thread.Sleep(4000);

                    var div = driver.FindElementById("view_container");
                    if(div != null)
                    {
                        var editBtn = div.FindElement(By.XPath(".//div[@role='button'][@aria-haspopup='true'][@aria-disabled='false']"));
                        if(editBtn != null)
                        {
                            DivClick(driver, editBtn, 2000);
                        }
                    }

                    var dialog = driver.FindElementByXPath("//div[@role='dialog']");
                    if(dialog != null)
                    {
                        //DivClick(driver, dialog, 1000);

                        var opts = dialog.FindElements(By.XPath(".//div[@role='option']"));
                        if(opts != null && opts.Count > 0)
                        {
                            foreach (var item in opts)
                            {
                                if(item.Text.ToLower() == "english")
                                {
                                    DivClick(driver, item, 1000);
                                    
                                    var btns = dialog.FindElements(By.XPath(".//div[@role='button']"));
                                    if(btns != null && btns.Count > 0)
                                    {
                                        foreach (var btn in btns)
                                        {
                                            if(btn.Text.ToLower() == "ok")
                                            {
                                                DivClick(driver, btn, 1000);
                                                break;
                                            }
                                        }
                                       
                                    }
                                        

                                    if (UpdateTextEvent != null)
                                    {
                                        UpdateTextEvent(emailInfo + " - " + "Change language to English!");
                                    }
                                    break;
                                }
                            }
                        }
                    }

                    


                }
                
                return true;
            }
            catch (Exception ex)
            {
                if (UpdateTextEvent != null)
                {
                    UpdateTextEvent(emailInfo + " - " + string.Format("Error: {0}, Details: {1}", ex.Message, ex.StackTrace));
                }
                logger.ErrorFormat("Error: {0}, Details: {1}", ex.Message, ex.StackTrace);
            }

            return false;
        }

        //public void Run()
        //{
          
        //    for (int i = 0; i < actionList.Count; i++)
        //    {
        //        var gplusAction = actionList[i];        
                
        //        Plus1Action(gplusAction.MinDelay, gplusAction.MaxDelay);
        //    }
        //}

        private void GetMorePosts()
        {
            try
            {
                IJavaScriptExecutor js = driver as IJavaScriptExecutor;
                js.ExecuteScript("window.scrollBy(0,10000);", driver);
               

            }
            catch
            {
            }
        }

        private void HidePressPlus1Button(IWebElement element)
        {
            try
            {
                var parent = element.FindElement(By.XPath("../../../../.."));
                if (parent != null)
                {
                    IJavaScriptExecutor js = driver as IJavaScriptExecutor;
                    js.ExecuteScript("arguments[0].style='display: none;'", parent);
                }

            }
            catch
            {
            }
          
        }


#endregion  


        public bool ExecuteAction(SingleGPlusAction action)
        {
            if(action.Action == GPLUS_ACTIONS.PLUS_1_FEEDS)
            {
                return ExecutePlus1Action(action);
            }else if(action.Action == GPLUS_ACTIONS.JOIN_COMMUNITY)
            {
                //string groupLink = GmailDataController.Instance.GetSequenceGroup();
                //if(!string.IsNullOrEmpty(groupLink))
                //{
                //    return ExecuteJoinCommunity(action, groupLink);
                //}else
                   return ExecuteJoinCommunity(action);
            }
            else if (action.Action == GPLUS_ACTIONS.POST_IMAGE_HOME)
            {
                CloseAlert();
                Thread.Sleep(2000);
                driver.Navigate().GoToUrl("https://plus.google.com/");

                Thread.Sleep(3000);
               

                return PostImageOnHome(action, true);
            }
            else if (action.Action == GPLUS_ACTIONS.POST_IMAGE_COMMUNITY)
            {
                if (CommunitiesIds.Count == 0)
                {
                    GetCommunities();
                }
                //CommunitiesIds.Add("116645192393550705583");
                //string groupLink = GmailDataController.Instance.GetSequenceGroup();
                //if (!string.IsNullOrEmpty(groupLink))
                if (CommunitiesIds.Count > 0)
                {
                    NavigateToRandomCommunity();
                    //NavigateToCommunity(groupLink);
                    return PostImageOnHome(action);
                    //return PostImageOnCommunity(action);
                }
                else
                {
                    if (UpdateTextEvent != null)
                    {
                        UpdateTextEvent(emailInfo + " - is join in any communites! please join at leat one!");
                    }
                }
              
            }
            else if (action.Action == GPLUS_ACTIONS.POST_LINK_LIST_COMMUNITY_LIST)
            {
                //if (CommunitiesIds.Count == 0)
                //{
                //    GetCommunities();
                //}
                //CommunitiesIds.Add("116645192393550705583");
                string groupLink = GmailDataController.Instance.GetSequenceGroup();
                if (!string.IsNullOrEmpty(groupLink))
                {
                    //NavigateToRandomCommunity();
                  
                    NavigateToCommunity(groupLink);
                    return PostLinkOnHome(action);
                    //return PostImageOnCommunity(action);
                }
                else
                {
                    if (UpdateTextEvent != null)
                    {
                        UpdateTextEvent(emailInfo + " - is join in any communites! please join at leat one!");
                    }
                }

            }
            else if (action.Action == GPLUS_ACTIONS.COMBO_POST_LINK)
            {
                string groupLink = GmailDataController.Instance.GetSequenceGroup();
                if (!string.IsNullOrEmpty(groupLink))
                {
                    NavigateToCommunity(groupLink);

                    ExecuteJoinCommunity(action, groupLink);

                    return PostLinkOnHome(action);
                    //return PostImageOnCommunity(action);
                }
                else
                {
                    if (UpdateTextEvent != null)
                    {
                        UpdateTextEvent(emailInfo + " - is join in any communites! please join at leat one!");
                    }
                }

            }
            else if (action.Action == GPLUS_ACTIONS.COMBO_POST_IMAGE)
            {
                string groupLink = GmailDataController.Instance.GetSequenceGroup();
                if (!string.IsNullOrEmpty(groupLink))
                {
                    NavigateToCommunity(groupLink);

                    ExecuteJoinCommunity(action, groupLink);

                    return PostImageOnHome(action);
                    //return PostImageOnCommunity(action);
                }
                else
                {
                    if (UpdateTextEvent != null)
                    {
                        UpdateTextEvent(emailInfo + " - is join in any communites! please join at leat one!");
                    }
                }

            }
            //else if (action.Action == GPLUS_ACTIONS.POST_COMMENT_HOME)
            //{
            //    return PostCommentOnHome(action);
            //}
            else if (action.Action == GPLUS_ACTIONS.POST_COMMENT_COMMUNITY)
            {
                if (CommunitiesIds.Count == 0)
                {
                    GetCommunities();
                }

                if (CommunitiesIds.Count > 0)
                {
                    NavigateToRandomCommunity();

                    return PostCommentOnPost(action);                   
                }
                else
                {
                    if (UpdateTextEvent != null)
                    {
                        UpdateTextEvent(emailInfo + " - is join in any communites! please join at leat one!");
                    }
                }
            }
            else if (action.Action == GPLUS_ACTIONS.FOLLOW_MEMBER)
            {
                if(FollowedIds.Count == 0)
                {
                    GetFollowedIds();
                }
                if(CommunitiesIds.Count == 0)
                {
                    GetCommunities();
                }

                if(CommunitiesIds.Count > 0)
                {
                    NavigateToRandomCommunity();
                    return FollowMemberInCommunity(action);
                }else
                {
                    if (UpdateTextEvent != null)
                    {
                        UpdateTextEvent(emailInfo + " - is join in any communites! please join at leat one!");
                    }
                }
                              
               
            }

            return false;
        }


        private bool FollowMemberInCommunity(SingleGPlusAction action)
        {
            try
            {
                Thread.Sleep(2000);
                string url = driver.Url + "/members";
                driver.Navigate().GoToUrl(url);

                Thread.Sleep(3000);
                //document.querySelectorAll("div[role='list']>div")

                int lastSize = 0;
                do
                {
                    var divs = driver.FindElementsByXPath("//div[@role='list']/div");
                    bool found = false;
                    if (divs != null && divs.Count > 0)
                    {
                        if (lastSize == 0)
                            lastSize = divs.Count;
                        else if(lastSize == divs.Count)
                        {
                            break;
                        }
                        foreach (var item in divs)
                        {
                            string memberId = item.GetAttribute("data-memberid");
                            if (memberId != null)
                            {
                                if (!FollowedIds.Contains(memberId))
                                {
                                    found = true;
                                    FollowMember(memberId);
                                    break;
                                }
                            }
                        }
                    }

                    if(found)
                    {
                        break;
                    }
                    else
                    {
                        GetMorePosts();
                        Thread.Sleep(5000);
                    }
                } while (true);
                
                
                Random rand = new Random();
                int randSec = rand.Next(action.MinDelay, action.MaxDelay + 1);
                Thread.Sleep(randSec * 1000);
                

            }
            catch (Exception ex)
            {
                if (UpdateTextEvent != null)
                {
                    UpdateTextEvent(emailInfo + " - " + string.Format("Error: {0}, Details: {1}", ex.Message, ex.StackTrace));
                }
                logger.ErrorFormat("Error: {0}|Details: {1}", ex.Message, ex.StackTrace);
            }

            return false;
        }

        private bool PostImageOnHome(SingleGPlusAction action, bool sharePublic = false)
        {
            try
            {
                //Thread.Sleep(2000);
                //driver.Navigate().GoToUrl("https://plus.google.com/");

                //Thread.Sleep(3000);
                //document.querySelectorAll('div[role=button][aria-label="Share photos"]')[0].click();
                //type=file, aria-label="Upload photo"

                var uploadBtn = GetElement(driver, By.XPath("//div[@role='button'][@aria-label='Share photos']"), 10);
                if (uploadBtn != null)
                {
                    DivClick(driver, uploadBtn, 1500);
                    if (UpdateTextEvent != null)
                    {
                        UpdateTextEvent(emailInfo + " - Clicked upload image on home profile button");
                    }
                }

                var uploadFileInput = GetElement(driver, By.XPath("//input[@type='file'][@aria-label='Upload photo']"), 10);
                if (uploadFileInput != null)
                {
                    uploadFileInput.Clear();
                    string path = GmailDataController.Instance.GetRandomImage();
                    if (!string.IsNullOrEmpty(path))
                    {
                        uploadFileInput.SendKeys(path);
                        if (UpdateTextEvent != null)
                        {
                            UpdateTextEvent(emailInfo + " - Uploading image...");
                        }
                    }
                    else
                    {
                        if (UpdateTextEvent != null)
                        {
                            UpdateTextEvent(emailInfo + " - Random image failed");
                        }
                    }

                    Thread.Sleep(1000);
                }

                if (sharePublic)
                {
                    var span = driver.FindElementByXPath("//span[starts-with(@aria-label, 'Share with:')]");
                    if (span != null)
                    {
                        DivClick(driver, span, 1000);
                        var options = driver.FindElementsByXPath("//content[@role='option']");
                        if (options != null && options.Count > 0)
                        {
                            foreach (var opt in options)
                            {
                                string text = opt.Text.Trim().Replace("\r", "").Replace("\n", "");
                                if(text == "Public")
                                {
                                    DivClick(driver, opt, 1000);
                                    break;
                                }
                            }
                        }
                    }
                }


                Random rand = new Random();
                int randSec = rand.Next(action.MinDelay, action.MaxDelay + 1);

                var divs = driver.FindElementsByXPath("//div[@role='button']/content/span");
                if (divs != null && divs.Count > 0)
                {
                    foreach (var span in divs)
                    {
                        if (span.Text.ToLower() == "post")
                        {
                            var parent = span.FindElement(By.XPath("../.."));
                            if (parent != null)
                            {
                                do
                                {
                                    try
                                    {
                                        var disableAtt = parent.GetAttribute("aria-disabled");
                                        if(disableAtt == null)
                                        {
                                            break;
                                        }
                                        Thread.Sleep(1500);
                                    }
                                    catch {
                                        break;
                                    }
                                
                                } while (true);
                                
                                // check close dialog
                                // c-wiz[@role='dialog']
                                DivClick(driver, parent, 1500);

                                var dialog = driver.FindElementByXPath("//div[@role='dialog']");
                                if (dialog != null)
                                {
                                    var contents = dialog.FindElements(By.XPath(".//content/div/div/content"));
                                    if (contents != null && contents.Count > 0)
                                    {
                                        DivClick(driver, contents[0], 1500);
                                    }
                                }

                                // check if post dont click
                                int tryCount = 5;
                                do
                                {
                                    try
                                    {
                                        var cwizTag = driver.FindElementByXPath("//c-wiz[@role='dialog']");
                                        if (cwizTag != null)
                                        {
                                            DivClick(driver, parent, 1500);
                                            tryCount--;
                                            if (tryCount <= 0)
                                                break;
                                        }else
                                        {
                                            break;
                                        }
                                    }
                                    catch
                                    {
                                        break;
                                    }
                                    
                                } while (true);



                                if (UpdateTextEvent != null)
                                {
                                    UpdateTextEvent(emailInfo + " - posted image on home");
                                    UpdateTextEvent(emailInfo + " - " + "Wait time: " + randSec);
                                }
                                break;
                            }
                        }
                    }

                   
                    Thread.Sleep(randSec * 1000);
                }

            }
            catch (Exception ex)
            {
                if (UpdateTextEvent != null)
                {
                    UpdateTextEvent(emailInfo + " - " + string.Format("Error: {0}, Details: {1}", ex.Message, ex.StackTrace));
                }
                logger.ErrorFormat("Error: {0}|Details: {1}", ex.Message, ex.StackTrace);
            }

            return false;
        }


        private bool PostLinkOnHome(SingleGPlusAction action, bool sharePublic = false)
        {
            string postId = string.Empty;
            string tempid = string.Empty;
            try
            {
                Random rand = new Random();
                Thread.Sleep(1000);
                int tryBtn = 5;
                do
                {
                    try
                    {
                        var homePostBtn = driver.FindElementByXPath("//span[@role='button']");
                        if (homePostBtn != null)
                        {
                            DivClick(driver, homePostBtn, 1500);
                            if (UpdateTextEvent != null)
                            {
                                UpdateTextEvent(emailInfo + " - click home post button");
                            }
                        }

                       
                        var textAreaInput = GetElement(driver, By.XPath("//textarea[@role='textbox']"), 10);
                        if (textAreaInput != null)
                        {
                            textAreaInput.Click();
                            Thread.Sleep(500);
                            textAreaInput.Clear();
                            string text = GmailDataController.Instance.GetSequenceLink();
                            textAreaInput.SendKeys(text.Trim());
                            Thread.Sleep(1500);
                            textAreaInput.SendKeys(Keys.Return);
                            Thread.Sleep(5000);
                            int tryTest = 3;
                            do
                            {
                                var ahref = driver.FindElementByXPath($"//a[@href='{text.ToString()}']");
                                if (ahref != null)
                                {
                                    textAreaInput.Clear();
                                    Thread.Sleep(500);
                                    break;
                                }
                                else
                                {
                                    tryTest--;
                                }
                                if (tryTest <= 0)
                                    break;
                            } while (true);
                        }
                        break;
                    }
                    catch 
                    {
                        tryBtn--;
                        if (tryBtn < 0)
                            break;
                        Thread.Sleep(1500);
                    }
                    
                    
                } while (true);
             
                
              



                int randSec = rand.Next(action.MinDelay, action.MaxDelay + 1);

                var divs = driver.FindElementsByXPath("//div[@role='button']/content/span");
                if (divs != null && divs.Count > 0)
                {
                    foreach (var span in divs)
                    {
                        if (span.Text.ToLower() == "post")
                        {
                            var parent = span.FindElement(By.XPath("../.."));
                            if (parent != null)
                            {
                                do
                                {
                                    try
                                    {
                                        var disableAtt = parent.GetAttribute("aria-disabled");
                                        if (disableAtt == null)
                                        {
                                            break;
                                        }
                                        Thread.Sleep(1500);
                                    }
                                    catch
                                    {
                                        break;
                                    }

                                } while (true);
                                parent.Click();
                                Thread.Sleep(3000);
                                var dialog = driver.FindElementByXPath("//div[@role='dialog']");
                                if (dialog != null)
                                {
                                    var contents = dialog.FindElements(By.XPath(".//content/div/div/content"));
                                    if (contents != null && contents.Count > 0)
                                    {
                                        DivClick(driver, contents[0], 1500);
                                    }
                                }

                                // check if post dont click
                                int tryCount = 5;
                                do
                                {
                                    try
                                    {
                                        var cwizTag = driver.FindElementByXPath("//c-wiz[@role='dialog']");
                                        if (cwizTag != null)
                                        {
                                            DivClick(driver, parent, 1500);
                                            tryCount--;
                                            if (tryCount <= 0)
                                                break;
                                        }
                                        else
                                        {
                                            break;
                                        }
                                    }
                                    catch
                                    {
                                        break;
                                    }

                                } while (true);


                                if (UpdateTextEvent != null)
                                {
                                    UpdateTextEvent(emailInfo + " - posted comment");
                                    UpdateTextEvent(emailInfo + " - " + "Wait time: " + randSec);
                                }
                                //LiteDBController.Instance.InsertPostId(action.User.Email, postId);
                                break;
                            }
                        }
                    }


                    Thread.Sleep(randSec * 1000);
                    return true;
                }

            }
            catch (Exception ex)
            {
                if (UpdateTextEvent != null)
                {
                    UpdateTextEvent(emailInfo + " - " + string.Format("Error: {0}, Details: {1}", ex.Message, ex.StackTrace));
                }
                logger.ErrorFormat("Error: {0}|Details: {1}", ex.Message, ex.StackTrace);
            }

            return false;
        }


        private bool PostCommentOnHome(SingleGPlusAction action)
        {
            try
            {
                Thread.Sleep(2000);
                driver.Navigate().GoToUrl("https://plus.google.com/");

                Thread.Sleep(3000);
                //document.querySelectorAll("span[role='button']")[0].click();
                //document.querySelectorAll("textarea[role='textbox']")[0]

                var homePostBtn = GetElement(driver, By.XPath("//span[@role='button']"), 10);
                if (homePostBtn != null)
                {
                    DivClick(driver, homePostBtn, 1500);
                    if (UpdateTextEvent != null)
                    {
                        UpdateTextEvent(emailInfo + " - click home post button");
                    }
                }

                var textAreaInput = GetElement(driver, By.XPath("//textarea[@role='textbox']"), 10);
                if (textAreaInput != null)
                {
                    textAreaInput.Click();
                    Thread.Sleep(500);
                    textAreaInput.Clear();
                    textAreaInput.SendKeys("This is awesome long very long post ever and every!");
                    Thread.Sleep(1500);
                }


                Random rand = new Random();
                int randSec = rand.Next(action.MinDelay, action.MaxDelay + 1);

                var divs = driver.FindElementsByXPath("//div[@role='button']/content/span");
                if (divs != null && divs.Count > 0)
                {
                    foreach (var span in divs)
                    {
                        if (span.Text.ToLower() == "post")
                        {
                            var parent = span.FindElement(By.XPath("../.."));
                            if (parent != null)
                            {
                                do
                                {
                                    try
                                    {
                                        var disableAtt = parent.GetAttribute("aria-disabled");
                                        if (disableAtt == null)
                                        {
                                            break;
                                        }
                                        Thread.Sleep(1500);
                                    }
                                    catch
                                    {
                                        break;
                                    }

                                } while (true);
                                parent.Click();
                                if (UpdateTextEvent != null)
                                {
                                    UpdateTextEvent(emailInfo + " - posted comment on home");
                                    UpdateTextEvent(emailInfo + " - " + "Wait time: " + randSec);
                                }
                                break;
                            }
                        }
                    }


                    Thread.Sleep(randSec * 1000);
                }

            }
            catch (Exception ex)
            {
                if (UpdateTextEvent != null)
                {
                    UpdateTextEvent(emailInfo + " - " + string.Format("Error: {0}, Details: {1}", ex.Message, ex.StackTrace));
                }
                logger.ErrorFormat("Error: {0}|Details: {1}", ex.Message, ex.StackTrace);
            }

            return false;
        }

        private bool PostCommentOnPost(SingleGPlusAction action)
        {
            string postId = string.Empty;
            string tempid = string.Empty;
            try
            {
                
                Thread.Sleep(1000);
                bool found = false;
                int lastSize = 0;
                do
                {
                    
                    var commentBtns = driver.FindElementsByXPath("//div[@role='button'][@aria-label='Comment'][@aria-disabled='false']");
                    if(commentBtns != null && commentBtns.Count > 0)
                    {
                        if (lastSize == 0)
                            lastSize = commentBtns.Count;
                        else if (lastSize == commentBtns.Count)
                            break;
                        foreach (var btn in commentBtns)
                        {
                            var divParent = btn.FindElement(By.XPath(".."));
                            if (divParent != null)
                            {
                                string jsData = divParent.GetAttribute("jsdata");
                                if (!string.IsNullOrEmpty(jsData))
                                {
                                    string[] temp = jsData.Split(';');
                                    if (temp.Length >= 3)
                                    {
                                        string id = temp[1];
                                        if(id != null )
                                        {
                                            if(!LiteDBController.Instance.ExistsPostId(action.User.Email, id))
                                            {
                                                postId = id;
                                                found = true;
                                                DivClick(driver, btn, 1500);
                                                if (UpdateTextEvent != null)
                                                {
                                                    UpdateTextEvent(emailInfo + " - button comment clicked");
                                                }
                                                break;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    if(found)
                    {
                        break;
                    }else
                    {
                        GetMorePosts();
                    }
             
                } while (true);
             
                if (!found)
                    return false;

                Random rand = new Random();
                var textAreaInput = GetElement(driver, By.XPath("//textarea[@role='textbox']"), 10);
                if (textAreaInput != null)
                {
                    textAreaInput.Click();
                    Thread.Sleep(500);
                    textAreaInput.Clear();
                    string text = GmailDataController.Instance.GetRandomContent();
                    textAreaInput.SendKeys(text.Trim());
                    Thread.Sleep(1500);
                }


                
                int randSec = rand.Next(action.MinDelay, action.MaxDelay + 1);

                var divs = driver.FindElementsByXPath("//div[@role='button']/content/span");
                if (divs != null && divs.Count > 0)
                {
                    foreach (var span in divs)
                    {
                        if (span.Text.ToLower() == "post")
                        {
                            var parent = span.FindElement(By.XPath("../.."));
                            if (parent != null)
                            {
                                do
                                {
                                    try
                                    {
                                        var disableAtt = parent.GetAttribute("aria-disabled");
                                        if (disableAtt == null)
                                        {
                                            break;
                                        }
                                        Thread.Sleep(1500);
                                    }
                                    catch
                                    {
                                        break;
                                    }

                                } while (true);
                                parent.Click();
                                if (UpdateTextEvent != null)
                                {
                                    UpdateTextEvent(emailInfo + " - posted comment");
                                    UpdateTextEvent(emailInfo + " - " + "Wait time: " + randSec);
                                }
                                LiteDBController.Instance.InsertPostId(action.User.Email, postId);
                                break;
                            }
                        }
                    }


                    Thread.Sleep(randSec * 1000);
                    return true;
                }

            }
            catch (Exception ex)
            {
                if (UpdateTextEvent != null)
                {
                    UpdateTextEvent(emailInfo + " - " + string.Format("Error: {0}, Details: {1}", ex.Message, ex.StackTrace));
                }
                logger.ErrorFormat("Error: {0}|Details: {1}", ex.Message, ex.StackTrace);
            }

            return false;
        }


        private bool PostImageOnCommunity(SingleGPlusAction action)
        {
            try
            {
                Thread.Sleep(2000);
                //driver.Navigate().GoToUrl("https://plus.google.com/u/0/communities/116645192393550705583");
                //Thread.Sleep(5000);
                //document.querySelectorAll("div[role='button'][aria-label='Comment'][aria-disabled='false']")[0].click();

                var commentBtn = GetElement(driver, By.XPath("//div[@role='button'][@aria-label='Comment'][@aria-disabled='false']"), 10);
                if (commentBtn != null)
                {
                    DivClick(driver, commentBtn, 1500);
                    if (UpdateTextEvent != null)
                    {
                        UpdateTextEvent(emailInfo + " - button comment clicked");
                    }
                }

                var uploadBtn = GetElement(driver, By.XPath("//div[@role='button'][@aria-label='Add photos']"), 10);
                if (uploadBtn != null)
                {
                    DivClick(driver, uploadBtn, 1500);
                    if (UpdateTextEvent != null)
                    {
                        UpdateTextEvent(emailInfo + " - Clicked upload image on comment button");
                    }
                }

                var uploadFileInput = GetElement(driver, By.XPath("//input[@type='file'][@aria-label='Upload photo']"), 10);
                if (uploadFileInput != null)
                {
                    uploadFileInput.Clear();
                    string path = GmailDataController.Instance.GetRandomImage();
                    if(!string.IsNullOrEmpty(path))
                    {
                        uploadFileInput.SendKeys(path);
                        if (UpdateTextEvent != null)
                        {
                            UpdateTextEvent(emailInfo + " - Uploading image...");
                        }
                    }else
                    {
                        if (UpdateTextEvent != null)
                        {
                            UpdateTextEvent(emailInfo + " - Random image failed");
                        }
                    }
                   

                    Thread.Sleep(1000);
                }


                Random rand = new Random();
                int randSec = rand.Next(action.MinDelay, action.MaxDelay + 1);

                var divs = driver.FindElementsByXPath("//div[@role='button']/content/span");
                if (divs != null && divs.Count > 0)
                {
                    foreach (var span in divs)
                    {
                        if (span.Text.ToLower() == "post")
                        {
                            var parent = span.FindElement(By.XPath("../.."));
                            if (parent != null)
                            {
                                do
                                {
                                    try
                                    {
                                        var disableAtt = parent.GetAttribute("aria-disabled");
                                        if (disableAtt == null)
                                        {
                                            break;
                                        }
                                        Thread.Sleep(1500);
                                    }
                                    catch
                                    {
                                        break;
                                    }

                                } while (true);
                                parent.Click();
                                if (UpdateTextEvent != null)
                                {
                                    UpdateTextEvent(emailInfo + " - posted image on comment");
                                    UpdateTextEvent(emailInfo + " - " + "Wait time: " + randSec);
                                }
                                break;
                            }
                        }
                    }


                    Thread.Sleep(randSec * 1000);
                }

            }
            catch (Exception ex)
            {
                if (UpdateTextEvent != null)
                {
                    UpdateTextEvent(emailInfo + " - " + string.Format("Error: {0}, Details: {1}", ex.Message, ex.StackTrace));
                }
                logger.ErrorFormat("Error: {0}|Details: {1}", ex.Message, ex.StackTrace);
            }

            return false;
        }

        private bool ExecuteJoinCommunity(SingleGPlusAction action, string groupId)
        {
            try
            {
                CloseAlert();
                Thread.Sleep(2000);
                driver.Navigate().GoToUrl("https://plus.google.com/communities/"+ groupId);

                Thread.Sleep(3000);
                var divs = driver.FindElementsByXPath("//div[@role='button']/content/span");
                if (divs != null && divs.Count > 0)
                {
                    if (UpdateTextEvent != null)
                    {
                        UpdateTextEvent(emailInfo + " - " + "Communities found: " + divs.Count);
                    }

                    Random rand = new Random();

                    int randSec = rand.Next(action.MinDelay, action.MaxDelay + 1);
                    foreach (var span in divs)
                    {
                        if (span.Text.ToLower() == "join")
                        {
                            var parent = span.FindElement(By.XPath("../.."));
                            if (parent != null)
                            {
                                parent.Click();
                                if (UpdateTextEvent != null)
                                {
                                    UpdateTextEvent(emailInfo + " - joined 1 community, remains: " + action.Remains);
                                    UpdateTextEvent(emailInfo + " - " + "Wait time: " + randSec);
                                }
                                break;
                            }
                        }
                    }

                    Thread.Sleep(3 * 1000);

                    //if (divs.Count == 1)
                    //{
                    //    GetMorePosts();
                    //    Thread.Sleep(10000);
                    //}
                }
                else
                {
                    if (UpdateTextEvent != null)
                    {
                        UpdateTextEvent(emailInfo + " - " + "Cannot found any post at url: " + driver.Url);
                    }
                }


            }
            catch (Exception ex)
            {
                if (UpdateTextEvent != null)
                {
                    UpdateTextEvent(emailInfo + " - " + string.Format("Error: {0}, Details: {1}", ex.Message, ex.StackTrace));
                }
                logger.ErrorFormat("Error: {0}|Details: {1}", ex.Message, ex.StackTrace);
            }

            return false;
        }

        private bool ExecuteJoinCommunity(SingleGPlusAction action)
        {
            try
            {
                CloseAlert();
                Thread.Sleep(2000);
                driver.Navigate().GoToUrl("https://plus.google.com/communities/recommended");

                Thread.Sleep(3000);
                var divs = driver.FindElementsByXPath("//div[@role='button']/content/span");
                if (divs != null && divs.Count > 0)
                {
                    if (UpdateTextEvent != null)
                    {
                        UpdateTextEvent(emailInfo + " - " + "Communities found: " + divs.Count);
                    }

                    Random rand = new Random();
                   
                    int randSec = rand.Next(action.MinDelay, action.MaxDelay + 1);
                    foreach (var span in divs)
                    {
                        if(span.Text.ToLower() == "join")
                        {
                            var parent = span.FindElement(By.XPath("../.."));
                            if(parent != null)
                            {
                                parent.Click();
                                if (UpdateTextEvent != null)
                                {
                                    UpdateTextEvent(emailInfo + " - joined 1 community, remains: "+action.Remains);
                                    UpdateTextEvent(emailInfo + " - " + "Wait time: " + randSec);
                                }
                                break;
                            }
                        }
                    }
                   
                    Thread.Sleep(randSec * 1000);

                    //if (divs.Count == 1)
                    //{
                    //    GetMorePosts();
                    //    Thread.Sleep(10000);
                    //}
                }
                else
                {
                    if (UpdateTextEvent != null)
                    {
                        UpdateTextEvent(emailInfo + " - " + "Cannot found any post at url: " + driver.Url);
                    }
                }


            }
            catch (Exception ex)
            {
                if (UpdateTextEvent != null)
                {
                    UpdateTextEvent(emailInfo + " - " + string.Format("Error: {0}, Details: {1}", ex.Message, ex.StackTrace));
                }
                logger.ErrorFormat("Error: {0}|Details: {1}", ex.Message, ex.StackTrace);
            }

            return false;
        }

        private bool ExecutePlus1Action(SingleGPlusAction action)
        {
            try
            {
                CloseAlert();
                Thread.Sleep(2000);
                driver.Navigate().GoToUrl("https://plus.google.com/");

                Thread.Sleep(3000);
              

                var pressedDiv = driver.FindElementsByXPath("//div[@role='button'][@aria-pressed='true']");
                if (pressedDiv != null && pressedDiv.Count > 0)
                {
                    foreach (var item in pressedDiv)
                    {
                        HidePressPlus1Button(item);
                        Thread.Sleep(2000);
                    }
                }

                
                var divs = driver.FindElementsByXPath("//div[@role='button'][@aria-pressed='false']");
                if (divs != null && divs.Count > 0)
                {
                    if (UpdateTextEvent != null)
                    {
                        UpdateTextEvent(emailInfo + " - " + "Post found: " + divs.Count);
                    }

                    Random rand = new Random();
                    divs[0].Click();
                    int randSec = rand.Next(action.MinDelay, action.MaxDelay + 1);
                    if (UpdateTextEvent != null)
                    {
                        UpdateTextEvent(emailInfo + " - +1 done, remains: "+action.Remains);
                        UpdateTextEvent(emailInfo + " - " + "Wait time: " + randSec);
                    }
                    Thread.Sleep(randSec * 1000);
                    
                    if(divs.Count == 1)
                    {
                        GetMorePosts();
                        Thread.Sleep(10000);
                    }                    
                }
                else
                {
                    if (UpdateTextEvent != null)
                    {
                        UpdateTextEvent(emailInfo + " - " + "Cannot found any post at url: " + driver.Url);
                    }
                }
            

            }
            catch (Exception ex)
            {
                if (UpdateTextEvent != null)
                {
                    UpdateTextEvent(emailInfo + " - " + string.Format("Error: {0}, Details: {1}", ex.Message, ex.StackTrace));
                }
                logger.ErrorFormat("Error: {0}|Details: {1}", ex.Message, ex.StackTrace);
            }

            return false;
        }

        private bool Plus1Action(int min, int max)
        {
            try
            {
                Thread.Sleep(2000);
                driver.Navigate().GoToUrl("https://plus.google.com/");

                Thread.Sleep(3000);

                CloseAlert();
                var pressedDiv = driver.FindElementsByXPath("//div[@role='button'][@aria-pressed='true']");
                if(pressedDiv != null && pressedDiv.Count > 0)
                {
                    foreach (var item in pressedDiv)
                    {
                        HidePressPlus1Button(item);
                        Thread.Sleep(2000);
                    }
                }

                do
                {
                    var divs = driver.FindElementsByXPath("//div[@role='button'][@aria-pressed='false']");
                    if (divs != null && divs.Count > 0)
                    {
                        if (UpdateTextEvent != null)
                        {
                            UpdateTextEvent(emailInfo + " - " + "Post found: " + divs.Count);
                        }

                        Random rand = new Random();
                        foreach (var item in divs)
                        {
                            try
                            {
                                item.Click();
                                int randSec = rand.Next(min, max + 1);
                                if (UpdateTextEvent != null)
                                {
                                    UpdateTextEvent(emailInfo + " - " + "Wait time: " + randSec);
                                }
                                Thread.Sleep(randSec * 1000);
                            }
                            catch (Exception except)
                            {
                                if (UpdateTextEvent != null)
                                {
                                    UpdateTextEvent(emailInfo + " - " + "Click plus error: " + except.Message);
                                }
                            }


                        }

                        pressedDiv = driver.FindElementsByXPath("//div[@role='button'][@aria-pressed='true']");
                        if (pressedDiv != null && pressedDiv.Count > 0)
                        {
                            foreach (var item in pressedDiv)
                            {
                                HidePressPlus1Button(item);
                                Thread.Sleep(2000);
                            }
                        }

                        GetMorePosts();
                        Thread.Sleep(10000);
                    }
                    else
                    {
                        if (UpdateTextEvent != null)
                        {
                            UpdateTextEvent(emailInfo + " - " + "Cannot found any post at url: " + driver.Url);
                        }
                        break;
                    }
                } while (true);
                
            }
            catch (Exception ex)
            {
                if (UpdateTextEvent != null)
                {
                    UpdateTextEvent(emailInfo + " - " + string.Format("Error: {0}, Details: {1}", ex.Message, ex.StackTrace));
                }
                logger.ErrorFormat("Error: {0}|Details: {1}", ex.Message, ex.StackTrace);
            }

            return false;
        }


        private string CheckLoginFailed()
        {
            try
            {
                Thread.Sleep(2000);
                var divs = driver.FindElementsByXPath("//div[@aria-live='polite']");
                if(divs != null && divs.Count > 0)
                {
                    foreach (var item in divs)
                    {
                        var text = item.Text.Trim();
                        if(text.Length > 0)
                        {
                            return text;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                if (UpdateTextEvent != null)
                {
                    UpdateTextEvent(emailInfo + " - " + string.Format("Error: {0}, Details: {1}", ex.Message, ex.StackTrace));
                }
                logger.ErrorFormat("Error: {0}|Details: {1}", ex.Message, ex.StackTrace);
            }

            return null;
        }



        private void Init()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddExcludedArguments("excludeSwitches", "enable-automation");
            options.AddArgument("--lang=en-au");
            var driverService = ChromeDriverService.CreateDefaultService();
            //driverService.HideCommandPromptWindow = true;
            driver = new ChromeDriver(driverService, options);
            //IsStarted = true;
        }

        public void Dispose()
        {
            if(driver != null)
            {
                driver.Quit();
            }

            IsStarted = false;
        }



        #region private methods


        private IWebElement GetElement(IWebDriver driver, By by, int sleep = 5)
        {
            if (sleep > 0)
            {
                //driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(15));
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(sleep));
                //return wait.Until(drv => drv.FindElement(by));
                return wait.Until(ExpectedConditions.ElementExists(by));
            }
            return driver.FindElement(by);

        }

        private IWebElement GetElementWaitForClickable(IWebDriver driver, By by, int sleep = 5)
        {
            if (sleep > 0)
            {
                driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(15));
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(sleep));
                //return wait.Until(drv => drv.FindElement(by));
                return wait.Until(ExpectedConditions.ElementToBeClickable(by));
            }
            return driver.FindElement(by);

        }

        private void DivClick(IWebDriver driver, IWebElement div, int sleep = 0)
        {
            try
            {
                Actions actions = new Actions(driver);
                actions.MoveToElement(div);
                actions.Click();
                actions.Build().Perform();

                if (sleep > 0)
                    Thread.Sleep(sleep);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);
            }

        }


        private void GetCommunities()
        {
            try
            {
                Thread.Sleep(2000);
                driver.Navigate().GoToUrl("https://plus.google.com/communities/member");
                Thread.Sleep(2000);

                //document.querySelectorAll("div[role='listitem'][data-link^='./communities/']");
                var divs = driver.FindElementsByXPath("//div[@role='listitem'][starts-with(@data-link, './communities/')]");
                if (divs != null && divs.Count > 0)
                {
                    foreach (var div in divs)
                    {
                        var dataLink = div.GetAttribute("data-link");
                        if(dataLink != null)
                        {
                            CommunitiesIds.Add(dataLink.Replace("./communities/", ""));
                        }
                    }
                    if (UpdateTextEvent != null)
                    {
                        UpdateTextEvent(emailInfo + " - Clicked upload image on home profile button");
                    }

                    CommunitiesIds = CommunitiesIds.Distinct().ToList();
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);
            }
        }


        private void GetFollowedIds()
        {
            try
            {
                Thread.Sleep(2000);
                driver.Navigate().GoToUrl("https://plus.google.com/circles");
                Thread.Sleep(2000);

                //document.querySelectorAll("div[role='listitem'][data-link^='./communities/']");
                var divs = driver.FindElementsByXPath("//div[@role='list']/div");
                if (divs != null && divs.Count > 0)
                {
                    foreach (var div in divs)
                    {
                        var dataLink = div.GetAttribute("data-link");
                        if (dataLink != null)
                        {
                            FollowedIds.Add(dataLink.Replace("./", ""));
                        }
                    }
                    if (UpdateTextEvent != null)
                    {
                        UpdateTextEvent(emailInfo + " - Clicked upload image on home profile button");
                    }
                    FollowedIds = FollowedIds.Distinct().ToList();
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);
            }
            
        }

        private void CloseAlert()
        {
            try
            {
                var alert = driver.SwitchTo().Alert();
                alert.Accept();
            }
            catch
            {
            }
            Thread.Sleep(1000);
        }

        private void NavigateToRandomCommunity()
        {
            if (CommunitiesIds.Count > 0)
            {
                CloseAlert();
                Random rand = new Random();
                string link = CommunitiesIds[rand.Next(0, CommunitiesIds.Count)];

                driver.Navigate().GoToUrl("https://plus.google.com/communities/" + link);
                Thread.Sleep(3000);
            }
        }

        private void NavigateToCommunity(string groupId)
        {
            if (CommunitiesIds.Count > 0)
            {
                CloseAlert();

                driver.Navigate().GoToUrl("https://plus.google.com/communities/" + groupId);
                Thread.Sleep(3000);
            }
        }

        private bool FollowMember(string memberId)
        {
            try
            {
                Thread.Sleep(1000);
                string memberUrl = "https://plus.google.com/" + memberId;
                driver.Navigate().GoToUrl(memberUrl);
                Thread.Sleep(2000);

                //document.querySelectorAll("div[role='button'][aria-label=Follow]")[0].click();
                var followBtn = GetElement(driver, By.XPath("//div[@role='button'][@aria-label='Follow']"), 10);
                if (followBtn != null)
                {
                    followBtn.Click();
                    if (UpdateTextEvent != null)
                    {
                        UpdateTextEvent(emailInfo + " - followed member: "+memberUrl);
                    }
                    FollowedIds.Add(memberId);
                }
                return true;
            }
            catch (Exception ex)
            {
                if (UpdateTextEvent != null)
                {
                    UpdateTextEvent(emailInfo + " - Follow member error: " + ex.Message);
                }
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);
            }

            return false;
        }


        private void CheckPointFillRecovery(Gmail gmail)
        {
            try
            {
                int tryCount = 10;
                do
                {
                    if (driver.Url.Contains("flowEntry=ServiceLogin"))
                    {
                        var divs = driver.FindElementByXPath("//div[@role='button'][@data-challengetype='12']");
                        if(divs != null)
                        {
                            DivClick(driver, divs, 1500);

                            Thread.Sleep(2000);

                            var nameInput = driver.FindElement(By.Name("knowledgePreregisteredEmailResponse"));
                            nameInput.Clear();
                            nameInput.SendKeys(gmail.RecoveryEmail);
                            Thread.Sleep(1300);

                            var submitBtn = driver.FindElement(By.Id("next"));
                            submitBtn.Click();
                            Thread.Sleep(2500);
                            break;
                        }
                        //if (divs != null && divs.Count > 0)
                        //{
                        //    foreach (var div in divs)
                        //    {
                        //        var childs = div.FindElements(By.XPath(".//div"));

                        //        if(childs != null && childs.Count > 0)
                        //        {
                        //            bool found = false;
                        //            foreach (var ch in childs)
                        //            {
                        //                if(ch != null && ch.Text.ToLower() == "confirm your recovery email")
                        //                {
                        //                    found = true;
                        //                    break;
                        //                }
                        //            }
                        //            if(found)
                        //            {
                        //                DivClick(driver, div, 1500);

                        //                Thread.Sleep(2000);

                        //                var nameInput = driver.FindElement(By.Name("knowledgePreregisteredEmailResponse"));
                        //                nameInput.Clear();
                        //                nameInput.SendKeys(gmail.RecoveryEmail);
                        //                Thread.Sleep(1300);

                        //                var submitBtn = driver.FindElement(By.Id("next"));
                        //                submitBtn.Click();
                        //                Thread.Sleep(2500);
                        //                break;
                        //            }

                        //        }
                        //    }
                        //}
                        tryCount--;
                        if (tryCount <= 0)
                            break;
                        Thread.Sleep(1500);
                    }
                    else
                        break;
                } while (true);


                // //ssl.gstatic.com/accounts/marc/rescueemail.png
              
                //var imgTag = GetElement(driver, By.XPath("//img[contains(@src, 'ssl.gstatic.com/accounts/marc/rescueemail.png')]"), 10);
                //var btn = imgTag.FindElement(By.XPath(".."));
                //btn.Click();
               
            }
            catch (Exception ex)
            {
                logger.ErrorFormat("ID: " + gmail.Email + ", {0}, stacktrace: {1}", ex.Message, ex.StackTrace);
            }


        }

        private void CheckPointAddTrustedDevices()
        {
            try
            {
                //
                string url = "https://security.google.com/settings/u/2/security/activity";
                driver.Navigate().GoToUrl(url);
                Thread.Sleep(2000);

                // Yes, that was me

                // get temp
                var by = By.XPath(".//div[contains(.,'Yes, that was me')]");
                var divTemp = GetElement(driver, by, 10);
                var mainDiv = GetElement(driver, By.Id("view_container"));
                var divs = mainDiv.FindElements(by);
                foreach (var div in divs)
                {
                    if (div.Text == "YES, THAT WAS ME")
                    {
                        DivClick(driver, div, 1500);
                    }

                }

                // ok
                var byOk = By.XPath(".//div[contains(.,'OK')]");
                var temp = GetElement(driver, byOk, 1500);
                var divOks = mainDiv.FindElements(byOk);
                foreach (var div in divOks)
                {
                    if (div.Text == "OK")
                    {
                        DivClick(driver, div, 1500);
                    }
                }
            }
            catch (Exception ex)
            {
                logger.ErrorFormat("Error: {0}, stacktrace: {1}", ex.Message, ex.StackTrace);
            }


        }

        private void CreateProfile()
        {
            try
            {
                int tryCount = 5;
                do
                {
                    string url = "https://plus.google.com/communities/yours";
                    driver.Navigate().GoToUrl(url);
                    Thread.Sleep(4000);
                    if (driver.Url.Contains("https://plus.google.com/up/accounts/upgrade/"))
                    {
                        // choose gender
                        Random rand = new Random();
                        int randGenderIndex = rand.Next(0, 3);
                        int randMonth = rand.Next(1, 13);
                        int randDay = rand.Next(1, 31);
                        if (randMonth == 2 && randDay > 28)
                            randDay = 28;

                        int tryFailed = 10;
                        Failed:
                        var genderDivs = driver.FindElementsByXPath("//div[@role='option'][@aria-selected='true']");
                        if(genderDivs != null && genderDivs.Count > 0)
                        {
                            foreach (var child in genderDivs)
                            {
                                try
                                {
                                    string val = child.GetAttribute("aria-label");
                                    int dayParsed = 0;
                                    int.TryParse(val, out dayParsed);
                                    if (!string.IsNullOrEmpty(val))
                                    {
                                        if (val == "Male" || val == "Female" || val == "Rather not say" || val == "Custom")
                                        {
                                            DivClick(driver, child, 1500);
                                            var parentNode = child.FindElement(By.XPath("../../.."));
                                            if (parentNode != null)
                                            {
                                                //var childDiv = parentNode.FindElements(
                                                //    By.XPath(".//div[@role='presentation'][@aria-hidden='true']"));
                                                var childDiv = parentNode.FindElements(
                                                    By.XPath("./*"));
                                                var opts = childDiv[1].FindElements(By.XPath(".//div[@role='option']"));
                                                if (opts != null && opts.Count > 2)
                                                {
                                                    DivClick(driver, opts[randGenderIndex], 2000);
                                                }
                                            }
                                            //break;                                        
                                        }
                                        else if (val == "January" || val == "February" || val == "March"
                                           || val == "April" || val == "May" || val == "June" || val == "July"
                                           || val == "August" || val == "September" || val == "October"
                                           || val == "December" || val == "Birth Month")
                                        {
                                            DivClick(driver, child, 1500);
                                            var parentNode = child.FindElement(By.XPath("../../.."));
                                            if (parentNode != null)
                                            {
                                                var childDiv = parentNode.FindElements(
                                                    By.XPath("./*"));
                                                var opts = childDiv[1].FindElements(By.XPath(".//div[@role='option']"));
                                                if (opts != null && opts.Count > 11)
                                                {
                                                    DivClick(driver, opts[randMonth], 2000);
                                                }
                                            }
                                        }
                                        else if ((dayParsed > 0 && dayParsed <= 31) || val == "Birth Day")
                                        {
                                            DivClick(driver, child, 1500);
                                            var parentNode = child.FindElement(By.XPath("../../.."));
                                            if (parentNode != null)
                                            {
                                                var childDiv = parentNode.FindElements(
                                                    By.XPath("./*"));
                                                var opts = childDiv[1].FindElements(By.XPath(".//div[@role='option']"));
                                                if (opts != null && opts.Count >= 30)
                                                {
                                                    DivClick(driver, opts[randDay], 2000);
                                                }
                                            }
                                        }
                                    }
                                }
                                catch
                                {
                                    tryFailed--;
                                    if(tryFailed > 0)
                                        goto Failed;
                                }
                                
                            }
                        }
                        

                        var div = driver.FindElementByXPath("//div[@role='button'][@aria-label='Create Profile']");
                        if(div!= null)
                        {
                            DivClick(driver, div, 2000);

                            var saveDiv = driver.FindElementByXPath("//div[@role='button'][@aria-label='Save']");
                            if(saveDiv != null)
                            {
                                DivClick(driver, saveDiv, 2000);
                                break;
                            }
                                
                            
                        }
                    }else
                    {
                        break;
                    }
                    if (tryCount <= 0)
                        break;
                } while (true);
            }
            catch (Exception ex)
            {
                logger.ErrorFormat("Error: {0}, stacktrace: {1}", ex.Message, ex.StackTrace);
            }


        }

        #endregion
    }
}

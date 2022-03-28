using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.IO;
using Tweetinvi;
using Facebook;
using System.Net;


namespace ICT365_Assignment2
{
    public partial class Main : System.Web.UI.Page
    {
        //twitter api keys
        static string TwtApiKey = "KEY REMOVED FOR SECURITY";
        static string TwtApiSecret = "KEY REMOVED FOR SECURITY";
        static string TwtAccToken = "KEY REMOVED FOR SECURITY";
        static string TwtTokenSecret = "KEY REMOVED FOR SECURITY";
        static TwitterClient UserClient = new TwitterClient(TwtApiKey, TwtApiSecret, TwtAccToken, TwtTokenSecret);          //twitter client object using tweetinvi

        //facebook api keys
        static string FbAppID = "KEY REMOVED FOR SECURITY";
        static string FbAppSecret = "KEY REMOVED FOR SECURITY"; 
        static string scope = "publish_stream,manage_pages";

        protected void Page_Load(object sender, EventArgs e)
        {
            //on page load, load images into imagebuttons 
            addColBtn.ImageUrl = "~/Content/Images/addusericon.png";
            tweetBtn.ImageUrl = "~/Content/Images/twiticon.png";
            facebookBtn.ImageUrl = "~/Content/Images/fbicon.png";
        }

        protected void saveButton_Click(object sender, EventArgs e)
        {
            //called when save button on add collab form is clicked
            //input flags
            bool nameFlag = false;      
            bool typeFlag = false;
            bool startTimeFlag = false;
            bool endTimeFlag = false;
            bool picFlag = false;
            bool ptFlag = false; 

            //empty variables for initialization
            string name = "";
            string type = "";
            string time = "";
            string pic = "";
            string startTime = "";
            string endTime = "";
            double lat = 0;
            double lon = 0;

            //clears return message text from previous entries
            returnMsgLbl.Text = "<br />";

            if (!(String.IsNullOrWhiteSpace(addCollabName.Text)))
            {
                //if add collab name text is not empty 
                name = addCollabName.Text;                                                      //assign text value to name variable
                nameFlag = true;                                                                //name flag is true
                returnMsgLbl.Text += "<span style='color:green'>Name added.</span><br />";      //update return msg for confirmation
            }
            else
            {
                returnMsgLbl.Text += "Please enter a name.<br />";                              //else inform user to input into name field
            }

            if (!(String.IsNullOrWhiteSpace(addCollabType.Text)))                                   //if add collab type text is not empty
            {
                type = addCollabType.Text;                                                          //assign text value to type variable
                typeFlag = true;                                                                    //type flag is true
                returnMsgLbl.Text += "<span style='color:green'>Support type added.</span><br />";  //update return msg for confirmation
            }
            else
            {
                returnMsgLbl.Text += "Please enter a support type.<br />";                          //else inform user to input into support type field
            }

            if (!(String.IsNullOrWhiteSpace(addCollabStartTime.Text)))                                          //if collaborator start time field is not empty
            {  
                if (Regex.Match(addCollabStartTime.Text, "^(?:0?[0-9]|1[0-9]|2[0-3]):[0-5][0-9]$").Success)     //check with regex to ensure it is following a specific format
                {
                    startTimeFlag = true;                                                                       //if true, then startTimeFlag is true
                    startTime = addCollabStartTime.Text;                                                        //start time text value is assigned to start time variable
                    returnMsgLbl.Text += "<span style='color:green'>Start time added.</span><br />";            
                }
                else
                {                                                                                               //else inform user of formatting
                    returnMsgLbl.Text += "Please enter a time according to the 24 hour system, i.e. 01:00, 1:00, 17:00, 20:00, etc.<br />";
                }
            }
            else
            {
                returnMsgLbl.Text += "Please enter a start time.<br />";                                        //if string is empty, inform user to fill in field
            }

            if (!(String.IsNullOrWhiteSpace(addCollabEndTime.Text)))                                            //if collaborator end time field is not empty
            {
                if (Regex.Match(addCollabEndTime.Text, "^(?:0?[0-9]|1[0-9]|2[0-3]):[0-5][0-9]$").Success)       //check with regex to ensure it is following a specific time format
                {
                    endTimeFlag = true;                                                                         //if true, assign end time flag to true
                    endTime = addCollabEndTime.Text;                                                            //assign text value to end time variable
                    returnMsgLbl.Text += "<span style='color:green'>End time added. </span> <br />";            //update return msg for confirmation
                }
                else
                {                                                                                               //else inform user of formatting
                    returnMsgLbl.Text += "Please enter a time according to the 24 hour system, i.e. 01:00, 1:00, 17:00, 20:00, etc. <br />";
                }
            }
            else
            {
                returnMsgLbl.Text += "Please enter an end time. <br />";                                        //if string is empty, inform user to fill the field
            }

            if (startTimeFlag && endTimeFlag)                                                                   //if starttimeflag and endtimeflag are true
            {
                time = startTime + "-" + endTime;                                                               //assign both start time and endtime to time variable
            }

            if (addCollabPt.Value != "")                                                                        //if hidden field storing coordinates is not empty
            {
                string pt = addCollabPt.Value;                                                                  //assign value to string variable pt
                pt = pt.Substring(1, pt.Length - 2);                                                            //strip string of ( ) at the beginning and end of string
                string[] pts = pt.Split(',');                                                                   //split string between both beginning and ending coordinates
                lat = Double.Parse(pts[0]);                                                                     //assign first string token to latitude variable
                lon = Double.Parse(pts[1]);                                                                     //assign second string token to longitude variable
                ptFlag = true;                                                                                  //assign point flag to true
                returnMsgLbl.Text += "<span style='color:green;'>Location obtained.</span><br />";              //update return message for confirmation
            }
            else
            {                                                                                                   //if there is no coordinate value in the hidden field input
                returnMsgLbl.Text += "Invalid location. Click on the map to register the collaborator's location. <br />";  //inform user to register a location through button click
            }

            if (addCollabPic.HasFile)                                                                           //check if picture has been uploaded
            {
                string fileName = Path.GetFileName(addCollabPic.PostedFile.FileName);                           //if there is an image uploaded, get the filename 
                addCollabPic.PostedFile.SaveAs(Server.MapPath("~/Content/Images/") + fileName);                 //save the file to the images folder 
                returnMsgLbl.Text += "<span style='color:green;'>Picture uploaded.</span><br />";               //update return msg for confirmation
                pic = "../Content/Images/" + fileName;                                                          //assign variable pic to folder path and filename
                picFlag = true;                                                                                 //picflag is true
            }
            else
            {
                returnMsgLbl.Text += "Please add a picture.<br />";                                             //if there is no picture, inform user to upload one
            }

            if (nameFlag && typeFlag && startTimeFlag && endTimeFlag && picFlag && ptFlag)                      //if all flags are true
            {
                Collab person = new Collab(name, type, time, lat, lon, pic);                                    //initialize collaborator object with input values
                person.AddToXML();                                                                              //call function to add to xml 
                returnMsgLbl.Text = "<p style='color:green;'>Person added successfully.</p>";                   //update return message
            }
        }

        protected void PostTweetBtn_Click(object sender, EventArgs e)                                           //function to post tweet to twitter
        {
            if (!(String.IsNullOrWhiteSpace(tweetText.Text)))                                                   //if tweet text area is not null
            {
                if (!(tweetText.Text.Length > 140))                                                             //if text area's string input is not more than 140 characters
                {
                    UserClient.Tweets.PublishTweetAsync(tweetText.Text);                                        //publish tweet
                    tweetMsgDiv.Text = "<span style='color:green'> Tweet posted </span> <br />";                //return msg confirmation to user
                }
                else
                {                                                                   
                    tweetMsgDiv.Text = "Tweets limited to 140 characters.<br />";                               //if tweet is over 140 chars, inform user
                }
            }
            else
            {
                tweetMsgDiv.Text = "You cannot post an empty tweet.<br />";                                     //if text input is empty, inform user
            }
        }

        protected void PostStatusBtn_Click(object sender, EventArgs e)                                          //function to post status post to facebook
        {
            if (!(String.IsNullOrWhiteSpace(FBStatusText.Text)))                                                //if post text area is not null
            {
                //code retrieved from: https://www.c-sharpcorner.com/UploadFile/raj1979/post-on-facebook-users-wall-using-Asp-Net-C-Sharp/ by Raj Kumar
                // last updated february 9th 2021
                if (Request["code"] == null)
                {
                    Response.Redirect(string.Format(
                        "https://graph.facebook.com/oauth/authorize?client_id={0}&redirect_uri={1}&scope={2}",
                        FbAppID, Request.Url.AbsoluteUri, scope));
                }
                else
                {
                    Dictionary<string, string> tokens = new Dictionary<string, string>();
                    string url = string.Format("https://graph.facebook.com/oauth/access_token?client_id={0}&redirect_uri={1}&scope={2}&code={3}&client_secret={4}",
                        FbAppID, Request.Url.AbsoluteUri, scope, Request["code"].ToString(), FbAppSecret);
                    HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                    using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                    {
                        StreamReader reader = new StreamReader(response.GetResponseStream());
                        string vals = reader.ReadToEnd();
                        foreach (string token in vals.Split('&'))
                        {
                            tokens.Add(token.Substring(0, token.IndexOf("=")),
                                token.Substring(token.IndexOf("=") + 1, token.Length - token.IndexOf("=") - 1));
                        }
                    }
                    string access_token = tokens["access_token"];
                    var client = new FacebookClient(access_token);
                    client.Post("/me/feed", new { message = FBStatusText.Text });
                    fbMsgDiv.Text = "Status updated.<br />";
                }
            }
            else
            {
                fbMsgDiv.Text = "You cannot post an empty status.<br />";                                           //if post is null, inform user 
            }
        }

        //function to hide/show add collaborator form div
        protected void addColBtn_Click(object sender, ImageClickEventArgs e)                                        
        {
            if (addCollabDiv.Style["display"] == "none")
            {
                addCollabDiv.Style["display"] = "block";
                //addNewBtn.Text = "Close Collaborator Form";
            }
            else
            {
                addCollabDiv.Style["display"] = "none";
                //addNewBtn.Text = "Add New Collaborator";
            }
        }

        //function to hide/show tweet div
        protected void tweetBtn_Click1(object sender, ImageClickEventArgs e)
        {
            if (NewTweetDiv.Style["display"] == "none")
            {
                NewTweetDiv.Style["display"] = "block";
            }
            else
            {
                NewTweetDiv.Style["display"] = "none";
            }
        }

        //function to hide/show fb post div
        protected void facebookBtn_Click(object sender, ImageClickEventArgs e)
        {
            if (NewFBStatusDiv.Style["display"] == "none")
            {
                NewFBStatusDiv.Style["display"] = "block";
            }
            else
            {
                NewFBStatusDiv.Style["display"] = "none";
            }
        }
    }
}
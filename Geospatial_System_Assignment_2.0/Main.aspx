<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="ICT365_Assignment2.Main" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" 
    "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Lifeloggers</title>
    <link rel="stylesheet" type="text/css" href="Content/main.css" />
    <script type="text/javascript" src="Scripts/index.js"></script>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
</head>
<body>
    <form id='main' runat="server">
    
    <nav>      
        <asp:ScriptManager ID="ScriptManager1" 
                               runat="server" />
        <asp:UpdatePanel ID="NavPanel"
                             runat="server"
                             UpdateMode="Conditional">
        <ContentTemplate>
            <asp:ImageButton runat="server" ID="addColBtn" OnClick="addColBtn_Click" /> &emsp;&emsp;
            <asp:ImageButton runat="server" ID="tweetBtn" OnClick="tweetBtn_Click1"/> &emsp;&emsp;
            <asp:ImageButton runat="server" ID="facebookBtn" OnClick ="facebookBtn_Click" /> &emsp;&emsp;

           <div id="addCollabDiv" runat="server" style="display:none;">
            <br />
            <asp:Label ID="collabNameLabel" runat ="server">Collaborator Name</asp:Label>&emsp;&emsp;&nbsp;
            <asp:TextBox CssClass="addCollab" ID="addCollabName" runat="server"/> 
            <br />
            <asp:Label ID="collabTypeLabel" runat ="server">Support Type</asp:Label> &emsp;&emsp;&emsp;&emsp;&nbsp;
            <asp:TextBox CssClass="addCollab" ID="addCollabType" runat="server"/> 
            <br />
            <asp:Label ID="collabStartTimeLabel" runat ="server">Start Time (24hr system)</asp:Label> 
            <asp:TextBox CssClass="addCollab" ID="addCollabStartTime" runat="server"/> 
            <br />
            <asp:Label ID="collabEndTimeLabel" runat ="server">End Time (24hr system)</asp:Label> &thinsp;
            <asp:TextBox CssClass="addCollab" ID="addCollabEndTime" runat="server"/> 
            <br />
            <asp:Label ID="collabPicLabel" runat ="server">Upload Image</asp:Label> &emsp;&emsp;&emsp;&emsp;
            <asp:FileUpload CssClass="addCollab" ID="addCollabPic" runat="server" accept=".png,.jpg,.jpeg,.bmp"/>     
            <br />
            <asp:HiddenField ID="addCollabPt" runat="server" value=""/>
            <br />
            <asp:Button ID="saveCollabBtn" Text='Save Collaborator' runat="server" OnClick="saveButton_Click"/>
            <asp:Label ID="returnMsgLbl" runat ="server"></asp:Label>
            </div>

           <div id="NewTweetDiv" runat="server" style="display:none;">
            <br />
            <asp:Label ID="Tweet" runat ="server">What's Happening?</asp:Label>&emsp;&emsp;&nbsp; <br />
            <asp:TextBox CssClass="postTweet" ID="tweetText" TextMode="multiline" Columns="50" Rows="5" runat="server"/>  
            <br />
            <asp:Button ID="PostTweetBtn" Text='Tweet' runat="server" OnClick="PostTweetBtn_Click"/>
            <asp:Label ID="tweetMsgDiv" runat ="server"></asp:Label>
            </div>

            <div id="NewFBStatusDiv" runat="server" style="display:none;">
            <br />
            <asp:Label ID="FBStatus" runat ="server">What's on your mind?</asp:Label>&emsp;&emsp;&nbsp; <br />
            <asp:TextBox CssClass="postFbStatus" ID="FBStatusText" TextMode="multiline" Columns="50" Rows="5" runat="server"/>   
            <br />
            <asp:Button ID="PostStatusBtn" Text='Post' runat="server" OnClick="PostStatusBtn_Click"/>
            <asp:Label ID="fbMsgDiv" runat ="server"></asp:Label>
            </div>
        </ContentTemplate>
            <Triggers>
            <asp:PostBackTrigger ControlID="saveCollabBtn" /> 
            </Triggers>
        </asp:UpdatePanel>

    </nav>
        <div id="map" runat="server"></div>
        <!-- Async script executes immediately and must be after any DOM elements used in callback. -->
        <script async
            src="https://maps.googleapis.com/maps/api/js?key=REMOVED&callback=initMap&libraries=&v=weekly">
        </script>
    </form>
</body>
</html>

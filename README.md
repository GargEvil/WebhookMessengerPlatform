<h1>MessengerWebook</h1> 
This project is a sample code to show how to use Facebook's Messenger WebHook with .NET 5 Web API.

<hr/>
<b>Summary</b><br/>
Whenever someone writes to your Facebook page will send a request to your server. (visit https://developers.facebook.com/docs/graph-api/webhooks to know more about Facebook Webhooks)<br/>

This Web Application catch this request and do something accordingly. For now, it just sends same text to the sender. (you should decide what app will do)

<b>Prerequisites :</b><br/>
Visual Studio 2019 (Community Version is downloadable for free)<br/>
A Facebook Application (you can create one for free : https://developers.facebook.com/ )<br/>
You must be Admin of a page on Facebook<br/>
Ngrok -> this is a tool that will make your localhost https (Facebook requires that you have https)<br/>

<b>Instructions:</b><br/>
Open the solution in Visual Studio<br/>
Edit the configurations in appsettings.json<br/>

Facebook Configuration :
Create the Webhook<br/>
Note : This app has been tested with Facebook Webhooks v10<br/>


Feel free to adapt this application to any scenario you can imagine. 

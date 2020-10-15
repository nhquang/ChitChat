# ChitChat
Simple chat windows app
UDP Listener runs in the background, it waits for the incoming messages. Once UDP Listener receives a message, the app checks for the message's validity, then adds it to the BlockingCollection which is a thread-safe collection.  
Once the BlockingCollection is added a new message, the UI thread instantly takes that new message from the collection to check whether the message comes from someone you are already chatting with (having an open window chat with him/her) or comes from someone you havent initiated conversation with.  
&nbsp;&nbsp;&nbsp;If the new message comes from someone who you are chatting with (having an open window chat with him/her), the message is added to messagesToBeDisplayed. Each chat window loops through the messageToBeDisplay to find which message belongs to it.  
&nbsp;&nbsp;&nbsp;If the new message comes from someone who you havent initiated conversations with, the message is added to the notifications queue then displayed in the notification box.  

using System.Collections;
using System.Collections.Generic;
using PJ.Native.Messenger;
using PJ.Native.Proto;
using UnityEngine;

public class NativeComunicator
{
    private MessageHandler messageHandler;

    public NativeComunicator()
    {
        // Create messageHandler
        messageHandler = new MessageHandler(Tag.Game);
        
        // set handler with key.
        messageHandler.SetHandler("ALERT_RESULT", OnReceive); 
    }   

    private void OnReceive(MessageHolder messageHolder)
    {
        if(messageHolder.Message.Container.TryGetValue("pressOk", out bool pressOk))
        {
            Debug.Log("user press? " + pressOk);
        }
    }

    public void OpenNativeAlert(string alertMessage)
    {
        // Create Container object and set data
        Container container = new Container();
        container.Add("alertMessage", alertMessage);
        Message message = new Message("OPEN_ALERT", container);
        // NotifyMessage 
        messageHandler.Notify(message, Tag.Native);
    }
}
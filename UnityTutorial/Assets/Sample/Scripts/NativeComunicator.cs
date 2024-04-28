using System.Collections;
using System.Collections.Generic;
using PJ.Native.Proto;
using PJ.Native.PubSub;
using UnityEngine;
// using PJ.Native.PubSub;

public class NativeComunicator
{
    private Messenger messenger;

    public NativeComunicator()
    {
        // Create messageHandler
        messenger = new Messenger();
        messenger.SetReceivingRule(Tag.Game);
        
        // set handler with key.
        messenger.Subscribe("ALERT_RESULT", OnReceive); 
    }   

    private void OnReceive(Message message)
    {
        if(message.Container.TryGetValue("pressOk", out bool pressOk))
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
        messenger.Publish(message, Tag.Native);
    }
}
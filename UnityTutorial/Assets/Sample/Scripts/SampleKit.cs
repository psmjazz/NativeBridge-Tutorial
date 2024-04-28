using System;
using System.Collections;
using System.Collections.Generic;
using PJ.Native.PubSub;
using PJ.Native.Proto;
using UnityEngine;

public class SampleKit
{
    private Messenger messenger;

    public SampleKit()
    {
        messenger = new Messenger();
        messenger.SetReceivingRule(Tag.Game);
        messenger.Subscribe("testReturn", OnTestReturn);
        messenger.Subscribe("native", OnNative);
    } 

    private void OnTestReturn(Message message)
    {
        if(message.Container.TryGetValue("data", out string data))
            Debug.Log("Unity... SampleNode : " + data);
        else
            Debug.Log("Unity... SampleNode : no data");
    }

    private void OnNative(Message message)
    {
        if(message.Container.TryGetValue("data", out string data))
            Debug.Log("Unity... Native : " + data);
        else
            Debug.Log("Unity... Native : no data");
    }

    public void CallTest()
    {
        Container container1 = new Container();
        container1.Add("data", "first");
        Message message1 = new Message("test", container1);
        messenger.Publish(message1, Tag.Native);
        
        Container container2 = new Container();
        container2.Add("data", "second");
        Message message2 = new Message("testRecall", container2);
        messenger.Publish(message2, Tag.Native);
    }
}

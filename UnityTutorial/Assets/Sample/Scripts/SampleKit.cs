using System;
using System.Collections;
using System.Collections.Generic;
using PJ.Native.Messenger;
using PJ.Native.Proto;
using UnityEngine;

public class SampleKit
{
    private MessageHandler handler;

    public SampleKit()
    {
        handler = new MessageHandler(Tag.Game);
        handler.SetHandler("testReturn", OnTestReturn);
        handler.SetHandler("native", OnNative);
    } 

    private void OnTestReturn(MessageHolder messageHolder)
    {
        if(messageHolder.Message.Container.TryGetValue("data", out string data))
            Debug.Log("Unity... SampleNode : " + data);
        else
            Debug.Log("Unity... SampleNode : no data");
    }

    private void OnNative(MessageHolder messageHolder)
    {
        if(messageHolder.Message.Container.TryGetValue("data", out string data))
            Debug.Log("Unity... Native : " + data);
        else
            Debug.Log("Unity... Native : no data");
    }

    public void CallTest()
    {
        Container container1 = new Container();
        container1.Add("data", "first");
        Message message1 = new Message("test", container1);
        handler.Notify(message1, Tag.Native);
        
        Container container2 = new Container();
        container2.Add("data", "second");
        Message message2 = new Message("testRecall", container2);
        handler.Notify(message2, Tag.Native);
    }
}

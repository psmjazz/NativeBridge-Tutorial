using System;
using System.Collections;
using System.Collections.Generic;
using PJ.Native.Bridge;
using PJ.Native.Proto;
using UnityEngine;
using UnityEngine.UI;

public class SampleScene : MonoBehaviour
{
    private NativeComunicator communicator;

    [SerializeField] private Button openAlertButton;

    // Start is called before the first frame update
    void Start()
    {
        this.SampleTest();
        communicator = new NativeComunicator();

        openAlertButton.onClick.AddListener(() => communicator.OpenNativeAlert("Unity request alert"));
    }

    private void SampleTest()
    {
        SampleKit sample = new SampleKit();
        sample.CallTest();
    }
}
package com.pj.tutorial

import android.util.Log
import com.pj.pubsub.Messenger
import com.pj.pubsub.Tag
import com.pj.pubsub.extensions.ContainerBuilder
import com.pj.pubsub.extensions.Message
import com.pj.pubsub.extensions.add
import com.pj.pubsub.extensions.getString

class SampleKit {
    private val TAG = SampleKit::class.java.name

    private val messenger : Messenger = Messenger()

    init {
        messenger.apply {  }
        messenger.setReceivingRule(Tag.native)
        messenger.setBasePublishingTag(Tag.game)
        messenger.subscribe("test", this::onTest)
        messenger.subscribe("testRecall", this::onTestRecall)
    }

    private fun onTest(message: Message){
        Log.d(TAG, "onTest : $message" )

        val containerBuilder = ContainerBuilder()
        containerBuilder.add("data", "this is android message :D")
        val reply = Message("native", containerBuilder.build())
        messenger.publish(reply)
    }

    private fun onTestRecall(message: Message){
        val data = message.container.getString("data")
        Log.d(TAG, "onTestReCall : $data")

        val containerBuilder = ContainerBuilder()
        containerBuilder.add("data", "RECALL [$data]")
        val returned = Message("testReturn", containerBuilder.build())
        messenger.publish(returned)
    }
}
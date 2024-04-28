package com.pj.tutorial

import android.app.AlertDialog
import com.pj.pubsub.Messenger
import com.pj.pubsub.Tag
import com.pj.pubsub.extensions.ContainerBuilder
import com.pj.pubsub.extensions.Message
import com.pj.pubsub.extensions.add
import com.pj.pubsub.extensions.getString
import com.unity3d.player.UnityPlayer

class NativeUIController{
    private val messenger = Messenger()

    init {
        messenger.setReceivingRule(Tag.native)
        messenger.setBasePublishingTag(Tag.game)
        messenger.subscribe("OPEN_ALERT", ::onReceive)
    }

    private fun onReceive(message : Message){
        // Get data from container
        val alertMessage = message.container.getString("alertMessage")

        openAlert(alertMessage ?: "OPEN_ALERT")
    }

    private fun openAlert(alertMessage: String){
        fun giveBackResult(okPressed : Boolean){
            // Create ContainerBuilder and set data.
            val containerBuilder = ContainerBuilder()
            containerBuilder.add("pressOk", okPressed)
            val reply = Message("ALERT_RESULT", containerBuilder.build())

            // Give back message to message notifier
            messenger.publish(reply)
        }

        val builder = AlertDialog.Builder(UnityPlayer.currentActivity)
        builder.setTitle("Alert")
        builder.setMessage(alertMessage)
        builder.setPositiveButton("OK"){_, _ ->
            giveBackResult(true)
        }
        builder.setOnCancelListener {_->
            giveBackResult(false)
        }

        val dialog = builder.create()
        dialog.show()
    }

}
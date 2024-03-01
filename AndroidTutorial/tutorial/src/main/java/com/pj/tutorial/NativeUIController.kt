package com.pj.tutorial

import android.app.AlertDialog
import com.pj.core.MessageHandler
import com.pj.core.MessageHolder
import com.pj.core.Tag
import com.pj.core.extensions.ContainerBuilder
import com.pj.core.extensions.Message
import com.pj.core.extensions.add
import com.pj.core.extensions.getString
import com.unity3d.player.UnityPlayer
import kotlinx.coroutines.CoroutineDispatcher
import kotlinx.coroutines.GlobalScope
import kotlinx.coroutines.coroutineScope
import kotlinx.coroutines.delay
import kotlinx.coroutines.launch

class NativeUIController{
    private val handler = MessageHandler(Tag.native)

    init {
        // Set handler with key
        handler.setHandler("OPEN_ALERT", ::onReceive)
    }

    private fun onReceive(messageHolder : MessageHolder){
        // Get data from container
        val alertMessage = messageHolder.message.container.getString("alertMessage")

        openAlert(alertMessage ?: "OPEN_ALERT", messageHolder)
    }

    private fun openAlert(alertMessage: String, messageHolder: MessageHolder){
        fun giveBackResult(okPressed : Boolean){
            // Create ContainerBuilder and set data.
            val containerBuilder = ContainerBuilder()
            containerBuilder.add("pressOk", okPressed)
            val message = Message("ALERT_RESULT", containerBuilder.build())

            // Give back message to message notifier
            messageHolder.giveBack(message)
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
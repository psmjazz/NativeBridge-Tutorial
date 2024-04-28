//
//  NativeUIController.swift
//  iOSTutorial
//
//  Created by sangmin park on 3/1/24.
//

import Foundation
import iOSPubSub

public class NativeUIController{
    
    private let messageHandler : Messenger
    private let gameViewController : UIViewController
    
    init(gameViewController : UIViewController){
        self.gameViewController = gameViewController
        messageHandler = Messenger()
        messageHandler.setReceivingRule(all: .native)
        messageHandler.setBasePublishingTag(.game)
        messageHandler.subscribe(key: "OPEN_ALERT", handler: onReceive)
    }

    private func onReceive(message: Message){
        let alertMessage = message.container.getString(key: "alertMessage") ?? ""
        openAlert(alertMessage: alertMessage, message: message)
    }

    private func openAlert(alertMessage: String, message: Message){
        func giveBackResult(pressOk: Bool){
            var container = Container()
            container.add(key: "pressOk", value: pressOk)
            let reply = Message(key: "ALERT_RESULT", container: container)
            messageHandler.publish(message: reply)
        }
        
        let alertController = UIAlertController(title:"Alert", message: alertMessage, preferredStyle: .alert)
        alertController.addAction(UIAlertAction(title: "OK", style: .default, handler: { _ in
            giveBackResult(pressOk: true)
        }))
        alertController.addAction(UIAlertAction(title: "Cancel", style: .cancel, handler: { _ in
            giveBackResult(pressOk: false)
        }))
        print("NativeUIController open alert!")
        gameViewController.present(alertController, animated: true)
        
    }
}

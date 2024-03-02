//
//  NativeUIController.swift
//  iOSTutorial
//
//  Created by sangmin park on 3/1/24.
//

import Foundation
import iOSBridgeCore

public class NativeUIController{
    
    private let messageHandler : MessageHandler
    private let gameViewController : UIViewController
    
    init(gameViewController : UIViewController){
        self.gameViewController = gameViewController
        messageHandler = MessageHandler(tag: Tag.native)
        messageHandler.setHandler(key: "OPEN_ALERT", handler: onReceive)
    }

    private func onReceive(messageHolder: MessageHolder){
        let alertMessage = messageHolder.message.container.getString(key: "alertMessage") ?? ""
        openAlert(alertMessage: alertMessage, messageHolder: messageHolder)
    }

    private func openAlert(alertMessage: String, messageHolder: MessageHolder){
        func giveBackResult(pressOk: Bool){
            var container = Container()
            container.add(key: "pressOk", value: pressOk)
            let message = Message(key: "ALERT_RESULT", container: container)
            messageHolder.giveBack(message: message)
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

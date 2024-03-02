//
//  Loader.swift
//  SampleKit
//
//  Created by sangmin park on 1/31/24.
//

import Foundation
import UIKit

@objc public class Loader : NSObject{
    @objc public static func loadModule(gameViewController: UIViewController){
        let sample = SampleKit()
        let uiController = NativeUIController(gameViewController: gameViewController)
    }
}

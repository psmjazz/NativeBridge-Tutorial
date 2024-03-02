//
//  Loader.m
//  SampleKit
//
//  Created by sangmin park on 1/31/24.
//
#import <Foundation/Foundation.h>
#import <iOSTutorial/iOSTutorial-Swift.h>
#import <UnityFramework/UnityFramework.h>
@interface OnModuleLoad : NSObject

@end
    
@implementation OnModuleLoad

+(void) load{
    dispatch_after(dispatch_time(DISPATCH_TIME_NOW, (int64_t)(1 * NSEC_PER_SEC)), dispatch_get_main_queue(), ^{
        UIViewController *controller = [[[UnityFramework getInstance] appController] rootViewController];
        [Loader loadModuleWithGameViewController:controller];
    });
}

@end

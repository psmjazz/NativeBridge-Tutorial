# iOS tutorial
## Add Dependency
1. Init pod file
```
pod init
```
2. Open pod file and add iOSBridgeCore dependency
```
target 'iOSTutorial' do
  platform :ios, '12.0'
  # Comment the next line if you don't want to use dynamic frameworks
  use_frameworks!
  # Pods for iOSTutorial
  pod 'iOSBridgeCore', :git => 'https://github.com/psmjazz/NativeBridge-iOS.git', :tag => '0.0.1' 
end

post_install do |installer|
  installer.pods_project.targets.each do |target|
    target.build_configurations.each do |config|
      config.build_settings['ENABLE_BITCODE'] = 'NO'
      config.build_settings['IPHONEOS_DEPLOYMENT_TARGET'] = '12.0'
    end
  end
end
```
3. Run pod install
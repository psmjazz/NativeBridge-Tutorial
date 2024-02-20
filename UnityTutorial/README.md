# Unity Tutorial

1. Import UnityBridgeCore.unitypackage
2. Add your android/iOS native plugin in project

## iOS Build

1. Create pod file
```
pod init
```

2. Open Podfile and add iOSBridgeCore to 'UnityFramework' target
3. To make change dependent frameworks' minimum version add post_install clause

```
platform :ios, '12.0'

target 'Unity-iPhone' do
  use_frameworks!

  # Pods for Unity-iPhone
  target 'Unity-iPhone Tests' do
    inherit! :search_paths
    # Pods for testing
  end

end

target 'UnityFramework' do
  # Comment the next line if you don't want to use dynamic frameworks
  use_frameworks!
  # Pods for UnityFramework
  # pod 'iOSBridgeCore', :path => '../../../iOSBridge'
  pod 'iOSBridgeCore', :git => 'https://github.com/psmjazz/NativeBridge-iOS.git', :tag => '0.0.1' 

end

# Add this if you want to change frameworks' minimum version
post_install do |installer|
  installer.pods_project.targets.each do |target|
    target.build_configurations.each do |config|
      config.build_settings['ENABLE_BITCODE'] = 'NO'
      config.build_settings['IPHONEOS_DEPLOYMENT_TARGET'] = '12.0'
    end
  end
end

```
4. Run pod install and open xcworkspace
```
pod install
```
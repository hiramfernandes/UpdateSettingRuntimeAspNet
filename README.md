# UpdateSettingRuntimeAspNet
The project is a demonstration of how to update the appSettings.json file in runtime. 
There is a lot of controversy on this approach, but wether correct or not, I'm showing how it works in case someone ever needs it.

In my case, I had to run the application using some encrypted values. In case they were not, the application would detect those, update and persist them in runtime.
Hope it's useful.

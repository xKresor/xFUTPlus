
C# program that use Memory64.dll and C++ imports to modify the memory of a process.

You'll need to install those libraries in order to work : 

1.Memory64.dll

2.Siticone.Desktop.UI.2.0.9

3.Microsoft.Web.WebView2.1.0.1245.22

4.Autoupdater.NET.Official.1.7.3

This program modify the memory of FIFA22 process. It also auto-updates itself automatically if it detects a new version being pushed on your site.

I removed all the adresses and bytes for each trainer option. I did this because I don't want to upload a "plug n' play" trainer here on github. If you want to make it work, you can reverse engineer the game by yourself.

This code is meant to be used by begginers to see how you can make a simple UI in C# and also to learn how you can manipulate a process memory from USER-MODE.

You can learn the following from this code :

1.How to manipulate the memory of a process from user-mode.

2.How to create customizable hotkeys for every option.

3.How to use PINVOKES, so you can use VirtualFreeEx in C#.

4.How to create different type of menus, like a dropdown list, buttons etc..

5.How to search for different "cracking" programs and when it found something, it will BSOD the computer. (**Keep in mind this is just a simple code example, anyone with half of their brain functional will decompile the program just fine and NOP those checks instantly.

6.How to create a new thread for the timer, so it won't interfere with the main thread.

7.How to use different functions of Memory64.dll like aob scanning, write to memory, read memory etc...




C# program that use Memory64.dll and C++ imports to modify the memory of a process.

You'll need to install those libraries in order to work : 

  <li>Memory64.dll</li>
  <li>Siticone.Desktop.UI.2.0.9</li>
  <li>Microsoft.Web.WebView2.1.0.1245.22</li>
  <li>Autoupdater.NET.Official.1.7.3</li>

**You can unzip them from packages.rar into the xFUTPlus folder.**

This program modify the memory of FIFA22 process. It also auto-updates itself automatically if it detects a new version being pushed on your site.


**I wrote the code to search for the function itself when you patch it in memory, so if the game developer doesn't target that specific function when they pull an update, the hack will work just fine without needing you to update it.
So if for example, the adress for Player position function is at 0x140001000 offset and the game developer push an update and the adress is changed to 0x1446A1000 FOR EXAMPLE, the hack will still find it and patch it accordingly**


I removed all the adresses and bytes for each trainer option. I did this because I don't want to upload a "plug n' play" trainer here on github. If you want to make it work, you can reverse engineer the game by yourself.

This code is meant to be used by begginers to see how you can make a simple UI in C# and also to learn how you can manipulate a process memory from USER-MODE.

You can learn the following from this code :

1.How to manipulate the memory of a process from user-mode.

2.How to create customizable hotkeys for every option.

3.How to use PINVOKES, so you can use VirtualFreeEx in C#.

4.How to create different type of menus, like a dropdown list, buttons etc..

5.How to search for different "cracking" programs and when it found something, it will BSOD the computer. (**Keep in mind this is just a simple code example, anyone with half of their brain functional will decompile the program just fine and NOP those checks instantly.**)

6.How to create a new thread for the timer, so it won't interfere with the main thread.

7.How to use different functions of Memory64.dll like aob scanning, write to memory, read memory etc...





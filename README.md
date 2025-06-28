# Serger - Server Pinger

![Static Badge](https://img.shields.io/badge/Language-C%23-blue)
![Static Badge](https://img.shields.io/badge/Project%20Type-Library/App-yellow)
![Static Badge](https://img.shields.io/badge/License-Custom-green)
![Static Badge](https://img.shields.io/badge/Version-Beta%200.2.1-purple)

<h1>What is it for?</h1>
<i>Do you have some sort of server and you're worried about its availability? For example, do you have a website, and you're worried that your visitors won't be able to visit it?</i>
<b>This thing is for you!</b><br>
This program will notify you if there are any troubles with your server. Right now it can only check if the server is reachable, but soon it will be able to check a lot more and get a lot more details for you.

<h1>Current features</h1>

- You can change the ping delay (default: 10 000 ms/10 s)
- You can choose between 2 languages (Czech and English)
    - You can easily translate it to another language or even enter your own messages
- You can ping any type of server

<h1>Library</h1>
The library is written in C# for .NET9.0 and is not available on NuGet yet.
The library is now available in the <b>SRG_Core</b> folder.
The GUI will not be open source, but the CLI will stay open source as example code for the library.
Our custom license may change in the near future to make it more suitable for the Library and GUI.

<h1>GUI</h1>
Right now the GUI is under development and will be released with version Beta 0.3.0. The first stable GUI release will be version 1.0.0.
<br>
The GUI will be made in MAUI or UNO and will be available for Windows, macOS and Linux. MAUI and Uno support Android and iOS, but We don't plan to make a mobile app in the near future.
<br>
The GUI will soon include Windows notifications and tray icon features.

# ToDo
- ✅Logs
- ~~❌CLI commands~~ **(cancelled)**
- 🛠️Windows notifications
- 🛠️Windows tray icon
- ❗Languages (en, cs)
  - Language class needs to be refactored a lot, and it will be done soon **in the version 0.2.2**
- ❌Wiki
- 🛠️️️GUI (MAUI or Uno)
- ❌Linux support
- ❌Ping server software (1 server for pinging multiple IPs, sending notification to 1 computer)
- ❌API support

# Overlay Message Box

Shows a message on a dimmed desktop that the user is forced to acknowledge. This ensures the attention of the user and makes it a great app for any script that needs to inform about something. All options (Title, Text, Icons etc.) can be set using command line parameters.

```
OverlayMessageBoxCmd.exe -Caption "Title of message" --symbol Information -Text "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna"
```

![Example image](/images/example.png?raw=true "Example image")


## Using Overlay Message Box ##

The most basic example is that you only display a message with this command:

``OverlayMessageBoxCmd.exe -Text "My text"``

![Example 1 image](/images/example1.png?raw=true "Example image 1")

For more control, you can specify the title, the caption and a different symbol:

``OverlayMessageBoxCmd.exe -Caption "Title" -Text "My text" -Symbol Warning``

![Example 2 image](/images/example2.png?raw=true "Example image 2")

You can also use a "Task Dialog" style message box which supports a headline before the text. Just specify the parameter "Headline" to use it:

``OverlayMessageBoxCmd.exe -Caption "Title" -Headline "The headline" -Text "My text" -Symbol Warning``

![Example 3 image](/images/example3.png?raw=true "Example image 3")

**Note**: When using ``-Headline`` together with ``-Symbol Question``, no icon will appear because Task Dialog does not support this symbol.

If you wish to split your text among multiple lines, you can use \n to insert line breaks.

``OverlayMessageBoxCmd.exe -Caption "Title" -Text "My text\nLine 2\nLine 3\n\nLine 5" -Symbol Warning``

![Example 5 image](/images/example5.png?raw=true "Example image 5")

## Using buttons ##

You can also define which buttons are displayed and determine which button was selected by checking the return code.

```
OverlayMessageBoxCmd.exe -Caption "Title" -Headline "The headline" -Text "My text" -Symbol Warning -Buttonset YesNo 
SET RETCODE=%ERRORLEVEL%

IF "%RETCODE%"=="104" goto YES
IF "%RETCODE%"=="105" goto NO
```

![Example 4 image](/images/example4.png?raw=true "Example image 4")

Each button has a different return code. To see the full list, use ``OverlayMessageBoxCmd.exe -?``

The download file (see [Releases](releases/latest)) contains a folder examples that has ready-to-use batch files you might find useful. 
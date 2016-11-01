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

OverlayMessageBoxCmd.exe -Caption "Title" -Headline "The headline" -Text "My text" -Symbol Warning

![Example 3 image](/images/example3.png?raw=true "Example image 3")


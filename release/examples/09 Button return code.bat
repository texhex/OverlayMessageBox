@echo off
%~dp0..\OverlayMessageBoxCmd --text "Press a button" --buttonset YesNo --symbol Information --headline "Headline"

SET RETCODE=%ERRORLEVEL%

echo Return code was %RETCODE%

IF "%RETCODE%"=="104" goto YES
IF "%RETCODE%"=="105" goto NO

goto ende

:YES
echo You selected YES
goto ende

:NO
echo You selected NO
goto ende



:ende
pause
 
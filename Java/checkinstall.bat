@ECHO OFF
set path=%path%;c:\program files\java\jdk1.6.0_25\bin
WHERE javac 2> check.txt
FOR /F "usebackq" %%A IN ('check.txt') DO set size=%%~zA

IF %size% GTR 0 (   
    echo ERROR: javac not detected, opening Internet Explorer.
    start iexplore "http://www.oracle.com/technetwork/java/javase/downloads/jdk-6u25-download-346242.html"
) ELSE (
    echo SUCCESS: javac installed.
)
del check.txt
PAUSE
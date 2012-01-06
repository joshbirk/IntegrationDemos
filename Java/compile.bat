ECHO OFF
set path=%path%;c:\program files\java\jdk1.6.0_25\bin

mkdir classes

javac -classpath lib\*;classes src\com\sample\onpremise\AddMerchandise.java -d classes
java -cp classes;lib\* com.sample.onpremise.AddMerchandise

ECHO Compile complete.
PAUSE
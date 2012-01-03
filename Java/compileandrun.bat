ECHO OFF
mkdir classes

javac -classpath lib\*;classes src\com\sample\onpremise\AddMerchandise.java -d classes
ECHO Compile complete.

java -cp classes;lib\* com.sample.onpremise.AddMerchandise


PAUSE
DIR="$( cd "$( dirname "$0" )" && pwd )"
cd $DIR
pwd
echo ""

rm -R classes
mkdir classes
javac -classpath 'lib/*' './src/com/sample/onpremise/AddMerchandise.java' -d classes

cd classes
com=$(ls)
if [ "$com" == "com" ] 
	then
	echo "Classes compiled."
	cd ..
	java -cp classes:lib/* com.sample.onpremise.AddMerchandise
	else
	echo "Classes not found, errors may have occurred."
fi
echo ""
echo "Press Control-C to quit."
read
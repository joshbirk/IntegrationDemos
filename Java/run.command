DIR="$( cd "$( dirname "$0" )" && pwd )"
cd $DIR
pwd
echo ""

cd classes
com=$(ls)
if [ "$com" == "com" ]
	then
	cd ..
	java -cp classes:lib/* com.sample.onpremise.AddMerchandise
	else
	echo "Classes not found, try re-compiling the application."
fi
echo ""
echo "Press Control-C to quit."
read


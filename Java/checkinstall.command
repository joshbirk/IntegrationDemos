DIR="$( cd "$( dirname "$0" )" && pwd )"
cd $DIR
pwd
echo ""

jv=$(which javac)
echo ""
if [[ $jv == *javac* ]]
	then
	echo "java compiler found."
	else
	echo "java compiler not found."
fi

echo ""
echo "Press Control-C to quit."
read
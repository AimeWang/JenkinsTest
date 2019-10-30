#!/bin/sh
echo "start"
 
#unityapp的路径
export untiy=/Applications/Unity2018312/Unity.app/Contents/MacOS/Unity
#项目路径
export projectPath=/Users/wangyue15/Desktop/JenkinsTest
 
#把所有=后面的参数取出来
for a in $*
do
    r=`echo $a | sed "s/--//g"`
    eval $r
done
 
#这里就可以拿到jenkins传递进来的参数了
echo "version = $version"
 
#打开unity3d  执行MyEditorScript.MyMethod 方法。
#unity产生log就写在tmp/1.log里面，比如Debug.Log和Unity编辑器产生的。
$untiy -quit -batchmode -projectPath $projectPath -logFile /tmp/1.log  -executeMethod MyEditorScript.MyMethod "$version"

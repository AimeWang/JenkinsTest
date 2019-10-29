using UnityEditor;
using UnityEngine;

class MyEditorScript
{
    static void MyMethod()
    {
        string executeMethod = "MyEditorScript.MyMethod";
        foreach (string arg in System.Environment.GetCommandLineArgs())
        {
            Debug.Log(arg);
        }
    }
}
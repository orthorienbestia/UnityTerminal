using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEditor;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class TerminalWindow : EditorWindow
{
    private string command = "Hello World";
    private string output = "";

    private string userName="";
    private string pwdName;
    private string prefix;
    [MenuItem ("Window/Terminal Window")]
    public static void  ShowWindow () {
        var window = GetWindow(typeof(TerminalWindow)) as TerminalWindow;
        window.userName = ShellHelper.Instance.Bash( "whoami");
        window.pwdName = ShellHelper.Instance.Bash("pwd");
        window.prefix =  window.userName+ " :$"+window.pwdName;
    }
    private void OnGUI()
    {
        GUILayout.Label (userName, EditorStyles.boldLabel);
        command = EditorGUILayout.TextField (command);
        
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Run"))
        {
            
            output = ShellHelper.Instance.Bash(command);
            prefix =  userName+ " :$"+pwdName;
            Debug.Log(output);
        }
        EditorGUILayout.EndHorizontal();
    }
}

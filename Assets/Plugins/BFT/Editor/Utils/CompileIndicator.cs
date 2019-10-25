using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;

// I recommend dropping this script in an Editor folder.
// You should have two audio clips somewhere in the project.
// You'll need to edit-in the paths of those clips (from your project root folder) in the static initializer below.
// Example path: "Assets/Editor/CompileIndicator/start.mp3"

/// <summary>
///     Plays a sound effect when script compiling starts and ends.
/// </summary>
[InitializeOnLoad]
public static class CompileIndicator
{
    private const string CompileStatePrefsKey = "CompileIndicator.WasCompiling";
    private static readonly AudioClip StartClip;
    private static readonly AudioClip EndClip;

    static CompileIndicator()
    {
        EditorApplication.update += OnUpdate;
        StartClip = AssetDatabase.LoadAssetAtPath<AudioClip>("Assets/Plugins" +
                                                             "/Big Fat Tool/Editor/CompileStartSound.wav");
        EndClip = AssetDatabase.LoadAssetAtPath<AudioClip>("Assets/Plugins" +
                                                           "/Big Fat Tool/Editor/CompileEndSound.wav");
    }

    private static void OnUpdate()
    {
        var wasCompiling = EditorPrefs.GetBool(CompileStatePrefsKey);
        var isCompiling = EditorApplication.isCompiling;

        // Return early if compile status hasn't changed.
        if (wasCompiling == isCompiling)
            return;

        if (isCompiling)
            OnStartCompiling();
        else
            OnEndCompiling();

        EditorPrefs.SetBool(CompileStatePrefsKey, isCompiling);
    }

    private static void OnStartCompiling()
    {
        if (StartClip)
            PlayClip(StartClip);
    }

    private static void OnEndCompiling()
    {
        if (EndClip)
            PlayClip(EndClip);
    }

    private static void PlayClip(AudioClip clip)
    {
        Assembly unityEditorAssembly = typeof(AudioImporter).Assembly;
        Type audioUtilClass = unityEditorAssembly.GetType("UnityEditor.AudioUtil");
        MethodInfo method = audioUtilClass.GetMethod(
            "PlayClip",
            BindingFlags.Static | BindingFlags.Public,
            null,
            new[] {typeof(AudioClip)},
            null
        );
        if (method != null) method.Invoke(null, new object[] {clip});
    }
}
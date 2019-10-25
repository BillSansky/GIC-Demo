using BFT;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEditor;

public class BFTSettingsEditor : OdinEditorWindow
{
    private static BFTSettingsEditor instance;

    [ShowInInspector, HideLabel, InlineEditor(InlineEditorModes.FullEditor)]
    private BFTSettings settings;

    [MenuItem("BFT/BFT Settings", false)]
    public static void OpenBFTSettings()
    {
        instance = GetWindow<BFTSettingsEditor>();
        instance.settings = BFTSettings.Instance;
    }

    [OnInspectorGUI]
    private void CheckSettings()
    {
        if (!settings)
        {
            settings = BFTSettings.Instance;
        }
    }
}
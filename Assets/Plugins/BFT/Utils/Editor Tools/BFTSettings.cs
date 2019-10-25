using System.Collections.Generic;
using System.Linq;
using UnityEditor;
#if UNITY_EDITOR
using Sirenix.OdinInspector;

namespace BFT
{
    public class BFTSettings : SingletonSO<BFTSettings>
    {
        public static string CinemachineDefine = "BFT_CINE";
        public static string RewiredDefine = "BFT_REWIRED";
        public static string CurvyDefine = "BFT_CURVY";
        public static string DoTweenDefine = "BFT_DOTWEEN";
        public static string Puppet3DDefine = "BFT_PUPPET3D";
        public static string ObiDefine = "BFT_OBI";
        public static string TextMeshProDefine = "BFT_TEXTMESHPRO";
        public static string PostProcessDefine = "BFT_POSTPROCESS";
        public static string DeformDefine = "BFT_DEFORM";

        public bool UseCinemachine;
        public bool UseCurvy;
        public bool UseDeform;
        public bool UseDOTween;
        public bool UseObi;
        public bool UsePostProcessStack;
        public bool UsePuppet3D;
        public bool UseRewired;
        public bool UseTextMeshPro;

        static BFTSettings()
        {
            IsEditor = true;
        }


        [Button("Apply Changes", ButtonSizes.Medium)]
        private void UpdateDefineSymbols()
        {
            string definesString =
                PlayerSettings.GetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup);
            List<string> allDefines = definesString.Split(';').ToList();
            List<string> bftSymbols = new List<string>();

            ResolveBFTSymbol(UseCinemachine, CinemachineDefine, bftSymbols, allDefines);
            ResolveBFTSymbol(UseRewired, RewiredDefine, bftSymbols, allDefines);
            ResolveBFTSymbol(UseCurvy, CurvyDefine, bftSymbols, allDefines);
            ResolveBFTSymbol(UseDOTween, DoTweenDefine, bftSymbols, allDefines);
            ResolveBFTSymbol(UsePuppet3D, Puppet3DDefine, bftSymbols, allDefines);
            ResolveBFTSymbol(UseObi, ObiDefine, bftSymbols, allDefines);
            ResolveBFTSymbol(UseTextMeshPro, TextMeshProDefine, bftSymbols, allDefines);
            ResolveBFTSymbol(UsePostProcessStack, PostProcessDefine, bftSymbols, allDefines);
            ResolveBFTSymbol(UseDeform, DeformDefine, bftSymbols, allDefines);


            allDefines.AddRange(bftSymbols.Except(allDefines));
            PlayerSettings.SetScriptingDefineSymbolsForGroup(
                EditorUserBuildSettings.selectedBuildTargetGroup,
                string.Join(";", allDefines.ToArray()));
        }

        private void ResolveBFTSymbol(bool usage, string define, List<string> bftSymbols, List<string> allDefines)
        {
            if (usage)
            {
                bftSymbols.Add(define);
            }
            else
            {
                allDefines.Remove(define);
            }
        }
    }
}
#endif

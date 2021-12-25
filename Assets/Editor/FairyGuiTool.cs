using UnityEditor;

namespace CrashQuery.EditorTool
{
    public static class FairyGuiTool
    {
        [MenuItem("Tools/Framework/UI/打开编辑器 %#_e")]
        public static void OpenUiEditor()
        {
            EditorHelper.DoBat("Tools/FairyGUI-Editor/FairyGUI-Editor.exe", EditorHelper.GetProjPath("UIProject/CrashQueryUI.fairy"));
        }
    }
}
// Author: 
// Date:   2021.12.28
// Desc:

using System.Runtime.InteropServices;
using FairyGUI;
using UnityEngine;

namespace CrashQuery.Helper
{
    public class WebGLCopyAndPaste
    {
#if UNITY_WEBGL

        [DllImport("__Internal")]
        private static extern void initWebGLCopyAndPaste(StringCallback cutCopyCallback, StringCallback pasteCallback);

        [DllImport("__Internal")]
        private static extern void passCopyToBrowser(string str);
        
        [DllImport("__Internal")]
        private static extern void WebGLSaveFile(string str, string filename);
        delegate void StringCallback(string content);


        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
        private static void Init()
        {
            if (!Application.isEditor)
            {
                initWebGLCopyAndPaste(GetClipboard, ReceivePaste);
            }
        }

        private static void SendKey(string baseKey)
        {
            string appleKey = "%" + baseKey;
            string naturalKey = "^" + baseKey;

            var obj = Stage.inst.touchTarget;
            if (obj == null)
            {
                return;
            }

            /*var input = currentObj.GetComponent<UnityEngine.UI.InputField>();
            if (input != null)
            {
                // I don't know what's going on here. The code in InputField
                // is looking for ctrl-c but that fails on Mac Chrome/Firefox
                input.ProcessEvent(Event.KeyboardEvent(naturalKey));
                input.ProcessEvent(Event.KeyboardEvent(appleKey));
                // so let's hope one of these is basically a noop
                return;
            }*/
        }

      [AOT.MonoPInvokeCallback( typeof(StringCallback) )]
      private static void GetClipboard(string key)
      {
        //SendKey(key);
        passCopyToBrowser(GUIUtility.systemCopyBuffer);
      }

      [AOT.MonoPInvokeCallback( typeof(StringCallback) )]
      private static void ReceivePaste(string str)
      {
        GUIUtility.systemCopyBuffer = str;
      }


      public static void SaveFile(string data, string filename)
      {
          WebGLSaveFile(data,filename);
      }
#endif
    }
}
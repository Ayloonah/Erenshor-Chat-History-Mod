using BepInEx;
using BepInEx.Logging;
using UnityEngine;

namespace erenshor_chat_history_mod
{
    [BepInPlugin("com.ayloonah.erenshor_chat_history_mod", "Chat History Mod", "1.0.0")]
    public class Mod : BaseUnityPlugin
    {
        public static ManualLogSource Log;

        // This method is called when the mod is loaded
        public void Awake()
        {
            Log = Logger;

            Log.LogMessage("Chat History Mod loaded!");

            // Referring to the actual mod content
            GameObject go = new GameObject("ChatHistory");
            go.AddComponent<ChatHistory>();
            DontDestroyOnLoad(go);
        }

        // This method is called when the mod is unloaded
        public void OnDestroy()
        {
            Log.LogMessage("Chat History Mod unloaded!");
        }
    }
}

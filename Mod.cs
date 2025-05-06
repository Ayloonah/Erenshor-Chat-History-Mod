using BepInEx;
using UnityEngine;

namespace erenshor_chat_history_mod
{
    [BepInPlugin("com.ayloonah.erenshor_chat_history_mod", "Chat History Mod", "1.0.0")]
    public class Mod : BaseUnityPlugin
    {
        // This method is called when the mod is loaded
        public void Awake()
        {
            Logger.LogMessage("Chat History Mod loaded!");

            // Referring to the actual mod content
            GameObject go = new GameObject("ChatHistory");
            go.AddComponent<ChatHistory>();
            DontDestroyOnLoad(go);
        }

        // This method is called when the mod is unloaded
        public void OnDestroy()
        {
            Logger.LogMessage("Chat History Mod unloaded!");
        }
    }
}

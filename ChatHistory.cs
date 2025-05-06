using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace erenshor_chat_history_mod
{
    public class ChatHistory : MonoBehaviour
    {   
        // Creating a list to store sent messages Strings in
        private List<string> chatInputList = new List<string>();
        private int currentIndex = -1;
        private string sceneName;

        public void Update()
        {
            // Making sure we are in a scene in which there is a chat box
            sceneName = SceneManager.GetActiveScene().name;
            if (sceneName == "Menu" || sceneName == "LoadScene")
            {
                return;
            }
            
            // Finding the current chat box instance
            TypeText chatBox = UnityEngine.Object.FindObjectOfType<TypeText>();

            // Tracking button pressed
            bool isArrowUpPressed = Input.GetKeyDown(KeyCode.UpArrow);
            bool isArrowDownPressed = Input.GetKeyDown(KeyCode.DownArrow);
            
            // Capturing the player input after enter is pressed
            if (Input.GetKeyDown(KeyCode.Return))
            {
                // If the input is not empty, add it to the list, storing a maximum of 20
                string currentInputString = chatBox.typed.text.Trim();
                if (!string.IsNullOrWhiteSpace(currentInputString))
                {
                    if (chatInputList.Count == 20)
                    {
                        chatInputList.RemoveAt(0);
                    }
                    chatInputList.Add(currentInputString);
                }
            }

            // What to do if the up arrow is pressed and the list is not empty
            if (isArrowUpPressed && (chatInputList.Count > 0))
            {
                if (currentIndex == -1)
                {
                    currentIndex = chatInputList.Count - 1;
                }
                else if (currentIndex > 0)
                {
                    currentIndex--;
                }
                chatBox.typed.text = chatInputList[currentIndex];
            }

            // What to do if the down arrow is pressed and the list is not empty
            if (isArrowDownPressed && (chatInputList.Count > 0))
            {
                Mod.Log.LogMessage("Arrow down detected.");
                if (currentIndex == -1)
                {
                    chatBox.typed.text = "";
                }
                else if (currentIndex < (chatInputList.Count - 1))
                {
                    currentIndex++;
                    chatBox.typed.text = chatInputList[currentIndex];
                } else if (currentIndex == (chatInputList.Count - 1))
                {
                    currentIndex = -1;
                    chatBox.typed.text = "";
                }
            }

            // If the player presses enter, the current index is reset to -1
            if (Input.GetKeyDown(KeyCode.Return))
            {
                currentIndex = -1;
            }
        }
    }
}

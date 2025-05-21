using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace erenshor_chat_history_mod
{
    public class ChatHistory : MonoBehaviour {   
        // Creating a list to store sent messages Strings in
        private List<string> chatInputList = new List<string>();
        private int currentIndex = -1;
        private string sceneName;
        private TypeText chatBox;

        // When the mod loads, the chatbox object is updated based on scene name:
        private void Awake() {
            sceneName = SceneManager.GetActiveScene().name;
            UpdateChatBoxForScene(sceneName);
        }

        private void OnEnable() {
            SceneManager.activeSceneChanged += OnSceneChanged;
        }

        private void OnDisable() {
            SceneManager.activeSceneChanged -= OnSceneChanged;
        }

        // When the scene changes, the chatbox object is updated based on scene name:
        private void OnSceneChanged(Scene oldScene, Scene newScene) {
            sceneName = newScene.name;
            UpdateChatBoxForScene(sceneName);
        }

        // If the scene isn't Menu or LoadScene, finds the chatbox object:
        private void UpdateChatBoxForScene(string scene) {
            if (scene == "Menu" || scene == "LoadScene") {
                chatBox = null;
            } else {
                chatBox = UnityEngine.Object.FindObjectOfType<TypeText>();
            }
        }

        // Handles the chat input (meat and potatoes of the mod):
        public void Update()
        {
            // Checking to see if we have a chatbox cached:
            if (chatBox == null) {
                return;
            }

            // Tracking button pressed
            bool isArrowUpPressed = Input.GetKeyDown(KeyCode.UpArrow);
            bool isArrowDownPressed = Input.GetKeyDown(KeyCode.DownArrow);
            bool isReturnPressed = Input.GetKeyDown(KeyCode.Return);

            // Capturing the player input after enter is pressed
            if (isReturnPressed)
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
            if (isReturnPressed)
            {
                currentIndex = -1;
            }
        }
    }
}

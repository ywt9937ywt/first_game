using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CommandPattern.RebindKeys
{
    public class InputController : MonoBehaviour
    {
        public MoveObject object2Move;

        //Add commands here
        private Commands buttonW;

        private Stack<Commands> undoCommands = new Stack<Commands>();
        private Stack<Commands> redoCommands = new Stack<Commands>();

        private bool isReplaying = false;
        private Vector3 startPos;
        private const float REPLAY_INTERVAL_TIMER = 0.5f;

        private void Start()
        {
            //Initialize default commands

            startPos = object2Move.transform.position;
        }

        private void Update()
        {
            if (isReplaying) return;

            if (Input.GetKeyDown(KeyCode.U))
            {
                if(undoCommands.Count == 0)
                {
                    Debug.Log("Already at the start");
                }
                else
                {
                    Commands lastCommand = undoCommands.Pop();
                    lastCommand.Undo();
                    redoCommands.Push(lastCommand);
                }
            }
            else if (Input.GetKeyDown(KeyCode.R))
            {
                if (redoCommands.Count == 0)
                {
                    Debug.Log("Already at the start");
                }
                else
                {
                    Commands lastCommand = redoCommands.Pop();
                    lastCommand.Execute();
                    undoCommands.Push(lastCommand);
                }
            }
        }

        private void ExecuteCommand(Commands commandButton)
        {
            commandButton.Execute();
            undoCommands.Push(commandButton);
            redoCommands.Clear();
        }

    }
}


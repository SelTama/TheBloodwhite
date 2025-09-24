using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [Header("Runtime Data")]
    private RuntimeDialogueGraph _runtimeGraph;
    private Dictionary<string, RuntimeDialogueNode> _nodeLookup = new Dictionary<string, RuntimeDialogueNode>();
    private RuntimeDialogueNode _currentNode;

    [Header("UI Components")]
    public GameObject DialoguePanel;
    public TextMeshProUGUI SpeakerNameText;
    public TextMeshProUGUI DialogueText;

    [Header("Choice Button UI")]
    public Button ChoiceButtonPrefab;
    public Transform ChoiceButtonContainer;


    private void Start()
    {
        DialoguePanel.SetActive(false);

        //foreach (var node in RuntimeGraph.AllNodes) 
        //{
        //    _nodeLookup[node.NodeID] = node;
        //}
        ////if (!string.IsNullOrEmpty(RuntimeGraph.EntryNodeID))
        //{
        //    ShowNode(RuntimeGraph.EntryNodeID);
        //}
        //else 
        //{
        //    EndDialogue(); 
        //}

    }
    private void Update()
    {
        if (/*inDialogueRange &&*/ Keyboard.current.fKey.wasPressedThisFrame)
        {
            StartDialogue(_runtimeGraph);
        }

        //if (Mouse.current.leftButton.wasPressedThisFrame && _currentNode != null && _currentNode.Choices.Count == 0) 
        //{
        //    if (!string.IsNullOrEmpty(_currentNode.NextNodeID))
        //    {
        //        ShowNode(_currentNode.NextNodeID);
        //    }
        //    else
        //    {
        //        EndDialogue();
        //    }

        //}

    }

    private void ShowNode(string nodeID) 
    {
        if (!_nodeLookup.ContainsKey(nodeID)) 
        {
            EndDialogue();
            return;
        }

        _currentNode = _nodeLookup[nodeID];

        DialoguePanel.SetActive(true);
        SpeakerNameText.SetText(_currentNode.SpeakerName);
        DialogueText.SetText(_currentNode.DialogueText);

        foreach (Transform child in ChoiceButtonContainer) 
        {
            Destroy(child.gameObject);
        }

        if(_currentNode.Choices.Count > 0)
        {
            foreach (var choice in _currentNode.Choices)
            {
                Button button = Instantiate(ChoiceButtonPrefab, ChoiceButtonContainer);

                TextMeshProUGUI buttonText = button.GetComponentInChildren<TextMeshProUGUI>();

                if (buttonText != null)
                {
                    buttonText.text = choice.ChoiceText;
                }

                if (button != null)
                {
                    button.onClick.AddListener(() =>
                    {
                        if (!string.IsNullOrEmpty(choice.DestinationNodeID))
                        {
                            ShowNode(choice.DestinationNodeID);
                        }
                        else
                        {
                            EndDialogue();
                        }
                    });
                }             
            }
        }        
    }

    private void EndDialogue() 
    {
        DialoguePanel.SetActive(false);
        _currentNode = null;
        foreach (Transform child in ChoiceButtonContainer) 
        {
            Destroy(child.gameObject);
        
        }
    }

    public void StartDialogue(RuntimeDialogueGraph graph) 
    {
        _runtimeGraph = graph;
        _nodeLookup.Clear();

        foreach (var node in _runtimeGraph.AllNodes)
        {
            _nodeLookup[node.NodeID] = node;
        }

        if (!string.IsNullOrEmpty(_runtimeGraph.EntryNodeID))
        {
            ShowNode(_runtimeGraph.EntryNodeID);
        }

        else
        {
            EndDialogue();
        }
    }   
}

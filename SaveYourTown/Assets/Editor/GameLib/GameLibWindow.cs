using GameLib.Generator;
using System;
using UnityEditor;
using UnityEngine;

public class GameLibWindow : EditorWindow
{
    string[] _choices = new[] { "Player", "AI character" };
    private int _choiceIndex = 0;

    #region player setting
    private PlayerClassGenerator playerClassGenerator = new PlayerClassGenerator();
    #endregion

    #region AI character setting
    private AICharacterGenerator aiCharacterClassGenerator = new AICharacterGenerator();
    private Boolean removeItemFromList = false;
    private int selGridInt = 0;
    private string brainName;
    #endregion

    private void OnEnable()
    {
        aiCharacterClassGenerator.Brains.Add("Idle");
        aiCharacterClassGenerator.Brains.Add("Move");
        aiCharacterClassGenerator.Brains.Add("Attack");

        playerClassGenerator.NameOfClass = (playerClassGenerator.NameOfClass.Equals("")) ? "Player" : playerClassGenerator.NameOfClass;
        playerClassGenerator.NameOfNamespace = Application.productName;
        aiCharacterClassGenerator.NameOfNamespace = Application.productName;
        aiCharacterClassGenerator.NameOfClass = (aiCharacterClassGenerator.NameOfClass.Equals("")) ? "AICharacter" : aiCharacterClassGenerator.NameOfClass;
    }

    void OnGUI()
    {
        _choiceIndex = EditorGUILayout.Popup("Type of class", _choiceIndex, _choices);
        
        switch (_choiceIndex)
        {
            case 0:
                showPlayerOptions();
                break;
            case 1:
                showAICharacterOptions();
                break;
            default:

                break;
        }
    }

    private void showPlayerOptions()
    {
        GUILayout.Label("Name of your namespace", EditorStyles.boldLabel);
        playerClassGenerator.NameOfNamespace = EditorGUILayout.TextField("Text Field", playerClassGenerator.NameOfNamespace);

        GUILayout.Label("Name of your class", EditorStyles.boldLabel);
        playerClassGenerator.NameOfClass = EditorGUILayout.TextField("Text Field", playerClassGenerator.NameOfClass);

        playerClassGenerator.WithCustomStates = EditorGUILayout.BeginToggleGroup("With custom states", playerClassGenerator.WithCustomStates);
        playerClassGenerator.PlayerStateAttack = EditorGUILayout.Toggle("Attack", playerClassGenerator.PlayerStateAttack);
        playerClassGenerator.PlayerStateDeath = EditorGUILayout.Toggle("Death", playerClassGenerator.PlayerStateDeath);
        playerClassGenerator.PlayerStateFall = EditorGUILayout.Toggle("Fall", playerClassGenerator.PlayerStateFall);
        playerClassGenerator.PlayerStateHit = EditorGUILayout.Toggle("Hit", playerClassGenerator.PlayerStateHit);
        playerClassGenerator.PlayerStateIdle = EditorGUILayout.Toggle("Idle", playerClassGenerator.PlayerStateIdle);
        playerClassGenerator.PlayerStateJumpUp = EditorGUILayout.Toggle("Jump Up", playerClassGenerator.PlayerStateJumpUp);
        playerClassGenerator.PlayerStateMove = EditorGUILayout.Toggle("Move", playerClassGenerator.PlayerStateMove);
        EditorGUILayout.EndToggleGroup();
        if (GUILayout.Button("Generate"))
        {
            bool doGenerationOfPlayer = true;
            if (playerClassGenerator.WithCustomStates && (
                !playerClassGenerator.PlayerStateAttack &&
                !playerClassGenerator.PlayerStateDeath &&
                !playerClassGenerator.PlayerStateFall &&
                !playerClassGenerator.PlayerStateHit &&
                !playerClassGenerator.PlayerStateIdle &&
                !playerClassGenerator.PlayerStateJumpUp &&
                !playerClassGenerator.PlayerStateMove))
            {
                doGenerationOfPlayer = EditorUtility.DisplayDialog("Place Selection 1 Or More States",
                    "You did not select 1 or more states you want to customize, or deselect \"With custom states\"", 
                    "No custom states needed", 
                    "I will select states");
                if(doGenerationOfPlayer)
                {
                    playerClassGenerator.WithCustomStates = false;
                }
            }
            if (doGenerationOfPlayer)
            {
                playerClassGenerator.generatePlayerClass();
            }
        }
    }
    
    private void showAICharacterOptions()
    {
        GUILayout.BeginVertical("Box");
        GUILayout.Label("AI character class setting", EditorStyles.boldLabel);
        aiCharacterClassGenerator.NameOfNamespace = EditorGUILayout.TextField("Namespace", aiCharacterClassGenerator.NameOfNamespace);
        aiCharacterClassGenerator.NameOfClass = EditorGUILayout.TextField("Name", aiCharacterClassGenerator.NameOfClass);
        GUILayout.EndVertical();

        GUILayout.BeginVertical("Box");
        GUILayout.Label("Parent class setting", EditorStyles.boldLabel);
        aiCharacterClassGenerator.NamespaceOfParentClass = EditorGUILayout.TextField("Namespace", aiCharacterClassGenerator.NamespaceOfParentClass);
        aiCharacterClassGenerator.NameOfParentClass = EditorGUILayout.TextField("Class", aiCharacterClassGenerator.NameOfParentClass);

        if (GUILayout.Button("Select Parent Class"))
        {
            string path = EditorUtility.OpenFilePanel("Select parent class of the AI character", "", "cs");
            if (path.Length != 0)
            {
                string[] stringSeparators = new string[] { "/", "." };
                string[] parts = path.Split(stringSeparators, StringSplitOptions.None);

                if (parts[parts.Length - 3].Equals("NonPlayerCharacter"))
                {
                    aiCharacterClassGenerator.NamespaceOfParentClass = parts[parts.Length - 5];
                    aiCharacterClassGenerator.NameOfParentClass = parts[parts.Length - 2];
                }
            }
        }
        GUILayout.EndVertical();

        GUILayout.BeginVertical("Box");
        GUILayout.Label("Brain setting", EditorStyles.boldLabel);
        brainName = EditorGUILayout.TextField("Name of brain", brainName);
        if (GUILayout.Button("Add brain"))
        {
            aiCharacterClassGenerator.Brains.Add(brainName);
        }

        GUILayout.BeginVertical("Box");
        GUILayout.Label("Brain list", EditorStyles.boldLabel);
        selGridInt = GUILayout.SelectionGrid(selGridInt, aiCharacterClassGenerator.Brains.ToArray(), 1);
        GUILayout.EndVertical();

        if (GUILayout.Button("Remove brain from list"))
        {
            if (selGridInt >= 0 && selGridInt < aiCharacterClassGenerator.Brains.Count)
            {
                removeItemFromList = EditorUtility.DisplayDialog("Removing brain from list",
                        "Do you really want to remove the selected item?",
                        "Yes, remove " + aiCharacterClassGenerator.Brains[selGridInt],
                        "Don't remove it!");
                if (removeItemFromList)
                {
                    aiCharacterClassGenerator.Brains.RemoveAt(selGridInt);
                }
            }
        }
        GUILayout.EndVertical();

        if (GUILayout.Button("Generate"))
        {
            if(aiCharacterClassGenerator.Brains.Count == 0)
            {
                if (EditorUtility.DisplayDialog("Brains are missing", "Add one brain to the character or use the default brains", "Use default", "I will add a brain"))
                {
                    aiCharacterClassGenerator.UseDefaultBrains = true;
                }
            }
            if(aiCharacterClassGenerator.UseDefaultBrains ||
                aiCharacterClassGenerator.Brains.Count > 0)
            {
                aiCharacterClassGenerator.generateAICharacterClass();
            }
        }
    }

    [MenuItem("Window/GameLib")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(GameLibWindow));
    }
}

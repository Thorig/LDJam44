#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;

public class AnimationTransitionSetter : MonoBehaviour
{
    private static void setHashTable(out Hashtable stateConditionTable)
    {
        Dictionary<string, float> dict = new Dictionary<string, float>();
        
        addStateConditions(dict);

        stateConditionTable = new Hashtable(dict);
    }

    protected static void addStateConditions(Dictionary<string, float> dict)
    {
        dict.Add("idle", 0); //ANIMATION_IDLE = 0;
        dict.Add("walk", 1); //ANIMATION_WALK = 1;
        dict.Add("jump", 2); //ANIMATION_JUMPUP = 2;
        dict.Add("jump up", 2); //ANIMATION_JUMPUP = 2;
        dict.Add("jumpup", 2); //ANIMATION_JUMPUP = 2;
        dict.Add("fall", 3); //ANIMATION_FALL = 3;
        dict.Add("hitted", 4); //ANIMATION_HITTED = 4;
        dict.Add("attack", 5); //ANIMATION_ATTACK = 5;
        dict.Add("death", 6); //ANIMATION_DEATH = 6;

        dict.Add("jump kick", 7); //ANIMATION_JUMP_KICK = 7;
        dict.Add("jumpkick", 7); //ANIMATION_JUMP_KICK = 7;
        dict.Add("kick attack", 8); //ANIMATION_KICK_ATTACK = 8;
        dict.Add("kickattack", 8); //ANIMATION_KICK_ATTACK = 8;
        dict.Add("attack2", 9); //ANIMATION_PUNCH_002 = 9;
        dict.Add("attack3", 10); //ANIMATION_PUNCH_003 = 10;
        dict.Add("knocked down", 11); //ANIMATION_KNOCKED_DOWN = 11;
        dict.Add("get up", 12); //ANIMATION_GET_UP = 12;

        dict.Add("walk backwards", 13); //ANIMATION_WALK_BACKWARDS = 13;
        dict.Add("run", 14); //ANIMATION_RUN = 14;

        dict.Add("defend", 1000); //ANIMATION_RUN = 14;
    }

    [MenuItem("Animator/Set Transitions for states &0")]
    public static void ConvertTransitionToContinue()
    {
        Hashtable stateConditionTable = new Hashtable();
        AnimatorStateMachine stateMachine = null;

        setHashTable(out stateConditionTable);

        List<string> nameOfStates = new List<string>();
        List<AnimatorStateTransition> animatorStateTransitionList = new List<AnimatorStateTransition>();

        foreach (Object obj in Selection.objects)
        {
            if (obj.GetType() == typeof(AnimatorStateMachine))
            {
                stateMachine = (obj as AnimatorStateMachine);

                foreach (ChildAnimatorState state in stateMachine.states)
                {
                    if (animatorStateTransitionList.Count > 0)
                    {
                        animatorStateTransitionList.RemoveRange(0, animatorStateTransitionList.Count);
                    }

                    if (nameOfStates.Count > 0)
                    {
                        nameOfStates.RemoveRange(0, nameOfStates.Count);
                    }
                    
                    foreach (AnimatorStateTransition stateTransition in state.state.transitions)
                    {
                        nameOfStates.Add(stateTransition.destinationState.name);
                        setTransition(stateTransition, 
                            (float)stateConditionTable[stateTransition.destinationState.name.ToLower()]);
                        animatorStateTransitionList.Add(stateTransition);
                    }
                    foreach (ChildAnimatorState _state in stateMachine.states)
                    {
                        if (_state.state.name.CompareTo(state.state.name) != 0 && !nameOfStates.Contains(_state.state.name))
                        {
                            AnimatorStateTransition newState = new AnimatorStateTransition();
                            newState.destinationState = _state.state;
                            animatorStateTransitionList.Add(newState);
                            setTransition(newState, (float)stateConditionTable[_state.state.name.ToLower()]);
                            if (AssetDatabase.GetAssetPath(stateMachine) != "")
                                AssetDatabase.AddObjectToAsset(newState, AssetDatabase.GetAssetPath(stateMachine));
                        }
                    }
                    state.state.transitions = animatorStateTransitionList.ToArray();
                    AssetDatabase.SaveAssets();
                    AssetDatabase.Refresh();
                }
            }
        }
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    protected static void setTransition(AnimatorStateTransition transition, float stateId)
    {
        transition.hasExitTime = false;
        transition.exitTime = 0;
        transition.hasFixedDuration = true;
        transition.duration = 0;


        transition.conditions = new AnimatorCondition[1] { getAnimatorCondition(transition.destinationState.name,
            stateId) };
    }

    protected static AnimatorCondition getAnimatorCondition(string destinationStateName, float stateId)
    {
        AnimatorCondition condition = new AnimatorCondition();
        condition.mode = AnimatorConditionMode.Equals;
        condition.parameter = "State";
        condition.threshold = stateId;
        return condition;
    }
}
#endif
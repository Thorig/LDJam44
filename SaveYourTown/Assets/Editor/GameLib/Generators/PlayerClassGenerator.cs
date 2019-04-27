using System.IO;
using UnityEditor;
using UnityEngine;

namespace GameLib.Generator
{
    public class PlayerClassGenerator
    {
        private string nameOfClass = "";
        private string nameOfNamespace = "";
        private bool withCustomStates = false;
        private bool playerStateAttack = false;
        private bool playerStateDeath = false;
        private bool playerStateFall = false;
        private bool playerStateHit = false;
        private bool playerStateIdle = false;
        private bool playerStateJumpUp = false;
        private bool playerStateMove = false;

        #region props of attributes
        public string NameOfClass
        {
            get
            {
                return nameOfClass;
            }

            set
            {
                nameOfClass = value;
            }
        }

        public string NameOfNamespace
        {
            get
            {
                return nameOfNamespace;
            }

            set
            {
                nameOfNamespace = value;
            }
        }

        public bool WithCustomStates
        {
            get
            {
                return withCustomStates;
            }

            set
            {
                withCustomStates = value;
            }
        }

        public bool PlayerStateAttack
        {
            get
            {
                return playerStateAttack;
            }

            set
            {
                playerStateAttack = value;
            }
        }

        public bool PlayerStateDeath
        {
            get
            {
                return playerStateDeath;
            }

            set
            {
                playerStateDeath = value;
            }
        }

        public bool PlayerStateFall
        {
            get
            {
                return playerStateFall;
            }

            set
            {
                playerStateFall = value;
            }
        }

        public bool PlayerStateHit
        {
            get
            {
                return playerStateHit;
            }

            set
            {
                playerStateHit = value;
            }
        }

        public bool PlayerStateIdle
        {
            get
            {
                return playerStateIdle;
            }

            set
            {
                playerStateIdle = value;
            }
        }

        public bool PlayerStateJumpUp
        {
            get
            {
                return playerStateJumpUp;
            }

            set
            {
                playerStateJumpUp = value;
            }
        }

        public bool PlayerStateMove
        {
            get
            {
                return playerStateMove;
            }

            set
            {
                playerStateMove = value;
            }
        }
        #endregion

        public void generatePlayerClass()
        {
            Debug.Log("generatePlayerClass");
            string pathString = generateFoldersForPlayer();
            standardPlayerClass(pathString);
            if (withCustomStates)
            {
                generateStateAssets(pathString);
            }
            AssetDatabase.Refresh();
        }

        private void generateStateAssets(string pathString)
        {
            StateClassGenerator stateClassGenerator = new StateClassGenerator();
            stateClassGenerator.NameOfNamespace = NameOfNamespace;
            stateClassGenerator.PlayerStateAttack = playerStateAttack;
            stateClassGenerator.PlayerStateDeath = playerStateDeath;
            stateClassGenerator.PlayerStateFall = playerStateFall;
            stateClassGenerator.PlayerStateHit = playerStateHit;
            stateClassGenerator.PlayerStateIdle = playerStateIdle;
            stateClassGenerator.PlayerStateJumpUp = playerStateJumpUp;
            stateClassGenerator.PlayerStateMove = playerStateMove;

            BehaviourStateFactoryGenerator behaviourStateFactoryGenerator = new BehaviourStateFactoryGenerator();
            pathString = behaviourStateFactoryGenerator.generateFolderForBehaviourStateFactory(pathString);
            behaviourStateFactoryGenerator.generateFactory(pathString, stateClassGenerator);
            
            pathString = stateClassGenerator.generateFolderForStates(pathString);
            stateClassGenerator.generateStates(pathString, false);
        }
        
        private string generateFoldersForPlayer()
        {
            string folderName = "Assets/";
            string pathString = Path.Combine(folderName, "Script/");
            Directory.CreateDirectory(pathString);
            pathString = Path.Combine(pathString, nameOfNamespace + "/");
            Directory.CreateDirectory(pathString);
            pathString = Path.Combine(pathString, "Entity/");
            Directory.CreateDirectory(pathString);
            return pathString;
        }

        private void standardPlayerClass(string pathString)
        {
            string copyPath = pathString + nameOfClass + ".cs";
            Debug.Log("Creating Classfile: " + copyPath);
            if (!File.Exists(copyPath))
            { 
                using (StreamWriter outfile =
                    new StreamWriter(copyPath))
                {
                    if (withCustomStates)
                    {
                        outfile.WriteLine("using " + nameOfNamespace + ".Entity.Behaviour;");
                    }
                    if (!nameOfClass.Equals("Player"))
                    {
                        outfile.WriteLine("using GameLib.Entity;\n");
                    }

                    outfile.WriteLine("namespace " + nameOfNamespace + ".Entity");
                    outfile.WriteLine("{");
                    if (nameOfClass.Equals("Player"))
                    {
                        outfile.WriteLine("\tpublic class " + nameOfClass + " : GameLib.Entity.Player");
                    }
                    else
                    {
                        outfile.WriteLine("\tpublic class " + nameOfClass + " : Player");
                    }
                    outfile.WriteLine("\t{");
                    outfile.WriteLine("\t\t// Use this for initialization");
                    outfile.WriteLine("\t\tprotected override void init()");
                    outfile.WriteLine("\t\t{");
                    outfile.WriteLine("\t\t\tbase.init();");
                    outfile.WriteLine("\t\t\t//Initialize your attributes or other necessities below.\n");
                    outfile.WriteLine("\t\t}\n");

                    if (withCustomStates)
                    {
                        outfile.WriteLine("\t\t//If the following function is removed to standard factory will be used.");
                        outfile.WriteLine("\t\tprotected override void setEntity()");
                        outfile.WriteLine("\t\t{");
                        outfile.WriteLine("\t\t\tsetEntity(new BehaviourStateFactory());");
                        outfile.WriteLine("\t\t}\n");
                    }

                    outfile.WriteLine("\t\tpublic override void FixedUpdate()");
                    outfile.WriteLine("\t\t{");
                    outfile.WriteLine("\t\t\tbase.FixedUpdate();");
                    outfile.WriteLine("\t\t}");
                    outfile.WriteLine("\t}");
                    outfile.WriteLine("}");
                }//File written
            }
        }
    }
}
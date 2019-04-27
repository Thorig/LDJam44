using System.IO;
using UnityEngine;

namespace GameLib.Generator
{
    public class StateClassGenerator
    {
        private string nameOfNamespace = "";
        private bool playerStateAttack = false;
        private bool playerStateDeath = false;
        private bool playerStateFall = false;
        private bool playerStateHit = false;
        private bool playerStateIdle = false;
        private bool playerStateJumpUp = false;
        private bool playerStateMove = false;

        #region props of attributes
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
        #endregion

        public string generateFolderForStates(string pathString)
        {
            pathString = Path.Combine(pathString, "State/");
            Directory.CreateDirectory(pathString);
            return pathString;
        }

        public void generateStates(string pathString, bool generateFullPath)
        {
            if (playerStateIdle)
            {
                generateState(pathString, "Idle");
            }
            if (playerStateAttack)
            {
                generateState(pathString, "Attack");
            }
            if (playerStateDeath)
            {
                generateState(pathString, "Death");
            }
            if (playerStateFall)
            {
                generateState(pathString, "Fall");
            }
            if (playerStateHit)
            {
                generateState(pathString, "Hit");
            }
            if (playerStateJumpUp)
            {
                generateState(pathString, "JumpUp");
            }
            if (playerStateMove)
            {
                generateState(pathString, "Move");
            }
        }

        private void generateState(string pathString, string fileName)
        {
            string copyPath = pathString + fileName + ".cs";
            Debug.Log("Creating Classfile: " + copyPath);
            if (!File.Exists(copyPath))
            { 
                using (StreamWriter outfile =
                    new StreamWriter(copyPath))
                {
                    outfile.WriteLine("using GameLib.Entity.Behaviour;");
                    outfile.WriteLine("");

                    outfile.WriteLine("namespace " + NameOfNamespace + ".Entity.Behaviour.State");
                    outfile.WriteLine("{");

                    outfile.WriteLine("\tpublic class " + fileName + " : GameLib.Entity.Behaviour.State." + fileName);

                    outfile.WriteLine("\t{");
                    outfile.WriteLine("\t\tpublic override void update(IEntity entity)");
                    outfile.WriteLine("\t\t{");
                    outfile.WriteLine("\t\t\tbase.update(entity);");
                    outfile.WriteLine("\t\t\t//Add your code below\n");
                    outfile.WriteLine("\t\t}");

                    outfile.WriteLine("\t}");
                    outfile.WriteLine("}");
                }//File written
            }
        }
    }
}
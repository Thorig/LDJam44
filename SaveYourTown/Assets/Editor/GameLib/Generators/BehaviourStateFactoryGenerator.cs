using System.IO;
using UnityEngine;

namespace GameLib.Generator
{
    class BehaviourStateFactoryGenerator
    {
        public void generateFactory(string pathString, StateClassGenerator stateClassGenerator)
        {
            string copyPath = pathString + "BehaviourStateFactory.cs";
            Debug.Log("Creating Classfile: " + copyPath);
            if (!File.Exists(copyPath))
            { 
                addNewFactory(copyPath, stateClassGenerator);
            }
            else
            {

            }
        }

        private void addNewMethodes(string copyPath, StateClassGenerator stateClassGenerator)
        {

        }

        private void addNewFactory(string copyPath, StateClassGenerator stateClassGenerator)
        {
            using (StreamWriter outfile =
                    new StreamWriter(copyPath))
            {
                outfile.WriteLine("using GameLib.Entity.Behaviour;");
                outfile.WriteLine("using " + stateClassGenerator.NameOfNamespace + ".Entity.Behaviour.State;");
                outfile.WriteLine("");

                outfile.WriteLine("namespace " + stateClassGenerator.NameOfNamespace + ".Entity.Behaviour");
                outfile.WriteLine("{");

                outfile.WriteLine("\tpublic class BehaviourStateFactory : GameLib.Entity.Behaviour.BehaviourStateFactory");

                outfile.WriteLine("\t{");

                if (stateClassGenerator.PlayerStateIdle)
                {
                    addStateCreationFunction(outfile, "Idle");
                }
                if (stateClassGenerator.PlayerStateAttack)
                {
                    addStateCreationFunction(outfile, "Attack");
                }
                if (stateClassGenerator.PlayerStateDeath)
                {
                    addStateCreationFunction(outfile, "Death");
                }
                if (stateClassGenerator.PlayerStateFall)
                {
                    addStateCreationFunction(outfile, "Fall");
                }
                if (stateClassGenerator.PlayerStateHit)
                {
                    addStateCreationFunction(outfile, "Hit");
                }
                if (stateClassGenerator.PlayerStateJumpUp)
                {
                    addStateCreationFunction(outfile, "JumpUp");
                }
                if (stateClassGenerator.PlayerStateMove)
                {
                    addStateCreationFunction(outfile, "Move");
                }

                outfile.WriteLine("\t}");
                outfile.WriteLine("}");
            }//File written
        }

        private void addStateCreationFunction(StreamWriter outfile, string nameOfState)
        {
            outfile.WriteLine("\t\tpublic override AbstractState get" + nameOfState + "State(IEntity entity)");
            outfile.WriteLine("\t\t{");
            outfile.WriteLine("\t\t\tif (entity" + nameOfState + " == null)");
            outfile.WriteLine("\t\t\t{");
            outfile.WriteLine("\t\t\t\tentity" + nameOfState + " = new " + nameOfState + "();");
            outfile.WriteLine("\t\t\t}");
            outfile.WriteLine("\t\t\tentity" + nameOfState + ".init(entity);");
            outfile.WriteLine("\t\t\treturn entity" + nameOfState + ";");
            outfile.WriteLine("\t\t}");
            outfile.WriteLine("");
        }

        public string generateFolderForBehaviourStateFactory(string pathString)
        {
            pathString = Path.Combine(pathString, "Behaviour/");
            Directory.CreateDirectory(pathString);
            return pathString;
        }

    }
}

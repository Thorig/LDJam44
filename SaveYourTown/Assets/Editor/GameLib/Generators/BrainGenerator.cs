using System.IO;
using UnityEditor;
using UnityEngine;

namespace GameLib.Generator
{
    public class BrainGenerator
    {
        private string nameOfNamespace = "";
        private string nameOfClass = "";
        private string nameOfAICharacterClass = "";

        #region props for attributes
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

        public string NameOfAICharacterClass
        {
            get
            {
                return nameOfAICharacterClass;
            }

            set
            {
                nameOfAICharacterClass = value;
            }
        }
        #endregion

        public void generateBrainClass()
        {
            Debug.Log("generateBrainClass");
            string pathString = generateFoldersForBrain();
            standardBrainClass(pathString);
            AssetDatabase.Refresh();
        }

        private string generateFoldersForBrain()
        {
            string folderName = "Assets/";
            string pathString = Path.Combine(folderName, "Script/");
            Directory.CreateDirectory(pathString);
            pathString = Path.Combine(pathString, NameOfNamespace + "/");
            Directory.CreateDirectory(pathString);
            pathString = Path.Combine(pathString, "Entity/");
            pathString = Path.Combine(pathString, "NonPlayerCharacter/");
            pathString = Path.Combine(pathString, "StateMachine/");
            pathString = Path.Combine(pathString, "Logic/");
            pathString = Path.Combine(pathString, nameOfAICharacterClass + "/");
            Directory.CreateDirectory(pathString);
            return pathString;
        }

        private void standardBrainClass(string pathString)
        {
            string copyPath = pathString + NameOfClass + ".cs";
            Debug.Log("Creating Classfile: " + copyPath);

            if (!File.Exists(copyPath))
            {
                using (StreamWriter outfile =
                    new StreamWriter(copyPath))
                {
                    outfile.WriteLine("using GameLib.Entity.NonPlayerCharacter.StateMachine.Logic;\n");
                    outfile.WriteLine("using UnityEngine;\n");
                    outfile.WriteLine("namespace " + nameOfNamespace + ".Entity.NonPlayerCharacter.StateMachine.Logic." + nameOfAICharacterClass);
                    outfile.WriteLine("{");

                    outfile.WriteLine("\tpublic class " + NameOfClass + " : IBrain");
                    outfile.WriteLine("\t{");
                    outfile.WriteLine("\t\tprivate GameObject player;\n");
                    outfile.WriteLine("\t\tpublic override void init(GameLib.Entity.NonPlayerCharacter.AICharacter character)");
                    outfile.WriteLine("\t\t{");
                    outfile.WriteLine("\t\t\tbase.init(character);");
                    outfile.WriteLine("\t\t\tplayer = GameObject.FindGameObjectWithTag(\"Player\");");
                    outfile.WriteLine("\t\t\t//Initialize your attributes or other necessities below.\n");
                    outfile.WriteLine("\t\t}\n");

                    outfile.WriteLine("\t\tpublic override void update(GameLib.Entity.NonPlayerCharacter.AICharacter character)");
                    outfile.WriteLine("\t\t{");
                    outfile.WriteLine("\t\t\tbase.update(character);");
                    outfile.WriteLine("\t\t\t//Add your code below.\n");
                    outfile.WriteLine("\t\t}");

                    outfile.WriteLine("\t}");
                    outfile.WriteLine("}");
                }//File written
            }
        }
    }
}
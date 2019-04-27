using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace GameLib.Generator
{
    class AICharacterGenerator
    {
        private string nameOfNamespace = "";
        private bool withCustomBrains = false;
        private string nameOfClass = "";
        private string nameOfParentClass = "";
        private string namespaceOfParentClass = "";
        private bool useDefaultBrains = false;
        private List<string> brains = new List<string>();

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

        public bool WithCustomBrains
        {
            get
            {
                return withCustomBrains;
            }

            set
            {
                withCustomBrains = value;
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

        public string NameOfParentClass
        {
            get
            {
                return nameOfParentClass;
            }

            set
            {
                nameOfParentClass = value;
            }
        }

        public string NamespaceOfParentClass
        {
            get
            {
                return namespaceOfParentClass;
            }

            set
            {
                namespaceOfParentClass = value;
            }
        }

        public List<string> Brains
        {
            get
            {
                return brains;
            }

            set
            {
                brains = value;
            }
        }

        public bool UseDefaultBrains
        {
            get
            {
                return useDefaultBrains;
            }

            set
            {
                useDefaultBrains = value;
            }
        }
        #endregion

        public AICharacterGenerator()
        {
            namespaceOfParentClass= "GameLib";
            nameOfParentClass = "AICharacter";
        }

        public void generateAICharacterClass()
        {
            Debug.Log("generateAICharacterClass");
            string pathString = generateFoldersForAICharacter();
            standardAICharacterClass(pathString);
            //if (withCustomBrains)
            {
                generateBrainAssets(pathString);
            }
            AssetDatabase.Refresh();
        }

        private string generateFoldersForAICharacter()
        {
            string folderName = "Assets/";
            string pathString = Path.Combine(folderName, "Script/");
            Directory.CreateDirectory(pathString);
            pathString = Path.Combine(pathString, NameOfNamespace + "/");
            Directory.CreateDirectory(pathString);
            pathString = Path.Combine(pathString, "Entity/");
            pathString = Path.Combine(pathString, "NonPlayerCharacter/");            
            Directory.CreateDirectory(pathString);
            return pathString;
        }

        private void standardAICharacterClass(string pathString)
        {
            string copyPath = pathString + NameOfClass + ".cs";
            Debug.Log("Creating Classfile: " + copyPath);

            if (!File.Exists(copyPath))
            {
                using (StreamWriter outfile =
                    new StreamWriter(copyPath))
                {
                    if (withCustomBrains)
                    {
                        outfile.WriteLine("using " + nameOfNamespace + ".Entity.Behaviour;");
                    }
                    if (!nameOfNamespace.Equals(namespaceOfParentClass))
                    {
                        Debug.Log(nameOfNamespace + "   " + namespaceOfParentClass);
                        outfile.WriteLine("using " + namespaceOfParentClass + ".Entity.NonPlayerCharacter;");
                    }
                    outfile.WriteLine("using " + nameOfNamespace + ".Entity.NonPlayerCharacter.StateMachine.Logic." + NameOfClass + ";");
                    outfile.WriteLine("using GameLib.Entity.NonPlayerCharacter.StateMachine;\n");
                    outfile.WriteLine("namespace " + nameOfNamespace + ".Entity.NonPlayerCharacter");
                    outfile.WriteLine("{");
                    if (NameOfClass.Equals(nameOfParentClass))
                    {
                        outfile.WriteLine("\tpublic class " + NameOfClass + " : " + namespaceOfParentClass + ".Entity.NonPlayerCharacter." + nameOfParentClass);
                    }
                    else
                    {
                        outfile.WriteLine("\tpublic class " + NameOfClass + " : " + nameOfParentClass);
                    }
                    outfile.WriteLine("\t{");
                    outfile.WriteLine("\t\t// Use this for initialization");
                    outfile.WriteLine("\t\tprotected override void init()");
                    outfile.WriteLine("\t\t{");
                    outfile.WriteLine("\t\t\tbase.init();");
                    outfile.WriteLine("\t\t\tIAIControllerFactory controllerFactory = AIControllerFactory.getEntity();");
                    outfile.WriteLine("\t\t\taiCharacterController = controllerFactory.getAICharacterController(this, new BrainFactory());");
                    outfile.WriteLine("\t\t\t//Initialize your attributes or other necessities below.\n");
                    outfile.WriteLine("\t\t}\n");

                    outfile.WriteLine("\t\tprotected override void fixedUpdateBody()");
                    outfile.WriteLine("\t\t{");
                    outfile.WriteLine("\t\t\tbase.fixedUpdateBody();");
                    outfile.WriteLine("\t\t\t//Add your code below.\n");
                    outfile.WriteLine("\t\t}");
                    outfile.WriteLine("\t}");
                    outfile.WriteLine("}");
                }//File written
            }
        }

        private void generateBrainAssets(string pathString)
        {
            Debug.Log("generateBrainAssets");
            BrainGenerator brainGenerator = new BrainGenerator();
            brainGenerator.NameOfNamespace = nameOfNamespace;
            brainGenerator.NameOfAICharacterClass = nameOfClass;
            foreach (string brainName in brains)
            {
                Debug.Log("generateBrainAssets " + brainName);
                brainGenerator.NameOfClass = brainName;
                brainGenerator.generateBrainClass();
            }

            BrainFactoryGenerator brainFactoryGenerator = new BrainFactoryGenerator();
            brainFactoryGenerator.NameOfNamespace = nameOfNamespace;
            brainFactoryGenerator.NameOfAICharacterClass = nameOfClass;
            brainFactoryGenerator.NameOfClass = "BrainFactory";
            brainFactoryGenerator.Brains = brains;
            brainFactoryGenerator.generateBrainFactoryClass();
        }
    }
}

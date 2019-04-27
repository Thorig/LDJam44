using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class BrainFactoryGenerator
{
    private string nameOfNamespace = "";
    private string nameOfClass = "";
    private string nameOfAICharacterClass = "";
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
    #endregion

    public void generateBrainFactoryClass()
    {
        Debug.Log("generateBrainFactoryClass");
        string pathString = generateFoldersForBrainFactory();
        standardBrainFactoryClass(pathString);
        AssetDatabase.Refresh();
    }

    private string generateFoldersForBrainFactory()
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

    private void standardBrainFactoryClass(string pathString)
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

                outfile.WriteLine("\tpublic class BrainFactory : GameLib.Entity.NonPlayerCharacter.StateMachine.Logic.BrainFactory");
                outfile.WriteLine("\t{");

                foreach (string brainName in brains)
                {
                    if(brainName.Equals("NPCNoBrain"))
                    {
                        outfile.WriteLine("\t\tpublic override IBrain getNoBrain(GameLib.Entity.NonPlayerCharacter.AICharacter character)");
                        outfile.WriteLine("\t\t{");
                        outfile.WriteLine("\t\t\tIBrain noBrain = new NPCNoBrain();");
                        outfile.WriteLine("\t\t\tnoBrain.init(character); ");
                        outfile.WriteLine("\t\t\treturn noBrain; ");
                        outfile.WriteLine("\t\t}\n");
}
                    else if (brainName.Equals("NPCMoveBrain") || brainName.Equals("Move"))
                    {
                        outfile.WriteLine("\t\tpublic override IBrain getMoveBrain(GameLib.Entity.NonPlayerCharacter.AICharacter character)");
                        outfile.WriteLine("\t\t{");
                        outfile.WriteLine("\t\t\tIBrain noBrain = new NPCMoveBrain();");
                        outfile.WriteLine("\t\t\tnoBrain.init(character); ");
                        outfile.WriteLine("\t\t\treturn noBrain; ");
                        outfile.WriteLine("\t\t}\n");
                    }
                    else
                    {
                        outfile.WriteLine("\t\tpublic virtual IBrain get" + brainName + "Brain(GameLib.Entity.NonPlayerCharacter.AICharacter character)");
                        outfile.WriteLine("\t\t{");
                        outfile.WriteLine("\t\t\tIBrain newBrain = new "+ brainName + "();");
                        outfile.WriteLine("\t\t\tnewBrain.init(character); ");
                        outfile.WriteLine("\t\t\treturn newBrain; ");
                        outfile.WriteLine("\t\t}\n");
                    }
                }

                outfile.WriteLine("\t}");
                outfile.WriteLine("}");
            }//File written
        }
    }
}

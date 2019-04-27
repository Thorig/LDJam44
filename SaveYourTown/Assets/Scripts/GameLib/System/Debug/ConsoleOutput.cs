using UnityEngine;

public class ConsoleOutput
{
    public static void printMessage(string message)
    {
        Debug.Log(message);
    }

    public static void printMessage(string nameOfCaller, string message)
    {
        printMessage(nameOfCaller + "\n" + message);
    }
}

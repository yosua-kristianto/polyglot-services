using System;

namespace SystemConfigurator.Config;

 /// <summary>
/// I fr'ckin hate writing all this over and over. 
/// This facade help customize any Console writing.
/// </summary>
public class ConsoleHelper
{
    /// <summary>
    /// Print the message as "Warning" invocation by highlighting the text 
    /// with yellow while the text remains black.
    /// </summary>
    /// <param name="message"></param>
    public static void WriteLineWarning(string message) 
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(message);
        Console.ResetColor();
    }
    
    /// <summary>
    /// Print the message as "Error" invocation by highlighting the text 
    /// with red while the text is white.
    /// </summary>
    /// <param name="message"></param>
    public static void WriteLineError(string message)
    {
        Console.ForegroundColor = ConsoleColor.White;
        Console.BackgroundColor = ConsoleColor.Red;
        Console.WriteLine(message);
        Console.ResetColor();
    }
}
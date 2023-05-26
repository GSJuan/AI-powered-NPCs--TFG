using System;
using System.Collections.Generic;
using System.Text;

namespace GPT2_Dialog_Generator
{
    public class Program
    {
        static void Main(string[] args)
        {
            NeuralNet neural_network = new NeuralNet();
            while(true)
            {
                Console.WriteLine("Enter a prompt: ");
                string prompt = Console.ReadLine();
                if (prompt == "exit()") break;
                string result = neural_network.GetInference(prompt);              
            }
            nn.Cleanup();
        }
    }
}

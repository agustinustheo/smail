using System;
using System.Collections.Generic;

namespace SimpleMailCLI
{
    class Program
    {
        private static List<string> commands = new List<string>
        {
            "-r",
            "-s",
            "-d",
            "-c"
        };
        static void Main(string[] args)
        {
            string Recipient = "";
            string Subject = "";
            string DisplayName = "";
            string Content = "";

            try
            {
                if (args.Length == 0)
                {
                    Console.WriteLine($"Arguments cannot be empty");
                }

                for (int i = 0; i < args.Length; i++)
                {
                    if (args[i].StartsWith('/') || args[i].StartsWith('-'))
                    {
                        if (!commands.Contains(args[i]))
                        {
                            throw new Exception($"Invalid flag {args[i]}");
                        }
                        if (i + 1 >= args.Length)
                        {
                            throw new Exception($"Invalid entry for flag {args[i]}");
                        }
                        if (args[i + 1].StartsWith('/') || args[i + 1].StartsWith('-'))
                        {
                            throw new Exception($"Invalid entry for flag {args[i]}");
                        }

                        switch (args[i])
                        {
                            case "-r":
                                Recipient = args[i + 1];
                                break;
                            case "-s":
                                Subject = args[i + 1];
                                break;
                            case "-d":
                                DisplayName = args[i + 1];
                                break;
                            case "-c":
                                Content = args[i + 1];
                                break;
                        }
                    }
                }

                if (!(string.IsNullOrEmpty(Recipient) || string.IsNullOrEmpty(Subject) ||
                    string.IsNullOrEmpty(DisplayName) || string.IsNullOrEmpty(Content)))
                {
                    Mail.Send(new List<string>
                    {
                        Recipient
                    }, Subject, DisplayName, Content);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}

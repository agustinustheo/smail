using System;
using System.Collections.Generic;

namespace smail
{
    class Program
    {
        private static List<string> commands = new List<string>
        {
            "-f",
            "-m",
            "-h",
            "-t",
            "-s",
            "-p",
            "-dn",
            "-xu",
            "-xp",
            "-ssl",
        };

        static void Main(string[] args)
        {
            string from = "";
            string to = "";
            string subject = "";
            string displayName = "";
            string content = "";
            string host = "";
            string username = "";
            string password = "";
            int port = 587;
            bool enableSsl = true;

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
                            case "-f":
                                from = args[i + 1];
                                break;
                            case "-m":
                                content = args[i + 1];
                                break;
                            case "-h":
                                host = args[i + 1];
                                break;
                            case "-t":
                                to = args[i + 1];
                                break;
                            case "-s":
                                subject = args[i + 1];
                                break;
                            case "-p":
                                if (!int.TryParse(args[i + 1], out port))
                                {
                                    throw new Exception("Invalid entry for flag -p");
                                }
                                port = Convert.ToInt32(args[i + 1]);
                                break;
                            case "-dn":
                                displayName = args[i + 1];
                                break;
                            case "-xu":
                                username = args[i + 1];
                                break;
                            case "-xp":
                                password = args[i + 1];
                                break;
                            case "-ssl":
                                if(!bool.TryParse(args[i + 1], out enableSsl))
                                {
                                    throw new Exception("Invalid entry for flag -ssl");
                                }
                                enableSsl = Convert.ToBoolean(args[i + 1]);
                                break;
                        }
                    }
                }

                if (Validate(from, to, subject, displayName, content, username, password, host))
                {
                    var mail = new Mail(from, username, password, host, port);
                    mail.Send(new List<string>
                    {
                        to
                    }, subject, displayName, content, enableSsl);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static bool Validate(string from, string to, string subject, string displayName, string content, string username, string password, string host)
        {
            if (string.IsNullOrEmpty(from))
            {
                return false;
            }
            if (string.IsNullOrEmpty(to))
            {
                return false;
            }
            if (string.IsNullOrEmpty(subject))
            {
                return false;
            }
            if (string.IsNullOrEmpty(displayName))
            {
                return false;
            } 
            if (string.IsNullOrEmpty(content))
            {
                return false;
            }
            if (string.IsNullOrEmpty(password))
            {
                return false;
            }
            if (string.IsNullOrEmpty(host))
            {
                return false;
            }
            return true;
        }
    }
}

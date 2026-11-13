// Licensed to Schroedinger Entertainment (SOE) under the terms of the AGPLv3
// Licensed to you by SOE under the terms of the AGPLv3 or another OSI-approved license

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Soe.Runtime;

namespace Soe.Hamper
{
    /// <summary>
    /// Declares the host application for command line tools and services
    /// </summary>
    static partial class Hamper
    {
        [InlineArray(3)]
        private struct AvailableOptions
        {
            private const int DefaultCapacity = 3;
            private Option element0;
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Span<Option> AsSpan()
            {
                return MemoryMarshal.CreateSpan(ref element0, DefaultCapacity);
            }
        }

        /// <summary>
        /// Declares available command line options
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static AvailableOptions GetOptions()
        {
            AvailableOptions result = default;
            Span<Option> options = result.AsSpan();
            {
                options[0] = new Option("help", OptionFlags.Flag | OptionFlags.Optional, "Shows this help message");
                options[1] = new Option("h", OptionFlags.Alias, parameter: "help");
                options[2] = new Option("module", OptionFlags.Verb | OptionFlags.NoHeader, "A module loaded by this host. The module must contain an entry point");
            }
            return result;
        }
        
        /// <summary>
        /// Entry-point of the runtime host
        /// </summary>
        /// <param name="args">Parsed command line arguments</param>
        /// <returns>The exit code of the current run</returns>
        public static int Main(string[] args)
        {
            using (CommandReader<ParamSource> reader = new CommandReader<ParamSource>(new ParamSource(args), CommandReaderOptions.AllowVerbValues | CommandReaderOptions.IgnoreCase))
            {
                if (reader.Read(GetOptions().AsSpan()))
                {
                    switch (reader.Fragment.Type)
                    {
                        case CommandToken.Verb:
                            {
                                ParamSource source = reader.StreamSource;
                                return RunModule(reader.Fragment.Value.ToString(), ref source);
                            }
                        case CommandToken.Command: return ProcessFlags(reader);
                        case CommandToken.Unknown:
                            {
                                Application.Log(VerbosityFlags.Error, SeverityFlags.Silent, "Unknown command '{0}' found", reader.Fragment.Key.ToString());
                                Console.WriteLine();
                            }
                            break;
                    }
                }
            }

            ShowManPage();
            return Application.ExitCode;
        }
        
        /// <summary>
        /// Attempts to execute the chosen module
        /// </summary>
        static int RunModule(string moduleName, ref ParamSource parameter)
        {
            ModuleHost.Initialize();
            if (ModuleHost.Find(moduleName, out ModuleHost.RuntimeModule module))
            {
                if (module.Context.IsMainModule)
                {
                    module.Context.Load();
                    #region Load Submodules
                    foreach(SubmoduleAttribute submodule in ModuleHost.GetSubmodules(in module))
                    {
                        try
                        {
                            submodule.Load();
                        }
                        catch (Exception e)
                        {
                            Application.Log(VerbosityFlags.Warning, SeverityFlags.Silent, string.Concat("Submodule failed with error: ", e.Message));
                        }
                    }
                    #endregion
                    
                    Application.ExitCode = module.Context.Run(ref parameter);
                    
                    #region Unload Submodules
                    foreach(SubmoduleAttribute submodule in ModuleHost.GetSubmodules(in module))
                    {
                        // ReSharper disable EmptyGeneralCatchClause
                        #pragma warning disable CS0168
                        
                        try
                        {
                            submodule.Unload();
                        }
                        catch (Exception e)
                        { }
                        
                        #pragma warning restore CS0168
                        // ReSharper restore EmptyGeneralCatchClause
                    }
                    #endregion
                    module.Context.Unload();
                }
                else Application.Log(VerbosityFlags.Error, SeverityFlags.Silent, "No entry point found for module '{0}'", moduleName);
            }
            else
            {
                // TODO Load module from disk
                Application.Log(VerbosityFlags.Error, SeverityFlags.Silent, "Module '{0}' not found", moduleName);
            }
            return Application.ExitCode;
        }

        /// <summary>
        /// Performs actions based on the option read
        /// </summary>
        static int ProcessFlags(CommandReader<ParamSource> reader)
        {
            #region --help
            if(reader.Fragment.Key.Equals("help"))
            {
                ShowManPage();
            }
            #endregion
            
            return Application.ExitCode;
        }

        /// <summary>
        /// Displays the usage manual to the user
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static void ShowManPage()
        {
            if(Application.ExitCode == 0)
            {
                Console.Write("hamper [module] ");
            }
            Console.Write(InfoBuilder.Generate(GetOptions().AsSpan(), header: (Application.ExitCode == 0), headerPostfix: "[<args>]", terminalWidth: Console.WindowWidth));
        }
    }
}

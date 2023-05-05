using System;
using System.Collections.Generic;
using System.IO;

using Nuke.Common;
using Nuke.Common.IO;
using Nuke.Common.Tooling;

class BaseBuild : NukeBuild
{
        [Parameter("Personal Access Token to use for Git operations.")]
        readonly string AuthToken = null;

        [PathExecutable] Tool Git;

        public IReadOnlyCollection<Output> GitAuthorized(string command, string workingDirectory = null, IReadOnlyDictionary<string, string> environmentVariables = null, int? timeout = null, bool? logOutput = null, bool? logInvocation = null, Action<OutputType, string> customLogger = null, Func<string, string> outputFilter = null)
        {
            if (Git == null)
            {
                Serilog.Log.Warning("Git tool not initialized as expected.");
                Git =  ToolResolver.GetPathTool("git.exe");
            }
            if (!string.IsNullOrEmpty(AuthToken))
            {
                // Add authorization on headless build servers (and disable Credential Manager).
                return Git($"-c http.extraHeader=\"Authorization: Basic {AuthToken}\" {command}", workingDirectory, environmentVariables, timeout, logOutput, logInvocation, customLogger, outputFilter);
            }
            else
            {
                // On local dev boxes just use git (with Credential Manager)
                return Git($"{command}", workingDirectory, environmentVariables, timeout, logOutput, logInvocation, customLogger, outputFilter);
            }

        }

}

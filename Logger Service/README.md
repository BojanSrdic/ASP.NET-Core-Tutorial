## https://code-maze.com/net-core-web-development-part3/

We need to reference the LoggerService project to the main project.

To do that, we have to:

Navigate to the main project inside solution explorer
Right-click on the Dependences and choose the Add Project Reference
Under the Projects click the Solution and choose the LoggerService
Before we proceed to the implementation of the LoggerService, let’s do one more thing.

In the LoggerService project right click on the Dependencies and then click on the Add Project Reference. Inside check the Contracts checkbox to import its reference inside the LoggerService project. 
This would automatically add the Contracts reference to the main project because we already have a LoggerService referenced inside.


How to configure it in our project?

## Modify Program.cs class

using NLog;
var builder = WebApplication.CreateBuilder(args);
LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));

...
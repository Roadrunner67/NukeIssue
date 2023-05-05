# Nuke issue
Demonstrating odd behaviour with BaseBuild.

For reuse across multiple projects I have a BaseBuild (distributed as a NuGet package), that implements common Nuke functionality.

In my BaseBuild I have a Tool reference e.g. Git, but to my surprise this does not work as it does when declared in the actual build:

```csharp
    [PathExecutable] Tool Git;
```

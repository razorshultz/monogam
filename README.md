#### Adapted from a reddit tutorial by floatingsun

## Install MonoGame project templates.

```
dotnet new -i MonoGame.Template.CSharp
```

## Creating a MonoGame Solution File:

### Create a folder where you want to store all your MonoGame projects:

e.g:
E:/MonoGameProjects


NOTE: You can replace all instances of "MonoGameProjects" in this tutorial with anything you like.

Inside that folder, create another folder that will hold your MonoGame project:

e.g.

E:/MonoGameProjects/MyProject


NOTE: You can replace all instances of "MyProject" in this tutorial with anything you like.

## In the Command Prompt, navigate inside your project folder and run:

```
dotnet new sln
dotnet new mgdesktopgl -o MyProject
dotnet sln add MyProject/MyProject.csproj
```

This should create a solution file (MyProject.sln) that has a reference to your MonoGame project (MyProject/MyProject.csproj).

## Edit MyProject.csproj in a text editor and make it look like the following:
Important: For "TargetFramework", use  "netcoreapp" plus the first two digits of your .NET Core SDK installation (e.g. for 2.2.8, use 2.2.; for 3.0.1, use 3.0).#


```
<Project Sdk="Microsoft.NET.Sdk">

    <PropertyGroup>
        <OutputType>WinExe</OutputType>
        <!-- Set to "netcoreapp" plus the first 2 digits of your .NET Core SDK version. -->
        <TargetFramework>netcoreapp3.1</TargetFramework>
        <TargetLatestRuntimePatch>true</TargetLatestRuntimePatch>
        <MonoGamePlatform>DesktopGL</MonoGamePlatform>
    </PropertyGroup>

    <ItemGroup>
        <MonoGameContentReference Include="**\*.mgcb" />
    </ItemGroup>
   <ItemGroup>
      <SrcFolderReference Include="**\*.cs" />
   </ItemGroup>
    <ItemGroup>
        <PackageReference Include="MonoGame.Content.Builder.Task" Version="3.8.0.1641" />
        <PackageReference Include="MonoGame.Framework.DesktopGL.Core" Version="3.8.0.13" />
    </ItemGroup>
    <!-- Actually cleans your project when you run "dotnet clean" -->
    <Target Name="SpicNSpan" AfterTargets="Clean">
        <!-- Remove obj folder -->
        <RemoveDir Directories="$(BaseIntermediateOutputPath)" />
        <!-- Remove bin folder -->
        <RemoveDir Directories="$(BaseOutputPath)" />
    </Target>

</Project>
```
### Change:

```

<MonoGamePlatform>DesktopGL</MonoGamePlatform>

```



To whatever you need.



### Change: 

```

<SrcFolderReference Include="**\*.cs" />

```



To wherever you're including your .cs files






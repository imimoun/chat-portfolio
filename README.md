![Godot Badge](https://img.shields.io/badge/Godot%20v4.5.1-478CBF?logo=godotengine&logoColor=fff&style=flat)
![C#.NET 10.0](https://img.shields.io/badge/Client_C%23.NET-10.0-512BD4?logo=csharp&logoColor=white&style=flat-square)
![C#.NET 10.0](https://img.shields.io/badge/Server_C%23.NET-10.0-512BD4?logo=csharp&logoColor=white&style=flat-square)

# Chat Portfolio
A real time chat using C# server and godot client.<br />
This project was created in 24h using Gemini AI.

### Production
- Android version available on [Play Store](https://play.google.com/store/apps/details?id=com.chatportfolio).
- Server is hosted on [render.com](https://render.com/).

### Getting Started
1. Run the server: `docker build -t chatsignalserver-dev -f ChatSignalServer/Environment/Development/Dockerfile ChatSignalServer && docker run -d -p 8080:8080 chatsignalserver-dev`
1. Run the client using Godot exe
1. Run unitary test: `cd ChatSignalServer.Tests && dotnet restore && dotnet test --verbosity normal`

### Question & Answer
> The project is just a support.
> It is important to explain why did the developer make this choice and not an other.
> This `Question & Answer` section will explain those choices

1. Q - Why do you save 2 distinct project in the same git repo? Is it better to create one dedicated to `ChatGodotClient` project, and an other one dedicated to `ChatSignalServer` project.
A - It is a portfolio project and it is easier for the recrute to have both project.

1. Q - Why do you choose godot?
A - I want to show my knowlege in C#. I want to be able to use this project on multiple platform, and godot support both on mobile and on computer.

1. Q - Why you did not implement Authentication?
A - Yes, the client project is not secure.<br />
Anyone can easily decompiled its an get the `XGodotClientKey`.<br />
Of course in a production project, I will implement Authentication in the first release.<br />
Here, the goal of the project is to implement a real-time app (using Signal).<br />
Implement Authentication will add a module not revelent of the real-time aspect.

1. Q - Why you did not implement DoS Protection?
A - The goal of this project is to show code architecture.<br />
I deployed a version to check everything works in real.<br />
If someone want to DoS this, it is not a big deal.<br />
This project is focused on the implementation of a real-time aspect (using Signal).




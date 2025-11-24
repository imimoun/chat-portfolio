![Godot Badge](https://img.shields.io/badge/Godot%20v4.5.1-478CBF?logo=godotengine&logoColor=fff&style=flat)
![C#.NET 10.0](https://img.shields.io/badge/Client_C%23.NET-10.0-512BD4?logo=csharp&logoColor=white&style=flat-square)
![C#.NET 10.0](https://img.shields.io/badge/Server_C%23.NET-10.0-512BD4?logo=csharp&logoColor=white&style=flat-square)

# Chat Portfolio
A real-time chat using C# server, and Godot client.<br />
This project was created in 24h using Gemini AI.

## Motivation
I created this project to:
- improve my C# skills,
- improve my AI utilization in a development context,
- learn how real-time functionality works,
- show my skills to recruiters.

## Live Deployment
- **Android Application:** Available for public testing on the [**Google Play Store**](https://play.google.com/store/apps/details?id=com.chatportfolio).
- **Server Hosting:** Hosted and running 24/7 on [**render.com**](https://render.com/).


## Getting Started (Local Development)
### Prerequisites
- Docker
- Godot Engine v4.5.1 - C# version

### Steps
1.  **Run the Server (Containerized):**
    ```bash
    docker build -t chatsignalserver-dev -f ChatSignalServer/Environment/Development/Dockerfile ChatSignalServer && docker run -d -p 8080:8080 chatsignalserver-dev
    ```
    The server will be running on `http://localhost:8080`.

2.  **Run the Client:**
    * Open the `ChatGodotClient` project in the **Godot Engine Editor**.
    * Run the project using the Godot execution button.

3.  **Run Unit Tests (Server):**
    ```bash
    cd ChatSignalServer.Tests
    dotnet restore
    dotnet test --verbosity normal
    ```

## Architectural Decisions and Rationale
The project is just a support.<br />
It is important to explain why did the developer make this choice, and not an other one.<br />
This `Question & Answer` section will explain those choices.<br />

|||
|---|---|
|Q|Why do you save 2 distinct projects in the same git repo? Is it better to create one dedicated to `ChatGodotClient` project, and another one dedicated to `ChatSignalServer` project.|
|A|It is a portfolio project, and it is easier for the recruiter to have both projects in the same place.|

|||
|---|---|
|Q|Why did you choose Godot?|
|A|I want to show my knowledge in C#. I want to be able to use this project on multiple platforms, and Godot is supported on mobile and computers.|


|||
|---|---|
|Q|Why didn't you implement authentication?|
|A|Yes, the client project is not secure.<br />Anyone can easily decompile it, and get the `XGodotClientKey`.<br />Of course, in a production project, I will implement authentication in the first release.<br />Here, the goal of the project is to implement a real-time app (using Signal).<br />Implementing authentication will add a module not relevant to the real-time aspect.|


|||
|---|---|
|Q|Why didn't you implement DoS protection?|
|A|The goal of this project is to show code architecture.<br />I deployed a version to check that everything works in real.<br />If someone wants to DoS this, it is not a big deal.<br />This project is focused on the implementation of a real-time aspect (using Signal).|

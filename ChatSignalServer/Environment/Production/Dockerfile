# Stage 1: Build the application
# Use the SDK image for building and publishing the C# project
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy the project file and restore dependencies
# This leverages Docker's build cache if only code changes
COPY ["ChatSignalServer.csproj", "."]
RUN dotnet restore "ChatSignalServer.csproj"

# Copy the rest of the application source code
COPY . .
WORKDIR "/src"

# Publish the application to the 'app/publish' directory
RUN dotnet publish "ChatSignalServer.csproj" -c Release -o /app/publish --no-restore

# Stage 2: Create the final runtime image
# Use a smaller, secure runtime image for the final container
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app

# Copy the published output from the build stage
COPY --from=build /app/publish .

# Set the environment variable to ensure the application listens on all network interfaces
ENV ASPNETCORE_URLS=http://+:8080

# The default ASP.NET Core port is 8080 in the container
EXPOSE 8080

# Define the entry point for the application
ENTRYPOINT ["dotnet", "ChatSignalServer.dll"]

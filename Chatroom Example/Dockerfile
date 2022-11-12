#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Chatroom Example/Chatroom Example.csproj", "Chatroom Example/"]
RUN dotnet restore "Chatroom Example/Chatroom Example.csproj"
COPY . .
WORKDIR "/src/Chatroom Example"
RUN dotnet build "Chatroom Example.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Chatroom Example.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Chatroom Example.dll"]
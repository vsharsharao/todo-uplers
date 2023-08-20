#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 7020

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["todo.server/todo.server.csproj", "todo.server/"]
RUN dotnet restore "todo.server/todo.server.csproj"
COPY . .
WORKDIR "/src/todo.server"
RUN dotnet build "todo.server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "todo.server.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "todo.server.dll"]
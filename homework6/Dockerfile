FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["homework6/homework6.fsproj", "homework6/"]
RUN dotnet restore "homework6/homework6.fsproj"
COPY . .
WORKDIR "/src/homework6"
RUN dotnet build "homework6.fsproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "homework6.fsproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "homework6.dll"]

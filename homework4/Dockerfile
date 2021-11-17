FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["homework4/homework4.fsproj", "homework4/"]
RUN dotnet restore "homework4/homework4.fsproj"
COPY . .
WORKDIR "/src/homework4"
RUN dotnet build "homework4.fsproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "homework4.fsproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "homework4.dll"]

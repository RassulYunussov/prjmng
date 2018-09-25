FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["prjmng.csproj", "./"]
RUN dotnet restore "./prjmng.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "prjmng.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "prjmng.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "prjmng.dll"]

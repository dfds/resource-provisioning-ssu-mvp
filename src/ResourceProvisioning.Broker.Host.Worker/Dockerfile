#Setup build image
FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build-env

#TODO: The PAT value should be fetched from $(System.AccessToken) in the build pipeline.
ENV PAT_VALUE="k6u35igh7jbzqxcopgjeqwi3lzhtm5wsnh6hkm36tgjw3dr6fgha"
ENV PAT_USER="unused"

#Set container filesystem to /build (and create folder if it doesnt exist)
WORKDIR /build

#Copy files to container file system.
COPY . .

#Copy NuGet.config to root folder.
COPY NuGet.config /root/.nuget/NuGet/NuGet.Config

#Set workdir to current project folder
WORKDIR /build/src/BeHeroes.OrderProcessing.Host.Worker

#Restore csproj packages.
RUN dotnet restore

#Compile source code using standard Release profile
RUN dotnet publish -c Release -o /build/out

#Setup final container images.
FROM mcr.microsoft.com/dotnet/core/runtime:2.2 AS runtime
WORKDIR /app

#Application ports
EXPOSE 5000 5001 

#Visual studio override ports
EXPOSE 80 443 44307

#Copy binaries from publish container to final container
COPY --from=build-env /build/out .

#Run dotnet executable
ENTRYPOINT ["dotnet", "BeHeroes.OrderProcessing.Host.Worker.dll"]
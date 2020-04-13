#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

# download slim version of dotnet and name it as base
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base         
#in this directory finally built code will be deployed in Virtual machine
WORKDIR /app                                                               
EXPOSE 80		

# download buster version of 3.1 dotnet and name it as build
FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build                  
# copy all files from my local folder into source folder in VM ie docker
WORKDIR /src                                                                
COPY . .
# restore all nuget packages referenced in my .csproj file
RUN dotnet restore "WebMVC.csproj"                                   

#COMPILE 
# build the project in release mode and put the output in app with name build
RUN dotnet build "WebMVC.csproj" -c Release -o /app/build            

#DEPLOYED
# rename build as publish
FROM build AS publish                                                        
RUN dotnet publish "WebMVC.csproj" -c Release -o /app/publish

# rename base as final
FROM base AS final                                                         
# till now wrking dir was src but now it changed to app because in line 17 we have final published code in app
WORKDIR /app                                                               
# copy everything from publish to app/publish
COPY --from=publish /app/publish .                                         
# now dotnet and .dll is loaded into memory and microservice is containerised.
ENTRYPOINT ["dotnet", "WebMVC.dll"]                                
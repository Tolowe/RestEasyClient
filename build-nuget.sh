#run this from the root of the repo
#you will need to follow the instructions at https://docs.nuget.org/create/creating-and-publishing-a-package
#to actually publish a package
#incrementing the version is done by editing the [assembly: AssemblyVersion("0.0.0.1")]
#and [assembly: AssemblyFileVersion("0.0.0.1")] properties in src/RestEasyClient/Properties/AssemblyInfo.cs

nuget.exe pack src/RestEasyClient/RestEasyClient.csproj

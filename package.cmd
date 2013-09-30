%windir%\Microsoft.NET\Framework\v4.0.30319\msbuild.exe src\GitHubPushLib.sln /t:Clean,Rebuild /p:Configuration=Release /fileLogger

if not exist .builds\lib\net4 mkdir .builds\lib\net4

copy src\GitHubPushLib\bin\Release\GitHubPushLib.dll .builds\lib\net4\
copy LICENSE .builds\
copy README.md .builds\

src\.nuget\nuget.exe update -self
src\.nuget\nuget.exe pack GitHubPushLib.nuspec -BasePath .builds\ -Output .builds\ -Symbols
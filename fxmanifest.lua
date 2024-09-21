fx_version 'bodacious'
game 'gta5'

author 'You'
version '1.0.0'

fxdk_watch_command 'dotnet' {'watch', '--project', 'Client/NativeUI.Client.csproj', 'publish', '--configuration', 'Release'}
fxdk_watch_command 'dotnet' {'watch', '--project', 'Server/NativeUI.Server.csproj', 'publish', '--configuration', 'Release'}

file 'Client/bin/Release/net452/CitizenFX.Core.Client.dll'

client_script 'Client/bin/Release/net452/NativeUI.Client.net.dll'
server_script 'Server/bin/Release/netstandard2.0/NativeUI.Server.net.dll'

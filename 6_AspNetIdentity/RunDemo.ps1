$global:p = @();                                  
                                                          
function global:Find-ChildProcess {
  param($ID=$PID)

  $result = Get-CimInstance win32_process | 
    where { $_.ParentProcessId -eq $ID } 
    select -Property ProcessId 

  $result
  $result | 
    Where-Object { $_.ProcessId -ne $null } | 
    ForEach-Object {
      Find-ChildProcess -id $_.ProcessId
    }
}

function global:Kill-Demo {
  $Global:p | 
    foreach { Find-ChildProcess -ID $_.Id } | 
    foreach { kill -id $_.ProcessId }
}

[System.Environment]::SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Development");

# Jedi Identity Server
Push-Location "./src/JediIdentityServerWithAspNetIdentity"
dotnet restore
dotnet build --no-incremental #rebuild
$global:p += Start-Process dotnet -ArgumentList "run" -PassThru
Pop-Location

# Web Api
Push-Location "./src/WebApi"
dotnet restore
dotnet build --no-incremental
$global:p += Start-Process dotnet -ArgumentList "run" -PassThru
Pop-Location

# Web Api
Push-Location "./src/MvcClient"
dotnet restore
dotnet build --no-incremental
$global:p += Start-Process dotnet -ArgumentList "run" -PassThru
Pop-Location

# Client
# Push-Location "./src/Client"
# dotnet restore
# dotnet build --no-incremental
# $global:p += Start-Process dotnet -ArgumentList "run" -PassThru
# Pop-Location

# Client
# Push-Location "./src/ResourceOwnerClient"
# dotnet restore
# dotnet build --no-incremental
# $global:p += Start-Process dotnet -ArgumentList "run" -PassThru
# Pop-Location

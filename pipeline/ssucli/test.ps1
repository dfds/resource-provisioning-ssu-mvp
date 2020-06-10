
$outputFolder = $PSScriptRoot + "/test-results/"
New-Item -Path $outputFolder -ItemType "directory" -Force | Out-Null

Write-Host ("Placing test results in: $outputFolder")

dotnet test ../../src/ResourceProvisioning.Cli.AcceptanceTests `
 --logger "trx;LogFileName=$outputFolder/AcceptanceTests-testresults.trx" `
  /p:CollectCoverage=true /p:CoverletOutputFormat=cobertura /p:CoverletOutput=$outputFolder

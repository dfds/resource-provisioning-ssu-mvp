[CmdletBinding()]

param(
  [Parameter(Mandatory = $True, Position = 0, ValueFromPipeline = $false)]
  [string]
  $runtime,

  [Parameter(Mandatory = $True, Position = 1, ValueFromPipeline = $false)]
  [string]
  $appVersion,

  [Parameter(Mandatory = $False, Position = 2, ValueFromPipeline = $false)]
  [string]
  $outputFolder ="./output"

)
$publishOutputFolder  = "./publish-output"
dotnet publish -p:Version="$appVersion" ../../src/ResourceProvisioning.Cli.Host.Console -c release /p:PublishSingleFile=true  --output $publishOutputFolder --runtime $runtime

$publishedFileName

$fileToCheck = $publishOutputFolder + "/ResourceProvisioning.Cli.Host.Console.exe"
$destinationFile

## windows path
if (Test-Path $fileToCheck -PathType leaf)
{
  $publishedFileName = "ResourceProvisioning.Cli.Host.Console.exe"
  $destinationFile = $outputFolder + "/SsuCli-" + $runtime + ".exe"
}
else
{
  $publishedFileName = "ResourceProvisioning.Cli.Host.Console"
  $destinationFile = $outputFolder + "/SsuCli-" + $runtime
}


New-Item -Path $outputFolder -ItemType "directory" -Force | Out-Null

$sourceFile = $publishOutputFolder + "/" + $publishedFileName

Move-Item -Path $sourceFile -Destination $destinationFile 
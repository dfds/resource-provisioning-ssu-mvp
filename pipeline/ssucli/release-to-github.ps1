[CmdletBinding()]

param(
  [Parameter(Mandatory = $False, Position = 0, ValueFromPipeline = $false)]
  [string]
  $inputFolder = "./output",

  [Parameter(Mandatory = $True, Position = 1, ValueFromPipeline = $false)]
  [string]
  $appVersion
)

$commandArgs = ""

Join-Path -Path $inputFolder -ChildPath "/*" | Get-Item | ForEach-Object {
  $commandArgs += " -a $($_.FullName)"
}

$command = "hub release create -m `"$appVersion`" $appVersion$($commandArgs)"
Invoke-Expression $command
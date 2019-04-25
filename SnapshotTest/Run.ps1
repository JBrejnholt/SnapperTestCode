$executionPath = Split-Path -Parent $PSCommandPath
Set-Location -Path $PSScriptRoot
try {
  dotnet vstest SnapshotTest.dll
}
finally {
  Set-Location -Path $executionPath  
}
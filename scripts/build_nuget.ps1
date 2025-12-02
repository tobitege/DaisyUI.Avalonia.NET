# Build NuGet package for Flowery.NET
param(
    [string]$Configuration = "Release",
    [string]$OutputDir = "./nupkg"
)

$ErrorActionPreference = "Stop"
$ProjectPath = "$PSScriptRoot/../Flowery.NET/Flowery.NET.csproj"

Write-Host "Building NuGet package..." -ForegroundColor Cyan
dotnet pack $ProjectPath -c $Configuration -o $OutputDir

if ($LASTEXITCODE -eq 0) {
    Write-Host "Package created in $OutputDir" -ForegroundColor Green
    Get-ChildItem $OutputDir -Filter "*.nupkg" | ForEach-Object { Write-Host "  $_" }
} else {
    Write-Host "Build failed" -ForegroundColor Red
    exit 1
}

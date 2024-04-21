$loop = 10
$starting = ""

# Exe
$dotnet_exe = ".\DotNetIndexer\bin\Release\net8.0\DotNetIndexer.exe"
$cpp_exe = ".\CppIndexer\Main.exe"
$rust_exe = ".\Rust\rust-indexer\target\release\rust-indexer.exe"

# Params
$dotnet_param = "dotnet-index.txt", $starting
$cpp_param = "cpp-index.txt", $starting
$rust_param = "rust-index.txt", $starting

function Measure-ExecutionTime {
    param (
        [string]$executable,
        [string[]]$parametres
    )
    $sw = [System.Diagnostics.Stopwatch]::StartNew()
    & $executable $parametres > $null
    $sw.Stop() 
    return $sw.Elapsed.TotalMilliseconds
}

$dotnet_times = @()
$cpp_times = @()
$rust_times = @()

for ($i = 1; $i -le $loop; $i++) {
    Write-Host "Loop $i"
    $dotnet_times += Measure-ExecutionTime $dotnet_exe $dotnet_param
    $cpp_times += Measure-ExecutionTime $cpp_exe $cpp_param
    $rust_times += Measure-ExecutionTime $rust_exe $rust_param
}

# Calculer les moyennes
$dotnet_avg = ($dotnet_times | Measure-Object -Average).Average
$cpp_avg = ($cpp_times | Measure-Object -Average).Average
$rust_avg = ($rust_times | Measure-Object -Average).Average

# Afficher les résultats
Write-Host "Avg C# : $dotnet_avg ms"
Write-Host "Avg C++ : $cpp_avg ms"
Write-Host "Avg Rust : $rust_avg ms"
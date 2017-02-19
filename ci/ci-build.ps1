$solution_path = ".\Adapt.Analyzer.sln"
$configuration = "Release"
$platform = "Any CPU"

.\.nuget\nuget.exe restore $solution_path

msbuild.exe $solution_path /t:rebuild /p:configuration=$configuration /p:platform=$platform

$test_assemblies = Get-ChildItem -Path .\Adapt.Analyzer -Recurse *.Test.dll | where-object -FilterScript {$_.FullName -notlike "*obj*" } | select -expand FullName 
foreach ($assembly in $test_assemblies)
{
	$assembly_name = split-path $assembly -leaf
	.\packages\NUnit.ConsoleRunner.3.6.0\tools\nunit3-console.exe $assembly --result results.$assembly_name.xml
}
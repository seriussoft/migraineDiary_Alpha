param($installPath, $toolsPath, $package, $project)

$project |
	Remove-Paths 'kendo.data' |
	Remove-Shims 'kendo.data' | 
	Out-Null

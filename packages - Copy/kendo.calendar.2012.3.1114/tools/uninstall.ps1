param($installPath, $toolsPath, $package, $project)

$project |
	Remove-Paths 'kendo.calendar' |
	Remove-Shims 'kendo.calendar' | 
	Out-Null

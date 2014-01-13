param($installPath, $toolsPath, $package, $project)

$project |
	Add-Paths "{
		'kendo.calendar' : 'Scripts/kendo.calendar.min'
	}" |
	Add-Shims "{ 
		'kendo.calendar': { 
			'deps': ['kendo.core']
		}
	}" | 
	Out-Null

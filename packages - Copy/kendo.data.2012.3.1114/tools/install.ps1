param($installPath, $toolsPath, $package, $project)

$project |
	Add-Paths "{
		'kendo.data' : 'Scripts/kendo.data.min'
	}" |
	Add-Shims "{ 
		'kendo.data': { 
			'deps': ['kendo.core']
		}
	}" | 
	Out-Null

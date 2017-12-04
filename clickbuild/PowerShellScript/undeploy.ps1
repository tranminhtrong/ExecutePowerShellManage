################################################################
#
# Specify parameters
#
################################################################

param([string]$SiteListParam = "SiteList.xml", [switch]$whatif, [switch]$verbose)
$SiteListName = (Join-Path $pwd $SiteListParam)

if (!(Test-Path $SiteListName))
{
    Write-Warning "$SiteListParam does not exist"
    Break
}

if ($verbose) {
	Write-Output "SiteListName: $SiteListName"
	Write-Output "whatif: $whatif"
}

################################################################
#
# Check if we have administrator rights
#
################################################################

if (!([Security.Principal.WindowsPrincipal] [Security.Principal.WindowsIdentity]::GetCurrent()).IsInRole([Security.Principal.WindowsBuiltInRole] "Administrator"))
{
    Write-Warning "You do not have Administrator rights to run this script!`nPlease re-run this script as an Administrator!"
    Break
}

if ($verbose) {
	Write-Output "Administrator rights"
}

################################################################
#
# Load Web Administration Module
#
################################################################

Import-Module WebAdministration

if ($verbose) {
	Write-Output "WebAdministration module imported"
}

################################################################
#
# Read SiteList
#
################################################################

[xml]$sitelist = Get-Content $SiteListName

if ($verbose) {
	Write-Output "$SiteListName read in"
}

################################################################
#
# Get the Version Number
#
################################################################

$version = ($sitelist.SiteList.Version)

if ($verbose) {
	Write-Output "Version: $version"
}

################################################################
#
# Get the Deployment Directory
#
################################################################

$dirpath = ($sitelist.SiteList.DirPath)

################################################################
#
# Get the Logging Directory
#
################################################################

$loggingdir = ($sitelist.SiteList.LOG4NET)

################################################################
#
# Make-SiteName
#   Given a node return the sitename
#
################################################################

function Make-SiteName($node) {
	$sitename = ("V" + $version + "." + $node.Name)
	$suffix = $node.Suffix
	if ($suffix) {
		$sitename = ($sitename + "." + $suffix)
	}
	return $sitename
}

################################################################
#
# Check-RemoveSite
#   check if a site exists, if so stop it, then remove it
#
################################################################

function Check-RemoveSite($node) {
	$sitename = Make-SiteName $node

	if (Test-Path IIS:\Sites\$sitename) {
		Stop-Website $sitename
		Remove-Item IIS:\Sites\$sitename -recurse
	}
}

################################################################
#
# Check-RemoveAppPool
#   check if an application pool, if so remove it
#	Assumes application pool name is same as site name
#
################################################################

function Check-RemoveAppPool($node){
	$apppool = Make-SiteName $node

	if (Test-Path IIS:\AppPools\$apppool) {
		Remove-Item IIS:\AppPools\$apppool -recurse
	}
}

################################################################
#
# Check-RemoveDir
#   check if a directory exists, if so remove it
#
################################################################

function Check-RemoveDir($path){
	if (Test-Path -path $path) {
		Remove-Item $path -recurse -force
	}
}

################################################################
#
# Recurse-RemoveLinks
#   Remove any symbolic links in the path
#
################################################################

function Recurse-RemoveLinks($path) {
	if (Test-Path -path $path) {
		if ($verbose) {
			Write-Output "Recurse-RemoveLinks $path"
		}
		Get-ChildItem -Path $path | foreach {
			$child = Join-Path $path $_
			if ($_.PSIsContainer) {
				if ($verbose) {
					Write-Output "Dir:  $child"
				}
				Recurse-RemoveLinks $child

				if ($_.Attributes -match "ReparsePoint") {
					if ($whatif) {
						Write-Output "> cmd /c rmdir $child"
					} else {
						cmd /c rmdir $child
					}
				}
			} else {
				if ($verbose) {
					Write-Output "File: $child"
				}
				if ($_.Attributes -match "ReparsePoint") {
					if ($whatif) {
						Write-Output "> cmd /c del $child"
					} else {
						cmd /c del $child
					}
				}
			}
		}
	}
}

################################################################
#
# Check if task is already registered
#   if exists the unregister
# Limitation: this does not read the KeepAliveTemplate
#   assumes the name of the task is KeepAlive
#
################################################################

$taskname = "KeepAlive"

$taskexists = Get-ScheduledTask | Where-Object { $_.TaskName -like $taskname }
if ($taskexists) {
	Write-Output "Task $taskname exists deleting"
	Unregister-ScheduledTask -TaskName $taskname -Confirm:$false
}
else {
	Write-Output "Task $taskname does not exist"
}

################################################################
#
# Remove the Sites
#
################################################################

foreach ($node in $sitelist.SiteList.Sites.Site) {
	Check-RemoveSite $node
	Check-RemoveAppPool $node
}

Check-RemoveDir $loggingdir
Recurse-RemoveLinks $dirpath
Check-RemoveDir $dirpath






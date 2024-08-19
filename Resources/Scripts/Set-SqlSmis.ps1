<#
.SYNOPSIS
Create SQL user if it doesn't exist and apply permissions

.DESCRIPTION
Create SQL user if it doesn't exist and apply permissions

.EXAMPLE
.\Set-SqlUser.ps1

.NOTES

#>
[CmdletBinding()]
param(
    [Parameter(Mandatory=$true)]
    [string]$SqlServerName,
    [Parameter(Mandatory=$true)]
    [string]$SqlDatabaseName,
    [Parameter(Mandatory=$true)]
    [string]$ApiSMI,
    [Parameter(Mandatory=$true)]
    [string]$ApiSMIStaging
)
$sqlServerFQDN = "$($SqlServerName).database.windows.net"

$userQuery = "if not exists (
                    select 1 from sys.database_principals
                    where name = '$($ApiSMI)'
                        and type = 'E'
                )
                begin
                    print 'Adding user $($ApiSMI) to database $($SqlDatabaseName) on server $($SqlServerName)'
                    CREATE USER [$($ApiSMI)] FROM EXTERNAL PROVIDER
                    ALTER ROLE db_datareader ADD MEMBER [$($ApiSMI)]
                    ALTER ROLE db_datawriter ADD MEMBER [$($ApiSMI)]
                    ALTER ROLE db_ddladmin ADD MEMBER [$($ApiSMI)]
                    GRANT EXECUTE TO [$($ApiSMI)]
                end
                else
                begin
                    print 'User $($ApiSMI) already exists in database $($SqlDatabaseName) on server $($SqlServerName)'
                end
                if not exists (
                    select 1 from sys.database_principals
                    where name = '$($ApiSMIStaging)'
                        and type = 'E'
                )
                begin
                    print 'Adding user $($ApiSMIStaging) to database $($SqlDatabaseName) on server $($SqlServerName)'
                    CREATE USER [$($ApiSMIStaging)] FROM EXTERNAL PROVIDER
                    ALTER ROLE db_datareader ADD MEMBER [$($ApiSMIStaging)]
                    ALTER ROLE db_datawriter ADD MEMBER [$($ApiSMIStaging)]
                    ALTER ROLE db_ddladmin ADD MEMBER [$($ApiSMIStaging)]
                    GRANT EXECUTE TO [$($ApiSMIStaging)]
                end
                else
                begin
                    print 'User $($ApiSMIStaging) already exists in database $($SqlDatabaseName) on server $($SqlServerName)'
                end;"

Write-Verbose "Getting access token for database access"

$access_token = $null
$access_token = (Get-AzAccessToken -ResourceUrl https://database.windows.net).Token

Write-Verbose "Creating users on $sqlServerFQDN in database $SqlDatabaseName"
Invoke-Sqlcmd `
    -ServerInstance $sqlServerFQDN `
    -Database $SqlDatabaseName `
    -AccessToken $access_token `
    -query $userQuery `
    -Verbose

Write-Verbose "Users have been created."
%windir%\system32\inetsrv\appcmd.exe set config -section:applicationPools -applicationPoolDefaults.processModel.idleTimeout:00:00:00
exit /b 0
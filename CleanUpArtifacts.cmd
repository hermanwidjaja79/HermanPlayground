@echo off
for /d %%d in (*.*) do (
    for /d %%e in (%%d\*.*) do (
    	echo rmdir /s /q %%e\bin
    	rmdir /s /q %%e\bin

    	echo rmdir /s /q %%e\obj
    	rmdir /s /q %%e\obj
    )
    echo rmdir /s /q %%d\packages
    rmdir /s /q %%d\packages

    echo rmdir /s /q %%d\TestResults
    rmdir /s /q %%d\TestResults

    echo rmdir /s /q %%d\.axoCover
    rmdir /s /q %%d\.axoCover
)
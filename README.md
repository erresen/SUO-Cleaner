# SUO-Cleaner
Removes .suo files from Visual Studio solution directories. 

This can be useful for very large solutions with multiple TFS branches in which the Solution User Options files get corrupted, causing Visual Studio to hang when opening the solution.

Pass in a base directory as the first argument, and SUO-Cleaner will find and delete any .suo files within the directory or it's subdirectories.

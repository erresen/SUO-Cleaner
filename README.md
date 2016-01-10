# SUO-Cleaner
Removes .suo files from Visual Studio Solutions. This can be useful in very large solutions in which the Solution User Options file gets corrupted, causing Visual Studio to hang when opening the solution.

Pass in a base directory as the first argument, and SUO-Cleaner will find and delete any .suo files within the directory or it's subdirectories.

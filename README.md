# Air-3550
Final project for the Software Engineering (EECS 3550) class at the University of Toledo in Spring 2021.
Initial project statement is located in Air-3550.pdf.

## Initial Setup
1. Make sure you have a local copy of the code, you can accomplish this by cloining the github repository, see this [link](https://docs.github.com/en/github/getting-started-with-github/about-remote-repositories#cloning-with-https-urls) for more info about cloning specifically and this [link](https://github.com/qkleinfelter/Air-3550) for our private repository which will give you the clone command by pressing the green download arrow.
2. Make sure you have downloaded Visual Studio 2019 version 16.9 or newer, and ensure that you download the Universal Windows Platform Development tools and Windows 10 SDK in the workloads and individual components sections of the installer. Also ensure that you check .NET Desktop Development. (Note that these smaller pieces are not actually required if you already have Visual Studio installed, as installing the extension will download them for you anyway)
3. Download the Project Reunion Extension for Visual Studio, you can do this by clicking Extensions at the top of the page => Manage Extensions => Search for Project Reunion download it and close visual studio to install it.
4. Open the solution for Air-3550 once you have updated & installed the extension then you should be able to edit & run the project.
    - When running the project, ensure at the top of your screen you are running in Debug - x86 - Air-3550 (Package)

## Database access
If you need to manually access the database, to change things such as making flights full, I recommend the use of [DB Browser for SQLite](https://sqlitebrowser.org/). You can find the local database on your machine in the path `%appdata%/Air 3550 Team 11`, or if for some reason it decided not to save there it may also be located in `%localappdata%/Packages/Most Recently Modified Package Folder/Local Cache/Roaming/Air 3550 Team 11`.
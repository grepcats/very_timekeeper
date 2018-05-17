# The Very Timekeeper

##### A productivity app built with C#/ASP.NET

## By Kayla Ondracek

# Description
This app allows the user to create tasks and assign times to them. On starting the timer, the program will progress through the tasks.

FOr now, you must be logged in to create a task. Tasks are not "per-user" yet!

## Specs

Coming soon !

## Technologies Used
* Bootstrap
* C#/ASP.NET
* Entity Framework
* MySql/MAMP
* JavaScript

## Setup/Installation Instructions
* Clone the GitHub repository:
  ```
  $ git clone https://github.com/grepcats/very_timekeeper
  ```

* Install the .NET Framework and MAMP

    .NET Core 1.1 SDK (Software Development Kit)
    .NET runtime.
    MAMP

    See https://www.learnhowtoprogram.com/c/getting-started-with-c/installing-c for instructions and links.

* Start the Apache and MySql Servers in MAMP. Make sure you use the default port settings for Apache and MySql (8888 and 8889, respectively)

* Restore dependencies, set up the database, and run
```
$ dotnet restore
$ cd VeryTimekeeper
$ dotnet ef database update
$ dotnet run
```

### Known Bugs
Tasks are not per user. You must log in to create a task. Anonymous use is forthcoming.

### License

*MIT License*

Copyright (c) 2018 **_Kayla Ondracek_**

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.

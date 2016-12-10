Green Shelter
===
Green Shelter is an ASP.NET vNext MVC website that provides basic content, client and organization management, and client bed/mat check-in. Its aim is to allow notification of  client check-ins via Green Shelter mobile app or mobile SMS.

This application is based on the ASP.NET vNext that includes documentation, samples and getting started instruction at the [Home](https://github.com/aspnet/home) repo.

The project demonstrates a few features such as Identity (customization and configuration), Entity Framework 7 (configuration and migration), and external login providers (Facebook, Google and MSN) provided via the ASP.NET vNext framework, and commonly used javascript libraries such as AngularJS, Gulp and Bower. The goal is to create an API that would also allow the mobile application to perform many of the same tasks. 

[[https://github.com/kscott5/GreenShelter/blob/master/src/wwwroot/images/screenshot1.jpg|alt=Screen Shoot]]

Identity 
---------
#### Customization
Most of the cusomization occurs by extendend IdentityDbContext, IdentityUser and IdentityRole classes. This was done to override the base functionality related to the Id property and its underline type. Basicly, I wanted to integer Id key for better index. Review any class marked with either User or Role found in code in src/mvc/Models folders.

This is IMPORTANT because I extended ALL Identity class to include int for TKey which is reflected in the code snippet above.

Entity Framework 7
-------
I read over and over again how you can configure additions commands with the project.json file to allow command-line execution for say starting a local instance of a web server or unit test. But completely missed the fact, it also works for Migration/Scaffolding of the database using your DbContext class.

Simply, you can add the following lines of JSON text to the project.json dependencies and commands section. 

```json
...
  "dependencies": {
    /* more dependencies here */
	
	"EntityFramework.Commands": "7.0.0-*",
    "EntityFramework.Core": "7.0.0-*",
    "EntityFramework.Relational": "7.0.0-*",
    "EntityFramework.InMemory": "7.0.0-*",
    "EntityFramework.Sqlite": "7.0.0-*"
  },
  "commands": {
	"ef": "EntityFramework.Commands"
  },
...
```

This allow execution of *dnx ef migration add GreenShelterDbContext*

External Providers
-------

Javacript Library downloaded with Bower and installed using Gulp.
--------
Below is a some of the packages used by this application. 

angular
angular-resource
angular-route


Here are some command-line scripts you could run.

npm install
bower install
gulp 

NOTE: 
--------
DNX support for .NET Core is not available for CentOS, Fedora and derivative in this release, but will be enabled in a future release.
This means no coreCLR for x86 or x64. Use clr for either OS architecture.
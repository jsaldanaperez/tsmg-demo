# tsmg-demo

Demo project to generate TypeScript models and http-services from a Microsoft.AspNetCore Web API.

Following directories are generated when running the WebApi project.
/Website/src/app/models
/Website/src/app/services

Exposed models like in '/WebApi/WebApi/Controllers/WeatherForeCastController.cs' will be added to 'models' directory.

To try it out: Add new Controllers/Models to the WebApi project and run.

Configuration: '/WebApi/WebApi/TypeScriptGeneration.cs'.

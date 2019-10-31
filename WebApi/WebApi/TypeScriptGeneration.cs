using System;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TypeScriptModelsGenerator;
using TypeScriptModelsGenerator.HttpServices;
using TypeScriptModelsGenerator.Options;

namespace WebApi
{
    public class TypeScriptGeneration
    {
        private static DirectoryInfo RootDirectory => new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory).Parent
            .Parent.Parent.Parent.Parent;
        
        public static void Execute()
        {
            var path = RootDirectory;
            var controllers = typeof(TypeScriptGeneration).Assembly.ExportedTypes
                .Where(x => x.BaseType == typeof(ControllerBase)).ToList();

            var destination = Path.Combine(RootDirectory.FullName, "Website", "src", "app");
            
            TypeScriptModelsGeneration
                .Setup(controllers, destination, (options) =>
                {
                    options.GenerationMode = GenerationMode.Crawl;
                    options.AddNamespaceReplaceRule(nameof(WebApi), "models");
                    options.AddHttpServices(template =>
                    {
                        template.Imports = x =>
                            new[]
                            {
                                "import { HttpClient } from '@angular/common/http';",
                                "import { Injectable } from '@angular/core';",
                                "import { Observable } from 'rxjs';"
                            };
                        template.Decorators = x => new[] {"@Injectable({ providedIn: 'root' })"};
                    });
                    options.CleanDestinationDirectories.AddRange(new []{"models", "services"});
                })
                .Execute();
                
        }
    }
}
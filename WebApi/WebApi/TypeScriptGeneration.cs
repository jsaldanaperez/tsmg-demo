using System;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TypeScriptModelsGenerator;
using TypeScriptModelsGenerator.Definitions;
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
            var controllers = typeof(TypeScriptGeneration).Assembly.ExportedTypes
                .Where(x => x.BaseType == typeof(ControllerBase)).ToList();
            var destination = Path.Combine(RootDirectory.FullName, "Website", "src", "app");

            var typeScriptDefinition = TypeScriptDefinitionFactory.Create()
                .Folder("services", servicesFolder => servicesFolder.HttpServices(controllers, serviceBuilder =>
                    serviceBuilder
                        .WithContent(content =>
                            content
                                .AddImport("import { HttpClient } from '@angular/common/http';")
                                .AddImport("import { Injectable } from '@angular/core';")
                                .AddImport("import { Observable } from 'rxjs';")
                                .AddDecorator("@Injectable({ providedIn: 'root' })")
                        )));

            TypeScriptModelsGeneration
                .Setup(typeScriptDefinition, destination, (options) =>
                {
                    options.GenerationMode = GenerationMode.Crawl;
                    options.AddNamespaceReplaceRule(nameof(WebApi), "models");
                    options.CleanDestinationDirectories.AddRange(new []{"models", "services"});
                })
                .Execute();
        }
    }
}
